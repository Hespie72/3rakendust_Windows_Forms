using System;
using System.Collections.Generic;
using System.Drawing;
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