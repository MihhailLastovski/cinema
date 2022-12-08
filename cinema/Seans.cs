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

namespace cinema
{
    public partial class Seans : Form
    {
        SqlCommand cmd;
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        public Seans()
        {
        }
        public class SeansFrame 
        {
            public SeansFrame(int seansid) 
            {
                co
            }
        }
    }
}
