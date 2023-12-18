namespace LabAcademiaBlazor.Services;

public class AlunoService : IUsuarioService<AlunoDTO>
{
    public HttpClient? C_HttpClient { get; set; }

    public AlunoService(HttpClient? p_HttpClient)
    {
        C_HttpClient = p_HttpClient;
    }

    public async Task<string> CM_CriarQRCodeAsync(string p_Matricula)
    {
        var m_Endereco = $"https://localhost:7121/api/aluno/qrcode?p_Matricula={p_Matricula}";
        var m_RespostaHttp = await C_HttpClient!.GetAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public async Task<string> CM_CriarUsuarioAsync(AlunoDTO p_Usuario)
    {
        var m_UsuarioJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_UsuarioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = "https://localhost:7121/api/aluno";
        var m_RespostaHttp = await C_HttpClient!.PostAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public Task<IEnumerable<AlunoDTO>> CM_ObterUsuariosAsync(string p_Nome)
    {
        throw new NotImplementedException();
    }

    public async Task<Tuple<bool, string>> CM_UsuarioPossuiSenhaAsync(string p_Nome)
    {
        var m_Endereco = $"https://localhost:7121/api/aluno/temsenha?p_Nome={p_Nome}";
        var m_RespostaHttp = await C_HttpClient!.GetAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<Tuple<bool, string>>(await m_RespostaHttp.Content.ReadAsStringAsync())!;
    }

    public async Task CM_AtivarOuDesativarUsuarioAsync(string p_Nome)
    {
        var m_Endereco = $"https://localhost:7121/api/aluno?p_Nome={p_Nome}";
        var m_RespostaHttp = await C_HttpClient!.DeleteAsync(m_Endereco);
        m_RespostaHttp.EnsureSuccessStatusCode();
    }

    public async Task CM_AlterarTelefoneUsuarioAsync(AlunoDTO p_Usuario)
    {
        var m_AlunoJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_AlunoJSON, Encoding.UTF8, "application/json");
        var m_Endereco = $"https://localhost:7121/api/aluno";
        var m_RespostaHttp = await C_HttpClient!.PutAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();
    }
}
