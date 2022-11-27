using System.ComponentModel.DataAnnotations;
namespace Blog.ViewModels;

public class LoginViewModel {
    public string Username { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
