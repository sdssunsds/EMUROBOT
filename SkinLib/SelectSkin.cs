using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Skin
{
    public partial class SelectSkin : Form
    {
        private List<SkinItem> skinList = new List<SkinItem>();

        public string SSK
        {
            get; private set;
        }

        public SelectSkin()
        {
            InitializeComponent();
        }

        private void SelectSkin_Load(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(Application.StartupPath + "\\Skin");
            foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
            {
                FileInfo[] files = directoryInfo.GetFiles("*.gif");
                foreach (FileInfo file in files)
                {
                    SkinItem item = new SkinItem();
                    item.Name = file.Name;
                    item.SSK = file.FullName.Replace(".gif", ".ssk");
                    item.ImagePath = file.FullName;
                    item.CheckChanged += Item_CheckChanged;
                    skinList.Add(item);
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void Item_CheckChanged(bool obj, SkinItem skin)
        {
            if (obj)
            {
                foreach (SkinItem item in skinList)
                {
                    if (item.Checked && item != skin)
                    {
                        item.Checked = false;
                        break;
                    }
                }
            }
        }

        private void SelectSkin_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (SkinItem item in skinList)
            {
                if (item.Checked)
                {
                    SSK = item.SSK;
                    break;
                }
            }
        }
    }
}
