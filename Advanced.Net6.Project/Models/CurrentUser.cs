using System.ComponentModel.DataAnnotations;

namespace Advanced.Net6.Project.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        [Display(Name = "用户名")]
        public string? Name { get; set; }
        public string? Account { get; set; }
        [Display(Name = "密码")]
        public string? Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
