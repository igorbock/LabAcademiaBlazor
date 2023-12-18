namespace LabAcademiaBlazor.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient C_HttpClient;
    private readonly AuthenticationStateProvider C_AuthenticationStateProvider;
    private readonly ProtectedSessionStorage c_ProtectedSessionStorage;

    public AuthService(HttpClient p_HttpClient,
        AuthenticationStateProvider p_AuthenticationStateProvider,
        ProtectedSessionStorage p_ProtectedSessionStorage)
    {
        C_HttpClient = p_HttpClient;
        C_AuthenticationStateProvider = p_AuthenticationStateProvider;
        c_ProtectedSessionStorage = p_ProtectedSessionStorage;
    }

    public async Task CM_Login(LoginDTO p_Login)
    {
        var m_LoginJSON = JsonSerializer.Serialize(p_Login);
        var m_Content = new StringContent(m_LoginJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await C_HttpClient.PostAsync("https://localhost:7121/api/Token", m_Content);
        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized || m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.BadRequest)
            throw new UnauthorizedAccessException();
        m_RespostaHttp.EnsureSuccessStatusCode();

        var m_Token = await m_RespostaHttp.Content.ReadAsStringAsync();
        await c_ProtectedSessionStorage.SetAsync("Token", m_Token);

        ((LabAuthenticationStateProvider)C_AuthenticationStateProvider).CM_NotificarUsuarioAutenticado(m_Token);

        C_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_Token);
    }

    public async Task CM_Logout()
    {
        await c_ProtectedSessionStorage.DeleteAsync("Token");
        ((LabAuthenticationStateProvider)C_AuthenticationStateProvider).CM_NotificarSaidaUsuario();
        C_HttpClient.DefaultRequestHeaders.Authorization = null;
    }
}
