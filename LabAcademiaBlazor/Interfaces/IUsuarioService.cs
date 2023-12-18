namespace LabAcademiaBlazor.Interfaces;

public interface IUsuarioService<TipoUsuario> where TipoUsuario : class
{
    Task<IEnumerable<TipoUsuario>> CM_ObterUsuariosAsync(string p_Nome);
    Task<string> CM_CriarUsuarioAsync(TipoUsuario p_Usuario);
    Task<string> CM_CriarQRCodeAsync(string p_Matricula);
    Task<Tuple<bool, string>> CM_UsuarioPossuiSenhaAsync(string p_Nome);
    Task CM_AtivarOuDesativarUsuarioAsync(string p_Nome);
    Task CM_AlterarTelefoneUsuarioAsync(TipoUsuario p_Usuario);
}
