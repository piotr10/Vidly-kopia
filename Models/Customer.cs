using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.Ajax.Utilities;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; } //navigation propeprty która pozwala nam poruszać się z jednego
        //typu do drugiego w tym przpadku od Customer do MemershipType

        [Display(Name = "Membership Type")] //zmiana nazwy na Membership Type
        public byte MembershipTypeId { get; set; } // klucz obcy
        
        [Display(Name = "Date of Birth")] // zmiana nazwy na Date of Birth
        [Min18YearsIfAMember] //specjalna klasa utworzona przez nas w celu niestandardowego sprawdzenia daty urodzenia
        public DateTime? Birthdate { get; set; }
    }
}