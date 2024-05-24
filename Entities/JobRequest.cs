using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class JobRequest
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime DateRequested { get; set; }

        public string Status { get; set; }

        [ForeignKey("MainTable")]
        public int MainTableFKId { get; set; }

        public virtual MainTable MainTable { get; set; }
    }
}
