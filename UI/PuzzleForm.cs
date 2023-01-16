using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class PuzzleForm : Form
    {
        private bool isPuzzle = false;
        private int rollIndex = 0;
        private int rollSpeed = 10;
        private int puzzleIndex = 0;
        private int puzzleGIndex = 0;
        private int puzzleCount = 0;
        private string selectedPath
        {
            get { return tb_selectedPath.Text; }
            set { tb_selectedPath.Text = value; }
        }
        private string puzzlePath = "";
        private Bitmap puzzleMap = null;
        private Graphics puzzleG = null;
        private int[] puzzleMax = new int[8];
        private Label[] labels = null;
        private ProgressBar[] bars = null;
        private TextBox[] textBoxes = null;

        public PuzzleForm()
        {
            InitializeComponent();
        }

        private void PuzzleForm_Load(object sender, EventArgs e)
        {
            selectedPath = Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.LinePathName;
            labels = new Label[] { lb_unit1, lb_unit2, lb_unit3, lb_unit4, lb_unit5, lb_unit6, lb_unit7, lb_unit8 };
            bars = new ProgressBar[] { progressBar3, progressBar4, progressBar5, progressBar6, progressBar7, progressBar8, progressBar9, progressBar2 };
            textBoxes = new TextBox[] { tb_num1, tb_num2, tb_num3, tb_num4, tb_num5, tb_num6, tb_num7, tb_num8 };
            radioButton3_CheckedChanged(null, null);
            foreach (TextBox item in textBoxes)
            {
                item.Text = "12.5";
            }
            int _w = 410 - pictureBox.Width;
            panel1.Size = new Size(410, panel1.Height);
            pictureBox.Size = new Size(410, 226);
            this.Size = new Size(this.Width + _w, this.Height);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                selectedPath = Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.LinePathName;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btn_selectDir.Enabled = radioButton2.Checked;
            if (radioButton2.Checked)
            {
                btn_selectDir_Click(null, null);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                foreach (Label item in labels)
                {
                    item.Text = "%";
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                foreach (Label item in labels)
                {
                    item.Text = "张";
                }
                foreach (TextBox item in textBoxes)
                {
                    if (item.Text.Contains("."))
                    {
                        item.Text = item.Text.Substring(0, item.Text.IndexOf("."));
                    }
                }
            }
        }

        private void btn_selectDir_Click(object sender, EventArgs e)
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = null;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = selectedPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }
        }

        private void btn_puzzlePath_Click(object sender, EventArgs e)
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = null;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(tb_puzzlePath.Text))
            {
                folderBrowserDialog.SelectedPath = tb_puzzlePath.Text; 
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb_puzzlePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btn_puzzle_Click(object sender, EventArgs e)
        {
            if (btn_puzzle.Text == "停止拼图")
            {
                btn_puzzle.Text = "开始拼图";
                isPuzzle = false;
                return;
            }

            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("未选择原始图片位置");
                return;
            }
            if (string.IsNullOrEmpty(tb_puzzlePath.Text))
            {
                MessageBox.Show("未选择拼图保存位置");
                return;
            }
            FileInfo[] files = new DirectoryInfo(selectedPath).GetFiles("*.jpg");

            var puzzleFunc = new Action(() =>
            {
                try
                {
                    foreach (FileInfo file in files)
                    {
                        if (!isPuzzle)
                        {
                            return;
                        }
                        SetImage(Image.FromFile(file.FullName));
                        BeginInvoke(new Action(() => { progressBar1.Value++; }));
                        if (!cb_high.Checked)
                        {
                            Thread.Sleep(1000); 
                        }
                    }
                    Invoke(new Action(() =>
                    {
                        PuzzleImage(null);
                        btn_puzzle.Text = "开始拼图";
                    }));
                }
                catch (Exception) { }
            });
            
            progressBar1.Visible = true;
            progressBar1.Maximum = puzzleCount = files.Length;
            if (radioButton3.Checked)
            {
                double d1, d2, d3, d4, d5, d6, d7, d8;
                if (!double.TryParse(textBoxes[0].Text, out d1))
                {
                    MessageBox.Show("第一节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[1].Text, out d2))
                {
                    MessageBox.Show("第二节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[2].Text, out d3))
                {
                    MessageBox.Show("第三节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[3].Text, out d4))
                {
                    MessageBox.Show("第四节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[4].Text, out d5))
                {
                    MessageBox.Show("第五节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[5].Text, out d6))
                {
                    MessageBox.Show("第六节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[6].Text, out d7))
                {
                    MessageBox.Show("第七节车输入的数值不正确");
                    return;
                }
                if (!double.TryParse(textBoxes[7].Text, out d8))
                {
                    MessageBox.Show("第八节车输入的数值不正确");
                    return;
                }
                puzzleMax[7] = (int)(d8 / 100 * puzzleCount);
                puzzleMax[6] = (int)(d7 / 100 * puzzleCount);
                puzzleMax[5] = (int)(d6 / 100 * puzzleCount);
                puzzleMax[4] = (int)(d5 / 100 * puzzleCount);
                puzzleMax[3] = (int)(d4 / 100 * puzzleCount);
                puzzleMax[2] = (int)(d3 / 100 * puzzleCount);
                puzzleMax[1] = (int)(d2 / 100 * puzzleCount);
                puzzleMax[0] = (int)(d1 / 100 * puzzleCount);
            }
            else if (radioButton4.Checked)
            {
                if (textBoxes[0].Text.Contains("-") &&
                    textBoxes[1].Text.Contains("-") &&
                    textBoxes[2].Text.Contains("-") &&
                    textBoxes[3].Text.Contains("-") &&
                    textBoxes[4].Text.Contains("-") &&
                    textBoxes[5].Text.Contains("-") &&
                    textBoxes[6].Text.Contains("-") &&
                    textBoxes[7].Text.Contains("-"))
                {
                    int[,] range = new int[8, 2];
                    try
                    {
                        int max = 0;
                        for (int i = 0; i < textBoxes.Length; i++)
                        {
                            string[] vs = textBoxes[i].Text.Split('-');
                            range[i, 0] = int.Parse(vs[0].Trim());
                            range[i, 1] = int.Parse(vs[1].Trim());
                            puzzleMax[i] = range[i, 1] - range[i, 0] + 1;
                            max += puzzleMax[i];
                        }
                        progressBar1.Maximum = max;
                        puzzleFunc = new Action(() =>
                        {
                            for (int i = 0; i < range.GetLength(0); i++)
                            {
                                for (int j = range[i, 0]; j < files.Length && j <= range[i, 1]; j++)
                                {
                                    if (!isPuzzle)
                                    {
                                        return;
                                    }
                                    SetImage(Image.FromFile(files[j].FullName));
                                    BeginInvoke(new Action(() => { progressBar1.Value++; }));
                                    if (!cb_high.Checked)
                                    {
                                        Thread.Sleep(1000); 
                                    }
                                }
                            }
                            Invoke(new Action(() =>
                            {
                                PuzzleImage(null);
                                btn_puzzle.Text = "开始拼图";
                            }));
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else
                {
                    int i1, i2, i3, i4, i5, i6, i7, i8;
                    if (!int.TryParse(textBoxes[0].Text, out i1))
                    {
                        MessageBox.Show("第一节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[1].Text, out i2))
                    {
                        MessageBox.Show("第二节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[2].Text, out i3))
                    {
                        MessageBox.Show("第三节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[3].Text, out i4))
                    {
                        MessageBox.Show("第四节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[4].Text, out i5))
                    {
                        MessageBox.Show("第五节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[5].Text, out i6))
                    {
                        MessageBox.Show("第六节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[6].Text, out i7))
                    {
                        MessageBox.Show("第七节车输入的数值不正确");
                        return;
                    }
                    if (!int.TryParse(textBoxes[7].Text, out i8))
                    {
                        MessageBox.Show("第八节车输入的数值不正确");
                        return;
                    }
                    puzzleMax[7] = i8;
                    puzzleMax[6] = i7;
                    puzzleMax[5] = i6;
                    puzzleMax[4] = i5;
                    puzzleMax[3] = i4;
                    puzzleMax[2] = i3;
                    puzzleMax[1] = i2;
                    puzzleMax[0] = i1; 
                }
            }
            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].Value = 0;
                bars[i].Maximum = puzzleMax[i];
            }
            btn_puzzle.Text = "停止拼图";
            isPuzzle = true;
            progressBar1.Value = 0;
            rollIndex = 0;
            puzzleIndex = 0;
            puzzleGIndex = 0;
            puzzlePath = tb_puzzlePath.Text + "\\";
            Task.Run(puzzleFunc);
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

        private void SetImage(Image image)
        {
            Invoke(new Action(() =>
            {
                PuzzleImage(image);
            }));
        }

        private void PuzzleImage(Image image)
        {
            if (puzzleIndex >= puzzleMax.Length || puzzleGIndex == puzzleMax[puzzleIndex])
            {
                rollIndex = 0;
                puzzleGIndex = 0;
                puzzleIndex++;
                if (puzzleIndex <= puzzleMax.Length)
                {
                    if (!string.IsNullOrEmpty(tb_name.Text))
                    {
                        CompressImg(puzzlePath + string.Format(tb_name.Text, puzzleIndex) + ".jpg", puzzleMap, 100);
                    }
                    else
                    {
                        CompressImg(puzzlePath + puzzleIndex + ".jpg", puzzleMap, 100);
                    } 
                }
                puzzleG?.Dispose();
                puzzleG = null;
                puzzleMap?.Dispose();
                puzzleMap = null;
                GC.Collect();
            }
            if (image != null && puzzleIndex < puzzleMax.Length)
            {
                if (puzzleGIndex == 0)
                {
                    puzzleMap = new Bitmap(2048, puzzleMax[puzzleIndex] * 1130);
                    puzzleG = Graphics.FromImage(puzzleMap);
                    if (!cb_high.Checked)
                    {
                        pictureBox.Location = new Point(0, 0);
                        pictureBox.Size = new Size(410, puzzleMax[puzzleIndex] * 226);
                        pictureBox.Image = puzzleMap; 
                    }
                }
                int index = rollIndex;
                puzzleG.DrawImage(image, new Rectangle(0, 1130 * puzzleGIndex, puzzleMap.Width, 1130), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                puzzleGIndex++;
                rollIndex++;
                bars[puzzleIndex].Value++;
                if (!cb_high.Checked)
                {
                    pictureBox.Refresh();
                    Task.Run(() =>
                    {
                        Roll(index);
                    });  
                }
            }
        }

        private void CompressImg(string path, Image img, int quality)
        {
            int i = 0;
        SaveImage:
            try
            {
                EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
                ImageCodecInfo jpegCodec = null;
                ImageCodecInfo[] codes = ImageCodecInfo.GetImageEncoders();
                for (int j = 0; j < codes.Length; j++)
                {
                    if (codes[j].MimeType == "image/jpeg")
                    {
                        jpegCodec = codes[j];
                        break;
                    }
                }

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                img.Save(path, jpegCodec, encoderParams);
            }
            catch (Exception)
            {
                if (i < 10)
                {
                    Thread.Sleep(1000);
                    i++;
                    goto SaveImage;
                }
            }
        }

        private void cb_high_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_high.Checked)
            {
                MessageBox.Show("高速模式无法显示左侧预览图，同时会消耗巨大的系统资源。", "警告", MessageBoxButtons.OK);
            }
        }
    }
}
