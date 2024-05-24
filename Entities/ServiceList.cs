using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ServiceList
    {
        [Key]
        public int Id { get; set; }

        public string TypeOfService { get; set; }
    }
}
