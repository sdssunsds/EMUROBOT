using EMU.Util;
using MapLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class MapManagerForm : Form
    {
        private bool isMove = false;
        private int mapWidth = 0, mapHeight = 0, radius = 20;
        private double mapZoom = 1d;
        private Bitmap map = null;
        private Brush back = null;
        private Point startPoint;
        private Point lastLocation;
        private ItemType selectedItemType = ItemType.agv;
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        private Dictionary<Point, RangeMapItem> itemDict = new Dictionary<Point, RangeMapItem>();

        public MapManagerForm()
        {
            InitializeComponent();
            back = new SolidBrush(Color.GreenYellow);
        }

        private void MapManagerForm_Load(object sender, EventArgs e)
        {
            Array array = Enum.GetNames(typeof(ItemType));
            foreach (string item in array)
            {
                if (item != "agv")
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Text = item;
                    radioButton.CheckedChanged += RadioButton_CheckedChanged;
                    flowLayoutPanel1.Controls.Add(radioButton);
                }
            }
            array = Enum.GetNames(typeof(SlopeType));
            foreach (string item in array)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.Enabled = false;
                checkBoxes.Add(checkBox);
                flowLayoutPanel1.Controls.Add(checkBox);
            }
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                radius = int.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入有效半径");
                textBox1.Text = "20";
                radius = 20;
            }
            BindPic();
            DrawPic(pictureBox1.Image);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            selectedItemType = (ItemType)Enum.Parse(typeof(ItemType), radioButton.Text);
            if (selectedItemType == ItemType.slope)
            {
                foreach (CheckBox item in checkBoxes)
                {
                    item.Enabled = radioButton.Checked;
                }
            }
        }

        private void pictureBox1_LocationChanged(object sender, EventArgs e)
        {
            int width = (pictureBox1.Width - mapWidth) / 2;
            int height = (pictureBox1.Height - mapHeight) / 2;
            lastLocation = new Point(pictureBox1.Location.X + width, pictureBox1.Location.Y + height);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && selectedItemType != ItemType.agv)
            {
                double x = e.X / mapZoom;
                double y = e.Y / mapZoom;
                DrawPic(pictureBox1.Image, x, y, selectedItemType);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                int x = pictureBox1.Location.X;
                int y = pictureBox1.Location.Y;
                x += e.X - startPoint.X;
                y += e.Y - startPoint.Y;
                pictureBox1.Location = new Point(x, y); 
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                isMove = false;
            }
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                mapZoom += 0.02;
            }
            else
            {
                mapZoom -= 0.02;
            }
            if (mapZoom <= 0)
            {
                mapZoom = 0.02;
            }
            ZoomMap();
        }

        private void 加载地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "地图文件（pgm）|*.pgm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                map = Extend.PGM2BitMap(path);
                map.RotateFlip(RotateFlipType.RotateNoneFlipY);
                mapWidth = map.Width;
                mapHeight = map.Height;
                pictureBox1.Size = new Size(mapWidth, mapHeight);
                pictureBox1.Location = new Point(0 - mapWidth / 2 + Width / 2, 60 - mapHeight / 2 + Height / 2);
                mapZoom = 1d;
                BindPic();
            }
        }

        private void 加载配置的地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "地图配置文件（json）|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                using (StreamReader sr = new StreamReader(path))
                {
                    itemDict = JsonManager.JsonToObject<Dictionary<Point, RangeMapItem>>(sr.ReadToEnd());
                }
                DrawPic(pictureBox1.Image);
            }
        }

        private void 保存配置的地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "地图配置文件（json）|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(JsonManager.ObjectToJson(itemDict));
                }
                MessageBox.Show("保存完成");
            }
        }

        private void 预览配置的地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
            DrawPic(bitmap);
            new ImageForm() { Image = bitmap }.ShowDialog(this);
        }

        private void 清空配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemDict.Clear();
            BindPic();
        }

        private void BindPic()
        {
            try
            {
                Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(map, 0, 0);
                int colCount = mapWidth / radius;
                int rowCount = mapHeight / radius;
                Pen pen = new Pen(Color.Black);
                for (int i = 0; i < colCount; i++)
                {
                    g.DrawLine(pen, i * radius, 0, i * radius, mapHeight);
                }
                for (int i = 0; i < rowCount; i++)
                {
                    g.DrawLine(pen, 0, i * radius, mapWidth, i * radius);
                }
                pictureBox1.Image = null;
                pictureBox1.Image = bitmap;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DrawPic(Image img, double x = 0d, double y = 0d, ItemType itemType = ItemType.agv)
        {
            Graphics g = Graphics.FromImage(img);
            Brush[] brushes =
            {
                new SolidBrush(Color.Red),
                new SolidBrush(Color.OrangeRed),
                new SolidBrush(Color.GreenYellow),
                new SolidBrush(Color.Orange),
            };
            Brush brush = null;
            if (itemType != ItemType.agv)
            {
                switch (itemType)
                {
                    case ItemType.obstacle:
                        brush = brushes[0];
                        break;
                    case ItemType.airWall:
                        brush = brushes[1];
                        break;
                    case ItemType.road:
                        brush = brushes[2];
                        break;
                    case ItemType.slope:
                        brush = brushes[3];
                        break;
                }
                int col = (int)(x / radius);
                int row = (int)(y / radius);
                int slope = 0;
                foreach (CheckBox item in checkBoxes)
                {
                    if (item.Checked)
                    {
                        SlopeType slopeType = (SlopeType)Enum.Parse(typeof(SlopeType), item.Text);
                        slope += (int)slopeType; 
                    }
                }
                Point point = new Point(col, row);
                if (itemDict.ContainsKey(point))
                {
                    itemDict[point].ItemType = itemType;
                    itemDict[point].SlopeType = (SlopeType)slope;
                }
                else
                {
                    itemDict.Add(point, new RangeMapItem()
                    {
                        NumId = row * (mapWidth / radius) + col,
                        Name = itemType.ToString() + itemDict.Count,
                        ItemType = itemType,
                        Point = point,
                        SlopeType = (SlopeType)slope
                    });
                }
                DrawRectangle(col, row, g, brush, itemType, (SlopeType)slope);
            }
            else
            {
                foreach (KeyValuePair<Point, RangeMapItem> item in itemDict)
                {
                    switch (item.Value.ItemType)
                    {
                        case ItemType.obstacle:
                            brush = brushes[0];
                            break;
                        case ItemType.airWall:
                            brush = brushes[1];
                            break;
                        case ItemType.road:
                            brush = brushes[2];
                            break;
                        case ItemType.slope:
                            brush = brushes[3];
                            break;
                    }
                    DrawRectangle(item.Key.X, item.Key.Y, g, brush, item.Value.ItemType, item.Value.SlopeType);
                }
            }
            pictureBox1.Refresh();
        }

        private void DrawRectangle(int x, int y, Graphics g, Brush brush, ItemType itemType, SlopeType slopeType)
        {
            x *= radius;
            y *= radius;
            Rectangle rect = new Rectangle(x, y, radius, radius);
            g.FillRectangle(brush, rect);
            if (itemType == ItemType.slope)
            {
                int w = radius / 3;
                int _x = radius % 3 == 2 ? 1 : 0;
                rect = new Rectangle(x + _x + w, y + _x + w, w, w);
                g.FillRectangle(back, rect);
                RangeMapItem.CheckSlopeType(slopeType, (SlopeType slope) =>
                {
                    switch (slope)
                    {
                        case SlopeType.top:
                            rect = new Rectangle(x + _x + w, y, w, w + _x);
                            break;
                        case SlopeType.right:
                            rect = new Rectangle(x + _x + w * 2, y + _x + w, w + _x, w);
                            break;
                        case SlopeType.bottom:
                            rect = new Rectangle(x + _x + w, y + _x + w * 2, w, w + _x);
                            break;
                        case SlopeType.left:
                            rect = new Rectangle(x, y + _x + w, w + _x, w);
                            break;
                    }
                    g.FillRectangle(back, rect);
                }); 
            }
        }

        private void ZoomMap()
        {
            if (map != null)
            {
                pictureBox1.Size = new Size((int)(mapWidth * mapZoom), (int)(mapHeight * mapZoom));
                int moveX = Math.Abs(pictureBox1.Width - mapWidth) / 2;
                int moveY = Math.Abs(pictureBox1.Height - mapHeight) / 2;
                pictureBox1.LocationChanged -= pictureBox1_LocationChanged;
                pictureBox1.Location = new Point(lastLocation.X + (mapZoom > 1 ? 0 - moveX : moveX), lastLocation.Y + (mapZoom > 1 ? 0 - moveY : moveY));
                pictureBox1.LocationChanged += pictureBox1_LocationChanged;
                pictureBox1.Refresh();
            }
        }
    }
}
