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
        
        public string Password { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Department { get; set; }

        public string PasswordResetCode { get; set; }

        public DateTime? PasswordResetExpiration { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }


        public List<GroupMessageTable> GroupMessageTable { get; set; }

        public List<PersonalChatsTable> PersonalChatsTable { get; set; }

    }
}
