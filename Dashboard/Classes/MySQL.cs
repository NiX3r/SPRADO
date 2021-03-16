using Dashboard.Instances;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard.Classes
{
    class MySQL
    {

        private MySqlConnection connection;

        public MySQL()
        {

            connection = new MySqlConnection("too secret for show!!");

            try
            {
                connection.Open();
                Debug.WriteLine("MySQL connection opened successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MySQL connection opened unsuccessfully\nError: " + ex.Message);

            }

        }

        public void DestroyConnection()
        {
            connection.Close();
        }

        public String GetHashedPasswordByEmail(String email)
        {

            String output = "";

            var command = new MySqlCommand("SELECT Password FROM Users WHERE Email='" + email + "';", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            output = reader.GetString(0);
            reader.Close();

            return output;

        }

        public String GetFullnameByEmail(String email)
        {

            String output = "";

            var command = new MySqlCommand("SELECT FName,LName FROM Users WHERE Email='" + email + "' AND Verify=1 AND Administrator=1;", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            output = reader[0] + " " + reader[1];
            reader.Close();

            return output;

        }

        public String GetCountHouses()
        {
            String output = "";

            var command = new MySqlCommand("SELECT Count(IDHouse) FROM Houses;", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            output = reader.GetInt64(0).ToString();
            reader.Close();

            return output;
        }

        public String GetCountUsers()
        {
            String output = "";

            var command = new MySqlCommand("SELECT Count(IDUser) FROM Users;", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            output = reader.GetInt64(0).ToString();
            reader.Close();

            return output;
        }

        public List<RevisionMan> GetRevisionMen()
        {

            List<RevisionMan> output = new List<RevisionMan>();

            var command = new MySqlCommand("SELECT * FROM RevisionMan;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                RevisionMan rm = new RevisionMan(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                output.Add(rm);

            }

            reader.Close();

            return output;

        }

        public List<RevisionType> GetRevisionTypes()
        {

            List<RevisionType> output = new List<RevisionType>();

            var command = new MySqlCommand("SELECT * FROM RevisionType;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                RevisionType rt = new RevisionType(reader.GetString(1), reader.GetInt16(2), reader.GetString(3));
                output.Add(rt);

            }

            reader.Close();

            return output;

        }

        public List<House> GetHouses()
        {
            List<House> output = new List<House>();

            var command = new MySqlCommand("SELECT * FROM Houses;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                
                int key = reader.GetInt16(0);
                String name = reader.GetString(3) + " " + reader.GetInt16(4);
                output.Add(new House(key, name));

            }

            reader.Close();

            return output;
        }

        public List<Contact> GetContactsByHouseID(int id)
        {

            List<Contact> output = new List<Contact>();

            var command = new MySqlCommand("SELECT * FROM Contacts WHERE HouseID=" + id + ";", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                
                String name = reader.GetString(1);
                String fullname = reader.GetString(3);
                String email = reader.GetString(5);
                String info = reader.GetString(6);
                String phone = reader.GetString(7);
                bool isOwner = reader.GetBoolean(4);
                output.Add(new Contact(name, fullname, email, info, phone, isOwner));

            }

            reader.Close();

            return output;

        }

        public List<Revision> GetRevisionsByHouseID(int id, List<RevType> revType, List<RevMan> revMan)
        {

            List<Revision> output = new List<Revision>();

            var command = new MySqlCommand("SELECT * FROM Revision WHERE HouseID=" + id + ";", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                int key = reader.GetInt16(0);
                int t = reader.GetInt16(2);
                int m = reader.GetInt16(1);
                String info = reader.GetString(5);
                DateTime lastDone = reader.GetDateTime(4);
                RevType rt = null;
                RevMan rm = null;

                foreach(RevType rtt in revType)
                {
                    if (rtt.GetKey().Equals(t))
                    {
                        rt = rtt;
                    }
                }

                foreach (RevMan rmt in revMan)
                {
                    if (rmt.GetKey().Equals(m))
                    {
                        rm = rmt;
                    }
                }

                output.Add(new Revision(info, lastDone, rm, rt));

            }

            reader.Close();

            return output;

        }

        public List<RevType> GetRevType()
        {
            List<RevType> output = new List<RevType>();

            var command = new MySqlCommand("SELECT * FROM RevisionType;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                int key = reader.GetInt16(0);
                String fullname = reader.GetString(1);
                output.Add(new RevType(key, fullname));

            }

            reader.Close();

            return output;
        }
        public List<RevMan> GetRevMan()
        {
            List<RevMan> output = new List<RevMan>();

            var command = new MySqlCommand("SELECT * FROM RevisionMan;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                int key = reader.GetInt16(0);
                String fullname = reader.GetString(1);
                output.Add(new RevMan(key, fullname));

            }

            reader.Close();

            return output;
        }

        public List<DateTime> GetRevisionDates()
        {
            List<DateTime> output = new List<DateTime>();

            var command = new MySqlCommand("SELECT Revision.LastDone, RevisionType.YearLoop FROM Revision RIGHT JOIN RevisionType ON Revision.RevisionTypeID = RevisionType.ID;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                try
                {
                    DateTime dt = reader.GetDateTime(0);
                    dt = dt.AddYears(reader.GetInt16(1));
                    output.Add(dt);
                }
                catch(Exception ex)
                {

                }

            }

            reader.Close();

            return output;
        }

        public List<RevisionForList> GetRevisionForLists()
        {
            List<RevisionForList> output = new List<RevisionForList>();

            var command = new MySqlCommand("SELECT Revision.LastDone, Houses.Street, Houses.StreetNumber, RevisionType.Name, RevisionMan.Fullname, Revision.Info, RevisionType.YearLoop FROM Revision RIGHT JOIN Houses ON Revision.HouseID=Houses.IDHouse RIGHT JOIN RevisionType ON Revision.RevisionTypeID=RevisionType.ID RIGHT JOIN RevisionMan ON Revision.RevisionManID=RevisionMan.ID;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                try
                {
                    RevisionForList rfl = new RevisionForList(reader.GetDateTime(0), reader.GetString(1) + " " + reader.GetInt16(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt16(6));

                    output.Add(rfl);
                }
                catch(Exception ex)
                {

                }

                

            }

            reader.Close();

            return output;
        }

        public String GetEmailListByHouseID(int id)
        {
            String output = "";

            var command = new MySqlCommand("SELECT * FROM Contacts WHERE HouseID=" + id + ";", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                output += reader.GetString(5) + ", ";

            }

            reader.Close();

            return output.Substring(0, output.Length - 2);
        }

        public String GetOwnerEmailListByHouseID(int id)
        {
            String output = "";

            var command = new MySqlCommand("SELECT * FROM Contacts WHERE HouseID=" + id + " AND Owner=true;", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                output += reader.GetString(5) + ", ";

            }

            reader.Close();

            return output.Substring(0, output.Length - 2);
        }

        public bool CheckRevisionManExists(String Fullname)
        {
            bool output;

            var command = new MySqlCommand("SELECT * FROM RevisionMan WHERE Fullname='" + Fullname + "';", connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
                output = true;
            else
                output = false;

            reader.Close();

            return output;
        }

        public bool CheckRevisionExists(int house, int type)
        {
            bool output;

            var command = new MySqlCommand("SELECT * FROM Revision WHERE HouseID=" + house + " AND RevisionTypeID=" + type + ";", connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
                output = true;
            else
                output = false;

            reader.Close();

            return output;
        }

        public bool CheckRevisionTypeExists(String Name)
        {
            bool output;

            var command = new MySqlCommand("SELECT * FROM RevisionType WHERE Name='" + Name + "';", connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
                output = true;
            else
                output = false;

            reader.Close();

            return output;
        }

        public bool CheckContactExists(String Name)
        {
            bool output;

            var command = new MySqlCommand("SELECT * FROM Contacts WHERE Name='" + Name + "';", connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
                output = true;
            else
                output = false;

            reader.Close();

            return output;
        }

        public void AddRevisionMan(RevisionMan revisionMan)
        {

            var command = new MySqlCommand("INSERT INTO RevisionMan(Fullname, Company, Email, Phone, Info) VALUES('" + revisionMan.GetFullname() + "', '" + revisionMan.GetCompany() + "', '" + revisionMan.GetEmail() + "', '" + revisionMan.GetPhone() + "', '" + revisionMan.GetInfo() + "');", connection);
            command.ExecuteNonQuery();

        }
        public void AddContact(Contact contact, int house)
        {
            var command = new MySqlCommand("INSERT INTO Contacts(Name, HouseID, Fullname, Owner, Email, Phone, Info) VALUES('" + contact.GetName() + "', '" + house + "', '" + contact.GetFullname() + "', " + contact.GetIsOwner() + ", '" + contact.GetEmail() + "', '" + contact.GetPhone() + "', '" + contact.GetInfo() + "');", connection);
            command.ExecuteNonQuery();
        }
        public void AddRevisionType(RevisionType revisionType)
        {
            var command = new MySqlCommand("INSERT INTO RevisionType(Name, YearLoop, Info) VALUES('" + revisionType.GetName() + "', " + revisionType.GetYearLoop() + ", '" + revisionType.GetInfo() + "');", connection);
            command.ExecuteNonQuery();
        }
        public void AddRevision(Revision revision, int house)
        {
            var command = new MySqlCommand("INSERT INTO Revision(RevisionManID, RevisionTypeID, HouseID, LastDone, Info) VALUES(" + revision.GetRevisionMan().GetKey() + ", " + revision.GetRevisionType().GetKey() + ", " + house + ", '" + revision.GetLastDone().ToString("yyyy-MM-dd HH:mm:ss") + "', '" + revision.GetInfo() + "');", connection);
            command.ExecuteNonQuery();
        }

        public void UpdateRevisionMan(RevisionMan revisionMan)
        {
            var command = new MySqlCommand("UPDATE RevisionMan SET Company='" + revisionMan.GetCompany() + "', Email='" + revisionMan.GetEmail() + "', Phone='" + revisionMan.GetPhone() + "', Info='" + revisionMan.GetInfo() + "' WHERE Fullname='" + revisionMan.GetFullname() + "';", connection);
            command.ExecuteNonQuery();
        }
        public void UpdateRevisionType(RevisionType revisionType)
        {
            var command = new MySqlCommand("UPDATE RevisionType SET YearLoop=" + revisionType.GetYearLoop() + ", Info='" + revisionType.GetInfo() + "' WHERE Name='" + revisionType.GetName() + "';", connection);
            command.ExecuteNonQuery();
        }
        public void UpdateContact(Contact contact)
        {
            var command = new MySqlCommand("UPDATE Contacts SET Fullname='" + contact.GetFullname() + "', Email='" + contact.GetEmail() + "', Phone='" + contact.GetPhone() + "', Owner=" + contact.GetIsOwner() + ", Info='" + contact.GetInfo() + "' WHERE Name='" + contact.GetName() + "';", connection);
            command.ExecuteNonQuery();
        }
        public void UpdateRevision(Revision revision, int house)
        {
            var command = new MySqlCommand("UPDATE Revision SET RevisionManID=" + revision.GetRevisionMan().GetKey() + ", LastDone='" + revision.GetLastDone().ToString("yyyy-MM-dd HH:mm:ss") + "', Info='" + revision.GetInfo() + "' WHERE HouseID=" + house + " AND RevisionTypeID=" + revision.GetRevisionType().GetKey() + ";", connection);
            command.ExecuteNonQuery();
        }

        public void DeleteRevisionMan(String Fullname)
        {
            var command = new MySqlCommand("DELETE FROM RevisionMan WHERE Fullname='" + Fullname + "';", connection);
            command.ExecuteNonQuery();
        }
        public void DeleteRevisionType(String Name)
        {
            try
            {
                var command = new MySqlCommand("DELETE FROM RevisionType WHERE Name='" + Name + "';", connection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Jsou vytvořeny revize s tímto typem. Musíš je první odebrat!");
            }
        }
        public void DeleteContact(String Name, int house)
        {
            var command = new MySqlCommand("DELETE FROM Contacts WHERE Name='" + Name + "' AND HouseID=" + house + ";", connection);
            command.ExecuteNonQuery();
        }
        public void DeleteRevision(int house, int type)
        {
            var command = new MySqlCommand("DELETE FROM Revision WHERE HouseID=" + house + " AND RevisionTypeID=" + type + ";", connection);
            command.ExecuteNonQuery();
        }

    }
}
