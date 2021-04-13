using CafeShop_ASM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { instance = value; }
        }
        public BillDAO()
        {

        }
        public DataTable getListBillByDate(string date1, string date2)
        {
            string sql = "SELECT billID AS [Mã đơn], dateCheckIn AS [Thời gian], fullname AS [Nhân viên], billStatus AS [Trạng thái] " +
                         "FROM tblBill, tblUsers " +
                         "WHERE tblBill.username = tblUsers.username AND tblBill.dateCheckIn BETWEEN '"+date1+"' AND '"+date2+"' ";
            return DataAccess.Instance.ExecuteQuery(sql);
        }
        public DataTable getListBill()
        {
            string sql = "SELECT billID AS [Mã đơn], dateCheckIn AS [Thời gian], fullname AS [Nhân viên], billStatus AS [Trạng thái] " +
                         "FROM tblBill, tblUsers " +
                         "WHERE tblBill.username = tblUsers.username ";
            return DataAccess.Instance.ExecuteQuery(sql);
        }
        public int getLastBillID()
        {
            string sql = "SELECT MAX(billID) as [billID]" +
                         "FROM tblBill ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach (DataRow item in data.Rows)
            {
                return (int)item["billID"];
            }
            return -1;
        }
        public void insertBill(string username)
        {
            string sql = "INSERT INTO tblBill(username) " +
                         "VALUES ('"+ username + "')";
            DataAccess.Instance.ExecuteNonQuery(sql);
        }
        public void updateBillStatus(string billID,bool isPay)
        {
            string sql = "UPDATE tblBill " +
                         "SET billStatus = '"+isPay.ToString()+"' " +
                         "WHERE billID='"+billID+"'";
            DataAccess.Instance.ExecuteNonQuery(sql);
        }
    }
}
