namespace LabAcademiaBlazor.Models;

public class LoginDTO
{
    //[Required(ErrorMessage = "O campo não pode ficar vazio!")]
    public string? Usuario { get; set; }

    //[Required(ErrorMessage = "O campo não pode ficar vazio!")]
    public string? Senha { get; set; }
}
