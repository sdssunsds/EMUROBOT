using EMU.Parameter;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Basler
{
    public partial class CameraControl : UserControl
    {
        private int rollIndex = 0;
        private int rollSpeed = 3;
        private int puzzleGIndex = 0;
        private Bitmap puzzleMap = null;
        private Graphics puzzleG = null;

        public CameraControl()
        {
            InitializeComponent();
        }

        private void CameraControl_Load(object sender, EventArgs e)
        {
            panel.Size = new Size(410, panel.Height);
            pictureBox.Size = new Size(410, 226);
        }

        private void Roll(int index)
        {
            try
            {
                int heightCount = (index + 1) * 226;
                if (heightCount > panel1.Height)
                {
                    int moveLength = heightCount - panel1.Height + pictureBox.Location.Y;
                    int moveCount = moveLength / rollSpeed;
                    if (moveLength % rollSpeed > 0)
                    {
                        moveCount++;
                    }
                    for (int i = 0; i < moveCount; i++)
                    {
                        Invoke(new Action(() =>
                        {
                            pictureBox.Location = new Point(0, pictureBox.Location.Y - rollSpeed);
                        }));
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception) { }
        }

        private void SetLineImage(Image image)
        {
            BeginInvoke(new Action(() =>
            {
                if (puzzleGIndex == 30)
                {
                    rollIndex = 0;
                    puzzleGIndex = 0;
                    puzzleG.Dispose();
                    puzzleG = null;
                    puzzleMap.Dispose();
                    puzzleMap = null;
                }
                if (image != null)
                {
                    if (puzzleGIndex == 0)
                    {
                        puzzleMap = new Bitmap(2048, 30 * 1130);
                        puzzleG = Graphics.FromImage(puzzleMap);
                        pictureBox.Location = new Point(0, 0);
                        pictureBox.Size = new Size(410, 30 * 226);
                        pictureBox.Image = puzzleMap;
                    }
                    int index = rollIndex;
                    puzzleG.DrawImage(image, new Rectangle(0, 1130 * puzzleGIndex, puzzleMap.Width, 1130), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                    puzzleGIndex++;
                    rollIndex++;
                    pictureBox.Refresh();
                    EMU.Util.ThreadManager.TaskRun((EMU.Util.ThreadEventArgs threadEventArgs) =>
                    {
                        threadEventArgs.ThreadName = "Basler.CameraControl.SetLineImage_0";
                        threadEventArgs.AddVariable(index);
                        threadEventArgs.AddVariableName("index");
                        Roll(index);
                    }); 
                }
            }));
        }

        public void ClearImage(CameraName camera)
        {
            BeginInvoke(new Action(() =>
            {
                switch (camera)
                {
                    case CameraName.Line:
                        rollIndex = 0;
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = null;
                        break;
                    case CameraName.Front:
                        pb_front.Image?.Dispose();
                        pb_front.Image = null;
                        break;
                    case CameraName.Back:
                        pb_back.Image?.Dispose();
                        pb_back.Image = null;
                        break;
                }
            }));
        }

        public void SetImage(Image image, CameraName camera)
        {
            BeginInvoke(new Action(() =>
            {
                switch (camera)
                {
                    case CameraName.Line:
                        SetLineImage(image);
                        break;
                    case CameraName.Front:
                        pb_front.Image?.Dispose();
                        pb_front.Image = image;
                        break;
                    case CameraName.Back:
                        pb_back.Image?.Dispose();
                        pb_back.Image = image;
                        break;
                }
            }));
        }
    }
}
