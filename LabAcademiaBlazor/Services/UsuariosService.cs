namespace LabAcademiaBlazor.Services;

public class UsuariosService : IUsuariosService
{
    private HttpClient C_HttpClient;
    private NavigationManager C_NavigationManager;

    public UsuariosService(HttpClient p_HttpClient, NavigationManager p_NavigationManager)
    {
        C_HttpClient = p_HttpClient;
        C_NavigationManager = p_NavigationManager;
    }

    public async Task<IEnumerable<UsuarioDTO>> CM_ExibirUsuarios(string? p_Nome, bool p_SomenteAlunos = false)
    {
        HttpResponseMessage? m_RespostaHttp;
        if (p_SomenteAlunos)
            m_RespostaHttp = await C_HttpClient.GetAsync($"https://localhost:7121/api/usuario/alunos?p_Nome={p_Nome}");
        else
            m_RespostaHttp = await C_HttpClient.GetAsync($"https://localhost:7121/api/usuario/professores?p_Nome{p_Nome}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<UsuarioDTO>>();

        return m_Retorno ?? Enumerable.Empty<UsuarioDTO>();
    }

    public async Task<IEnumerable<AlunoDTO>> CM_ObterAlunosAsync(string? p_Nome)
    {
        var m_RespostaHttp = await C_HttpClient.GetAsync($"https://localhost:7121/api/usuario/alunos?p_Nome={p_Nome}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<AlunoDTO>>();
        return m_Retorno ?? Enumerable.Empty<AlunoDTO>();
    }

    public async Task<IEnumerable<IdentityUserClaim<string>>> CM_ObterClaims()
    {
        var m_RespostaHttp = await C_HttpClient.GetAsync("https://localhost:7121/api/claim");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<IdentityUserClaim<string>>>();
        return m_Retorno ?? Enumerable.Empty<IdentityUserClaim<string>>();
    }

    public async Task<IEnumerable<RoleDTO>> CM_ObterRoles()
    {
        var m_RespostaHttp = await C_HttpClient.GetAsync("https://localhost:7121/api/role");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<RoleDTO>>();
        return m_Retorno ?? Enumerable.Empty<RoleDTO>();
    }
}
