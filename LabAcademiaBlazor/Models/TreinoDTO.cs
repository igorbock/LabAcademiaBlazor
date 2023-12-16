namespace LabAcademiaBlazor.Models;

public class TreinoDTO
{
    public int Id { get; set; }
    public char? Codigo { get; set; }
    public string? Nome { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Inicio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Fim { get; set; }
    public bool Ativo { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [NotMapped]
    public int? Matricula { get; set; }
}
