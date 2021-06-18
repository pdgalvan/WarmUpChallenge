using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarmUpChallenge.Models
{
    public class PostVm
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
