namespace LabAcademiaBlazor.Services;

public class UsuariosService : IUsuariosService
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public ILocalStorageService C_Storage { get; private set; }
    private NavigationManager C_NavigationManager;

    public UsuariosService(
        IHttpClientFactory? p_HttpClientFactory,
        NavigationManager p_NavigationManager,
        ILocalStorageService p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_NavigationManager = p_NavigationManager;
        C_Storage = p_Storage;
    }

    public async Task<IEnumerable<UsuarioDTO>> CM_ExibirUsuarios(string? p_Nome, bool p_SomenteAlunos = false)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);

        HttpResponseMessage? m_RespostaHttp;
        if (p_SomenteAlunos)
            m_RespostaHttp = await m_HttpClient.GetAsync($"api/usuario/alunos?p_Nome={p_Nome}");
        else
            m_RespostaHttp = await m_HttpClient.GetAsync($"api/usuario/professores?p_Nome{p_Nome}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<UsuarioDTO>>();

        return m_Retorno ?? Enumerable.Empty<UsuarioDTO>();
    }

    public async Task<IEnumerable<AlunoDTO>> CM_ObterAlunosAsync(string? p_Nome)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_RespostaHttp = await m_HttpClient.GetAsync($"api/usuario/alunos?p_Nome={p_Nome}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<AlunoDTO>>();
        return m_Retorno ?? Enumerable.Empty<AlunoDTO>();
    }

    public async Task<IEnumerable<IdentityUserClaim<string>>> CM_ObterClaims()
    {
        using var m_HttpClient = C_HttpClientFactory!.CreateClient("LabAspNetIdentity");
        var m_RespostaHttp = await m_HttpClient.GetAsync("api/claim");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<IdentityUserClaim<string>>>();
        return m_Retorno ?? Enumerable.Empty<IdentityUserClaim<string>>();
    }

    public async Task<IEnumerable<RoleDTO>> CM_ObterRoles()
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_RespostaHttp = await m_HttpClient.GetAsync("api/role");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<RoleDTO>>();
        return m_Retorno ?? Enumerable.Empty<RoleDTO>();
    }
}
