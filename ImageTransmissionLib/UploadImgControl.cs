using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMU.ImageTransmission
{
    public partial class UploadImgControl : UserControl
    {
        public UploadImgControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                folderBrowserDialog.SelectedPath = textBox1.Text; 
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                FileInfo[] fileInfos = new DirectoryInfo(textBox1.Text).GetFiles();
                checkedListBox1.Items.Clear();
                foreach (FileInfo file in fileInfos)
                {
                    checkedListBox1.Items.Add(file.Name);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();
            foreach (object item in checkedListBox1.Items)
            {
                string path = textBox1.Text + "\\" + item.ToString();
                string Extension = path.Split('.')[1].ToLower();
                if (Extension == "jpg" || Extension == "png" || Extension == "gif" || Extension == "bmp" || Extension == "tif" || Extension == "tiff")
                {
                    files.Add(path);
                }
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = files.Count;
            Task.Run(() =>
            {
                for (int i = 0; i < files.Count; i++)
                {
                    string file = files[i];
                    if (File.Exists(file))
                    {
                        using (Image image = Image.FromFile(file))
                        {
                            UploadImages.Instance.UploadImage(image, 10000 + i);
                        }
                    }
                    Invoke(new Action(() =>
                    {
                        progressBar1.Value++;
                    }));
                }
                Invoke(new Action(() =>
                {
                    if (MessageBox.Show("上传完成，是否进行合并？","提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        UploadImages.Instance.UploadComplete();
                    }
                }));
            });
        }
    }
}
