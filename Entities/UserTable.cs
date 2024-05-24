using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class UserTable
    {
        [Key]
        public int Id { get; set; }

        public Boolean isCertified { get; set; }

        public string proofOfCertification { get; set; }

        [ForeignKey("MainTable")]

        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }

    }
}
