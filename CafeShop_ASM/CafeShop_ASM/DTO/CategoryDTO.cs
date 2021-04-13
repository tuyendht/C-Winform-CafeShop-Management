using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    class CategoryDTO
    {
        private string cateID;
        private string cateName;
        public CategoryDTO(DataRow row)
        {
            this.cateID = (string)row["Mã Danh Mục"];
            this.cateName = (string)row["Tên Danh Mục"];
        }
        public CategoryDTO()
        {

        }
        public CategoryDTO(string cateID, string cateName)
        {
            CateID = cateID;
            CateName = cateName;
        }

        public string CateID { get => cateID; set => cateID = value; }
        public string CateName { get => cateName; set => cateName = value; }
    }
}
