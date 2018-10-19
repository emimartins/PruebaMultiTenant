using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    public class Fabrica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string nombre { get; set; }
        public virtual ICollection<Golosina> golosinas { get; set; }

        public Fabrica()
        {

        }
    }
}
