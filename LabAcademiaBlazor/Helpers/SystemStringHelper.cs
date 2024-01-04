namespace LabAcademiaBlazor.Helpers;

public class SystemStringHelper : ISystemStringHelper
{
    public byte[]? CM_AjustarBase64SemPreenchimento(string p_Base64)
        => (p_Base64.Length % 4) switch
        {
            2 => Convert.FromBase64String(p_Base64 += "=="),
            3 => Convert.FromBase64String(p_Base64 += "="),
            _ => Convert.FromBase64String(p_Base64)
        };

    public IEnumerable<Claim> CM_TransformarTokenEmClaims(string p_Token)
    {
        var m_Claims = new List<Claim>();
        var m_Payload = p_Token.Split('.')[1];
        var m_JsonBytes = CM_AjustarBase64SemPreenchimento(m_Payload);
        var m_ParesChaveValor = JsonSerializer.Deserialize<Dictionary<string, object>>(m_JsonBytes) ?? throw new JsonException();

        m_ParesChaveValor!.TryGetValue(ClaimTypes.Role, out object? m_Roles);
        if (m_Roles == null)
        {
            m_Claims.AddRange(m_ParesChaveValor.Select(a => new Claim(a.Key, a.Value.ToString()!)));
            return m_Claims;
        }

        if (m_Roles.ToString()!.Trim().StartsWith("[") == false)
            m_Claims.Add(new Claim(ClaimTypes.Role, m_Roles.ToString()!));
        else
        {
            var m_ParsedRoles = JsonSerializer.Deserialize<string[]>(m_Roles.ToString()!) ?? throw new JsonException();
            foreach (var item in m_ParsedRoles)
                m_Claims.Add(new Claim(ClaimTypes.Role, item));
        }

        m_Claims.Add(new Claim(ClaimTypes.Name, m_ParesChaveValor[ClaimTypes.Name].ToString()!));
        m_ParesChaveValor.Remove(ClaimTypes.Role);

        return m_Claims;
    }
}
