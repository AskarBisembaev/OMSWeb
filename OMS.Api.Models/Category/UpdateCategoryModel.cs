using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Api.Models.Category
{
	public class UpdateCategoryModel
	{
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; } = null!;
    }
}
