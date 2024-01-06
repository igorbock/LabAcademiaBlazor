namespace LabAcademiaBlazor.Components.Pages;

public partial class Usuarios
{
    [Inject]
    public IUsuariosService? C_UsuariosService { get; set; }
    [Inject]
    public IUsuarioService<AlunoDTO>? C_AlunoService { get; set; }
    [Inject]
    public ITreinosService? C_TreinoService { get; set; }
    [Inject]
    public IUsuarioTreinoService? C_UsuarioTreinoService { get; set; }
    [Inject]
    public IDialogService? C_DialogService { get; set; }
    [Inject]
    public ISnackbar? C_Snackbar { get; set; }
    [Inject]
    public NavigationManager? C_NavigationManager { get; set; }

    [Parameter]
    public string? Mostrar { get; set; }

    public IEnumerable<UsuarioDTO>? C_Usuarios { get; private set; }
    public IEnumerable<TreinoDTO>? C_Treinos { get; private set; }
    public IEnumerable<UsuarioTreinoDTO>? C_UsuariosTreinos { get; private set; }
    public IEnumerable<TreinoDTO>? C_TreinosSelecionados { get; set; }
    public string? C_QRCode { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Mostrar) == false && (Mostrar.Equals("PROFESSORES") || Mostrar.Equals("professores")))
            C_Usuarios = await C_UsuariosService!.CM_ExibirUsuarios(string.Empty);
        else
        {
            C_UsuariosTreinos = await C_UsuarioTreinoService!.CM_ObterRelacoes();
            C_Usuarios = await C_UsuariosService!.CM_ExibirUsuarios(string.Empty, p_SomenteAlunos: true);
            C_Treinos = await C_TreinoService!.CM_ObterTreinos(null);

            C_TreinosSelecionados = from usuarios_treinos in C_UsuariosTreinos
                                    join usuarios in C_Usuarios on usuarios_treinos.CodigoUsuario equals usuarios.Matricula
                                    join treinos in C_Treinos on usuarios_treinos.CodigoTreino equals treinos.Id
                                    select new TreinoDTO
                                    {
                                        Id = treinos.Id,
                                        Codigo = treinos.Codigo,
                                        Nome = treinos.Nome,
                                        Matricula = usuarios.Matricula
                                    };
        }
    }

    protected async Task cm_UsuarioSelecionado(DataGridRowClickEventArgs<UsuarioDTO> p_Evento)
    {
        var m_UsuarioPossuiSenha = await C_AlunoService!.CM_UsuarioPossuiSenhaAsync(p_Evento.Item.Nome!);
        if (m_UsuarioPossuiSenha.Item1)
        {
            var m_Parametros = new DialogParameters<Usuario>
            {
                { "C_UsuarioDTO", p_Evento.Item }
            };
            var m_Dialogo = C_DialogService!.Show<Usuario>(p_Evento.Item.Nome, m_Parametros);
            var m_Resultado = await m_Dialogo.Result;
            if (m_Resultado.Canceled == false)
            {
                await OnInitializedAsync();
                C_Snackbar!.Add("Alterado com sucesso!", Severity.Success);
            }
        }
        else
        {
            C_QRCode = await C_AlunoService.CM_CriarQRCodeAsync(m_UsuarioPossuiSenha.Item2);

            var m_Opcoes = new DialogOptions
            {
                MaxWidth = MaxWidth.Small
            };
            var m_Parametros = new DialogParameters<Registro>
            {
                { "C_QRCode", C_QRCode }
            };
            C_DialogService!.Show<Registro>("QRCode", m_Parametros, m_Opcoes);
        }
    }

    protected async Task cm_AdicionarRelacao(int? p_Usuario)
    {
        var m_Parametros = new DialogParameters<UsuarioTreino>
            {
                { "C_Matricula", p_Usuario }
            };

        var m_Dialogo = await C_DialogService!.ShowAsync<UsuarioTreino>("Relacionar usuário/treino", m_Parametros);
        var m_Resultado = await m_Dialogo.Result;
        if (m_Resultado.Canceled == false)
        {
            await OnInitializedAsync();
            C_Snackbar!.Add("Alterado com sucesso!", Severity.Success);
        }
    }

    protected async Task cm_ExcluirTreinoAsync(int p_IdTreino)
    {
        var m_Options = new MessageBoxOptions
        {
            Title = "Confirmar",
            Message = $"Deseja remover a relação com o treino '{p_IdTreino}'?",
            YesText = "Confirmar",
            CancelText = "Cancelar"
        };
        var m_Resultado = await C_DialogService!.ShowMessageBox(m_Options);
        if (m_Resultado.HasValue && m_Resultado.Value)
        {
            await C_UsuarioTreinoService!.CM_RemoverRelacaoUsuarioTreinoAsync(p_IdTreino);
            await OnInitializedAsync();
            C_Snackbar!.Add("Relação removida com sucesso!", Severity.Success);
        }
    }

    protected void cm_AdicionarUsuario() => C_NavigationManager!.NavigateTo("/registrar");
}
