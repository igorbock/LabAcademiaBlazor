namespace LabAcademiaBlazor.Extensions;

public static class IHttpClientFactoryExtensions
{
    public static async Task<HttpClient> CMX_ObterHttpClientAsync(
        this IHttpClientFactory p_HttpClientFactory,
        string p_NomeHttpClient,
        ILocalStorageService p_Storage)
    {
        var m_Retorno = p_HttpClientFactory!.CreateClient(p_NomeHttpClient);
        var m_Token = await p_Storage!.GetItemAsync<string>("Token");
        if (string.IsNullOrEmpty(m_Token))
            return m_Retorno;

        m_Retorno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_Token);
        return m_Retorno;
    }
}
