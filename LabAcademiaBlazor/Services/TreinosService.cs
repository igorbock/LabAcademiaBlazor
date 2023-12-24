namespace LabAcademiaBlazor.Services;

public class TreinosService : ITreinosService
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public NavigationManager? C_NavigationManager { get; private set; }
    public ILocalStorageService? C_Storage { get; private set; }

    public TreinosService(
        IHttpClientFactory? p_HttpClientFactory, 
        NavigationManager? p_NavigationManager,
        ILocalStorageService? p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_NavigationManager = p_NavigationManager;
        C_Storage = p_Storage;

    }

    public async Task<IEnumerable<TreinoDTO>> CM_ObterTreinos(int? p_Codigo)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_RespostaHttp = await m_HttpClient!.GetAsync($"api/treinos?p_Codigo={p_Codigo}");

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<TreinoDTO>>();
        return m_Retorno ?? Enumerable.Empty<TreinoDTO>();
    }

    public async Task CM_RemoverTreinoAsync(int p_Codigo)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_Endereco = $"api/treinos?p_Codigo={p_Codigo}";
        var m_RespostaHttp = await m_HttpClient!.DeleteAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");
    }

    public async Task CM_AdicionarNovoTreinoAsync(TreinoDTO p_Treino)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_TreinoJSON = JsonSerializer.Serialize(p_Treino);
        var m_Endereco = $"api/treinos";
        var m_Content = new StringContent(m_TreinoJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await m_HttpClient!.PostAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AtualizarTreinoAsync(TreinoDTO p_Treino)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_TreinoJSON = JsonSerializer.Serialize(p_Treino);
        var m_Endereco = $"api/treinos/editar";
        var m_Content = new StringContent(m_TreinoJSON, Encoding.UTF8, "application/json");
        var m_RespostaHttp = await m_HttpClient!.PutAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
