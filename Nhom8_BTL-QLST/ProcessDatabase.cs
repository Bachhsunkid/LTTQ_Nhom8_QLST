using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom8_BTL_QLST
{
    class ProcessDatabase
    {
        public ProcessDatabase()
        {
            conn = new SqlConnection(strConn);
        }

        SqlConnection conn;
        //Bach string connection
        //string strConn = "Data Source=DESKTOP-A8FKSRA\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //string strConn = "Data Source=DESKTOP-A8FKSRA\\SQLEXPRESS;Initial Catalog=QLVuonThu;Integrated Security=True";
        //Ngoc string connection
        string strConn = "Data Source=DESKTOP-9ATFPMT\\SQLEXPRESS;Initial Catalog=QLVuonThu;Integrated Security=True";
        public ProcessDatabase(string s)
        {
            conn = new SqlConnection(s);
        }
        public void ketNoi()
        {

            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public void dongKetNoi()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        public DataTable docBang(string query)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ketNoi();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
                sqlDataAdapter.Fill(dataTable);
                dataTable.Dispose();

                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

                dongKetNoi();
            }
            return null;
        }
        public void thucThiSQL(string query)
        {

            try
            {
                ketNoi();
                SqlCommand sqlcommand = new SqlCommand(query, conn);
                sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                dongKetNoi();
            }
        }
    }
}
