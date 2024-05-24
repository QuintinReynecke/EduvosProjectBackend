using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ContactDetailsTable
    {
        [Key]
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }
    }
}
