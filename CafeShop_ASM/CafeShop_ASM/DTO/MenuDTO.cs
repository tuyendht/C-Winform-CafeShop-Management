using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    public class MenuDTO
    {
        private string proName;
        private int quantity;
        private double unitPrice;
        private double totalPrice;
        public MenuDTO()
        {

        }

        public MenuDTO(string proName, int quantity, double unitPrice, double totalPrice)
        {
            this.proName = proName;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
            this.totalPrice = totalPrice;
        }
        public MenuDTO(DataRow row)
        {
            this.proName = (string)row["Tên sản phẩm"];
            this.quantity = (int)row["Số lượng"];
            this.unitPrice = (double)row["Đơn giá"];
            this.totalPrice = (double)row["Tổng tiền"];
        }

        public string ProName { get => proName; set => proName = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
