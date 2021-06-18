using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarmUpChallenge.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(35)]
        [Display(Name = "Categoria")]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
