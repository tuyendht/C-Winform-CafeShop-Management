using CafeShop_ASM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { instance = value; } 
        }
        private AccountDAO() { }
        public bool checkLogin(string username, string password)
        {
            string sql = "SELECT username, password, fullname, isAdmin " +
                         "FROM tblUsers " +
                         "WHERE username = N'" + username + "' AND password = N'" + password + "' ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            return data.Rows.Count > 0;
        }
        public AccountDTO getLoginAccount(string username)
        {
            string sql = "SELECT username, password, fullname, isAdmin " +
                         "FROM tblUsers " +
                         "WHERE username = N'" + username + "' ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            AccountDTO dto = null;
            if (data.Rows.Count > 0)
            {
                dto = new AccountDTO(data.Rows[0]);
            }
            return dto;
        }
        public bool updateAccountByUser(string username, string fullname, string password)
        {
            string sql = "UPDATE tblUsers " +
                         "SET fullname = N'" + fullname + "',password = '" + password + "' " +
                         "WHERE username = '" + username + "'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool addAccount(string username, string fullname, bool isAdmin)
        {
            string isAdmins = isAdmin ? "1" : "0";
            string sql = "INSERT INTO tblUsers " +
                         "VALUES ('" + username + "',N'" + fullname + "','" + "0" + "'," + isAdmins + ")";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool updateAccountByAdmin(string username, string fullname, bool isAdmin)
        {
            string isAdmins = isAdmin ? "1" : "0";
            string sql = "UPDATE tblUsers " +
                         "SET fullname = N'"+fullname+"',isAdmin = '"+isAdmins+"' " +
                         "WHERE username = '"+username+"'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool deleteAccount(string username)
        {
            string sql = "DELETE FROM tblUsers " +                         
                         "WHERE username = '" + username + "'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool resetAccount(string username)
        {
            string sql = "UPDATE tblUsers " +
                         "SET password = '0' " +
                         "WHERE username = '" + username + "'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public DataTable getListAccount()
        {
            string sql = "SELECT username AS [Tên tài khoản], fullname AS [Họ tên], password AS [Mật khẩu], isAdmin AS [Quyền Admin] " +
                         "FROM tblUsers";
            return DataAccess.Instance.ExecuteQuery(sql);
        }
        public DataTable getAccountByID(string username)
        {
            string sql = "SELECT username AS [Tên tài khoản], fullname AS [Họ tên], password AS [Mật khẩu], isAdmin AS [Quyền Admin] " +
                         "FROM tblUsers " +
                         "WHERE username " +
                         "LIKE N'%" + username + "%'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public bool getUsername(string username)
        {
            string sql = "SELECT username " +
                         "FROM tblUsers " +
                         "WHERE username = '"+username+"'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            return data.Rows.Count>0;
        }
        public DataTable getAccountByName(string fullname)
        {
            string sql = "SELECT username AS [Tên tài khoản], fullname AS [Họ tên], password AS [Mật khẩu], isAdmin AS [Quyền Admin] " +
                         "FROM tblUsers " +
                         "WHERE fullname " +
                         "LIKE N'%" + fullname + "%'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }

    }
}
