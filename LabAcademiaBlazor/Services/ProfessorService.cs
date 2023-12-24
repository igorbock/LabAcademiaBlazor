namespace LabAcademiaBlazor.Services;

public class ProfessorService : IUsuarioService<RegistrarUsuarioDTO>
{
    public IHttpClientFactory? C_HttpClientFactory { get; private set; }
    public ILocalStorageService C_Storage { get; private set; }

    public ProfessorService(IHttpClientFactory? p_HttpClientFactory, ILocalStorageService p_Storage)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_Storage = p_Storage;
    }

    public Task<string> CM_CriarQRCodeAsync(string p_Matricula) => throw new NotImplementedException();

    public async Task<string> CM_CriarUsuarioAsync(RegistrarUsuarioDTO p_Usuario)
    {
        using var m_HttpClient = await C_HttpClientFactory!.CMX_ObterHttpClientAsync("LabAspNetIdentity", C_Storage!);
        var m_UsuarioJSON = JsonSerializer.Serialize(p_Usuario);
        var m_Content = new StringContent(m_UsuarioJSON, Encoding.UTF8, "application/json");
        var m_Endereco = "api/usuario/professor";
        var m_RespostaHttp = await m_HttpClient!.PostAsync(m_Endereco, m_Content);
        m_RespostaHttp.EnsureSuccessStatusCode();

        return await m_RespostaHttp.Content.ReadAsStringAsync();
    }

    public Task<IEnumerable<RegistrarUsuarioDTO>> CM_ObterUsuariosAsync(string p_Nome) => throw new NotImplementedException();

    public Task<Tuple<bool, string>> CM_UsuarioPossuiSenhaAsync(string p_Nome) => throw new NotImplementedException();

    public Task CM_AtivarOuDesativarUsuarioAsync(string p_Nome) => throw new NotImplementedException();

    public Task CM_AlterarTelefoneUsuarioAsync(RegistrarUsuarioDTO p_Usuario) => throw new NotImplementedException();
}
