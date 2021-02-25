using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class Contact
    {

        private String name, fullname, email, info;
        private String phone;
        private bool isOwner;

        public Contact(String name, String fullname, String email, String info, String phone, bool isOwner)
        {
            this.name = name;
            this.fullname = fullname;
            this.email = email;
            this.info = info;
            this.phone = phone;
            this.isOwner = isOwner;
        }

        // GETTERS
        public void SetName(String name)
        {
            this.name = name;
        }
        public void SetFullname(String fullname)
        {
            this.fullname = fullname;
        }
        public void SetEmail(String email)
        {
            this.email = email;
        }
        public void SetInfo(String info)
        {
            this.info = info;
        }
        public void SetPhone(String phone)
        {
            this.phone = phone;
        }
        public void SetIsOwner(bool isOwner)
        {
            this.isOwner = isOwner;
        }

        // SETTERS
        public String GetName()
        {
            return this.name;
        }
        public String GetFullname()
        {
            return this.fullname;
        }
        public String GetEmail()
        {
            return this.email;
        }
        public String GetInfo()
        {
            return this.info;
        }
        public String GetPhone()
        {
            return this.phone;
        }
        public bool GetIsOwner()
        {
            return this.isOwner;
        }

    }
}
