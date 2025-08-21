using PresenterAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresenterAPI.Models
{
    public class Presentation : IAuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        // Navigation
        public List<Slide> Slides { get; set; } = [];
    }
}
