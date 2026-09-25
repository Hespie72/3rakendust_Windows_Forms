using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3rakendust_Windows_Forms
{
    public partial class matchgame : Form
    {
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N",
            "c", "c", ",", ",",
            "b", "b", "f", "f",
            "k", "k", "v", "v"
        };

        Random random = new Random();

        Label firstClicked = null;
        Label secondClicked = null;

        Timer timer = new Timer();

        Timer gameTimer = new Timer();
        int timeLeft = 60;

        int errors = 0;

        Label timeLabel;
        Label errorsLabel;

        public matchgame(string Name, int width, int height)
        {
            this.Text = Name;
            this.Width = width;
            this.Height = height;

            InitializeComponent();

            Text = "Sobitamismäng";
            Size = new Size(550, 550);

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 60";
            timeLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(10, 10);
            Controls.Add(timeLabel);

            errorsLabel = new Label();
            errorsLabel.Text = "Vead: 0";
            errorsLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            errorsLabel.AutoSize = true;
            errorsLabel.Location = new Point(400, 10);
            Controls.Add(errorsLabel);

            CreateGameBoard();

            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            gameTimer.Start();
        }

        private void CreateGameBoard()
        {
            TableLayoutPanel table = new TableLayoutPanel();

            table.Dock = DockStyle.Fill;
            table.BackColor = Color.Orange;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            table.ColumnCount = 4;
            table.RowCount = 4;

            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25F)
                );

                table.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25F)
                );
            }

            ShuffleIcons();

            for (int i = 0; i < 16; i++)
            {
                Label label = new Label();

                label.Dock = DockStyle.Fill;
                label.BackColor = Color.Orange;
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.MiddleCenter;

                label.Font = new Font(
                    "Webdings",
                    48,
                    FontStyle.Bold
                );

                label.Text = "";

                label.Tag = icons[i];

                label.Click += Label_Click;

                table.Controls.Add(label);
            }

            Controls.Add(table);
        }

        private void ShuffleIcons()
        {
            for (int i = icons.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                string temp = icons[i];
                icons[i] = icons[j];
                icons[j] = temp;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (timer.Enabled)
                return;

            if (gameTimer.Enabled == false)
                return;

            if (clickedLabel.Text != "")
                return;

            if (firstClicked != null && secondClicked != null)
                return;

            clickedLabel.Text = clickedLabel.Tag.ToString();

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                return;
            }

            secondClicked = clickedLabel;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (firstClicked.Tag.ToString() ==
                secondClicked.Tag.ToString())
            {
                firstClicked = null;
                secondClicked = null;

                CheckGameFinished();
            }
            else
            {
                errors++;
                errorsLabel.Text = "Vead: " + errors;

                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.Text = "";
            secondClicked.Text = "";

            firstClicked = null;
            secondClicked = null;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            timeLabel.Text = "Aeg: " + timeLeft;

            if (timeLeft <= 0)
            {
                gameTimer.Stop();

                MessageBox.Show(
                    "Aeg sai otsa!\nVead: " + errors,
                    "Mäng läbi"
                );

                DisableGame();
            }
        }

        private void CheckGameFinished()
        {
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel table)
                {
                    foreach (Control card in table.Controls)
                    {
                        Label label = card as Label;

                        if (label != null && label.Text == "")
                            return;
                    }
                }
            }

            gameTimer.Stop();

            MessageBox.Show(
                "Sa sobitasid kõik ikoonid!\nVead: " + errors,
                "Palju õnne!"
            );
        }

        private void DisableGame()
        {
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel table)
                {
                    foreach (Control card in table.Controls)
                    {
                        Label label = card as Label;

                        if (label != null)
                            label.Enabled = false;
                    }
                }
            }
        }
    }
}
