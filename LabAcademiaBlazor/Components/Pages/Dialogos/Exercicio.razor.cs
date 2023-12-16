namespace LabAcademiaBlazor.Components.Pages.Dialogos;

public partial class Exercicio
{
    [Inject]
    public IExercicioService? C_ExercicioService { get; set; }
    [Inject]
    public IDialogService? C_DialogoService { get; set; }

    [CascadingParameter]
    public MudDialogInstance? MudDialog { get; set; }

    [Parameter]
    public ExercicioDTO? C_ExercicioDTO { get; set; }
    [Parameter]
    public int C_CodigoTreino { get; set; }

    public ExercicioDTO? C_NovoExercicio { get; set; }
    public bool C_Editando { get; private set; }


    protected override Task OnParametersSetAsync()
    {
        if (C_ExercicioDTO == null)
        {
            C_NovoExercicio = new ExercicioDTO();
            C_NovoExercicio.CodigoTreino = C_CodigoTreino; 
        }
        else
            C_Editando = true;

        return base.OnParametersSetAsync();
    }

    protected async Task cm_Excluir()
    {
        var m_Resultado = await C_DialogoService!.ShowMessageBox("Confirma", "Confirmar remoção?", cancelText: "Cancelar");
        if (m_Resultado == false)
            return;

        await C_ExercicioService!.CM_RemoverExercicioAsync(C_ExercicioDTO!.Id);
        MudDialog!.Close(DialogResult.Ok(true));
        StateHasChanged();
    }

    protected async Task cm_Salvar()
    {
        var m_Resultado = await C_DialogoService!.ShowMessageBox("Confirma", "Confirmar alteração?", cancelText: "Cancelar");
        if (m_Resultado == false)
            return;

        if (C_Editando)
            await C_ExercicioService!.CM_AtualizarExercicioAsync(C_ExercicioDTO!);
        else
            await C_ExercicioService!.CM_AdicionarNovoExercicioAsync(C_NovoExercicio!);

        MudDialog!.Close(DialogResult.Ok(true));
        StateHasChanged();
    }
}
