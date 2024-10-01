using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class FAQTable
    {
        [Key]
        public int Id { get; set; }

        public string question { get; set; }

        public string answer { get; set; }

        public string department { get; set; }
    }

}
