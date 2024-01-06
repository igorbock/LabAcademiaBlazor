namespace LabAcademiaBlazor.Models;

public record AlunoDTO
{
    public string? Nome { get; init; }
    public string? Email { get; init; }
    public string? Telefone { get; init; }
    public string? Matricula { get; init; }
}