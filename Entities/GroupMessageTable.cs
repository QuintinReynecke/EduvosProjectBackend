using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class GroupMessageTable
    {
        [Key]
        public int Id { get; set; }

        public int group_id { get; set; }

        public string message { get; set; }

        public DateTime? DateAdded { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }

    }

}
