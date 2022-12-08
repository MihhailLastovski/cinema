using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace cinema
{
    public partial class Kinokava : Form
    {
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        Timer timer1 = new Timer();
        Label lbl2;
        PictureBox pictureBox = new PictureBox();
        bool playing = false;
        int counter = 0;
        List<string> namepilt, zanr, nimetus;
        List<int> movieLength, year;
        List<float> rating;
        string[] labelsnimi = new string[] { "Filmi pealkiri: ", "Žanr: ", "Aasta: ", "Hinnang IMDB-s: ", "Filmi pikkus: " };
        List<Label> labels = new List<Label>();
        public Kinokava()
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;

            timer1.Interval = 4000;
            timer1.Tick += Timer1_Tick;
            pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(430, 640),
                Location = new Point(180, 10),
                BorderStyle = BorderStyle.Fixed3D
            };
            int y = 0;
            int y2 = 0;
            for (int i = 0; i < labelsnimi.Length; i++)
            {
                Label lbl = new Label
                {
                    Text = labelsnimi[i],
                    Location = new Point(650, y += 30),
                    Size = new Size(190, 30),
                    Font = new Font("Arial", 16),
                    ForeColor = Color.White,
                };
                lbl2 = new Label
                {
                    Name = labelsnimi[i],
                    Location = new Point(850, y2 += 30),
                    Size = new Size(330, 30),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleRight
                };
                labels.Add(lbl2);
                this.Controls.Add(lbl);
                this.Controls.Add(lbl2);
            }
            this.Controls.Add(pictureBox);

            namepilt = new List<string>();
            zanr = new List<string>();
            nimetus = new List<string>();
            rating = new List<float>();
            year = new List<int>();
            movieLength = new List<int>();

            cmd = new SqlCommand("SELECT poster, nimetus, zanr, rating, movieLength, year FROM filmid WHERE rating > 8.5", connenction);
            connenction.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                namepilt.Add(reader["poster"].ToString());
                zanr.Add(reader["zanr"].ToString());
                nimetus.Add(reader["nimetus"].ToString());
                movieLength.Add((int)reader["movieLength"]);
                year.Add((int)reader["year"]);
                rating.Add((float)reader["rating"]);
            }
            if (!playing)
            {
                timer1.Start();
                playing = true;
            }
            else
            {
                playing = false;
                timer1.Stop();
            }
            connenction.Close();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (counter > namepilt.Count - 1)
            {
                counter = 0;
            }

            pictureBox.ImageLocation = namepilt[counter];
            labels[0].Text = nimetus[counter];
            labels[1].Text = zanr[counter];
            labels[2].Text = year[counter].ToString();
            labels[3].Text = rating[counter].ToString();
            labels[4].Text = movieLength[counter].ToString();

            counter++;
        }
    }
}
