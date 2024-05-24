using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class WorkLocationTable
    {
        [Key]
        public int Id { get; set; }

        public string workInCountry { get; set; }

        public string province { get; set; }

        public string suburb { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }
    }
}
