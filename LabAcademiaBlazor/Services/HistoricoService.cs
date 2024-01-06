namespace LabAcademiaBlazor.Services;

public class HistoricoService : IHistoricoService
{
    public HttpClient? C_HttpClient { get; set; }
    public IHttpClientFactory C_HttpClientFactory { get; set; }
    public ILocalStorageService C_Storage { get; private set; }

    public HistoricoService(IHttpClientFactory p_HttpClientFactory, ILocalStorageService p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_Storage = p_Storage;
    }

    ~HistoricoService()
    {
        C_HttpClient?.Dispose();
    }

    public async Task<ObservableCollection<TreinoDTO>> CM_ObterHistoricoAsync(AlunoDTO p_Aluno, DateTime? p_Inicio, DateTime? p_Fim)
    {
        if (p_Aluno == null)
            return new ObservableCollection<TreinoDTO>();

        C_HttpClient = await C_HttpClientFactory.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage);
        var m_Entidade = new
        {
            Inicio = p_Inicio,
            Fim = p_Fim,
            Matricula = int.Parse(p_Aluno.Matricula!)
        };
        var m_JSON = JsonSerializer.Serialize(m_Entidade);
        var m_StringContent = new StringContent(m_JSON, Encoding.UTF8, "application/json");
        var m_Resultado = await C_HttpClient!.PostAsync("api/historicos", m_StringContent);
        m_Resultado.EnsureSuccessStatusCode();
        var m_Resposta = await m_Resultado.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ObservableCollection<TreinoDTO>>(m_Resposta) ?? new ObservableCollection<TreinoDTO>();
    }
}
