using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expeditions.Models
{
    [Table("NewsArticle")]
    public partial class NewsArticle
    {
        public NewsArticle()
        {
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
