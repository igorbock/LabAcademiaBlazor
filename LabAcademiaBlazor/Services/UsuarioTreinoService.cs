namespace LabAcademiaBlazor.Services;

public class UsuarioTreinoService : IUsuarioTreinoService
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public NavigationManager? C_NavigationManager { get; set; }
    public ILocalStorageService? C_Storage { get; set; }

    public UsuarioTreinoService(
        IHttpClientFactory? p_HttpClientFactory, 
        NavigationManager? p_NavigationManager,
        ILocalStorageService? p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_NavigationManager = p_NavigationManager;
        C_Storage = p_Storage;
    }

    public async Task<IEnumerable<UsuarioTreinoDTO>> CM_ObterRelacoes()
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_RespostaHttp = await m_HttpClient!.GetAsync("api/usuariostreinos");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<UsuarioTreinoDTO>>();
        return m_Retorno ?? Enumerable.Empty<UsuarioTreinoDTO>();
    }

    public async Task CM_RelacionarUsuarioTreinoAsync(int p_Matricula, int p_Treino)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);

        var m_RequestDTO = new
        {
            CodigoUsuario = p_Matricula.ToString(),
            CodigoTreino = p_Treino
        };
        var m_RequestJSON = JsonSerializer.Serialize(m_RequestDTO);
        var m_StringContent = new StringContent(m_RequestJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await m_HttpClient!.PostAsync("api/usuariostreinos", m_StringContent);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_RemoverRelacaoUsuarioTreinoAsync(int p_Treino)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_RespostaHttp = await m_HttpClient!.DeleteAsync($"api/usuariostreinos?p_CodigoTreino={p_Treino}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
