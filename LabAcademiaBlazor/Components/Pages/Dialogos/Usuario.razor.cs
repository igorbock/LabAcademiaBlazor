namespace LabAcademiaBlazor.Components.Pages.Dialogos;

public partial class Usuario
{
    [Inject]
    public IDialogService? C_DialogService { get; set; }
    [Inject]
    public IUsuarioService<AlunoDTO>? C_AlunoService { get; set; }

    [CascadingParameter]
    MudDialogInstance? MudDialog { get; set; }

    [Parameter]
    public UsuarioDTO? C_UsuarioDTO { get; set; }

    public PatternMask C_MascaraTelefone = new PatternMask("(##)#####-####")
    {
        MaskChars = [new MaskChar('#', @"[0-9]")],
        Placeholder = '_',
        CleanDelimiters = true
    };

    void cm_Cancelar() => MudDialog!.Cancel();
    void cm_Salvar()
    {
        C_AlunoService!.CM_AlterarTelefoneUsuarioAsync(new AlunoDTO { Email = C_UsuarioDTO!.Email, Nome = C_UsuarioDTO.Nome, Telefone = C_UsuarioDTO.Telefone });
        MudDialog!.Close(DialogResult.Ok(true));
    }
    protected async Task cm_Desativar(string p_Nome)
    {
        var m_Resultado = await C_DialogService!.ShowMessageBox("Desativar aluno?", "O aluno perde o acesso completo", yesText: "Sim", noText: "Não");
        if(m_Resultado == false)
            return;

        await C_AlunoService!.CM_AtivarOuDesativarUsuarioAsync(p_Nome);
        MudDialog!.Close(DialogResult.Ok(true));
    }
}
