using AlgorithmLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class FrameSelectControl : UserControl
    {
        private bool isControl = false;
        private bool isDown = false;
        private bool isDrag = false;
        private int drawMax = 0;
        private int dragIndex = -1;
        private int selectId = -1;
        private Point downLoaction = new Point(0, 0);
        private Rectangle dragRect = new Rectangle(0, 0, 0, 0);
        private Rectangle? nowRect = null;
        private Size? imgSize = null;
        private Image nowImage = null;
        private Image originalImg = null;
        private Pen rectPen = new Pen(Color.Green, 2);
        private Pen redPen = new Pen(Color.Red, 2);
        private TestForm testForm = new TestForm();
        private List<RectModel> rectangles = new List<RectModel>();

        public AlgorithmStateEnum AlgorithmState { get; set; }

        public Image Image
        {
            get { return pictureBox1.Image; }
            set
            {
                drawMax = 0;
                rectangles.Clear();
                RectModel.max = 0;
                nowImage = null;
                pictureBox1.Image = originalImg = value;
                if (value != null)
                {
                    imgSize = new Size(value.Width, value.Height);
                }
                else
                {
                    imgSize = null;
                }
            }
        }

        public Action<Keys> AltAndKeyUpAction { private get; set; }
        public Action<Keys> CtrlAndKeyUpAction { private get; set; }
        public Action<RectModel> DataChanged { private get; set; }
        public Action<RectModel> SelectRectangle { private get; set; }

        public FrameSelectControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加方框
        /// </summary>
        public void AddRectangle(AlgorithmStateEnum algorithmState, Rectangle rectangle)
        {
            RectModel model = RectModel.GetNewRectModel(algorithmState, rectangle);
            rectangles.Add(model);
            drawMax = rectangles.Count;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
        todo:
            int i = rectangles.FindLastIndex(r => r.ID == selectId && !r.Deleted);
            if (i >= 0)
            {
                if (drawMax < rectangles.Count)
                {
                    rectangles.RemoveRange(drawMax, rectangles.Count - drawMax);
                    goto todo;
                }
                rectangles.Add(new RectModel(rectangles[i]) { Rectangle = new Rectangle(0, 0, 0, 0) });
                rectangles[i].Deleted = true;
                drawMax = rectangles.Count;
                DataChanged?.Invoke(rectangles[i]);
                DrawRectangles();
            }
        }

        /// <summary>
        /// 绘制方框
        /// </summary>
        public void DrawRectangles()
        {
            if (imgSize != null)
            {
                Bitmap bitmap = new Bitmap(imgSize.Value.Width, imgSize.Value.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(originalImg, 0, 0);
                for (int i = 0; i < drawMax; i++)
                {
                    RectModel item = rectangles[i];
                    if (!item.Deleted && item.Rectangle.Width > 0 && item.Rectangle.Height > 0)
                    {
                        Pen pen = item.AlgorithmState == AlgorithmStateEnum.正常 ? rectPen : redPen;
                        g.DrawRectangle(pen, item.Rectangle);
                        if (item.ID == selectId)
                        {
                            g.DrawRectangle(pen, new Rectangle(item.Rectangle.X, item.Rectangle.Y, 5, 5));
                            g.DrawRectangle(pen, new Rectangle(item.Rectangle.X, item.Rectangle.Y + item.Rectangle.Height - 5, 5, 5));
                            g.DrawRectangle(pen, new Rectangle(item.Rectangle.X + item.Rectangle.Width - 5, item.Rectangle.Y, 5, 5));
                            g.DrawRectangle(pen, new Rectangle(item.Rectangle.X + item.Rectangle.Width - 5, item.Rectangle.Y + item.Rectangle.Height - 5, 5, 5));
                        }
                    }
                }
                pictureBox1.Image = bitmap;
            }
        }

        /// <summary>
        /// 获取所有方框
        /// </summary>
        public Dictionary<AlgorithmStateEnum, List<Rectangle>> GetRectangles()
        {
            Dictionary<AlgorithmStateEnum, List<Rectangle>> dict = new Dictionary<AlgorithmStateEnum, List<Rectangle>>();
            for (int i = 0; i < drawMax; i++)
            {
                RectModel item = rectangles[i];
                if (!item.Deleted)
                {
                    if (!dict.ContainsKey(item.AlgorithmState))
                    {
                        dict.Add(item.AlgorithmState, new List<Rectangle>());
                    }
                    dict[item.AlgorithmState].Add(item.Rectangle);
                }
            }
            return dict;
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            drawMax++;
            if (drawMax > rectangles.Count)
            {
                drawMax = rectangles.Count;
            }
            if (rectangles.Count > 0)
            {
                RectModel model = rectangles[drawMax - 1];
                model.Deleted = false;
                int i = rectangles.GetRange(0, drawMax - 1).FindLastIndex(r => r.ID == model.ID);
                if (i >= 0 && i != drawMax - 1)
                {
                    rectangles[i].Deleted = true;
                }
                DataChanged?.Invoke(model);
                DrawRectangles(); 
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Revoke()
        {
            drawMax--;
            if (drawMax < 0)
            {
                drawMax = 0;
            }
            if (rectangles.Count > 0)
            {
                RectModel model = rectangles[drawMax];
                int i = rectangles.GetRange(0, drawMax).FindLastIndex(r => r.ID == model.ID);
                if (i >= 0)
                {
                    rectangles[i].Deleted = false;
                    DataChanged?.Invoke(rectangles[i]);
                }
                DrawRectangles(); 
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        public RectModel SelectRect(int x, int y)
        {
            Point p = new Point(x, y);
            foreach (RectModel rectangle in rectangles)
            {
                if (!rectangle.Deleted && rectangle.Rectangle.Contains(p))
                {
                    selectId = rectangle.ID;
                    DrawRectangles();
                    return rectangle;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        public void SetRectangleData(RectModel rectangle)
        {
        todo:
            int i = rectangles.FindLastIndex(r => r.ID == rectangle.ID && !r.Deleted);
            if (i >= 0)
            {
                if (drawMax < rectangles.Count)
                {
                    rectangles.RemoveRange(drawMax, rectangles.Count - drawMax);
                    goto todo;
                }
                rectangles.Add(rectangle);
                rectangles[i].Deleted = true;
                selectId = rectangle.ID;
                drawMax = rectangles.Count;
                DataChanged?.Invoke(rectangle);
                DrawRectangles();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Cursor == Cursors.Default)
            {
                isDown = true;
            }
            else
            {
                isDrag = true;
                if (dragIndex >= 0)
                {
                    dragRect = rectangles[dragIndex].Rectangle; 
                }
            }
            downLoaction = new Point(e.X, e.Y);
            nowImage = pictureBox1.Image;
            this.Focus();
            TestVariable();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (isDown)
            {
                isDown = false;
                DrawRectangles();
            }
            else if (isDrag)
            {
                isDrag = false;
                dragIndex = -1;
                pictureBox1.Cursor = Cursors.Default;
            }
            TestVariable();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                int w = Math.Abs(downLoaction.X - e.X);
                int h = Math.Abs(downLoaction.Y - e.Y);
                if ((w > 5 && h > 0) || (h > 5 && w > 0))
                {
                    int x = downLoaction.X < e.X ? downLoaction.X : e.X;
                    int y = downLoaction.Y < e.Y ? downLoaction.Y : e.Y;
                    nowRect = new Rectangle(x, y, w, h);
                    DrawRectangle(nowRect.Value);
                }
            }
            else if (isDrag && dragIndex >= 0)
            {
                int x = e.X - downLoaction.X;
                int y = e.Y - downLoaction.Y;
                RectModel model = rectangles[dragIndex];
                if (pictureBox1.Cursor == Cursors.SizeAll)
                {
                    model.Rectangle = new Rectangle(model.Rectangle.X + x, model.Rectangle.Y + y, model.Rectangle.Width, model.Rectangle.Height);
                }
                else if (pictureBox1.Cursor == Cursors.SizeWE)
                {
                    bool isLeft = Math.Abs(model.Rectangle.X - downLoaction.X) <= 5;
                    model.Rectangle = new Rectangle(model.Rectangle.X + (isLeft ? x : 0), model.Rectangle.Y, model.Rectangle.Width + (isLeft ? 0 - x : x), model.Rectangle.Height);
                }
                else if (pictureBox1.Cursor == Cursors.SizeNS)
                {
                    bool isTop = Math.Abs(model.Rectangle.Y - downLoaction.Y) <= 5;
                    model.Rectangle = new Rectangle(model.Rectangle.X, model.Rectangle.Y + (isTop ? y : 0), model.Rectangle.Width, model.Rectangle.Height + (isTop ? 0 - y : y));
                }
                else if (pictureBox1.Cursor == Cursors.SizeNWSE)
                {
                    bool isLT = Math.Abs(model.Rectangle.X - downLoaction.X) <= 5;
                    model.Rectangle = new Rectangle(model.Rectangle.X + (isLT ? x : 0), model.Rectangle.Y + (isLT ? y : 0), model.Rectangle.Width + (isLT ? 0 - x : x), model.Rectangle.Height + (isLT ? 0 - y : y));
                }
                else if (pictureBox1.Cursor == Cursors.SizeNESW)
                {
                    bool isRT = Math.Abs(model.Rectangle.X - downLoaction.X) <= 5;
                    model.Rectangle = new Rectangle(model.Rectangle.X + (isRT ? x : 0), model.Rectangle.Y + (isRT ? 0 : y), model.Rectangle.Width + (isRT ? 0 - x : x), model.Rectangle.Height + (isRT ? y : 0 - y));
                }
                downLoaction = new Point(e.X, e.Y);
                nowRect = model.Rectangle;
                DrawRectangles();
            }
            else if (!isDown && !isDrag && isControl)
            {
                int we = rectangles.FindIndex(r => !r.Deleted && (new Rectangle(r.Rectangle.X, r.Rectangle.Y + 5, 5, r.Rectangle.Height - 10).Contains(e.X, e.Y) || new Rectangle(r.Rectangle.X + r.Rectangle.Width - 5, r.Rectangle.Y + 5, 5, r.Rectangle.Height - 10).Contains(e.X, e.Y)));
                if (we >= 0)
                {
                    pictureBox1.Cursor = Cursors.SizeWE;
                    dragIndex = we;
                    return;
                }
                int ns = rectangles.FindIndex(r => !r.Deleted && (new Rectangle(r.Rectangle.X + 5, r.Rectangle.Y, r.Rectangle.Width - 10, 5).Contains(e.X, e.Y) || new Rectangle(r.Rectangle.X + 5, r.Rectangle.Y + r.Rectangle.Height - 5, r.Rectangle.Width - 10, 5).Contains(e.X, e.Y)));
                if (ns >= 0)
                {
                    pictureBox1.Cursor = Cursors.SizeNS;
                    dragIndex = ns;
                    return;
                }
                int nwse = rectangles.FindIndex(r => !r.Deleted && (new Rectangle(r.Rectangle.X, r.Rectangle.Y, 5, 5).Contains(e.X, e.Y) || new Rectangle(r.Rectangle.X + r.Rectangle.Width - 5, r.Rectangle.Y + r.Rectangle.Height - 5, 5, 5).Contains(e.X, e.Y)));
                if (nwse >= 0)
                {
                    pictureBox1.Cursor = Cursors.SizeNWSE;
                    dragIndex = nwse;
                    return;
                }
                int nesw = rectangles.FindIndex(r => !r.Deleted && (new Rectangle(r.Rectangle.X + r.Rectangle.Width - 5, r.Rectangle.Y, 5, 5).Contains(e.X, e.Y) || new Rectangle(r.Rectangle.X, r.Rectangle.Y + r.Rectangle.Height - 5, 5, 5).Contains(e.X, e.Y)));
                if (nesw >= 0)
                {
                    pictureBox1.Cursor = Cursors.SizeNESW;
                    dragIndex = nesw;
                    return;
                }
                int i = rectangles.FindIndex(r => r.ID == selectId && !r.Deleted && r.Rectangle.Contains(e.X, e.Y));
                if (i >= 0)
                {
                    pictureBox1.Cursor = Cursors.SizeAll;
                    dragIndex = i;
                    return;
                }
                pictureBox1.Cursor = Cursors.Default;
            }
            TestVariable();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (nowRect != null)
            {
                RectModel model = null;
                if (isDrag && dragIndex >= 0)
                {
                    model = new RectModel(rectangles[dragIndex]);
                    rectangles[dragIndex].Rectangle = dragRect;
                    SetRectangleData(model);
                    SelectRectangle?.Invoke(model);
                }
                else
                {
                    model = RectModel.GetNewRectModel(AlgorithmState, nowRect.Value);
                    selectId = model.ID;
                    if (drawMax < rectangles.Count)
                    {
                        rectangles.RemoveRange(drawMax, rectangles.Count - drawMax);
                    }
                    rectangles.Add(model);
                    drawMax = rectangles.Count;
                    DrawRectangles();
                    SelectRectangle?.Invoke(model);
                }
            }
            else
            {
                RectModel rect = SelectRect(e.X, e.Y);
                SelectRectangle?.Invoke(rect);
            }
            isDown = isDrag = false;
            dragIndex = -1;
            nowRect = null;
            nowImage = null;
            TestVariable();
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                isControl = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                testForm.FormClosed += TestForm_FormClosed;
                testForm.Show();
            }
            TestVariable();
        }

        private void control_KeyUp(object sender, KeyEventArgs e)
        {
            isControl = isDrag = false;
            pictureBox1.Cursor = Cursors.Default;
            if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.Z)
            {
                Revoke();
            }
            else if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.Y)
            {
                Redo();
            }
            else if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.D)
            {
                Delete();
            }
            else if ((Control.ModifierKeys & Keys.Control) != 0)
            {
                CtrlAndKeyUpAction?.Invoke(e.KeyCode);
            }
            else if ((Control.ModifierKeys & Keys.Alt) != 0)
            {
                AltAndKeyUpAction?.Invoke(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
            TestVariable("键盘按键", e.KeyCode);
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            testForm = null;
            testForm = new TestForm();
        }

        private void DrawRectangle(Rectangle rectangle)
        {
            if (nowImage != null && imgSize != null)
            {
                Bitmap bitmap = new Bitmap(imgSize.Value.Width, imgSize.Value.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(nowImage, 0, 0);
                g.DrawRectangle(AlgorithmState == AlgorithmStateEnum.正常 ? rectPen : redPen, rectangle);
                pictureBox1.Image = bitmap;
            }
        }

        private void TestVariable(string name = "", object value = null)
        {
            testForm.SetTestVariable("isControl", isControl);
            testForm.SetTestVariable("isDown", isDown);
            testForm.SetTestVariable("isDrag", isDrag);
            testForm.SetTestVariable("dragIndex", dragIndex);
            testForm.SetTestVariable("downLoaction", downLoaction);
            testForm.SetTestVariable("nowRect", nowRect);
            testForm.SetTestVariable("选中编号", selectId);
            testForm.SetTestVariable("操作数量", rectangles.Count);
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectModel item = rectangles[i];
                testForm.SetTestVariable($"对象{i}", $"ID = {item.ID}, Del = {item.Deleted}, State = {item.AlgorithmState}");
            }
            if (!string.IsNullOrEmpty(name))
            {
                testForm.SetTestVariable(name, value);
            }
        }
    }

    public class RectModel
    {
        public static int max = 0;
        public bool Deleted { get; set; }
        public int ID { get; set; }
        public AlgorithmStateEnum AlgorithmState { get; set; }
        public Rectangle Rectangle { get; set; }
        public RectModel() { }
        public RectModel(RectModel model)
        {
            this.Deleted = model.Deleted;
            this.ID = model.ID;
            this.AlgorithmState = model.AlgorithmState;
            this.Rectangle = model.Rectangle;
        }
        public static RectModel GetNewRectModel(AlgorithmStateEnum state, Rectangle rectangle)
        {
            max++;
            return new RectModel()
            {
                Deleted = false,
                ID = max,
                AlgorithmState = state,
                Rectangle = rectangle
            };
        }
    }
}
