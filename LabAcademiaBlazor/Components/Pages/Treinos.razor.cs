namespace LabAcademiaBlazor.Components.Pages;

public partial class Treinos : ComponentBase
{
    public IEnumerable<TreinoDTO>? C_Treinos { get; set; }
    public IEnumerable<ExercicioDTO>? C_Exercicios { get; set; }

    protected override async Task OnInitializedAsync()
    {
        C_Treinos = await C_TreinosService.CM_ObterTreinos(null);
        C_Exercicios = await C_ExercicioService.CM_ObterExerciciosAsync();
    }

    protected async Task cm_TreinoSelecionado(DataGridRowClickEventArgs<TreinoDTO> p_Evento)
    {
        var m_Parametros = new DialogParameters<Treino>
        {
            { "C_TreinoDTO", p_Evento.Item },
        };
        var m_Resultado = await C_DialogService.ShowAsync<Treino>(p_Evento.Item.Nome, m_Parametros);
        if (await m_Resultado.Result == DialogResult.Ok<bool>(true))
            await OnInitializedAsync();
    }

    protected async Task cm_ExercicioSelecionado(TableRowClickEventArgs<ExercicioDTO> p_TableRowClickEventArgs)
    {
        var m_Parametros = new DialogParameters<Exercicio>
        {
            { "C_ExercicioDTO", p_TableRowClickEventArgs.Item },
        };
        var m_Resultado = await C_DialogService.ShowAsync<Exercicio>(p_TableRowClickEventArgs.Item.Descricao, m_Parametros);
        if (await m_Resultado.Result == DialogResult.Ok<bool>(true))
            await OnInitializedAsync();
    }

    protected void cm_AdicionarTreino()
    {
        var m_Parametros = new DialogParameters<Treino>();
        C_DialogService.Show<Treino>("Novo Treino", m_Parametros);
    }

    protected void cm_AdicionarExercicio(TreinoDTO p_Treino)
    {
        var m_Parametros = new DialogParameters<Exercicio>
        {
            { "C_CodigoTreino", p_Treino.Id }
        };
        C_DialogService.Show<Exercicio>("Novo Exercício", m_Parametros);
    }
}
