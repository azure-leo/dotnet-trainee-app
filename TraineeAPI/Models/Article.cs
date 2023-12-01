using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeAPI.Models
{
	public class Article : BaseModel
	{
		//"id": 0,
		//"created_at": "now",
		//"authorId": 0,
		//"email": "user@example.com",
		//"author": "string",
		//"title": "string",
		//"content": "string",
		[Required, MaxLength(100)]
		public string Title = string.Empty;

		[Required, MaxLength(1000)]
		public string Content = string.Empty;

		[Required]
		public int AuthorId { get; set; }
		
		[ForeignKey("AuthorId")]
		public User Author { get; set; }

		public Article()
		{
		}
	}
}

