namespace LabAcademiaBlazor.Services;

public class ExercicioService : IExercicioService
{
    public HttpClient? C_HttpClient { get; private set; }
    public NavigationManager? C_NavigationManager { get; set; }

    public ExercicioService(HttpClient? p_HttpClient, NavigationManager? p_NavigationManager)
    {
        C_HttpClient = p_HttpClient;
        C_NavigationManager = p_NavigationManager;
    }

    public async Task<IEnumerable<ExercicioDTO>> CM_ObterExerciciosAsync()
    {
        var m_Endereco = $"http://localhost:5239/api/exercicios";
        var m_RespostaHttp = await C_HttpClient!.GetAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        var m_Retorno = await m_RespostaHttp.Content.ReadFromJsonAsync<IEnumerable<ExercicioDTO>>();
        return m_Retorno ?? Enumerable.Empty<ExercicioDTO>();
    }

    public async Task CM_RemoverExercicioAsync(int p_Codigo)
    {
        var m_Endereco = $"http://localhost:5239/api/exercicios?p_Codigo={p_Codigo}";
        var m_RespostaHttp = await C_HttpClient!.DeleteAsync(m_Endereco);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AdicionarNovoExercicioAsync(ExercicioDTO p_Exercicio)
    {
        var m_ExercicioJSON = JsonSerializer.Serialize(p_Exercicio);
        var m_Content = new StringContent(m_ExercicioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"http://localhost:5239/api/exercicios";
        var m_RespostaHttp = await C_HttpClient!.PostAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AtualizarExercicioAsync(ExercicioDTO p_Exercicio)
    {
        var m_ExercicioJSON = JsonSerializer.Serialize(p_Exercicio);
        var m_Content = new StringContent(m_ExercicioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"http://localhost:5239/api/exercicios";
        var m_RespostaHttp = await C_HttpClient!.PutAsync(m_Endereco, m_Content);

        if (m_RespostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            C_NavigationManager!.NavigateTo("naoautorizado");

        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
