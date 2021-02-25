using Dashboard.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    static class Program
    {

        private static bool isLogin = false;
        private static MySQL mySQL;
        private static Form1 ui;

        [STAThread]
        static void Main()
        {
            mySQL = new MySQL();
            ui = new Form1();
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ui);
        }

        public static bool IsLogin()
        {
            return isLogin;
        }
        public static void SetLogin(bool state)
        {
            isLogin = state;
        }

        public static Form1 GetUI()
        {
            return ui;
        }

        public static MySQL GetMySQL()
        {
            return mySQL;
        }

        public static bool DoesPasswordCheck(String email, String password)
        {
            String passFromDB = mySQL.GetHashedPasswordByEmail(email);
            String[] splitter = passFromDB.Split('$');

            String s = encode(password);
            s = encode(s + splitter[1]);

            if (passFromDB.Equals(splitter[0] + "$" + splitter[1] + "$" + s))
            {
                Debug.WriteLine("Access allowed");

                ui.SetFullname(mySQL.GetFullnameByEmail(email));

                return true;
            }
            Debug.WriteLine("Access denied");
            return false;
        }

        private static String encode(string input)
        {
            string saltedPassword = input;

            var sha256 = new System.Security.Cryptography.SHA256Managed();
            var plaintextBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            var hashString = sb.ToString();
            Debug.WriteLine(hashString);
            return hashString;

        }

    }
}
