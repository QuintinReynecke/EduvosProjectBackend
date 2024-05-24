using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ReviewTable
    {
        [Key]
        public int Id { get; set; }

        public int SumOfTotalRatings { get; set; }

        public int SumOfTotalUserRated { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }
    }
}
