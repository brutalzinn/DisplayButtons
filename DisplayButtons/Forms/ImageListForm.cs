using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Forms
{
    public partial class ImageListForm : Form
    {
        //Dynamically create Image List at run-time
        System.Windows.Forms.ImageList myImageList = new ImageList();
        System.Windows.Forms.ImageList myImageListSmall = new ImageList();
        System.Windows.Forms.ImageList myImageListLarge = new ImageList();
        int count = 0;
        public string ImageNewName = "";
        OpenFileDialog ofd = new OpenFileDialog()
        {
            Multiselect = true,
            ValidateNames = true,
            Filter =
         "PNG|*.png"
        };
        FileInfo fi;
        public ImageListForm()
        {
            InitializeComponent();
            listViewFile.SmallImageList = myImageListSmall;
            listViewFile.SmallImageList = myImageListLarge;
            this.KeyPreview = true;
        }
        private void radioButtonLarge_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLarge.Checked == true)
            {
                listViewFile.LargeImageList = myImageListLarge;
                listViewFile.View = View.LargeIcon;
            }
        }
        //Code for Small Image icon 
        //Generate event for CheckedChanged
        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSmall.Checked == true)
            {
                listViewFile.SmallImageList = myImageListSmall;

                listViewFile.View = View.SmallIcon;
            }
        }
        //Code for List Image icon 
        //Generate event  CheckedChanged
        private void radioButtonList_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButtonList.Checked == true)
            {

                listViewFile.View = View.List;
            }
        }

        private void listViewFile_MouseClick(object sender, MouseEventArgs e)
        {

            ContextMenu cm = new ContextMenu();
            listViewFile.ContextMenu = cm;
            var mi = new MenuItem("Rename");

            mi.MenuItems.Add(mi);
            mi.Click += OnMenuItemClick;
            cm.MenuItems.Add(mi);

        }
        private void OnMenuItemClick(object sender, EventArgs e)
        {
            listViewFile.SelectedItems[0].BeginEdit();
        }
        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        private void ImageListForm_Load(object sender, EventArgs e)
        {
            myImageList.ImageSize = new Size(50, 50);
            myImageListSmall.ImageSize = new Size(32, 32);
            myImageListLarge.ImageSize = new Size(80, 80);
            listViewFile.View = View.LargeIcon;
            listViewFile.Refresh();
            String searchFolder = Application.StartupPath + @"\Data\imgs";
            var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            var files = GetFilesFrom(searchFolder, filters, false);
            foreach (string fileName in files)
            {
                fi = new FileInfo(fileName);
                FileInfo fileinfo = new FileInfo(fileName);
                using (FileStream stream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                {
                    myImageList.Images.Add(Image.FromStream(stream));
                    myImageListSmall.Images.Add(Image.FromStream(stream));
                    myImageListLarge.Images.Add(Image.FromStream(stream));



                }
                listViewFile.LargeImageList = myImageList;

                listViewFile.Items.Add(new ListViewItem
                {
                    ImageIndex = count,
                    Text = fi.Name,
                    Tag = fi.FullName

                });
             //   fileinfo.MoveTo(Application.StartupPath + @"\Data\imgs\" + fileinfo.Name);
                count++;
            }
        
        }

        private void ModernButton1_Click(object sender, EventArgs e)
        {
            //set the size of imagelist
            myImageList.ImageSize = new Size(50, 50);
            myImageListSmall.ImageSize = new Size(32, 32);
            myImageListLarge.ImageSize = new Size(80, 80);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
             //   listViewFile.Items.Clear();
                foreach (string fileName in ofd.FileNames)
                {
                    fi = new FileInfo(fileName);
                    FileInfo fileinfo = new FileInfo(fileName);
                    using (FileStream stream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                    {
                        myImageList.Images.Add(Image.FromStream(stream));
                        myImageListSmall.Images.Add(Image.FromStream(stream));
                        myImageListLarge.Images.Add(Image.FromStream(stream));

                 

                    }
                    listViewFile.LargeImageList = myImageList;

                    listViewFile.Items.Add(new ListViewItem
                    {
                        ImageIndex = count,
                        Text = fi.Name,
                        Tag = fi.FullName

                    });
                    fileinfo.MoveTo(Application.StartupPath + @"\Data\imgs\" + fileinfo.Name);
                    count++;
                }
            }
        }

        private void ListViewFile_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

            if (e.Label == null)
                return;
            ImageNewName = Convert.ToString(e.Label);

            ListViewItem item1 = listViewFile.SelectedItems[0];

            FileInfo fileInfo = new FileInfo(item1.Tag.ToString());
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + ImageNewName + fileInfo.Extension);
            listViewFile.Items[count].Text = ImageNewName;

        
    }

        private void ModernButton2_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja excluir essa imagem?", "Caixa de confirmação", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (ListViewItem eachItem in listViewFile.SelectedItems)
                {
                    

                    FileInfo fileInfo = new FileInfo(eachItem.Tag.ToString());
                    fileInfo.Delete();
                    listViewFile.Items.Remove(eachItem);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
           


        }
    }
}
