namespace DigitalBank.Portal.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public bool Sex { get; set; }
    }
}
