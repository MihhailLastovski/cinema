using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using static cinema.Seans;

namespace cinema
{
    public partial class Seans : Form
    {
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        SeansFrame seansFrame;
        public Seans()
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            AutoScroll = true;
            List<int> id = new List<int>();
            connenction.Open();
            cmd = new SqlCommand("SELECT Id FROM seanss", connenction);
            reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                id.Add((int)reader["Id"]);
            }
            connenction.Close();
            int y = 0;
            for (int i = 0; i < id.Count; i++)
            {
                seansFrame = new SeansFrame(id[i])
                {
                    BorderStyle = BorderStyle.Fixed3D,
                    Size = new Size(Width - 300, 100),
                    Location = new Point(124, 50+y),
                    BackColor = Color.FromArgb(114, 114, 120),

                };
                var request = WebRequest.Create(seansFrame.Poster.ToString());

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    using (Image image = Image.FromStream(stream))
                    {
                        Bitmap bitmap = new Bitmap(image, 100, 100);
                        seansFrame.Image = bitmap;
                    }

                }
                this.Controls.Add(seansFrame);
                y += 150;
            }
            
        }



        public class SeansFrame : PictureBox
        {
            //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
            SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
            SqlCommand cmd;
            SqlDataReader reader;
            int filmID, hallID, seansid;
            Label label, label2, label3, label4, label5, label6, label7, label8, label9;
            string nimetus, zanr, time, movieLength, rating, description, poster;
            Button button;
            public SeansFrame(int seansid)
            {
                
                this.seansid = seansid;
                cmd = new SqlCommand("SELECT time, filmID, hallID FROM seanss WHERE Id =" + seansid, connenction);
                connenction.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    time = reader["time"].ToString();
                    filmID = (int)reader["filmID"];
                    hallID = (int)reader["hallID"];
                }
                connenction.Close();

                connenction.Open();
                cmd = new SqlCommand("SELECT nimetus, poster, zanr, movieLength, rating, description FROM filmid WHERE Id = " + filmID, connenction);
                reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    poster = reader["poster"].ToString();
                    nimetus = reader["nimetus"].ToString();
                    zanr = reader["zanr"].ToString();
                    movieLength = reader["movieLength"].ToString();
                    rating = reader["rating"].ToString();
                    description = reader["description"].ToString();
                }
                connenction.Close();
                label = new Label 
                {
                    Text = nimetus,
                    Location = new Point(150, 0),
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Size = new Size(390, 50),

                };
                label2 = new Label
                {
                    Text = "Žanr: ",
                    Location = new Point(155, 50),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.White,
                    Size = new Size(50, 50),
                };
                label3 = new Label
                {
                    Text = zanr,
                    Location = new Point(210, 50),
                    Font = new Font("Arial", 11, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Size = new Size(100, 50),
                };
                label4 = new Label
                {
                    Text = "Filmi pikkus: ",
                    Location = new Point(310, 50),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.White,
                    Size = new Size(95, 50),
                };
                label5 = new Label
                {
                    Text = movieLength + "min",
                    Location = new Point(410, 50),
                    Font = new Font("Arial", 11, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Size = new Size(80, 50),
                };
                label6 = new Label
                {
                    Text = "Hinnang IMDB - s:",
                    Location = new Point(490, 50),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.White,
                    Size = new Size(140, 50),
                };
                label7 = new Label
                {
                    Text = rating,
                    Location = new Point(630, 50),
                    Font = new Font("Arial", 11, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Size = new Size(80, 50),
                };
                label8 = new Label
                {
                    Text = "Seanss: ",
                    Location = new Point(670, 0),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.White,
                    Size = new Size(89, 50),
                };
                label9 = new Label
                {
                    Text = time,
                    Location = new Point(780, 0),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Size = new Size(150, 50),
                };
                button = new Button 
                {
                    Text = "Vali piletid",
                    Size = new Size(180, 40),
                    Font = new Font("Arial", 16),
                    ForeColor = Color.White,
                    Location = new Point(710, 50)
                };
                button.Click += Button_Click;
                this.Controls.Add(label);
                this.Controls.Add(label2);
                this.Controls.Add(label3);
                this.Controls.Add(label4);
                this.Controls.Add(label5);
                this.Controls.Add(label6);
                this.Controls.Add(label7);
                this.Controls.Add(label8);
                this.Controls.Add(label9);
                this.Controls.Add(button);

            }

            private void Button_Click(object sender, System.EventArgs e)
            {
                Pilet pilet = new Pilet(description, poster, hallID, time, nimetus, seansid);
                this.Hide();
                pilet.FormClosed += Pilet_FormClosed; ;
                pilet.ShowDialog();
            }

            private void Pilet_FormClosed(object sender, FormClosedEventArgs e)
            {
                this.Show();
            }

            public string Poster
            {
                get => poster;
                set => poster = value;
            }

        }
    }
}
