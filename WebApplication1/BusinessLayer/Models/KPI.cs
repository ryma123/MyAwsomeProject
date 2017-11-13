using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BusinessLayer.Models
{
    [Table("KPI")]
    public class KPI
    {
        [Key]
        public int blaid { get; set; }
        public string SomeValue { get; set; }

        public virtual product product { get; set; }
        public virtual Release release { get; set; }
    }
}
