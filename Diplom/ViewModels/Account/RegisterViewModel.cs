using System.ComponentModel.DataAnnotations;

namespace Diplom.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Укажите логин")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6,ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = " Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set;}

        [AllowedValues("Admin", "User", ErrorMessage = "Введите Admin или User")]
        public string Role { get; set; } = "User";
    }
}
