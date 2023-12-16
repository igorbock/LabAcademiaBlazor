namespace LabAcademiaBlazor.Interfaces;

public interface ITreinosService
{
    Task<IEnumerable<TreinoDTO>> CM_ObterTreinos(int? p_Codigo);
    Task CM_RemoverTreinoAsync(int p_Codigo);
    Task CM_AdicionarNovoTreinoAsync(TreinoDTO p_Treino);
    Task CM_AtualizarTreinoAsync(TreinoDTO p_Treino);
}
