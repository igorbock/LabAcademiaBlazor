namespace LabAcademiaBlazor.Services;

public class TreinosService : ITreinosService
{
    public HttpClient? C_HttpClient { get; private set; }
    public NavigationManager? C_NavigationManager { get; private set; }

    public TreinosService(HttpClient? p_HttpClient, NavigationManager? p_NavigationManager)
    {
        C_HttpClient = p_HttpClient;
        C_NavigationManager = p_NavigationManager;
    }

    public async Task<IEnumerable<TreinoDTO>> CM_ObterTreinos(int? p_Codigo)
    {
        var m_RespostaHttp = await C_HttpClient!.GetAsync($"http://localhost:5239/api/treinos?p_Codigo={p_Codigo}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<TreinoDTO>>();
        return m_Retorno ?? Enumerable.Empty<TreinoDTO>();
    }

    public async Task CM_RemoverTreinoAsync(int p_Codigo)
    {
        var m_Endereco = $"http://localhost:5239/api/treinos?p_Codigo={p_Codigo}";
        var m_RespostaHttp = await C_HttpClient!.DeleteAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");
    }

    public async Task CM_AdicionarNovoTreinoAsync(TreinoDTO p_Treino)
    {
        var m_TreinoJSON = JsonSerializer.Serialize(p_Treino);
        var m_Endereco = $"http://localhost:5239/api/treinos";
        var m_Content = new StringContent(m_TreinoJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await C_HttpClient!.PostAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AtualizarTreinoAsync(TreinoDTO p_Treino)
    {
        var m_TreinoJSON = JsonSerializer.Serialize(p_Treino);
        var m_Endereco = $"http://localhost:5239/api/treinos/editar";
        var m_Content = new StringContent(m_TreinoJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await C_HttpClient!.PutAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
