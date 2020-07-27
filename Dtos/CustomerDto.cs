using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        #region Skopiowane z Customer.cs

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipTypeDto { get; set; }

        //[Min18YearsIfAMember] 
        public DateTime? Birthdate { get; set; }

        #endregion
    }
}