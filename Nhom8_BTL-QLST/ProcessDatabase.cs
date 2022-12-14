using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Nhom8_BTL_QLST
{
    class ProcessDatabase
    {
        SqlConnection conn;
        public ProcessDatabase()
        {
            Conn = new SqlConnection(strConn);
        }

        
        //Bach string connection
        string strConn = "Data Source=DESKTOP-A8FKSRA\\SQLEXPRESS;Initial Catalog=QLVuonThu;Integrated Security=True";
        //Ngoc string connection
        //string strConn = "Data Source=DESKTOP-9ATFPMT\\SQLEXPRESS;Initial Catalog=QLVuonThu;Integrated Security=True";

        public SqlConnection Conn { get => conn; set => conn = value; }

        public ProcessDatabase(string s)
        {
            Conn = new SqlConnection(s);
        }
        public void ketNoi()
        {

            if (Conn.State != System.Data.ConnectionState.Open)
            {
                Conn.Open();
            }
        }
        public void dongKetNoi()
        {
            if (Conn.State != System.Data.ConnectionState.Closed)
            {
                Conn.Close();
            }
        }

        public DataTable docBang(string query)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ketNoi();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, Conn);
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
                SqlCommand sqlcommand = new SqlCommand(query, Conn);
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
        public void InsertIntoThu()
        {
                ketNoi();
                SqlCommand command = new SqlCommand(null, Conn);

                // Create and prepare an SQL statement.
                command.CommandText =
                    "INSERT INTO Thu (mathu, TenThu, MaLoai, SoLuong, SachDo, TenKhoaHoc, TenTA, MaKieuSinh, GioiTinh, NgayVao, MaNguonGoc, DacDiem, NgaySinh, TuoiTho, Anh) " +
                    "VALUES (@mathu, @TenThu, @MaLoai, @SoLuong, @SachDo, @TenKhoaHoc, @TenTA, @MaKieuSinh, @GioiTinh, @NgayVao, @MaNguonGoc, @DacDiem, @NgaySinh, @TuoiTho, @Anh)";
                SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int, 0);
                SqlParameter descParam =new SqlParameter("@desc", SqlDbType.Text, 100);
                idParam.Value = 20;
                descParam.Value = "First Region";
                command.Parameters.Add(idParam);
                command.Parameters.Add(descParam);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();

                // Change parameter values and call ExecuteNonQuery.
                command.Parameters[0].Value = 21;
                command.Parameters[1].Value = "Second Region";
                command.ExecuteNonQuery();
        }
    }
}
