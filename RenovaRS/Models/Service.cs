using System.ComponentModel.DataAnnotations;

namespace RenovaRS.Models
{
    public class Service
    {
        public int Id { get; set; }

        public byte[]? Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<string> Type { get; set; }

        public Address? Address { get; set; }

        [Required]
        public List<string> CityRanges { get; set; }

        [Required]
        public Availability Availability { get; set; }

        [Required]
        public bool IsPaid { get; set; }
    }
}
