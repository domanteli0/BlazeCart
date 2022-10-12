
using System.ComponentModel.DataAnnotations;

namespace BlazeCart.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$", ErrorMessage = "Name must start with a capital letter and contain only letters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$", ErrorMessage = "Surname must start with a capital letter and contain only letters.")]
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password should have at least 8 characters, one uppercase letter, one lowercase letter and a digit.")]
        public string Password { get; set; }

        public User(int id, string name, string surname, string email, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;

        }
        public User(){}


    }
}
