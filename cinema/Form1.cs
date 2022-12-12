using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Timer = System.Windows.Forms.Timer;

namespace cinema
{
    public partial class Form1 : Form
    {
        Button button, button2, button3, button4;
        Random random = new Random();
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form1(string result)
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            button = new Button
            {
                Text = "Kinokava",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(450, 280),
            };
            button2 = new Button
            {
                Text = "ADMIN",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(450, 350)
            };
            button3 = new Button
            {
                Text = "SEANS",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(450, 420)
            };
            button4 = new Button
            {
                Text = "Generate Seanss",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(450, 490)
            };
            button.Click += Button_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            this.Controls.Add(button);
            if (result == "admin")
            {
                this.Controls.Add(button2);
                this.Controls.Add(button4);
            }
            this.Controls.Add(button3);


        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<int> hall_id = new List<int>();
            List<int> film_id = new List<int>();
            connenction.Open();
            cmd = new SqlCommand("SELECT Id FROM hall", connenction);
            reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                hall_id.Add((int)reader["id"]);
            }
            connenction.Close();
            connenction.Open();
            cmd = new SqlCommand("SELECT Id FROM filmid", connenction);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                film_id.Add((int)reader["id"]);
            }
            connenction.Close();
            DateTime dob = new DateTime(2022, 12, (int)DateTime.Now.Day + random.Next(1, 5), random.Next(0, 24), random.Next(0,55), 0);
            connenction.Open();
            cmd = new SqlCommand("INSERT INTO seanss(filmID, hallID, time) VALUES(@filmID, @hallID, @time)", connenction);
            cmd.Parameters.AddWithValue("@filmID", film_id[random.Next(film_id.Count)]);
            cmd.Parameters.AddWithValue("@hallID", hall_id[random.Next(hall_id.Count)]);
            cmd.Parameters.AddWithValue("@time", dob);
            cmd.ExecuteNonQuery();
            connenction.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Seans seans = new Seans();
            this.Hide();
            seans.FormClosed += Form2_FormClosed;
            seans.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.FormClosed += Form2_FormClosed;
            form2.ShowDialog();

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();   
        }

        
        private void Button_Click(object sender, EventArgs e)
        {
            Kinokava kinokava = new Kinokava();
            this.Hide();
            kinokava.FormClosed += Form2_FormClosed;
            kinokava.ShowDialog();
        }

        
    }
}
