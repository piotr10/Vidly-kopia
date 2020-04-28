using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    public class MembershipType //Członkostwo
    {
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public short SignUpFree { get; set; } // zarejestruj się za darmo
        public byte DurationInMonths { get; set; } //czas trwania w miesiącu
        public byte DiscountRate { get; set; } // przecena

        /* Magick Numbers :
         Unknown i PayAsYouGo służą temu aby zastąpić 0 i 1 w customer.MembershipTypeId == 0 ||customer.MembershipTypeId == 1
         w klasie Min18YearsIfAMember */
        public static readonly byte Unknow = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}
 