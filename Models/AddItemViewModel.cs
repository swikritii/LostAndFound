using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public class AddItemViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public String Status { get; set; }

        [ValidateNever]
        public int CategoryId { get; set; }
  
        public List<CategoryOption> Categories { get; set; } = new();

        public class CategoryOption
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
