using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TitleModel
    {
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Year published is required.")]
        [Range(1000, 2100, ErrorMessage = "Must be a valid year.")]
        public int Year_Published { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN length must be between 10 and 20.")]
        public string? ISBN { get; set; }

        [Required(ErrorMessage = "Publisher ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Publisher Id must be selected from the list")]
        public int PubID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Notes are required")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        public string? Comments { get; set; }
    }
}
