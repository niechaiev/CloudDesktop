using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Download;
using System.Web.Hosting;

namespace formsDiplom
{
    public partial class FileBrowser : Form
    {
        private List<string> Folders;
        private bool isFile = false;
        private string currentlySelectedItemName;

        int currentUser = 0;

        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "CloudApp";

        public string Query { get;  set; }
        List<DriveService> services = new List<DriveService>();
        public List<string> usernames;
        List<IList<Google.Apis.Drive.v3.Data.File>> filesList;
        bool dontClear;

        public FileBrowser(List<string> usernames, string email)
        {
            InitializeComponent();
            filePathTextBox.Text = email;
            this.usernames = usernames;
            startFileBrowser();
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {
            //filePathTextBox.Text = filePath;
         
        }

        public void startFileBrowser()
        {
            listView1.Items.Clear();
            dontClear = true;
            filesList = new List<IList<Google.Apis.Drive.v3.Data.File>>();
            Folders = new List<string>();
            currentlySelectedItemName = "";
            Query = "";
            if (services.Count==0)
                for (int i = 0; i < usernames.Count; i++)
                {
                    googleService(usernames[i]);
                    LoadFilesAndDirectories(i);
                }
            else
            {
                for (int i = 0; i < usernames.Count; i++)
                {
                    LoadFilesAndDirectories(i);
                }
            }
            dontClear = false;
        }

            private void googleService(string username)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    username,
                    CancellationToken.None,                
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            services.Add(new DriveService());
            services[services.Count-1] = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private void LoadFilesAndDirectories(int serviceId)
        {

            checkButton();
            // Define parameters of request.
            FilesResource.ListRequest listRequest = services[serviceId].Files.List();
            listRequest.PageSize = 500;
            listRequest.Fields = "nextPageToken, files(name, id, fileExtension, mimeType)";
            if(Query.Length==0)
                listRequest.Q = "'root' in parents and trashed = false";
            else
                listRequest.Q = Query;
            IList<Google.Apis.Drive.v3.Data.File> files;
            // List files.

            files = listRequest.Execute().Files;
    
            filesList.Add(files);
     
            Console.WriteLine("Files:");
            ShowFilesAndDirectories(filesList.Count-1);
        }



        private void ShowFilesAndDirectories(int filesListId)
        {
            DirectoryInfo fileList;
            string tempFilePath = "";

            var googleFiles = filesList[filesListId];
            FileAttributes fileAttributes;
            try
            {
                if (isFile)
                {
                    //tempFilePath = filePath + "/" + currentlySelectedItemName;
                    var file = GetFile(currentlySelectedItemName);
                    DownloadFile(services[filesListId], GetFile(currentlySelectedItemName), "D:/temp/"+ file.Name);
                        


                    //FileInfo fileDetails = new FileInfo(tempFilePath);
                    //fileAttributes = System.IO.File.GetAttributes(tempFilePath);
                    Process.Start("D:/temp/" + file.Name);
                }
                else
                {
                    string fileExtension = "";
                    if (!dontClear)
                        listView1.Items.Clear();

                    for (int i = 0; i < googleFiles.Count; i++)
                    {
                        int index = -1;
                        fileExtension = googleFiles[i].FileExtension;
                        if (fileExtension != null)
                        {
                            index = iconList.Images.IndexOfKey(fileExtension + ".png");
                        }
                        if (index == -1)
                        {
                            string type = googleFiles[i].MimeType.Substring(googleFiles[i].MimeType.LastIndexOf('.') + 1);
                            index = iconList.Images.IndexOfKey(type + ".png");
                        }
                        if (index == -1) index = iconList.Images.IndexOfKey("file" + ".png");
                        listView1.Items.Add(googleFiles[i].Id, googleFiles[i].Name, index);
                    }

                }

                //fileList = new DirectoryInfo(filePath);
                //FileInfo[] files = fileList.GetFiles();
                //DirectoryInfo[] dirs = fileList.GetDirectories();


                //for (int i = 0; i < dirs.Length; i++)
                //{
                //    listView1.Items.Add(dirs[i].Name, 40);
                //}

            }
            catch (Exception e)
            {

            }
           
        }

        private static void DownloadFile(Google.Apis.Drive.v3.DriveService service, Google.Apis.Drive.v3.Data.File file, string saveTo)
        {

            var request = service.Files.Get(file.Id);
            var stream = new System.IO.MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, saveTo);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine(progress.Exception);
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);

        }

        public void uploadFile(int id, string path)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(path),
                MimeType = System.Web.MimeMapping.GetMimeMapping(path)
            };
  
            FilesResource.CreateMediaUpload request;
            byte[] byteArray = System.IO.File.ReadAllBytes(path);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            request = services[id].Files.Create(
                    fileMetadata, stream, fileMetadata.MimeType);
                request.Fields = "id";
                
                request.Upload();


            
            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);

            if(Folders.Count>0)          
            {
                Query = "parents in '" + Folders[Folders.Count - 1] + "'";
            }
            LoadFilesAndDirectories(currentUser);

        }


        private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }




        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Name;
            Google.Apis.Drive.v3.Data.File file = GetFile(currentlySelectedItemName);

            //FileAttributes fileAttr = File.GetAttributes(filePath + "/" + currentlySelectedItemName);
            if (file.MimeType== "application/vnd.google-apps.folder")
            {
               isFile = false;
            //    filePathTextBox.Text = filePath + "/" + currentlySelectedItemName;
            }
            else
            {
               isFile = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //filePath = filePathTextBox.Text;
            int userId = GetUserId(currentlySelectedItemName);
            if (!isFile)
            {
                Query = "parents in '" + currentlySelectedItemName + "'";
                Folders.Add(currentlySelectedItemName);

                currentUser = userId;
                filesList.Clear();
                for (int i = 0; i < userId; i++)
                {
                    filesList.Add(new List<Google.Apis.Drive.v3.Data.File>());
                }
                LoadFilesAndDirectories(userId);
            }
            else {
                ShowFilesAndDirectories(userId);
                isFile = false;
            }
        }

        public int GetUserId(string item)
        {
            for (int i = 0; i < filesList.Count; i++)
                for (int j = 0; j < filesList[i].Count; j++)
                    if (filesList[i][j].Id == item)
                        return i;
            return -1;
        }

        public Google.Apis.Drive.v3.Data.File GetFile(string id)
        {
            for (int i = 0; i < filesList.Count; i++)
                for (int j = 0; j < filesList[i].Count; j++)
                    if (filesList[i][j].Id == id)
                        return filesList[i][j];
            return null;
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            //filePath = filePathTextBox.Text;
            filesList.Clear();
            if (Folders.Count == 1)
                startFileBrowser();
                //Query = "";
            else if (Folders.Count > 1)
            {
                Query = "parents in '" + Folders[Folders.Count - 2] + "'";
                Folders.RemoveAt(Folders.Count - 1);

                LoadFilesAndDirectories(currentUser);
                isFile = false;
            }
        }

        private void checkButton() {
            if (Folders.Count > 0) backButton.Enabled = true;
            else backButton.Enabled = false;

        }

        private void FileBrowser_DragDrop(object sender, DragEventArgs e)
        {
            for(int i=0; i< (e.Data.GetData(DataFormats.FileDrop) as string[]).Length; i++)
            uploadFile(0, (e.Data.GetData(DataFormats.FileDrop) as string[])[i]);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }
    }
}
