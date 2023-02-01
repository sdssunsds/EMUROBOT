using MapLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AStarAlgorithm
{
    public partial class DrawForm : Form
    {
        private int width, height;
        private int size = 3;
        private float zoom = 1f;
        private Bitmap bitmap;
        private Graphics g;

        public DrawForm()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            g.Clear(Color.White);
            Invoke(new Action(() => { pic.Refresh(); }));
        }

        private void DrawForm_Load(object sender, EventArgs e)
        {
            AStar.TestMap += AStar_TestMap;
            AStar.TestRun += AStar_TestRun;
            pic.MouseWheel += Pic_MouseWheel;
        }

        private void AStar_TestMap(RangeMap map)
        {
            width = map.Range.Width * size;
            height = map.Range.Height * size;
            int w = (int)(width * zoom);
            int h = (int)(height * zoom);
            if (pic.Width != w || pic.Height != h)
            {
                pic.Size = new Size(w, h);
                bitmap = new Bitmap(width, height);
                g = Graphics.FromImage(bitmap);
                pic.Image = bitmap;
            }
            Pen pen = new Pen(Color.White);
            foreach (KeyValuePair<Point, RangeMapItem> item in map.RangeItemDict)
            {
                switch (item.Value.ItemType)
                {
                    case ItemType.obstacle:
                        pen.Color = Color.Red;
                        break;
                    case ItemType.agv:
                        pen.Color = Color.Blue;
                        break;
                    case ItemType.airWall:
                        pen.Color = Color.OrangeRed;
                        break;
                    case ItemType.road:
                        pen.Color = Color.White;
                        break;
                    case ItemType.slope:
                        pen.Color = Color.Green;
                        break;
                }
                if (item.Value.ItemType == ItemType.slope)
                {
                    Dictionary<SlopeType, Line> lineDict = new Dictionary<SlopeType, Line>()
                    {
                        { SlopeType.top, new Line(item.Key, new Point(item.Key.X + size, item.Key.Y)) },
                        { SlopeType.right, new Line(new Point(item.Key.X + size, item.Key.Y), new Point(item.Key.X + size, item.Key.Y + size)) },
                        { SlopeType.bottom, new Line(new Point(item.Key.X, item.Key.Y + size), new Point(item.Key.X + size, item.Key.Y + size)) },
                        { SlopeType.left, new Line(item.Key, new Point(item.Key.X, item.Key.Y + size)) },
                    };
                    RangeMapItem.CheckSlopeType(item.Value, (SlopeType slope) =>
                    {
                        lineDict.Remove(slope);
                    });
                    foreach (KeyValuePair<SlopeType, Line> line in lineDict)
                    {
                        g.DrawLine(pen, line.Value.Start, line.Value.End);
                    }
                }
                else
                {
                    g.DrawRectangle(pen, new Rectangle(item.Key, new Size(size, size)));
                }
            }
        }

        private void AStar_TestRun(List<Point> list)
        {
            Pen pen = new Pen(Color.Purple);
            foreach (Point point in list)
            {
                g.DrawRectangle(pen, new Rectangle(point, new Size(size, size)));
            }
            Invoke(new Action(() => { pic.Refresh(); }));
        }

        private void Pic_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom += 0.1f;
            }
            else
            {
                zoom -= 0.1f;
            }
            pic.Size = new Size((int)(width * zoom), (int)(height * zoom));
        }
    }
}
