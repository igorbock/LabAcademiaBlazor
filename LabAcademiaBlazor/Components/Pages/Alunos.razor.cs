namespace LabAcademiaBlazor.Components.Pages;

public partial class Alunos : ComponentBase
{
    public IEnumerable<AlunoDTO>? C_Alunos { get; set; }

    protected override async Task OnInitializedAsync()
    {
        C_Alunos = await C_UsuariosService.CM_ObterAlunosAsync(string.Empty);
    }
}
