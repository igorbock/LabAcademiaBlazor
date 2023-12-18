namespace LabAcademiaBlazor.Services;

public class ProfessorService : IUsuarioService<RegistrarUsuarioDTO>
{
    public HttpClient? C_HttpClient { get; set; }

    public ProfessorService(HttpClient? p_HttpClient)
    {
        C_HttpClient = p_HttpClient;
    }

    public Task<string> CM_CriarQRCodeAsync(string p_Matricula) => throw new NotImplementedException();

    public async Task<string> CM_CriarUsuarioAsync(RegistrarUsuarioDTO p_Usuario)
    {
        var m_UsuarioJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_UsuarioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = "https://localhost:7121/api/usuario/professor";
        var m_RespostaHttp = await C_HttpClient!.PostAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public Task<IEnumerable<RegistrarUsuarioDTO>> CM_ObterUsuariosAsync(string p_Nome) => throw new NotImplementedException();

    public Task<Tuple<bool, string>> CM_UsuarioPossuiSenhaAsync(string p_Nome) => throw new NotImplementedException();

    public Task CM_AtivarOuDesativarUsuarioAsync(string p_Nome) => throw new NotImplementedException();

    public Task CM_AlterarTelefoneUsuarioAsync(RegistrarUsuarioDTO p_Usuario) => throw new NotImplementedException();
}
