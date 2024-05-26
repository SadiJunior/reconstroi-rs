using System.ComponentModel.DataAnnotations;

namespace RenovaRS.Models
{
    public class User
    {
        public int Id { get; set; }

        byte[]? Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public LegalEntity LegalEntity { get; set; }

        public string? CPF { get; set; }

        public string? CNPJ { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Instagram { get; set; }

        public string? Site { get; set; }

        public Address? Address { get; set; }

        public List<string>? CityRanges { get; set; }

        public List<Service>? Services { get; set; }
    }
}
