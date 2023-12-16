namespace LabAcademiaBlazor;

public class LabAuthenticationStateProvider :  AuthenticationStateProvider
{
    private readonly HttpClient? c_HttpClient;
    private readonly ProtectedSessionStorage c_ProtectedSessionStorage;
    private readonly ISystemStringHelper? c_SystemStringHelper;

    public LabAuthenticationStateProvider(
        HttpClient p_HttpClient, 
        ISystemStringHelper p_SystemStringHelper,
        ProtectedSessionStorage p_ProtectedSessionStorage)
    {
        c_HttpClient = p_HttpClient;
        c_SystemStringHelper = p_SystemStringHelper;
        c_ProtectedSessionStorage = p_ProtectedSessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var m_TokenArmazenado = string.Empty;
        try
        {
            var m_ResultadoProtegido = await c_ProtectedSessionStorage.GetAsync<string>("Token");
            m_TokenArmazenado = m_ResultadoProtegido.Value;
        }
        catch (Exception)
        {
        }

        if (string.IsNullOrWhiteSpace(m_TokenArmazenado))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        c_HttpClient!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_TokenArmazenado);

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
