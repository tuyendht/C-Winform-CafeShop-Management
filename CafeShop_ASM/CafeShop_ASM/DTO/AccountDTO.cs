using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DTO
{
    public class AccountDTO
    {
        private string username;
        private string fullname;
        private string password;
        private bool isAdmin;
        public AccountDTO(DataRow row)
        {
            this.username = row["username"].ToString();
            this.fullname = row["fullname"].ToString();
            this.password = row["password"].ToString();
            this.isAdmin = (bool)row["isAdmin"];
        }
        public AccountDTO()
        {

        }
        public AccountDTO(string username, string fullname, string password, bool isAdmin)
        {
            this.username = username;
            this.fullname = fullname;
            this.password = password;
            this.isAdmin = isAdmin;
        }


        public string Username { get => username; set => username = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
    }
}
