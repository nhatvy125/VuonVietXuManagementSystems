using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VuonVietXuStore.Helpers
{
    /// <summary>
    /// DbHelper: Tập trung quản lý kết nối SQL Server.
    /// Thay thế việc tạo SqlConnection rải rác ở từng UserControl.
    /// </summary>
    public static class DbHelper
    {
        private static readonly string _connStr =
            ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        /// <summary>Lấy connection mới (caller phải Dispose).</summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        /// <summary>Thực thi query trả về DataTable.</summary>
        public static DataTable ExecuteQuery(string sql, Action<SqlCommand> paramSetup = null)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                paramSetup?.Invoke(cmd);
                var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>Thực thi INSERT/UPDATE/DELETE, trả về số dòng ảnh hưởng.</summary>
        public static int ExecuteNonQuery(string sql, Action<SqlCommand> paramSetup = null)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Thực thi query trả về giá trị đơn (scalar).</summary>
        public static object ExecuteScalar(string sql, Action<SqlCommand> paramSetup = null)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                paramSetup?.Invoke(cmd);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Chạy nhiều câu lệnh trong một Transaction.
        /// Nếu bất kỳ lệnh nào lỗi → rollback toàn bộ.
        /// </summary>
        public static void ExecuteTransaction(Action<SqlConnection, SqlTransaction> work)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        work(conn, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
