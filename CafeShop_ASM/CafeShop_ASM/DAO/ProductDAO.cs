using CafeShop_ASM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get { if (instance == null) instance = new ProductDAO(); return ProductDAO.instance; }
            private set { ProductDAO.instance = value; }
        }

        private ProductDAO() { }
        public bool getProductID(string proID)
        {
            string sql = "SELECT proID " +
                         "FROM tblProduct " +
                         "WHERE proID = '" + proID + "'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            return data.Rows.Count > 0;
        }
        public string getCateIDByCateName(string cate)
        {
            string sql1 = "SELECT cateID, cateName " +
                          "FROM tblCategory ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql1);
            foreach (DataRow item in data.Rows)
            {
                string cateID = item["cateID"].ToString();
                string cateName = item["cateName"].ToString();
                if (cate.Equals(cateName))
                {
                    return cateID;
                }
            }
            return null;
        }
        public List<ProductDTO> getListProductByCategoryID(string cateID)
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string sql = "SELECT proID AS [Mã Sản Phẩm], cateID AS [Mã Danh Mục], proName AS [Tên Sản Phẩm], unitPrice AS [Đơn giá] " +
                         "FROM  tblProduct " +
                         "WHERE cateID = '"+cateID+"'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO dto = new ProductDTO(item);
                listProduct.Add(dto);
            }
            return listProduct;
        }
        public DataTable getListProductAsDataTable()
        {
            string sql = "SELECT proID AS [Mã Sản Phẩm], cateName AS [Tên Danh Mục], proName AS [Tên Sản Phẩm], unitPrice AS [Đơn giá] " +
                         "FROM tblCategory, tblProduct " +
                         "WHERE tblProduct.cateID = tblCategory.cateID";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            return data;
        }
        public DataTable getProductByProductID(string proID)
        {
            string sql = "SELECT proID AS [Mã Sản Phẩm], cateName AS [Tên Danh Mục], proName AS [Tên Sản Phẩm], unitPrice AS [Đơn giá] " +
                         "FROM tblCategory, tblProduct " +
                         "WHERE tblCategory.cateID = tblProduct.cateID AND proID " +
                         "LIKE N'%"+ proID + "%' ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public DataTable getProductByProductName(string proName)
        {
            string sql = "SELECT proID AS [Mã Sản Phẩm], cateName AS [Tên Danh Mục], proName AS [Tên Sản Phẩm], unitPrice AS [Đơn giá] " +
                         "FROM tblCategory, tblProduct " +
                         "WHERE tblCategory.cateID = tblProduct.cateID AND proName " +
                         "LIKE N'%" + proName + "%' ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public DataTable getProductByCategoryName(string cateName)
        {
            string sql = "SELECT proID AS [Mã Sản Phẩm], cateName AS [Tên Danh Mục], proName AS [Tên Sản Phẩm], unitPrice AS [Đơn giá] " +
                         "FROM tblCategory, tblProduct " +
                         "WHERE tblCategory.cateID = tblProduct.cateID AND cateName " +
                         "LIKE N'%" + cateName + "%' ";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public string getProductIDbyProName(string proName)
        {
            string sql = "SELECT proID " +
                         "FROM tblProduct " +
                         "WHERE proName='"+proName+"'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach (DataRow item in data.Rows)
            {
                return (string)item["proID"];
            }
            return null;
        }
        public bool addProduct(string proID, string cateName, string proName, float unitPrice)
        {
            string sql = "INSERT INTO tblProduct " +
                         "VALUES ('"+proID+"', '"+getCateIDByCateName(cateName)+"',N'"+proName+"',"+unitPrice+") ";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool updateProduct(string proID, string cateName, string proName, float unitPrice)
        {
            string sql = "UPDATE tblProduct " +
                         "SET cateID = '"+ getCateIDByCateName(cateName) + "', proName = '"+proName+"', unitPrice = "+unitPrice+" " +
                         "WHERE proID = '" + proID + "' ";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool deleteProduct(string proID)
        {
            string sql = "DELETE FROM tblProduct " +
                         "WHERE proID = '" + proID + "'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
    }
}
