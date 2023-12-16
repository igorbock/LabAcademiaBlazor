namespace LabAcademiaBlazor.Interfaces;

public interface IUsuarioTreinoService
{
    Task<IEnumerable<UsuarioTreinoDTO>> CM_ObterRelacoes();
    Task CM_RelacionarUsuarioTreinoAsync(int p_Matricula, int p_Treino);
    Task CM_RemoverRelacaoUsuarioTreinoAsync(int p_Treino);
}
