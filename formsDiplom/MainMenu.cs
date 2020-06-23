using FerretLib.WinForms.Controls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formsDiplom
{
    public partial class MainMenu : Form
    {
        public MainMenu(string email, bool reset)
        {          
            InitializeComponent();
            emailLabel.Text = email;
            if(reset)
                Reset();
        }

        List<string> tags = new List<string>();
        List<string> tags1 = new List<string>();
        List<string> tags2 = new List<string>();
        private void MainMenu_Load(object sender, EventArgs e)
        {

            tagListControl1.ContextMenuStrip = contextMenuStrip1;
            var extensions = new string[] { "jpg", "png", "mp3", "psd", "wav" };
            var types = new string[] { "document", "photo", "video", "audio", "spreadsheet" };
            BuildMenuItems(ref toolStripMenuItem1, extensions);
            BuildMenuItems(ref toolStripMenuItem2, types);
 

            //tags.AddRange(new string[] { "Text: Лето", "Size: <500 MB", "Extension: png" });
            //tagListControl1.Tags = tags;

            //tags1.AddRange(new string[] { "Size: >100 MB","Text: Ухань", "Type: video" });
            //tagListControl2.Tags = tags1;

            //tags2.AddRange(new string[] { "Extension: avi", "Extension: mp4" });
            //tagListControl3.Tags = tags2;


            //tagListControl = new TagListControl();
            //tagListControl.Size = new Size(300, 542);
            //tagListControl.Location = new Point(10, 12);
            //this.Controls.Add(tagListControl);
            //tagListControl.ContextMenuStrip = contextMenuStrip1;

        }

        public class TagValue
        {
            public TagType tagType;
            public string value;


        }
        public enum TagType
        {
            Extension,
            Type,
            Size,
            Name
        }
        public TagValue TagParser(string txt)
        {
            TagValue tagValue = new TagValue();
            tagValue.value = txt.Split(':')[1];
            tagValue.tagType = (TagType)Enum.Parse(typeof(TagType), txt.Split(':')[0].Substring(1));
            return tagValue;

        }

        public void TagCreate(string value, TagListControl tagListControl, TagType tagType)
        {
            var tags = tagListControl.Tags;
            switch (tagType)
            {
                case TagType.Extension:
                    tags.Add("Extension: " + value);
                    break;
                case TagType.Type:
                    tags.Add("Type: " + value);
                    break;
                case TagType.Size:
                    double size = Double.Parse(value);
                    if(size<0) tags.Add("Size: <" + size*(-1) + " MB");
                    else tags.Add("Size: >" + size + " MB");
                    break;
                case TagType.Name:
                    tags.Add("Text: " + value);
                    break;
            }
            tagListControl.Tags = tags;
        }

    private void BuildMenuItems(ref ToolStripMenuItem toolStripMenuItem, string[] strings)
        {

            ToolStripMenuItem[] items = new ToolStripMenuItem[strings.Length];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Text = strings[i];
                items[i].Click += new EventHandler(MenuItemClickHandler);
            }

            toolStripMenuItem.DropDownItems.AddRange(items);
        
        }


        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            var txt = clickedItem.Text;
            TagListControl tlc = (((sender as ToolStripMenuItem).OwnerItem.Owner as ContextMenuStrip).SourceControl as TagListControl);
            TagType tagType = (TagType)Enum.Parse(typeof(TagType), ((sender as ToolStripMenuItem).OwnerItem).Text.Split(' ')[1]);
            TagCreate(txt, tlc, tagType);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = System.IO.File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            FileBrowser f = new FileBrowser(new List<string> { "user0" }, label1.Text);
            f.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileBrowser f = new FileBrowser(new List<string> { "user1" }, label2.Text);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileBrowser f = new FileBrowser(new List<string> { "user2" }, label3.Text);
            f.Show();
        }

        private void Reset()
        {
            bool x = false;
            button1.Visible = x;
            label1.Visible = x;
            tagListControl1.Visible = x;
                button2.Visible = x;
                label2.Visible = x;
                tagListControl2.Visible = x;
                button3.Visible = x;
                label3.Visible = x;
                tagListControl3.Visible = x;
            button4.Location = new Point(button4.Location.X - 179*3, button4.Location.Y);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string usr = "";
            if (!button1.Visible)
                usr = "user0";
            else if (!button2.Visible)
                usr = "user1";
            else usr = "user2";
            CloudSelect cloudSelect = new CloudSelect(usr);
            cloudSelect.ShowDialog();
            button4.Location = new Point(button4.Location.X+179, button4.Location.Y);
            if (!button1.Visible)
            {
                button1.Visible = true;
                label1.Visible = true;
                tagListControl1.Visible = true;
            }
            else if (!button2.Visible)
            {
                label2.Visible = true;
                button2.Visible = true;
 
        
                tagListControl2.Visible = true;
                
            }
            else if (!button3.Visible)
            {
                label3.Visible = true;
                button3.Visible = true;
                
                tagListControl3.Visible = true;
                
            }
        }


        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                var txt = (sender as ToolStripTextBox).Text;
                TagListControl tlc = (((sender as ToolStripTextBox).OwnerItem.Owner as ContextMenuStrip).SourceControl as TagListControl);
                TagType tagType = (TagType)Enum.Parse(typeof(TagType), ((sender as ToolStripTextBox).OwnerItem).Text.Split(' ')[1]); 
                TagCreate(txt, tlc, tagType);
                (sender as ToolStripTextBox).Clear();
                ((sender as ToolStripTextBox).OwnerItem.Owner as ContextMenuStrip).Close();
            }
        }



        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var txt = (sender as ToolStripTextBox).Text;
                TagListControl tlc = ((((sender as ToolStripTextBox).OwnerItem.Owner as ToolStripDropDownMenu).OwnerItem.Owner as ContextMenuStrip).SourceControl as TagListControl);
                TagType tagType = TagType.Size;
                TagCreate(txt, tlc, tagType);
                (sender as ToolStripTextBox).Clear();
                (((sender as ToolStripTextBox).OwnerItem.Owner as ToolStripDropDownMenu).OwnerItem.Owner as ContextMenuStrip).Close();
           }
        }

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var txt = (sender as ToolStripTextBox).Text;
                TagListControl tlc = ((((sender as ToolStripTextBox).OwnerItem.Owner as ToolStripDropDownMenu).OwnerItem.Owner as ContextMenuStrip).SourceControl as TagListControl);
                TagType tagType = TagType.Size;
                TagCreate("-"+txt, tlc, tagType);
                (sender as ToolStripTextBox).Clear();
                (((sender as ToolStripTextBox).OwnerItem.Owner as ToolStripDropDownMenu).OwnerItem.Owner as ContextMenuStrip).Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileBrowser f = new FileBrowser(new List<string> { "user0","user1" }, label1.Text + " & " + label2.Text);
            f.Show();
        }
        List<DriveService> services = new List<DriveService>();
        static string[] Scopes = { DriveService.Scope.Drive };
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
            services[services.Count - 1] = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "CloudApp",
            });
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


        }
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            googleService("user0");
            googleService("user1");
            for (int i = 0; i < (e.Data.GetData(DataFormats.FileDrop) as string[]).Length; i++)
                    if((e.Data.GetData(DataFormats.FileDrop) as string[])[i] == "D:\\Files\\Small Shock.mp3" || ((e.Data.GetData(DataFormats.FileDrop) as string[])[i]) == "D:\\Files\\Dogbass.mp3")
                        uploadFile(0, (e.Data.GetData(DataFormats.FileDrop) as string[])[i]);
                    else
                        uploadFile(1, (e.Data.GetData(DataFormats.FileDrop) as string[])[i]);

        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }
    }
}
