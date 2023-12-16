namespace LabAcademiaBlazor.Models;

public class RegistrarUsuarioDTO
{
    [MinLength(4, ErrorMessage = "O campo 'Nome' deve ter no mínimo 4 caracteres")]
    [MaxLength(30, ErrorMessage = "O campo 'Nome' deve ter no máximo 30 caracteres")]
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
    public string? Nome { get; set; }

    [MinLength(10, ErrorMessage = "O campo 'Email' deve ter no mínimo 10 caracteres")]
    [MaxLength(50, ErrorMessage = "O campo 'Email' deve ter no máximo 50 caracteres")]
    [Required(ErrorMessage = "O campo 'Email' é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório")]
    [RegularExpression("^[(][0-9]{2}[)][9][0-9]{4}[-][0-9]{4}$", ErrorMessage = "O campo 'Telefone' deve ter o formato: (xx)9xxxx-xxxx")]
    public string? Telefone { get; set; }

    //[MinLength(8, ErrorMessage = "O campo 'Senha' deve ter no mínimo 8 caracteres")]
    //[MaxLength(30, ErrorMessage = "O campo 'Senha' deve ter no máximo 30 caracteres")]
    //[Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
    public string? Senha { get; set; }

    [Compare("Senha", ErrorMessage = "As senhas devem ser iguais")]
    public string? ConfirmaSenha { get; set; }

    [NotMapped]
    [JsonIgnore]
    public string? Cargo { get; set; } = "ALUNO";
}
