using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BusinessLayer.Models
{
    [Table("Release")]
 public   class Release
    {
        [Key]
        public string Releaseid { get; set; }
        public virtual product Product { get; set; }
    }
}
