using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace _3rakendust_Windows_Forms
{
    public partial class FotoViewer : Form
    {
        private PictureBox pctbox;
        private TableLayoutPanel tblp;
        private FlowLayoutPanel flowLayoutPanel1;

        private Button showButton;
        private Button clearButton;
        private Button backgroundButton;
        private Button closeButton;
        private Button slideshowButton;
        private Button saveButton;

        private Button paintButton;
        private bool isPainting = false;
        private PointF lastImagePoint; // последняя точка в координатах ИЗОБРАЖЕНИЯ

        // НОВОЕ: кнопка поворота изображения
        private Button rotateButton;

        private CheckBox stretchCheckBox;

        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;
        private SaveFileDialog saveFileDialog1;

        private List<Image> images = new List<Image>();
        private int currentImage = 0;

        private Timer slideshowTimer;

        public FotoViewer(string name, int width, int height)
        {
            Text = name;
            Width = width;
            Height = height;

            CreateInterface();
            pctbox.SizeMode = PictureBoxSizeMode.Zoom;
            pctbox.MouseDown += Pctbox_MouseDown;
            pctbox.MouseMove += Pctbox_MouseMove;
        }

        private void CreateInterface()
        {
            tblp = new TableLayoutPanel();

            tblp.Dock = DockStyle.Fill;
            tblp.ColumnCount = 2;
            tblp.RowCount = 2;

            tblp.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 15F));
            tblp.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 85F));

            tblp.RowStyles.Add(
                new RowStyle(SizeType.Percent, 90F));
            tblp.RowStyles.Add(
                new RowStyle(SizeType.Percent, 10F));

            Controls.Add(tblp);

            pctbox = new PictureBox();

            pctbox.Dock = DockStyle.Fill;
            pctbox.BorderStyle = BorderStyle.Fixed3D;
            pctbox.BackColor = Color.White;
            pctbox.SizeMode = PictureBoxSizeMode.Zoom;

            tblp.Controls.Add(pctbox, 1, 0);

            stretchCheckBox = new CheckBox();

            stretchCheckBox.Text = "Venitama";
            stretchCheckBox.AutoSize = true;
            stretchCheckBox.CheckedChanged += StretchCheckBox_CheckedChanged;

            tblp.Controls.Add(stretchCheckBox, 0, 1);

            flowLayoutPanel1 = new FlowLayoutPanel();

            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.WrapContents = false;

            tblp.Controls.Add(flowLayoutPanel1, 1, 1);

            showButton = new Button();
            showButton.Text = "Näita pilti";
            showButton.AutoSize = true;
            showButton.Click += ShowButton_Click;

            clearButton = new Button();
            clearButton.Text = "Tühjenda";
            clearButton.AutoSize = true;
            clearButton.Click += ClearButton_Click;

            backgroundButton = new Button();
            backgroundButton.Text = "Taustavärv";
            backgroundButton.AutoSize = true;
            backgroundButton.Click += BackgroundButton_Click;

            slideshowButton = new Button();
            slideshowButton.Text = "Slaidishow";
            slideshowButton.AutoSize = true;
            slideshowButton.Click += SlideshowButton_Click;

            saveButton = new Button();
            saveButton.Text = "Salvesta";
            saveButton.AutoSize = true;
            saveButton.Click += SaveButton_Click;

            closeButton = new Button();
            closeButton.Text = "Sulge";
            closeButton.AutoSize = true;
            closeButton.Click += CloseButton_Click;

            paintButton = new Button();
            paintButton.Text = "Joonista";
            paintButton.AutoSize = true;
            paintButton.Click += PaintButton_Click;

            // НОВОЕ: создание кнопки поворота
            rotateButton = new Button();
            rotateButton.Text = "Pööra";
            rotateButton.AutoSize = true;
            rotateButton.Click += RotateButton_Click;

            // НОВОЕ: добавление кнопки поворота в панель
            flowLayoutPanel1.Controls.Add(rotateButton);
            flowLayoutPanel1.Controls.Add(paintButton);
            flowLayoutPanel1.Controls.Add(closeButton);
            flowLayoutPanel1.Controls.Add(saveButton);
            flowLayoutPanel1.Controls.Add(slideshowButton);
            flowLayoutPanel1.Controls.Add(backgroundButton);
            flowLayoutPanel1.Controls.Add(clearButton);
            flowLayoutPanel1.Controls.Add(showButton);

            colorDialog1 = new ColorDialog();

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter =
                "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                "JPEG Files (*.jpg)|*.jpg|" +
                "PNG Files (*.png)|*.png|" +
                "BMP Files (*.bmp)|*.bmp|" +
                "All files (*.*)|*.*";

            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter =
                "JPEG Image|*.jpg|" +
                "PNG Image|*.png|" +
                "Bitmap Image|*.bmp";

            slideshowTimer = new Timer();
            slideshowTimer.Interval = 2000;
            slideshowTimer.Tick += SlideshowTimer_Tick;
        }

        // НОВОЕ: поворот изображения на 90 градусов по часовой стрелке
        private void RotateButton_Click(object sender, EventArgs e)
        {
            if (pctbox.Image == null)
            {
                MessageBox.Show("Pilt puudub.");
                return;
            }

            // Image в списке images - тот же объект, поэтому поворот сохранится
            // и в слайдшоу, и при сохранении файла
            pctbox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pctbox.Refresh();
        }

        // Переводит координаты мыши (в PictureBox) в координаты самого изображения.
        // Учитывает режимы Zoom (с полями по краям) и StretchImage.
        private bool TryGetImagePoint(Point p, out PointF result, out float scale)
        {
            result = PointF.Empty;
            scale = 1f;

            Image img = pctbox.Image;
            if (img == null)
                return false;

            Rectangle cr = pctbox.ClientRectangle;
            if (cr.Width <= 0 || cr.Height <= 0)
                return false;

            if (pctbox.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                float sx = (float)cr.Width / img.Width;
                float sy = (float)cr.Height / img.Height;

                result = new PointF(p.X / sx, p.Y / sy);
                scale = (sx + sy) / 2f;
            }
            else // Zoom
            {
                scale = Math.Min(
                    (float)cr.Width / img.Width,
                    (float)cr.Height / img.Height);

                float offsetX = (cr.Width - img.Width * scale) / 2f;
                float offsetY = (cr.Height - img.Height * scale) / 2f;

                result = new PointF(
                    (p.X - offsetX) / scale,
                    (p.Y - offsetY) / scale);
            }

            return true;
        }

        // Рисует прямо на Bitmap, поэтому рисунок не пропадает при перерисовке,
        // переключении слайдов, повороте и попадает в сохранённый файл.
        private void DrawOnImage(PointF from, PointF to, float scale)
        {
            if (pctbox.Image == null)
                return;

            float penWidth = 5f / scale; // на экране линия всегда ~5 пикселей

            using (Graphics g = Graphics.FromImage(pctbox.Image))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (from == to)
                {
                    using (Brush brush = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(
                            brush,
                            to.X - penWidth / 2f,
                            to.Y - penWidth / 2f,
                            penWidth,
                            penWidth);
                    }
                }
                else
                {
                    using (Pen pen = new Pen(Color.Black, penWidth))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        pen.LineJoin = LineJoin.Round;

                        g.DrawLine(pen, from, to);
                    }
                }
            }

            pctbox.Invalidate();
        }

        private void Pctbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isPainting || e.Button != MouseButtons.Left)
                return;

            PointF point;
            float scale;

            if (TryGetImagePoint(e.Location, out point, out scale))
            {
                lastImagePoint = point;
                DrawOnImage(point, point, scale); // точка при простом клике
            }
        }

        private void Pctbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isPainting || e.Button != MouseButtons.Left)
                return;

            PointF point;
            float scale;

            if (!TryGetImagePoint(e.Location, out point, out scale))
                return;

            DrawOnImage(lastImagePoint, point, scale);
            lastImagePoint = point;
        }
        private void PaintButton_Click(object sender, EventArgs e)
        {
            isPainting = !isPainting;

            if (isPainting)
                paintButton.Text = "Peata joonistamine";
            else
                paintButton.Text = "Joonista";
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string file = openFileDialog1.FileName;

            try
            {
                using (Image temp = Image.FromFile(file))
                {
                    Image newImage = new Bitmap(temp);

                    images.Add(newImage);

                    currentImage = images.Count - 1;

                    pctbox.Image = images[currentImage];
                }
            }
            catch
            {
                MessageBox.Show("Pilti ei saanud avada.");
            }
        }

        private void SlideshowButton_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                MessageBox.Show("Lae kõigepealt vähemalt üks pilt.");
                return;
            }

            if (images.Count == 1)
            {
                MessageBox.Show("Lisa slideshow jaoks vähemalt kaks pilti.");
                return;
            }

            if (slideshowTimer.Enabled)
            {
                slideshowTimer.Stop();
                slideshowButton.Text = "Slaidishow";
            }
            else
            {
                currentImage = 0;
                pctbox.Image = images[currentImage];

                slideshowTimer.Start();
                slideshowButton.Text = "Peata slideshow";
            }
        }

        private void SlideshowTimer_Tick(object sender, EventArgs e)
        {
            if (images.Count == 0)
                return;

            currentImage++;

            if (currentImage >= images.Count)
                currentImage = 0;

            pctbox.Image = images[currentImage];
        }

        private void BackgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pctbox.BackColor = colorDialog1.Color;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pctbox.Image == null)
            {
                MessageBox.Show("Pilt puudub.");
                return;
            }

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string fileName = saveFileDialog1.FileName;

            ImageFormat format = ImageFormat.Jpeg;

            string extension =
                Path.GetExtension(fileName).ToLower();

            if (extension == ".png")
                format = ImageFormat.Png;
            else if (extension == ".bmp")
                format = ImageFormat.Bmp;

            pctbox.Image.Save(fileName, format);

            MessageBox.Show("Pilt salvestati edukalt!");
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            slideshowTimer.Stop();

            pctbox.Image = null;

            foreach (Image image in images)
                image.Dispose();

            images.Clear();
            currentImage = 0;
        }

        private void StretchCheckBox_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (stretchCheckBox.Checked)
                pctbox.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pctbox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            slideshowTimer.Stop();

            foreach (Image image in images)
                image.Dispose();

            images.Clear();

            Close();
        }
    }
}