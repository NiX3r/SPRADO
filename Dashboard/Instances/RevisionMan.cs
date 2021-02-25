using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class RevisionMan
    {
        
        private String Fullname, Company, Email, Phone, Info;

        public RevisionMan(String Fullname, String Company, String Email, String Phone, String Info)
        {
            this.Fullname = Fullname;
            this.Company = Company;
            this.Email = Email;
            this.Phone = Phone;
            this.Info = Info;
        }
        
        // GETTERS
        public String GetFullname()
        {
            return Fullname;
        }
        public String GetCompany()
        {
            return Company;
        }
        public String GetEmail()
        {
            return Email;
        }
        public String GetPhone()
        {
            return Phone;
        }
        public String GetInfo()
        {
            return Info;
        }

        // SETTERS
        public void SetFullname(String Fullname)
        {
            this.Fullname = Fullname;
        }
        public void SetCompany(String Company)
        {
            this.Company = Company;
        }
        public void SetEmail(String Email)
        {
            this.Email = Email;
        }
        public void SetPhone(String Phone)
        {
            this.Phone = Phone;
        }
        public void SetInfo(String Info)
        {
            this.Info = Info;
        }

    }
}
