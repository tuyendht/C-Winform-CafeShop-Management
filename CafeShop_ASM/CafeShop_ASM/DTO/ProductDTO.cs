using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    class ProductDTO
    {
        private string proID;
        private string cateID;
        private string proName;
        private float unitPrice;
        public ProductDTO(DataRow row)
        {
            this.ProID = row["Mã Sản Phẩm"].ToString();
            this.cateID = row["Mã Danh Mục"].ToString();
            this.proName = row["Tên Sản Phẩm"].ToString();
            this.unitPrice = (float)Convert.ToDouble(row["Đơn giá"].ToString());
        }
        public ProductDTO()
        {

        }

        public ProductDTO(string proID, string cateID, string proName, float unitPrice)
        {
            this.proID = proID;
            this.cateID = cateID;
            this.proName = proName;
            this.unitPrice = unitPrice;
        }

        public string CateID { get => cateID; set => cateID = value; }
        public string ProName { get => proName; set => proName = value; }
        public float UnitPrice { get => unitPrice; set => unitPrice = value; }
        public string ProID { get => proID; set => proID = value; }
    }
}
