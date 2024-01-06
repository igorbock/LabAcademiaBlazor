namespace LabAcademiaBlazor.Components.Pages;

public partial class Historico
{
    [Inject]
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    [Inject]
    public IHistoricoService? C_HistoricoService { get; private set; }
    [Inject]
    public IExercicioService? C_ExercicioService { get; private set; }
    [Inject]
    public ISnackbar? C_Snackbar { get; private set; }

    private AlunoDTO? c_Aluno;
    private DateTime? c_Inicio;
    private DateTime? c_Fim;

    public DateTime? C_Inicio
    {
        get => c_Inicio;
        set => c_Inicio = value;
    }

    public DateTime? C_Fim
    {
        get => c_Fim;
        set => c_Fim = value;
    }

    private ObservableCollection<TreinoDTO>? c_Historico { get; set; }
    private ObservableCollection<ExercicioDTO>? c_Exercicios { get; set; }

    protected override async Task OnInitializedAsync()
    {
        c_Exercicios = await C_ExercicioService!.CM_ObterExerciciosAsync();
    }

    private async Task<IEnumerable<AlunoDTO>> cm_Procurar(string p_Valor)
    {
        using var m_HttpClient = C_HttpClientFactory!.CreateClient("LabAspNetIdentity");

        return await m_HttpClient.GetFromJsonAsync<IEnumerable<AlunoDTO>>($"api/aluno?p_Nome={p_Valor}") ?? Enumerable.Empty<AlunoDTO>();
    }

    private async Task cm_ObterHistoricoAsync(AlunoDTO? p_Aluno, DateTime? p_Inicio, DateTime? p_Fim) => c_Historico = await C_HistoricoService!.CM_ObterHistoricoAsync(p_Aluno!, p_Inicio, p_Fim);

    private async Task cm_GerarRelatorioMensalAsync()
    {
        if (c_Aluno == null)
        {
            C_Snackbar!.Add("Nenhum usuário selecionado!", Severity.Error);
            return;
        }

        var m_DataAtual = DateTime.Today;

        C_Inicio = new DateTime(m_DataAtual.Year, m_DataAtual.Month, 1);
        C_Fim = C_Inicio.Value.AddMonths(1).AddDays(-1);
        await cm_ObterHistoricoAsync(c_Aluno, C_Inicio, C_Fim);
    }

    private async Task cm_PesquisarAsync() => await cm_ObterHistoricoAsync(c_Aluno, C_Inicio, C_Fim);
}
