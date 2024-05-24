using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class MainTable
    {
        [Key]
        public int Id { get; set; }

        public string ProfilePicture { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public float UserRating { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Province { get; set; }

        public string CallOutFee { get; set; }
      
        public Boolean Active { get; set; }

        public int TotalPhotos { get; set; }

        public List<ContactDetailsTable> ContactDetailsTable { get; set; }

        public List<BusinessHoursTable> BusinessHoursTable { get; set; }

        public List<PhotosTable> PhotosTable { get; set; }

        public List<WorkLocationTable> WorkLocationTable { get; set; }

        public List<UserTable> UserTable { get; set; }

        public List<ReviewTable> ReviewTable { get; set; }

        public List<JobRequest> JobRequests { get; set; }


    }
}
