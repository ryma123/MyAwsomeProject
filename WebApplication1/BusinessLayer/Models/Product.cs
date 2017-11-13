using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BusinessLayer.Models
{
    [Table("product")]
   public  class product
    {
        [Key]
        public string ProductId { get; set; }
        public string locatioh { get; set; }
        public List<Release> releases;
    }
}
