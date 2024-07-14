using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string AccountNo { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailConfirmed { get; set; }
        public int Age { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public DateTime Dob { get; set; }
        public int LGAId { get; set; }
        public int StateId { get; set; }
        public string Bvn { get; set; }
        public bool HasBvn { get; set; }
        public int NIN { get; set; }
        public string Address { get; set; }
        public string LandMark { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Status { get; set; }





    }
}
