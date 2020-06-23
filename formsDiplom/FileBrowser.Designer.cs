namespace formsDiplom
{
    partial class FileBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileBrowser));
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(93, 14);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(695, 20);
            this.filePathTextBox.TabIndex = 2;
            this.filePathTextBox.Text = "GoogleDrive: ";
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.iconList;
            this.listView1.Location = new System.Drawing.Point(12, 41);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(776, 397);
            this.listView1.SmallImageList = this.iconList;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileBrowser_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "after-effects.png");
            this.iconList.Images.SetKeyName(1, "ai.png");
            this.iconList.Images.SetKeyName(2, "audition.png");
            this.iconList.Images.SetKeyName(3, "avi.png");
            this.iconList.Images.SetKeyName(4, "bridge.png");
            this.iconList.Images.SetKeyName(5, "css.png");
            this.iconList.Images.SetKeyName(6, "csv.png");
            this.iconList.Images.SetKeyName(7, "dbf.png");
            this.iconList.Images.SetKeyName(8, "doc.png");
            this.iconList.Images.SetKeyName(9, "dreamweaver.png");
            this.iconList.Images.SetKeyName(10, "dwg.png");
            this.iconList.Images.SetKeyName(11, "exe.png");
            this.iconList.Images.SetKeyName(12, "file.png");
            this.iconList.Images.SetKeyName(13, "fireworks.png");
            this.iconList.Images.SetKeyName(14, "fla.png");
            this.iconList.Images.SetKeyName(15, "flash.png");
            this.iconList.Images.SetKeyName(16, "html.png");
            this.iconList.Images.SetKeyName(17, "illustrator.png");
            this.iconList.Images.SetKeyName(18, "indesign.png");
            this.iconList.Images.SetKeyName(19, "iso.png");
            this.iconList.Images.SetKeyName(20, "javascript.png");
            this.iconList.Images.SetKeyName(21, "jpg.png");
            this.iconList.Images.SetKeyName(22, "json-file.png");
            this.iconList.Images.SetKeyName(23, "mp3.png");
            this.iconList.Images.SetKeyName(24, "mp4.png");
            this.iconList.Images.SetKeyName(25, "pdf.png");
            this.iconList.Images.SetKeyName(26, "photoshop.png");
            this.iconList.Images.SetKeyName(27, "png.png");
            this.iconList.Images.SetKeyName(28, "ppt.png");
            this.iconList.Images.SetKeyName(29, "prelude.png");
            this.iconList.Images.SetKeyName(30, "premiere.png");
            this.iconList.Images.SetKeyName(31, "psd.png");
            this.iconList.Images.SetKeyName(32, "rtf.png");
            this.iconList.Images.SetKeyName(33, "search.png");
            this.iconList.Images.SetKeyName(34, "svg.png");
            this.iconList.Images.SetKeyName(35, "txt.png");
            this.iconList.Images.SetKeyName(36, "xls.png");
            this.iconList.Images.SetKeyName(37, "xml.png");
            this.iconList.Images.SetKeyName(38, "zip.png");
            this.iconList.Images.SetKeyName(39, "zip-1.png");
            this.iconList.Images.SetKeyName(40, "folder.png");
            this.iconList.Images.SetKeyName(41, "video.png");
            this.iconList.Images.SetKeyName(42, "tables.png");
            this.iconList.Images.SetKeyName(43, "presentation.png");
            this.iconList.Images.SetKeyName(44, "pdf_color.png");
            this.iconList.Images.SetKeyName(45, "movie.png");
            this.iconList.Images.SetKeyName(46, "images.png");
            this.iconList.Images.SetKeyName(47, "html_color.png");
            this.iconList.Images.SetKeyName(48, "excel.png");
            this.iconList.Images.SetKeyName(49, "audio.png");
            this.iconList.Images.SetKeyName(50, "document.png");
            this.iconList.Images.SetKeyName(51, "rar.png");
            // 
            // backButton
            // 
            this.backButton.BackgroundImage = global::formsDiplom.Properties.Resources.PinClipart_com_clipart_pfeil_48640;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(12, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // FileBrowser
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 446);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.backButton);
            this.Name = "FileBrowser";
            this.Text = "FileBrowser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileBrowser_DragDrop);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList iconList;
    }
}

