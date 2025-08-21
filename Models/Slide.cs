using PresenterAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PresenterAPI.Models
{
    public class Slide : IAuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string Content { get; set; } = string.Empty;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int Order { get; set; }
        public bool IsVisible { get; set; } = true;
        public string? Notes { get; set; }

        public int PresentationId { get; set; }

        // Navigation
        public Presentation Presentation { get; set; } = null!;
    }
}
