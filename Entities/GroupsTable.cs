using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class GroupsTable
    {
        [Key]
        public int Id { get; set; }

        public string group_name { get; set; }

        public string department { get; set; }

    }
}
