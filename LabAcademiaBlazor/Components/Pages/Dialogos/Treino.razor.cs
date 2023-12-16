namespace LabAcademiaBlazor.Components.Pages.Dialogos;

public partial class Treino
{
    [Inject]
    public IDialogService? C_DialogoService { get; set; }

    [CascadingParameter]
    public MudDialogInstance? MudDialog { get; set; }

    [Parameter]
    public TreinoDTO? C_TreinoDTO { get; set; }

    public TreinoDTO? C_NovoTreino { get; set; }
    public bool C_Editando { get; private set; }

    protected override Task OnParametersSetAsync()
    {
        if (C_TreinoDTO == null)
            C_NovoTreino = new TreinoDTO();
        else
            C_Editando = true;

        return base.OnParametersSetAsync();
    }

    protected async Task cm_Excluir()
    {
        var m_Resultado = await C_DialogoService!.ShowMessageBox("Confirma", "Confirmar remoção?", cancelText: "Cancelar");
        if (m_Resultado == false)
            return;

        await C_TreinoService.CM_RemoverTreinoAsync(C_TreinoDTO!.Id);
        MudDialog!.Close(DialogResult.Ok(true));
        StateHasChanged();
    }

    protected async Task cm_Salvar()
    {
        var m_Resultado = await C_DialogoService!.ShowMessageBox("Confirma", "Confirmar alteração?", cancelText: "Cancelar");
        if (m_Resultado == false)
            return;

        if (C_Editando)
            await C_TreinoService.CM_AtualizarTreinoAsync(C_TreinoDTO!);
        else
            await C_TreinoService.CM_AdicionarNovoTreinoAsync(C_NovoTreino!);

        MudDialog!.Close(DialogResult.Ok(true));
        StateHasChanged();
    }
}
