using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class BusinessHoursTable
    {
        [Key]
        public int Id { get; set; }

        public Boolean Monday { get; set; }

        public Boolean Tuesday { get; set; }

        public Boolean Wednesday { get; set; }

        public Boolean Thursday { get; set; }

        public Boolean Friday { get; set; }

        public Boolean Saturday { get; set; }

        public Boolean Sunday { get; set; }

        public string WorkHours { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }
    }
}
