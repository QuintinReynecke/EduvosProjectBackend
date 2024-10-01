using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class PersonalChatsTable
    {
        [Key]
        public int Id { get; set; }

        public string facultyType { get; set; }

        public string question { get; set; }

        public string answer { get; set; }

        public string department { get; set; }

        public DateTime? DateAdded { get; set; }

        [ForeignKey("MainTable")]
        public int mainTableFKId { get; set; }
        public virtual MainTable MainTable { get; set; }

    }

}
