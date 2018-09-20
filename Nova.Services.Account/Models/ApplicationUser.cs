using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nova.Services.Account.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(6)]
        [RegularExpression(@"^[0-9]\{6}$")]
        public string ZipCode { get; set; }
    }
}
