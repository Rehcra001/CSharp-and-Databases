using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PublisherModel
    {

        [Key]
        public int PubID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 characters")]
        public string? Name { get; set; }        

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 characters")]
        public string? Company_Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        public string? Zip { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Fax is required")]
        public string? Fax { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        public string? Comments { get; set; }
    }
}
