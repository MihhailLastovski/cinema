using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace cinema
{
    public partial class klient : Form
    {
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        TextBox textBox1, textBox2;
        SqlDataReader reader;
        public klient()
        {
            connenction.Open();
            cmd = new SqlCommand("UPDATE klient SET active = 0 WHERE active = 1", connenction);
            cmd.ExecuteNonQuery();
            connenction.Close();
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            Label label = new Label 
            {
                Text = "Email: ",
                AutoSize = true,
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(300, 250),
            };
            Label label2 = new Label
            {
                Text = "Nimi: ",
                AutoSize = true,
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(300, 300),
            };
            Button button = new Button
            {
                Text = "Register",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(600, 420),
            };
            Button button2 = new Button
            {
                Text = "Logi sisse",
                Size = new Size(200, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(300, 420),
            };
            textBox1 = new System.Windows.Forms.TextBox
            {
                Size = new Size(250, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(550, 250),
                BackColor = formparam._backcolorform,

            };
            textBox2 = new System.Windows.Forms.TextBox
            {
                Size = new Size(250, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(550, 300),
                BackColor = formparam._backcolorform,
            };
            button.Click += Button_Click1;
            button2.Click += button2_Click;
            this.Controls.Add(button);
            this.Controls.Add(button2);
            this.Controls.Add(label2);
            this.Controls.Add(label);
            this.Controls.Add(textBox2);
            this.Controls.Add(textBox1);

        }

        private void Button_Click1(object sender, EventArgs e)
        {

            if (textBox1.Text != null && textBox2.Text != null)
            {
                object result;
                cmd = new SqlCommand("SELECT nimi FROM klient WHERE nimi = @nimi AND post = @post;", connenction);
                connenction.Open();
                cmd.Parameters.AddWithValue("@post", textBox1.Text);
                cmd.Parameters.AddWithValue("@nimi", textBox2.Text);
                result = cmd.ExecuteScalar();
                connenction.Close();
                if (result == null)
                {
                    connenction.Open();
                    cmd = new SqlCommand("INSERT INTO klient(nimi, post) VALUES(@nimi, @post)", connenction);
                    cmd.Parameters.AddWithValue("@nimi", textBox2.Text);
                    cmd.Parameters.AddWithValue("@post", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    connenction.Close();
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        string post = "";
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                string nimi = "";
                cmd = new SqlCommand("SELECT nimi, post FROM klient WHERE nimi = @nimi AND post = @post;", connenction);
                connenction.Open();
                cmd.Parameters.AddWithValue("@post", textBox1.Text);
                cmd.Parameters.AddWithValue("@nimi", textBox2.Text);
                reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    nimi = reader["nimi"].ToString();
                    post = reader["post"].ToString();
                }
                connenction.Close();
                if (nimi != "" && post != "")
                {
                    connenction.Open();
                    cmd = new SqlCommand("UPDATE klient SET active = 1 WHERE nimi = @nimi AND post = @post", connenction);
                    cmd.Parameters.AddWithValue("@post", post);
                    cmd.Parameters.AddWithValue("@nimi", nimi);
                    cmd.ExecuteNonQuery();
                    connenction.Close();
                    Form1 form1 = new Form1(nimi);
                    this.Hide();
                    form1.FormClosed += Form2_FormClosed;
                    form1.ShowDialog();
                }
            }
        }

    }
}
