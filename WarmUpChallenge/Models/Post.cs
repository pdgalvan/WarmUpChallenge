using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarmUpChallenge.Models
{
    public class Post
    {
        
        public Guid PostId { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage ="Campo requerido")]
        [MaxLength(50, ErrorMessage ="El titulo no debe superar los 50 caracteres")]
        [MinLength(5, ErrorMessage = "El titulo debe superar los 5 caracteres")]
        public string Title { get; set; }

        [Display(Name = "Contenido")]
        [Required(ErrorMessage ="Campo requerido")]
        [MinLength(10, ErrorMessage ="El contenido del post debe superar los 10 caracteres")]
        public string Content { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage ="Debes seleccionar una categoria")]
        public Category Category { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yy}")]
        [Display(Name ="Fecha de creación")]
        public DateTime CreationDate { get; set; }

    }
}
