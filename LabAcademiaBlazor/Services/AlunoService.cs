namespace LabAcademiaBlazor.Services;

public class AlunoService : IUsuarioService<AlunoDTO>
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public ILocalStorageService C_Storage { get; private set; }

    public AlunoService(IHttpClientFactory? p_HttpClientFactory, ILocalStorageService p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_Storage = p_Storage;
    }

    public async Task<string> CM_CriarQRCodeAsync(string p_Matricula)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_Endereco = $"api/aluno/qrcode?p_Matricula={p_Matricula}";
        var m_RespostaHttp = await m_HttpClient!.GetAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public async Task<string> CM_CriarUsuarioAsync(AlunoDTO p_Usuario)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_UsuarioJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_UsuarioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = "api/aluno";
        var m_RespostaHttp = await m_HttpClient!.PostAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public Task<IEnumerable<AlunoDTO>> CM_ObterUsuariosAsync(string p_Nome) => throw new NotImplementedException();

    public async Task<Tuple<bool, string>> CM_UsuarioPossuiSenhaAsync(string p_Nome)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_Endereco = $"api/aluno/temsenha?p_Nome={p_Nome}";
        var m_RespostaHttp = await m_HttpClient!.GetAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<Tuple<bool, string>>(await m_RespostaHttp.Content.ReadAsStringAsync())!;
    }

    public async Task CM_AtivarOuDesativarUsuarioAsync(string p_Nome)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_Endereco = $"api/aluno?p_Nome={p_Nome}";
        var m_RespostaHttp = await m_HttpClient!.DeleteAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AlterarTelefoneUsuarioAsync(AlunoDTO p_Usuario)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_AlunoJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_AlunoJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"api/aluno";
        var m_RespostaHttp = await m_HttpClient!.PutAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
