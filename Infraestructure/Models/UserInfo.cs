namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;

    public class UserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public bool Sex { get; set; }
    }
}
