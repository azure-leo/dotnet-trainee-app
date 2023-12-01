using System;
using System.ComponentModel.DataAnnotations;

namespace TraineeAPI.Models
{
	public class User : BaseModel
	{
		[Required]
		[MinLength(2), MaxLength(50)]
		public string Username { get; set; } = string.Empty;
		
		[Required]
		[MinLength(2), MaxLength(50)]
		public string FirstName { get; set; } = string.Empty;
		
		[Required]
		[MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
		
        [Required]
        [MinLength(2), MaxLength(50)]
        public string Email { get; set; } = string.Empty;
		
        [Required]
        [MaxLength(500)]
        public string Password { get; set; }

        public User()
		{

		}
	}
}

