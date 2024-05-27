using System.ComponentModel.DataAnnotations;

namespace RenovaRS.Models
{
    public class User
    {
        public int Id { get; set; }

        byte[]? Image { get; set; }

        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        // TODO: Put sensible data somewhere else.
        public required string Password { get; set; }

        public required LegalEntity LegalEntity { get; set; }

        public string? CPF { get; set; }

        public string? CNPJ { get; set; }
    }
}
