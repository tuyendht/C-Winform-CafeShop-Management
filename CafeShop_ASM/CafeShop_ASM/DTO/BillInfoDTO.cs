using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    public class BillInfoDTO
    {
        private int billID;
        private string proID;
        private int quantity;
        private double unitPrice;
        public BillInfoDTO()
        {

        }
        public BillInfoDTO(DataRow row)
        {
            //this.idInfo = (int)row["idInfo"];
            this.billID = (int)row["Mã đơn"];
            this.proID = (string)row["Mã sản phẩm"];
            this.quantity = (int)row["Số lượng"];
            this.unitPrice = (double)row["Đơn giá"];
        }
        public BillInfoDTO(int billID, string proID, int quantity, double unitPrice)
        {
            this.billID = billID;
            this.proID = proID;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }

        public int BillID { get => billID; set => billID = value; }
        public string ProID { get => proID; set => proID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
    }
}
