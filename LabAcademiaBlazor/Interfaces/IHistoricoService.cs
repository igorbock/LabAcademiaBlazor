namespace LabAcademiaBlazor.Interfaces;

public interface IHistoricoService
{
    Task<ObservableCollection<TreinoDTO>> CM_ObterHistoricoAsync(AlunoDTO p_Aluno, DateTime? p_Inicio, DateTime? p_Fim);
}
