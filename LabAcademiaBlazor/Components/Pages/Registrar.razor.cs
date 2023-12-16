namespace LabAcademiaBlazor.Components.Pages;

public partial class Registrar
{
    [Inject]
    public IAuthService? C_AuthService { get; set; }
    [Inject]
    public IUsuariosService? C_UsuarioService { get; set; }
    [Inject]
    public IUsuarioService<AlunoDTO>? C_AlunoService { get; set; }
    [Inject]
    public IDialogService? C_DialogService { get; set; }
    [Inject]
    public NavigationManager? C_NavigationManager { get; set; }

    public RegistrarUsuarioDTO C_Usuario { get; set; } = new RegistrarUsuarioDTO();
    private bool C_MostrarErros { get; set; }
    private IEnumerable<string> C_Erros { get; set; } = new List<string>();

    public string? C_QRCode { get; private set; }

    private async Task cm_Registrar()
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
            C_DialogService!.Show<Registro>("QRCode", m_Parametros, m_Opcoes);

            C_NavigationManager!.NavigateTo("/usuarios/ALUNOS");
        }
        catch (Exception ex)
        {
            C_MostrarErros = true;
            C_Erros = new[] { ex.Message };
        }
    }
}
