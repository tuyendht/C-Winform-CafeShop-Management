using CafeShop_ASM.DAO;
using CafeShop_ASM.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShop_ASM
{
    public partial class fAdmin : Form
    {
        #region //fields
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource productList = new BindingSource();
        BindingSource billList = new BindingSource();
        List<BillInfoDTO> billListInfo = new List<BillInfoDTO>();
        bool isAddNewAccount = false;
        bool isAddNewProduct = false;
        bool isAddNewCategory = false;
        #endregion

        public fAdmin()
        {
            InitializeComponent();
            loadData();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }
        #region //load data
        private void loadData()
        {
            loadAccountList();
            loadListCategory();
            loadListProduct();
            loadBill();
            loadBillInfo();
        }
        #endregion
        #region //valid datea
        private bool checkAccount()
        {
            string check = "";
            check = txtAccountID.Text.Trim();
            if (isAddNewAccount)
            {              
                if (AccountDAO.Instance.getUsername(check))
                {
                    MessageBox.Show( "Tên tài khoản đã tồn tại", "Thông báo");
                    txtAccountID.Clear();
                    txtAccountID.Focus();
                    return false;
                }
                if (check.Length == 0)
                {
                    MessageBox.Show("Tên tài khoản không được bỏ trống", "Thông báo");
                    txtAccountID.Clear();
                    txtAccountID.Focus();
                    return false;
                }
            }
            check = txtAccountName.Text.Trim();
            if (check.Length == 0)
            {
                MessageBox.Show("Họ tên không được bỏ trống", "Thông báo");
                txtAccountName.Clear();
                txtAccountName.Focus();
                return false;
            }
            return true;
        }
        private bool checkCategory()
        {
            string check = "";
            check = txtCategoryID.Text.Trim();
            if (isAddNewCategory)
            {
                if (CategoryDAO.Instance.getCateID(check))
                {
                    MessageBox.Show("Mã danh mục đã tồn tại", "Thông báo");
                    txtCategoryID.Clear();
                    txtCategoryID.Focus();
                    return false;
                }
                if (check.Length == 0)
                {
                    MessageBox.Show("Mã danh mục không được bỏ trống", "Thông báo");
                    txtCategoryID.Clear();
                    txtCategoryID.Focus();
                    return false;
                }
                bool isTrue = Regex.IsMatch(check,"^[S][P][0-9]{2}$");
                if (!isTrue)
                {
                    MessageBox.Show("Mã danh mục phải theo đinh dạng [SP00]", "Thông báo");
                    txtCategoryID.Clear();
                    txtCategoryID.Focus();
                    return false;
                }
            }
            check = txtCategoryName.Text.Trim();
            if (check.Length == 0)
            {
                MessageBox.Show("Tên danh mục không được bỏ trống", "Thông báo");
                txtCategoryName.Clear();
                txtCategoryName.Focus();
                return false;
            }
            return true;
        }
        private bool checkProduct()
        {
            string check = "";
            check = txtFoodID.Text.Trim();
            if (isAddNewProduct)
            {
                string cate = cbCategory.Text;
                if (check.Length == 0)
                {
                    MessageBox.Show("Mã sản phẩm không được bỏ trống", "Thông báo");
                    txtFoodID.Clear();
                    txtFoodID.Focus();
                    return false;
                }              
                if (!Regex.IsMatch(check, "^[a-zA-Z]{2}[0-9]{2}$"))
                {
                    MessageBox.Show("Mã sản phẩm phải theo định dạng [XX00]\nVới XX là 2 chữ cái, 00 là 2 chữ số", "Thông báo");
                    txtFoodID.Clear();
                    txtFoodID.Focus();
                    return false;
                }
                
                if (ProductDAO.Instance.getProductID(check))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại", "Thông báo");
                    txtFoodID.Clear();
                    txtFoodID.Focus();
                    return false;
                }               
            }
            check = txtFoodName.Text.Trim();
            if (check.Length == 0)
            {
                MessageBox.Show("Tên sản phẩm không được bỏ trống", "Thông báo");
                txtFoodName.Clear();
                txtFoodName.Focus();
                return false;
            }
            check = txtUnitPrice.Text.Trim();
            if(check.Length == 0)
            {
                MessageBox.Show("Đơn giá không được bỏ trống", "Thông báo");
                txtUnitPrice.Clear();
                txtUnitPrice.Focus();
                return false;
            }
            if (!Regex.IsMatch(check, "^(((([0-9]+([.][0-9]*)?)|([.][0-9]+)))|((([0-9]+([,][0-9]*)?)|([,][0-9]+))))$"))
            {
                MessageBox.Show("Đơn giá phải là một chữ số", "Thông báo");
                txtUnitPrice.Clear();
                txtUnitPrice.Focus();
                return false;
            }
            return true;
        }
        #endregion
        #region //method of account
        private void loadAccountList()
        { 
            accountList.DataSource = AccountDAO.Instance.getListAccount();
            dtgvAccount.DataSource = accountList;
            dtgvAccount.RowHeadersVisible = false;
            dtgvAccount.Columns["Mật khẩu"].Visible = false;
        }
        private DataTable searchAccount(string searchValue)
        {
            DataTable data = AccountDAO.Instance.getAccountByID(searchValue);
            if (data.Rows.Count <= 0)
            {
                data = AccountDAO.Instance.getAccountByName(searchValue);
            }
            return data;
        }
        private void addAccount(string username, string fullname, bool isAdmin)
        {
            if (AccountDAO.Instance.addAccount(username, fullname, isAdmin))
            {
                MessageBox.Show("Thêm tài khoản thành công, mật khẩu là 0");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            txtAccountID.ReadOnly = true;
            txtAccountID.Clear();
            txtAccountName.Clear();
            cbAdmin.Checked = false;
            isAddNewAccount = false;
            loadAccountList();

        }
        private void updateAccount(string username, string fullname, bool isAdmin)
        {
            if (AccountDAO.Instance.updateAccountByAdmin(username, fullname, isAdmin))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            loadAccountList();
        }
        private void resetAccount(string username)
        {
            if (AccountDAO.Instance.resetAccount(username))
            {
                MessageBox.Show("Reset mật khẩu thành công\nMật khẩu là: 0");
            }
            else
            {
                MessageBox.Show("Reset mật khẩu thất bại");
            }

            loadAccountList();
        }
        private void deleteAccount(string username)
        {
            if (AccountDAO.Instance.deleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            txtAccountID.ReadOnly = true;
            txtAccountID.Clear();
            txtAccountName.Clear();
            cbAdmin.Checked = false;
            loadAccountList();
        }

        #endregion
        #region //event of account

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            resetAccount(username);
        }
        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            accountList.DataSource = searchAccount(txtSearchAccount.Text);
            if (accountList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy danh mục " + txtSearchAccount.Text.ToUpper());
                loadAccountList();
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (txtAccountID.ReadOnly == true) {
                txtAccountID.ReadOnly = false;
                txtAccountID.Clear();
                txtAccountName.Clear();
                cbAdmin.Checked = false;
                isAddNewAccount = true;
            }
            else
            {
                if (checkAccount())
                {
                    string username = txtAccountID.Text;
                    string fullname = txtAccountName.Text;
                    bool isAdmin = cbAdmin.Checked;
                    addAccount(username, fullname, isAdmin);
                }               
            }
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            isAddNewAccount = false;
            if (checkAccount())
            {
                string username = txtAccountID.Text;
                string fullname = txtAccountName.Text;
                bool isAdmin = cbAdmin.Checked;
                updateAccount(username, fullname, isAdmin);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txtAccountID.Text;
            deleteAccount(username);
        }

        private void btnResetAccountList_Click(object sender, EventArgs e)
        {
            loadAccountList();
        }


        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtgvAccount.Rows[e.RowIndex];
                txtAccountID.ReadOnly = true;
                txtAccountID.Text = row.Cells[0].Value.ToString();
                txtAccountName.Text = row.Cells[1].Value.ToString();
                bool isAdmin = row.Cells[3].Value.ToString() == "True";
                cbAdmin.Checked = isAdmin;
            }
            catch (Exception)
            {

            }

        }
        #endregion
        #region //method of category
        private DataTable searchCategory(string searchValue)
        {
            DataTable data = CategoryDAO.Instance.getCategoryByID(searchValue);
            if (data.Rows.Count <= 0)
            {
                data = CategoryDAO.Instance.getCategoryByName(searchValue);
            }
            return data;
        }
        private void loadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.getListCategoryAsDataTable();
            dtgvCategory.DataSource = categoryList;
            dtgvCategory.RowHeadersVisible = false;
        }
        private void addCategory(string cateID, string cateName)
        {
            if (CategoryDAO.Instance.addCategory(cateID,cateName))
            {
                MessageBox.Show("Thêm danh mục thành công");
            }
            else
            {
                MessageBox.Show("Thêm danh mục thất bại");
            }
            txtCategoryID.ReadOnly = true;
            txtCategoryID.Clear();
            txtCategoryName.Clear();
            loadListCategory();
            loadListProduct();
            isAddNewCategory = false;
        }
        private void updateCategory(string cateID, string cateName)
        {
            if (CategoryDAO.Instance.updateCategory(cateID, cateName))
            {
                MessageBox.Show("Cập nhật danh mục thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật danh mục thất bại");
            }

            loadListCategory();
            loadListProduct();
        }
        private void deleteCategory(string cateID)
        {
            if (CategoryDAO.Instance.deleteCategory(cateID))
            {
                MessageBox.Show("Xóa danh mục thành công");
            }
            else
            {
                MessageBox.Show("Xóa danh mục thất bại");
            }
            txtCategoryID.ReadOnly = true;
            txtCategoryID.Clear();
            txtCategoryName.Clear();
            loadListCategory();
            loadListProduct();
        }



        #endregion
        #region //event of category
        private void btnCategoryReset_Click(object sender, EventArgs e)
        {
            loadListCategory();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryID.ReadOnly == true)
            {
                txtCategoryID.ReadOnly = false;
                isAddNewCategory = true;
                txtCategoryID.Clear();
                txtCategoryName.Clear();
            }
            else
            {
                if (checkCategory())
                {
                    string cateID = txtCategoryID.Text;
                    string cateName = txtCategoryName.Text;
                    addCategory(cateID, cateName);
                }
                
            }
        }
        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            isAddNewCategory = false;
            if (checkCategory())
            {
                string cateID = txtCategoryID.Text;
                string cateName = txtCategoryName.Text;
                updateCategory(cateID, cateName);
            }
            
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string cateID = txtCategoryID.Text;
            foreach (DataGridViewRow item in dtgvFood.Rows)
            {
                string cateName = item.Cells[1].Value.ToString();
                if (txtCategoryName.Text.Equals(cateName))
                {
                    MessageBox.Show("Không thể xóa danh mục "+txtCategoryID.Text);
                    return;
                }
            }
            deleteCategory(cateID);
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            categoryList.DataSource = searchCategory(txtSearchCategory.Text);
            if (categoryList.Count!=0)
            {
            }
            else
            {
                MessageBox.Show("Không tìm thấy danh mục " + txtSearchCategory.Text.ToUpper());
                loadListCategory();
            }
        }
        private void dtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtgvCategory.Rows[e.RowIndex];
                txtCategoryID.ReadOnly = true;
                txtCategoryID.Text = row.Cells[0].Value.ToString();
                txtCategoryName.Text = row.Cells[1].Value.ToString();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region //method of product
        private void loadCategoryIntoComboBox(ComboBox cb)
        {        
            cb.DataSource = CategoryDAO.Instance.getCategoryComboBox();
            cb.DisplayMember = "Danh Mục";
        }
        private DataTable searchProduct(string searchValue)
        {
            DataTable data = ProductDAO.Instance.getProductByProductID(searchValue);
            if (data.Rows.Count <= 0)
            {
                data = ProductDAO.Instance.getProductByProductName(searchValue);
                if(data.Rows.Count <= 0)
                {
                    data = ProductDAO.Instance.getProductByCategoryName(searchValue);
                }
            }
            return data;
        }
        private void loadListProduct()
        {
            productList.DataSource = ProductDAO.Instance.getListProductAsDataTable();
            dtgvFood.DataSource = productList;
            dtgvFood.RowHeadersVisible = false;
            loadCategoryIntoComboBox(cbCategory);
        }
        private void addProduct(string proID, string cateID, string proName, float unitPrice)
        {
            if (ProductDAO.Instance.addProduct(proID, cateID,proName,unitPrice))
            {
                MessageBox.Show("Thêm sản phẩm thành công");
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại");
            }
            txtFoodID.ReadOnly = true;
            isAddNewProduct = false;
            txtFoodID.Clear();
            txtFoodName.Clear();
            cbCategory.SelectedIndex = 0;
            txtUnitPrice.Clear();
            loadListProduct();
        }
        private void updateProduct(string proID, string cateID, string proName, float unitPrice)
        {
            if (ProductDAO.Instance.updateProduct(proID, cateID, proName, unitPrice))
            {
                MessageBox.Show("Cập nhật sản phẩm thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật sản phẩm thất bại");
            }

            loadListProduct();
        }
        private void deleteProduct(string proID)
        {
            if (ProductDAO.Instance.deleteProduct(proID))
            {
                MessageBox.Show("Xóa sản phẩm thành công");
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm thất bại");
            }
            txtFoodID.ReadOnly = true;
            txtFoodID.Clear();
            txtFoodID.Focus();
            txtFoodName.Clear();
            txtUnitPrice.Clear();
            loadListProduct();
        }


        #endregion
        #region //event of product
        private void btnResetFood_Click(object sender, EventArgs e)
        {
            loadListProduct();
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (txtFoodID.ReadOnly == true)
            {
                txtFoodID.ReadOnly = false;
                isAddNewProduct = true;
                txtFoodID.Clear();
                txtFoodName.Clear();
                cbCategory.SelectedIndex = 0;
                txtUnitPrice.Clear();
            }
            else
            {
                if (checkProduct())
                {
                    string foodID = txtFoodID.Text;
                    string foodName = txtFoodName.Text;
                    string category = cbCategory.Text;
                    float unitPrice = Convert.ToInt64(txtUnitPrice.Text);
                    addProduct(foodID, category, foodName, unitPrice);
                }
                
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            isAddNewProduct = false;
            if (checkProduct())
            {
                string foodID = txtFoodID.Text;
                string foodName = txtFoodName.Text;
                string category = cbCategory.Text;
                float unitPrice = Convert.ToInt64(txtUnitPrice.Text);
                updateProduct(foodID, category, foodName, unitPrice);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            string proID = txtFoodID.Text;
            deleteProduct(proID);
        }


        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            productList.DataSource = searchProduct(txtSearchFood.Text);
            if (productList.Count != 0)
            {
            }
            else
            {
                MessageBox.Show("Không tìm thấy danh mục " + txtSearchFood.Text.ToUpper());
                loadListProduct();
            }
        }       

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtgvFood.Rows[e.RowIndex];
                txtFoodID.ReadOnly = true;
                txtFoodID.Text = row.Cells[0].Value.ToString();
                txtFoodName.Text = row.Cells[2].Value.ToString();
                cbCategory.SelectedItem = row.Cells[1].Value.ToString();
                txtUnitPrice.Text = row.Cells[3].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void loadListBillByDate(string day1, string day2)
        {
            dtgvBill.DataSource = BillDAO.Instance.getListBillByDate(day1, day2);
            txtTotalBill.Text = dtgvBill.Rows.Count.ToString();
            loadBillInfo();
        }
        #endregion
        #region//method of bill
        private void loadBill()
        {
            billList.DataSource = BillDAO.Instance.getListBill();
            dtgvBill.DataSource = billList;
            dtgvBill.RowHeadersVisible = false;
            txtTotalBill.Text = dtgvBill.Rows.Count.ToString();
        }
        private void loadBillInfo()
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            double totalIncome = 0;
            foreach (DataGridViewRow item in dtgvBill.Rows)
            {
                string billID = item.Cells[0].Value.ToString();
                billListInfo = BillInfoDAO.Instance.getListBillInfo(billID);
                foreach (BillInfoDTO dto in billListInfo)
                {
                    totalIncome += (dto.UnitPrice * dto.Quantity);
                }
            }
            txtTotalIncome.Text = totalIncome.ToString("c", culture);
        }
        #endregion
        #region //event of bill



        private void dtgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dtgvBill.Rows[e.RowIndex];
                lsvBillInfo.Items.Clear();
                CultureInfo culture = new CultureInfo("vi-VN");
                ListViewItem lsvItem = null;
                List<MenuDTO> listBillInfo = BillInfoDAO.Instance.getListBillInfoByID(row.Cells[0].Value.ToString());

                double totalPrice = 0;
                foreach (MenuDTO item in listBillInfo)
                {
                    //ListViewItem lsvItem = new ListViewItem(item.ProName.ToString());
                    lsvItem = new ListViewItem(item.ProName.ToString());
                    lsvItem.SubItems.Add(item.Quantity.ToString());
                    lsvItem.SubItems.Add(item.UnitPrice.ToString());
                    lsvItem.SubItems.Add(item.TotalPrice.ToString());
                    totalPrice += item.TotalPrice;
                    lsvBillInfo.Items.Add(lsvItem);
                }
                txtTotalPrice.Text = totalPrice.ToString("c", culture);
            }
            catch (Exception)
            {

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpkDate1.Value.ToString("yyyy/MM/dd"), dtpkDate2.Value.ToString("yyyy/MM/dd"));
        }
        

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            loadBill();
            loadBillInfo();
        }


        #endregion

        
    }
}
