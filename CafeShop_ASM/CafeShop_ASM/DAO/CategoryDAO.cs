using CafeShop_ASM.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    class CategoryDAO
    {
        private static CategoryDAO instance;

        internal static CategoryDAO Instance {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }
        private CategoryDAO() { }
        public bool getCateID(string cateID)
        {
            string sql = "SELECT cateID " +
                         "FROM tblCategory " +
                         "WHERE cateID = '" + cateID + "'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            return data.Rows.Count > 0;
        }
        public ArrayList getCategoryComboBox()
        {
            ArrayList list = new ArrayList();
            string sql = "SELECT cateName " +
                         "FROM tblCategory ";

            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            foreach (DataRow item in data.Rows)
            {
                string cateName = item["cateName"].ToString();
                list.Add(cateName);
            }

            return list;
        }
        public List<CategoryDTO> getListCategory()
        {
            List<CategoryDTO> listCategory = new List<CategoryDTO>();
            string sql = "SELECT cateID AS [Mã Danh Mục], cateName AS [Tên Danh Mục] " +
                         "FROM tblCategory";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            foreach(DataRow item in data.Rows)
            {
                CategoryDTO dto = new CategoryDTO(item);
                listCategory.Add(dto);
            }
            return listCategory;
        }
        public DataTable getListCategoryAsDataTable()
        {
            string sql = "SELECT cateID AS [Mã Danh Mục], cateName AS [Tên Danh Mục] " +
                         "FROM tblCategory";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public DataTable getCategoryByID(string cateID)
        {
            string sql = "SELECT cateID AS [Mã Danh Mục], cateName AS [Tên Danh Mục] " +
                         "FROM tblCategory " +
                         "WHERE cateID " +
                         "LIKE N'%" + cateID + "%'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);
            
            return data;
        }
        public DataTable getCategoryByName(string cateName)
        {
            string sql = "SELECT cateID AS [Mã Danh Mục], cateName AS [Tên Danh Mục] " +
                         "FROM tblCategory " +
                         "WHERE cateName " +
                         "LIKE N'%"+cateName+"%'";
            DataTable data = DataAccess.Instance.ExecuteQuery(sql);

            return data;
        }
        public bool addCategory(string cateID, string cateName)
        {
            string sql = "INSERT INTO tblCategory " +
                         "VALUES ('"+cateID+"',N'"+cateName+"') ";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool updateCategory(string cateID, string cateName)
        {
            string sql = "UPDATE tblCategory " +
                         "SET cateName = '" + cateName + "' " +
                         "WHERE cateID = '" + cateID + "' ";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
        public bool deleteCategory(string cateID)
        {
            string sql = "DELETE FROM tblCategory " +
                         "WHERE cateID = '" + cateID + "'";
            int result = DataAccess.Instance.ExecuteNonQuery(sql);
            return result > 0;
        }
    }
}
