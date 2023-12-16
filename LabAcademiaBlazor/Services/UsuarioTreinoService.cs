namespace LabAcademiaBlazor.Services;

public class UsuarioTreinoService : IUsuarioTreinoService
{
    public HttpClient? C_HttpClient { get; set; }
    public NavigationManager? C_NavigationManager { get; set; }

    public UsuarioTreinoService(HttpClient? p_HttpClient, NavigationManager? p_NavigationManager)
    {
        C_HttpClient = p_HttpClient;
        C_NavigationManager = p_NavigationManager;
    }

    public async Task<IEnumerable<UsuarioTreinoDTO>> CM_ObterRelacoes()
    {
        var m_Endereco = "http://localhost:5239/api/usuariostreinos";
        var m_RespostaHttp = await C_HttpClient!.GetAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<UsuarioTreinoDTO>>();
        return m_Retorno ?? Enumerable.Empty<UsuarioTreinoDTO>();
    }

    public async Task CM_RelacionarUsuarioTreinoAsync(int p_Matricula, int p_Treino)
    {
        var m_RequestDTO = new
        {
            CodigoUsuario = p_Matricula.ToString(),
            CodigoTreino = p_Treino
        };
        var m_RequestJSON = JsonSerializer.Serialize(m_RequestDTO);
        var m_StringContent = new StringContent(m_RequestJSON, Encoding.UTF8, "application/json");
        var m_Endereco = "http://localhost:5239/api/usuariostreinos";
        var m_RespostaHttp = await C_HttpClient!.PostAsync(m_Endereco, m_StringContent);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_RemoverRelacaoUsuarioTreinoAsync(int p_Treino)
    {
        var m_Endereco = $"http://localhost:5239/api/usuariostreinos?p_CodigoTreino={p_Treino}";
        var m_RespostaHttp = await C_HttpClient!.DeleteAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
