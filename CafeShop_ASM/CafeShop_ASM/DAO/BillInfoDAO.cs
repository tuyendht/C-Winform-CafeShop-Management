using CafeShop_ASM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { instance = value; }
        }
        public BillInfoDAO()
        {

        }
        public List<MenuDTO> getListBillInfoByID(string billID)
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string sql = "SELECT p.proName AS [Tên sản phẩm], bi.quantity AS [Số lượng], p.unitPrice AS [Đơn giá], p.unitPrice * bi.quantity AS [Tổng tiền] " +
                         "FROM tblBillInfo bi, tblBill b, tblProduct p " +
                         "WHERE bi.billID = b.billID AND bi.ProID = p.ProID AND b.billID = " + billID;
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO dto = new MenuDTO(item);
                listMenu.Add(dto);
            }
            return listMenu;
        }

        public List<BillInfoDTO> getListBillInfo(string billID)
        {
            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();
            string sql = "SELECT billID AS [Mã đơn], proID AS [Mã sản phẩm], quantity AS [Số lượng], unitPrice AS [Đơn giá] " +
                         "FROM tblBillInfo " +
                         "WHERE billID = "+ billID;
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach(DataRow item in data.Rows)
            {
                BillInfoDTO dto = new BillInfoDTO(item);
                listBillInfo.Add(dto);
            }

            return listBillInfo;
        }
        public void insertBillInfo(string billID, string proID, string quantity, string unitPrice)
        {
            string sql = "INSERT INTO tblBillInfo(billID,proID, quantity, unitPrice) " +
                         "VALUES ('"+ billID + "','"+ proID + "',"+ quantity + ","+ unitPrice + ")";
            DataAccess.Instance.ExecuteNonQuery(sql);
        }
    }
}
