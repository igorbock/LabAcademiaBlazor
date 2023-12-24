namespace LabAcademiaBlazor;

public class LabAuthenticationStateProvider :  AuthenticationStateProvider
{
    private readonly IHttpClientFactory? c_HttpClientFactory;
    private readonly ILocalStorageService c_Storage;
    private readonly ISystemStringHelper? c_SystemStringHelper;

    public LabAuthenticationStateProvider(
        IHttpClientFactory p_HttpClientFactory, 
        ISystemStringHelper p_SystemStringHelper,
        ILocalStorageService p_ProtectedSessionStorage)
    {
        c_HttpClientFactory = p_HttpClientFactory;
        c_SystemStringHelper = p_SystemStringHelper;
        c_Storage = p_ProtectedSessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var m_TokenArmazenado = string.Empty;
        try
        {
            var m_ResultadoProtegido = await c_Storage.GetItemAsync<string>("Token");
            m_TokenArmazenado = m_ResultadoProtegido;
        }
        catch (Exception)
        {
        }

        if (string.IsNullOrWhiteSpace(m_TokenArmazenado))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var m_Token = new JwtSecurityToken(m_TokenArmazenado);
        var m_TokenInvalido = m_Token.CMX_ValidarToken() == false;
        if(m_TokenInvalido)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        using var m_HttpClient = c_HttpClientFactory!.CreateClient("LabAspNetIdentity");
        m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_TokenArmazenado);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(c_SystemStringHelper!.CM_TransformarTokenEmClaims(m_TokenArmazenado), "jwt")));
    }

    public void CM_NotificarSaidaUsuario()
    {
        var m_UsuarioAnonimo = new ClaimsPrincipal(new ClaimsIdentity());
        var m_EstadoAutenticacao = Task.FromResult(new AuthenticationState(m_UsuarioAnonimo));
        NotifyAuthenticationStateChanged(m_EstadoAutenticacao);
    }

    public void CM_NotificarUsuarioAutenticado(string p_Token)
    {
        var m_Claims = c_SystemStringHelper!.CM_TransformarTokenEmClaims(p_Token);
        var m_UsuarioAutenticado = new ClaimsPrincipal(new ClaimsIdentity(m_Claims, "LabAcademia"));

        var m_EstadoAutenticacao = Task.FromResult(new AuthenticationState(m_UsuarioAutenticado));
        NotifyAuthenticationStateChanged(m_EstadoAutenticacao);
    }
}
