namespace LabAcademiaBlazor.Models;

public class UsuarioDTO
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Cargo { get; set; }
    public bool? Ativo { get; set; }
    public int? Matricula { get; set; }
}
