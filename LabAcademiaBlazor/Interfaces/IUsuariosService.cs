namespace LabAcademiaBlazor.Interfaces;

public interface IUsuariosService
{
    Task<IEnumerable<UsuarioDTO>> CM_ExibirUsuarios(string? p_Nome, bool p_SomenteAlunos = false);
    Task<IEnumerable<IdentityUserClaim<string>>> CM_ObterClaims();
    Task<IEnumerable<RoleDTO>> CM_ObterRoles();
    Task<IEnumerable<AlunoDTO>> CM_ObterAlunosAsync(string? p_Nome);
}
