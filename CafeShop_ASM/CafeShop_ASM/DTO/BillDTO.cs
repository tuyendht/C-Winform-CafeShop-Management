using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    public class BillDTO
    {
        private int billID;
        private DateTime? dateCheckIn;
        private string username;
        private bool status;

        public BillDTO()
        {

        }
        public BillDTO(DataRow row)
        {
            this.billID = (int)row["Mã đơn"];
            this.dateCheckIn = (DateTime?)row["Thời gian"];

            this.username = (string)row["Nhân viên"];
            this.status = (bool)row["Trạng thái"];
        }
        public BillDTO(int billID, DateTime? dateCheckIn, string username, bool status)
        {
            this.billID = billID;
            this.dateCheckIn = dateCheckIn;
            this.username = username;
            this.status = status;
        }

        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public string Username { get => username; set => username = value; }
        public bool Status { get => status; set => status = value; }
        public int BillID { get => billID; set => billID = value; }
    }
}
