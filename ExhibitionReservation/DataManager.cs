using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ExhibitionReservation
{
    class DataManager
    {
        public static List<Reservation> Reservations = new List<Reservation>();  //객체 생성
        public static List<User> Users = new List<User>();

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string reservationsOutput = File.ReadAllText(@"./Reservations.xml");
                XElement reservationsXElement = XElement.Parse(reservationsOutput);

                Reservations = (from item in reservationsXElement.Descendants("reservation")
                         select new Reservation()
                         {
                             No = item.Element("no").Value,
                             Name = item.Element("name").Value,
                             Time = item.Element("time").Value,
                             Place = item.Element("place").Value,
                             Person = item.Element("person").Value,
                             UserId = int.Parse(item.Element("userId").Value),
                             UserName = item.Element("userName").Value,
                            IsReserved = item.Element("isReserved").Value != "0" ? true : false,
                             //BorrowedAt = DateTime.Parse(item.Element("borrowedAt").Value)
                         }).ToList<Reservation>();

                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value,
                             PhoneNumber = item.Element("phonenumber").Value
                         }).ToList<User>();
            }
            catch (FileNotFoundException e)
            {
                Save();
            }
        }
        public static void Save() //Load와 반대로
        {
            string reservationsOutput = "";
            reservationsOutput += "<reservations>\n";
            foreach (var item in Reservations)
            {
                reservationsOutput += "<reservation>\n";

                reservationsOutput += "<no>" + item.No + "</no>\n";
                reservationsOutput += "<name>" + item.Name + "</name>\n";
                reservationsOutput += "<time>" + item.Time + "</time>\n";
                reservationsOutput += "<place>" + item.Place + "</place>\n";
                reservationsOutput += "<person>" + item.Person + "</person>\n";
                reservationsOutput += "<userId>" + item.UserId + "</userId>\n";
                reservationsOutput += "<userName>" + item.UserName + "</userName>\n";
                reservationsOutput += "<isReserved>" + (item.IsReserved ? 1 : 0) + "</isReserved>\n";
                //reservationsOutput += "<borrowedAt>" + item.BorrowedAt.ToLongDateString() + "</borrowedAt>\n";
                reservationsOutput += "</reservation>\n";
            }

            reservationsOutput += "</reservations>";
            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "<id>\n" + item.Id + "</id>\n";
                usersOutput += "<name>\n" + item.Name + "</name>\n";
                usersOutput += "<phonenumber>\n" + item.PhoneNumber + "</phonenumber>\n";
                usersOutput += "</user>\n";

            }

            usersOutput += "</users>\n";

            File.WriteAllText(@"./Reservations.xml", reservationsOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);
        }
    }
}
