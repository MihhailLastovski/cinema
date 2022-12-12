using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;
using Button = System.Windows.Forms.Button;
using System.Reflection;

namespace cinema
{
    public partial class Pilet : Form
    {
        TableLayoutPanel tableLayout;
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        List<int> rows_list = new List<int>();
        List<int> seat = new List<int>();
        int seansid;
        string time, nimetus, nimetus_hall;
        public Pilet(string description, string poster, int hallID, string time, string nimetus, int seansid)
        {
            this.seansid = seansid;
            this.time = time;
            this.nimetus = nimetus;
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            tableLayout = new TableLayoutPanel 
            {
                Size = new Size(800, 500),
            };
            int colWidth;
            int rowHeight;  
            PictureBox pbox;
            Random rnd = new Random();
            string tuup = "";
            connenction.Open();
            cmd = new SqlCommand("SELECT tuup, nimetus FROM hall WHERE Id =" + hallID, connenction);
            reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                tuup = reader["tuup"].ToString();
                nimetus_hall = reader["nimetus"].ToString();
            }
            connenction.Close();
            int cols = 0, rows = 0;
            if (tuup == "Suur")
            {
                rows = 10 + 1;
                cols = 15 + 1;
            }
            else if (tuup == "Keskmine")
            {
                rows = 7 + 1;
                cols = 12 + 1;
            }
            else if (tuup == "Vaike") 
            {
                rows = 5 + 1;
                cols = 10 + 1;
            }
            colWidth = 100 / cols;
            if (100 % cols != 0)
                colWidth--;

            rowHeight = 100 / rows;
            if (100 % rows != 0)
                rowHeight--;
            tableLayout.ColumnCount = cols+1;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
            for (int i = 1; i < cols; i++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colWidth));
                Label label = new Label     
                { 
                    Text = i.ToString(),
                    Font = new Font("Arial", 18, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.TopCenter,
                };
                tableLayout.Controls.Add(label, i, 0);
            }
            for (int f = 1; f < rows; f++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
                Label label = new Label 
                { 
                    Text = f.ToString(),
                    Font = new Font("Arial", 18, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(800*colWidth, 500*rowHeight)
                };
                tableLayout.Controls.Add(label, 0, f);

                for (int j = 1; j < cols; j++)
                {
                    connenction.Open();
                    cmd = new SqlCommand("SELECT Id FROM piletid WHERE row = @row AND seat = @seat AND seanssID = @seanssID", connenction);
                    cmd.Parameters.AddWithValue("@row", f);
                    cmd.Parameters.AddWithValue("@seat", j);
                    cmd.Parameters.AddWithValue("@seanssID", seansid);
                    object result = cmd.ExecuteScalar();
                    connenction.Close();
                    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colWidth));
                    if (result != null)
                    {
                        pbox = new PictureBox() { ImageLocation = @"..\..\chair.png" };
                        pbox.Dock = DockStyle.Fill;
                        pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbox.Tag = f;
                        pbox.Name = j.ToString();
                        pbox.Click += Pbox_Click;
                        tableLayout.Controls.Add(pbox, j, f);
                    }
                    else 
                    {
                        pbox = new PictureBox() { ImageLocation = @"..\..\chairgreen.png" };
                        pbox.Dock = DockStyle.Fill;
                        pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbox.Tag = f;
                        pbox.Name = j.ToString();
                        pbox.Click += Pbox_Click;
                        tableLayout.Controls.Add(pbox, j, f);
                    }


                }
            }
            Button button = new Button 
            {
                Text = "Piletit osta",
                Size = new Size(180, 40),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(20, 550)
            };
            Label label2 = new Label
            {
                Text = nimetus_hall,
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(220, 555),
                AutoSize = true,
            };
            this.Controls.Add(tableLayout);
            PictureBox pictureBox = new PictureBox 
            {
                Location = new Point(883, 0),
                Size = new Size(300,300),
                ImageLocation = poster,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Label label1 = new Label 
            {
                Text = description,
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(883, 320),
                Size = new Size(300, 320),
                BorderStyle = BorderStyle.Fixed3D
            };
            button.Click += Button_Click;
            this.Controls.Add(label2);
            this.Controls.Add(pictureBox);
            this.Controls.Add(label1);
            this.Controls.Add(button);

        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (rows_list.Count != 0 && seat.Count != 0)
            {                
                Osta osta = new Osta(rows_list, seat, seansid, time, nimetus, nimetus_hall);
                this.Hide();
                osta.FormClosed += Osta_FormClosed;
                osta.ShowDialog();   
            }
        }

        private void Osta_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void Pbox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (pictureBox.ImageLocation == @"..\..\chairgreen.png")
            {
                pictureBox.ImageLocation = @"..\..\chairyellow.png";
                rows_list.Add((int)pictureBox.Tag);
                seat.Add(Int32.Parse(pictureBox.Name));
            }
            else if (pictureBox.ImageLocation == @"..\..\chairyellow.png")
            {
                pictureBox.ImageLocation = @"..\..\chairgreen.png";
                rows_list.Remove((int)pictureBox.Tag);
                seat.Remove(Int32.Parse(pictureBox.Name));
            }
            else
            {
                MessageBox.Show("Koht on hõivatud!");
            }
        }

    }
}
