using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExhibitionReservation
{
    class Reservation
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Person { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsReserved{ get; set; }
    }
}
