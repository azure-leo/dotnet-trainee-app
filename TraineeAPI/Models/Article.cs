using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeAPI.Models
{
	public class Article : BaseModel
	{
		[Required]
		[MinLength(2), MaxLength(100)]
		public string Title { get; set; } = string.Empty;

		[Required] [MaxLength(1000)] public string Content { get; set; } = string.Empty;

		[Required]
		public int AuthorId { get; set; }
		
		[ForeignKey("AuthorId")]
		public User Author { get; set; }

		public Article()
		{
		}
	}
}

