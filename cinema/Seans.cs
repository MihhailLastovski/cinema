using System.Data.SqlClient;
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
            SeansFrame seansFrame = new SeansFrame(1);
        }
        public class SeansFrame : Label
        {
            int filmID;
            public SeansFrame(int seansid)
            {
                SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
                //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("SELECT time, filmID FROM seanss WHERE Id =" + seansid, connenction);
                connenction.Open();
                reader = cmd.ExecuteReader();
                string time;
                while (reader.Read()) 
                {
                    time = reader["time"].ToString();
                    filmID = (int)reader["filmID"];
                }
                connenction.Close();
                connenction.Open();
                cmd = new SqlCommand("SELECT nimetus FROM filmid WHERE Id = " + filmID, connenction);
                object result = cmd.ExecuteScalar();
                connenction.Close();
            }
        }
    }
}
