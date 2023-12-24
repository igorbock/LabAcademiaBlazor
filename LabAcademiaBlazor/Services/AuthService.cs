namespace LabAcademiaBlazor.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory C_HttpClientFactory;
    private readonly AuthenticationStateProvider C_AuthenticationStateProvider;
    private readonly ILocalStorageService c_Storage;
    private readonly IConfiguration c_Configuration;

    public AuthService(IHttpClientFactory p_HttpClientFactory,
        AuthenticationStateProvider p_AuthenticationStateProvider,
        ILocalStorageService p_Storage,
        IConfiguration p_Configuration)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_AuthenticationStateProvider = p_AuthenticationStateProvider;
        c_Storage = p_Storage;
        c_Configuration = p_Configuration;
    }

    public async Task CM_Login(LoginDTO p_Login)
    {
        using var m_HttpClient = C_HttpClientFactory!.CreateClient("LabAspNetIdentity");
        var m_LoginJSON = JsonSerializer.Serialize(p_Login);
        var m_Content = new StringContent(m_LoginJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await m_HttpClient.PostAsync("api/Token", m_Content);
        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized || m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.BadRequest)
            throw new UnauthorizedAccessException();
        m_RespostaHttp.EnsureSuccessStatusCode();

        var m_Token = await m_RespostaHttp.Content.ReadAsStringAsync();
        await c_Storage.SetItemAsync("Token", m_Token);

        ((LabAuthenticationStateProvider)C_AuthenticationStateProvider).CM_NotificarUsuarioAutenticado(m_Token);

        c_Configuration.GetSection("Token").Value = m_Token;
        //m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_Token);
    }

    public async Task CM_Logout()
    {
        await c_Storage.RemoveItemAsync("Token");
        ((LabAuthenticationStateProvider)C_AuthenticationStateProvider).CM_NotificarSaidaUsuario();
        c_Configuration.GetSection("Token").Value = string.Empty;
        //m_HttpClient.DefaultRequestHeaders.Authorization = null;
    }
}
