using System.ComponentModel.DataAnnotations;

namespace ReconstroiRS.Models
{
    public class Service
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public byte[]? Image { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required IList<string> Type { get; set; }

        public string? Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Url]
        public required string ContactLink { get; set; }

        public required IList<string> CityRanges { get; set; }

        public required Availability Availability { get; set; }

        public required bool IsPaid { get; set; }
    }
}
