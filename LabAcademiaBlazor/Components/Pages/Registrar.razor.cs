namespace LabAcademiaBlazor.Components.Pages;

public partial class Registrar
{
    [Inject]
    public IAuthService? C_AuthService { get; set; }
    [Inject]
    public IUsuarioService<RegistrarUsuarioDTO>? C_ProfessorService { get; set; }
    [Inject]
    public IUsuarioService<AlunoDTO>? C_AlunoService { get; set; }
    [Inject]
    public IDialogService? C_DialogService { get; set; }
    [Inject]
    public NavigationManager? C_NavigationManager { get; set; }
    [Inject]
    public ISnackbar? C_Snackbar { get; set; }

    public RegistrarUsuarioDTO C_Usuario { get; set; } = new RegistrarUsuarioDTO();
    private bool C_MostrarErros { get; set; }
    private IEnumerable<string> C_Erros { get; set; } = new List<string>();

    public string? C_QRCode { get; private set; }

    private async Task cm_RegistrarAluno()
    {
        C_MostrarErros = false;

        try
        {
            var m_AlunoDTO = new AlunoDTO
            {
                Nome = C_Usuario.Nome,
                Telefone = C_Usuario.Telefone,
                Email = C_Usuario.Email
            };
            var m_Matricula = await C_AlunoService!.CM_CriarUsuarioAsync(m_AlunoDTO);

            C_QRCode = await C_AlunoService.CM_CriarQRCodeAsync(m_Matricula);

            var m_Opcoes = new DialogOptions { CloseOnEscapeKey = false };
            var m_Parametros = new DialogParameters<Registro>();
            m_Parametros.Add("C_QRCode", C_QRCode);
            var m_Dialogo = C_DialogService!.Show<Registro>("QRCode", m_Parametros, m_Opcoes);
            await m_Dialogo.Result;

            C_NavigationManager!.NavigateTo("/");
        }
        catch (Exception ex)
        {
            C_MostrarErros = true;
            C_Erros = new[] { ex.Message };
        }
    }

    public MudButton? C_BtnRegistrarProfessor { get; set; }

    private async Task cm_RegistrarProfessor()
    {
        C_MostrarErros = false;

        try
        {
            var m_ProfessorDTO = new RegistrarUsuarioDTO
            {
                Nome = C_Usuario.Nome,
                Telefone = C_Usuario.Telefone,
                Email = C_Usuario.Email,
                Senha = C_Usuario.Senha,
                ConfirmaSenha = C_Usuario.ConfirmaSenha
            };
            var m_Resultado = await C_ProfessorService!.CM_CriarUsuarioAsync(m_ProfessorDTO);
            if (m_Resultado.Contains("Failed"))
                throw new Exception(m_Resultado);

            C_Snackbar!.Add("Professor registrado com sucesso!", Severity.Success);
            C_BtnRegistrarProfessor.Disabled = true;
        }
        catch (Exception ex)
        {
            C_MostrarErros = true;
            C_Erros = new[] { ex.Message };
        }
    }

    public PatternMask C_MascaraTelefone = new PatternMask("(##)#####-####")
    {
        MaskChars = [new MaskChar('#', @"[0-9]")],
        Placeholder = '_',
        CleanDelimiters = true
    };
}
