namespace LabAcademiaBlazor.Services;

public class ExercicioService : IExercicioService
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public NavigationManager? C_NavigationManager { get; set; }
    public ILocalStorageService? C_Storage { get; private set; }

    public ExercicioService(
        IHttpClientFactory? p_HttpClientFactory, 
        NavigationManager? p_NavigationManager,
        ILocalStorageService? p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_NavigationManager = p_NavigationManager;
        C_Storage = p_Storage;
    }

    public async Task<IEnumerable<ExercicioDTO>> CM_ObterExerciciosAsync()
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_Endereco = $"api/exercicios";
        var m_RespostaHttp = await m_HttpClient!.GetAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<ExercicioDTO>>();
        return m_Retorno ?? Enumerable.Empty<ExercicioDTO>();
    }

    public async Task CM_RemoverExercicioAsync(int p_Codigo)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_Endereco = $"api/exercicios?p_Codigo={p_Codigo}";
        var m_RespostaHttp = await m_HttpClient!.DeleteAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AdicionarNovoExercicioAsync(ExercicioDTO p_Exercicio)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_ExercicioJSON = JsonSerializer.Serialize(p_Exercicio);
        var m_Content = new StringContent(m_ExercicioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"api/exercicios";
        var m_RespostaHttp = await m_HttpClient!.PostAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AtualizarExercicioAsync(ExercicioDTO p_Exercicio)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAcademiaAPI", C_Storage!);
        var m_ExercicioJSON = JsonSerializer.Serialize(p_Exercicio);
        var m_Content = new StringContent(m_ExercicioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"api/exercicios";
        var m_RespostaHttp = await m_HttpClient!.PutAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
