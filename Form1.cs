using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3rakendust_Windows_Forms
{
    public partial class Form1 : Form
    {
        private TableLayoutPanel mainLayout;
        private Label titleLabel;
        private Label subtitleLabel;

        private Button nuppFoto;
        private Button nuppMat;
        private Button nuppMang;

        public Form1()
        {
            Text = "3 rakendust";
            Size = new Size(1000, 600);
            MinimumSize = new Size(600, 450);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10);

            CreateInterface();
        }

        private void CreateInterface()
        {
            // Главная таблица: 1 колонка, строки по центру
            mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 6;
            mainLayout.BackColor = Color.Transparent;

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F)); // верхний отступ
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // заголовок
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // подзаголовок
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // панель кнопок
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F)); // нижний отступ
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // подвал

            Controls.Add(mainLayout);

            // Заголовок
            titleLabel = new Label();
            titleLabel.Text = "Vali rakendus";
            titleLabel.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(40, 50, 70);
            titleLabel.AutoSize = true;
            titleLabel.Anchor = AnchorStyles.None;
            titleLabel.Margin = new Padding(0, 0, 0, 5);
            mainLayout.Controls.Add(titleLabel, 0, 1);

            // Подзаголовок
            subtitleLabel = new Label();
            subtitleLabel.Text = "Vali allolevatest võimalustest üks";
            subtitleLabel.Font = new Font("Segoe UI", 11);
            subtitleLabel.ForeColor = Color.FromArgb(120, 130, 150);
            subtitleLabel.AutoSize = true;
            subtitleLabel.Anchor = AnchorStyles.None;
            subtitleLabel.Margin = new Padding(0, 0, 0, 25);
            mainLayout.Controls.Add(subtitleLabel, 0, 2);

            // Панель с кнопками (вертикально, по центру)
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.TopDown;
            buttonPanel.WrapContents = false;
            buttonPanel.AutoSize = true;
            buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonPanel.Anchor = AnchorStyles.None;
            buttonPanel.BackColor = Color.Transparent;
            mainLayout.Controls.Add(buttonPanel, 0, 3);

            nuppFoto = CreateMenuButton(
                "🖼   Ava pildi näita aken",
                Color.FromArgb(231, 76, 60));
            nuppFoto.Click += NuppFoto_Click;

            nuppMat = CreateMenuButton(
                "➕   Ava matematika ülesanded aken",
                Color.FromArgb(46, 160, 90));
            nuppMat.Click += NuppMat_Click;

            nuppMang = CreateMenuButton(
                "🎮   Ava mängu aken",
                Color.FromArgb(32, 150, 160));
            nuppMang.Click += NuppMang_Click;

            buttonPanel.Controls.Add(nuppFoto);
            buttonPanel.Controls.Add(nuppMat);
            buttonPanel.Controls.Add(nuppMang);

            // Подвал
            Label footer = new Label();
            footer.Text = "3 rakendust • Windows Forms";
            footer.Font = new Font("Segoe UI", 9);
            footer.ForeColor = Color.FromArgb(160, 168, 180);
            footer.AutoSize = true;
            footer.Anchor = AnchorStyles.None;
            footer.Margin = new Padding(0, 0, 0, 10);
            mainLayout.Controls.Add(footer, 0, 5);
        }

        /// <summary>
        /// Создаёт единообразную плоскую кнопку меню с эффектом наведения.
        /// </summary>
        private Button CreateMenuButton(string text, Color baseColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(380, 55);
            btn.Margin = new Padding(0, 0, 0, 15);
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = baseColor;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Cursor = Cursors.Hand;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(baseColor, 0.15f);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(baseColor, 0.1f);

            return btn;
        }

        private void NuppMang_Click(object sender, EventArgs e)
        {
            matchgame mgame = new matchgame("Sobitamismäng", 1000, 600);
            mgame.Show();
        }

        private void NuppMat_Click(object sender, EventArgs e)
        {
            mathquizz mquiz = new mathquizz("Matematika ülesanded", 1000, 600);
            mquiz.Show();
        }

        private void NuppFoto_Click(object sender, EventArgs e)
        {
            FotoViewer fotoVorm = new FotoViewer("Pildi näita", 1000, 600);
            fotoVorm.Show();
        }
    }
}