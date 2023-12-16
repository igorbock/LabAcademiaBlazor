namespace LabAcademiaBlazor.Components.Pages.Dialogos;

public partial class UsuarioTreino
{
    [CascadingParameter]
    public MudDialogInstance? MudDialog { get; set; }

    [Inject]
    public IUsuarioTreinoService? C_UsuarioTreinoService { get; set; }
    [Inject]
    public ITreinosService? C_TreinoService { get; set; }

    [Parameter]
    public int? C_Matricula { get; set; }

    public UsuarioTreinoDTO? C_UsuarioTreinoDTO { get; set; }
    public IEnumerable<TreinoDTO>? C_Treinos { get; set; } = new List<TreinoDTO>();

    protected override async Task OnInitializedAsync()
    {
        C_UsuarioTreinoDTO = new UsuarioTreinoDTO
        {
            CodigoUsuario = C_Matricula.GetValueOrDefault()
        };
        C_Treinos = await C_TreinoService!.CM_ObterTreinos(null);
    }

    protected async Task cm_Salvar()
    {
        await C_UsuarioTreinoService!.CM_RelacionarUsuarioTreinoAsync(C_UsuarioTreinoDTO!.CodigoUsuario, C_UsuarioTreinoDTO.CodigoTreino);
        MudDialog!.Close(DialogResult.Ok(true));
        StateHasChanged();
    }
}
