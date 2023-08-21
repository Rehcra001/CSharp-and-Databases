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
        public int Year_Published { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN length must be between 10 and 20.")]
        public string? ISBN { get; set; }

        [Required(ErrorMessage = "Publisher ID is required.")]
        public int PubID { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Subject { get; set; }
        public string? Comments { get; set; }
    }
}
