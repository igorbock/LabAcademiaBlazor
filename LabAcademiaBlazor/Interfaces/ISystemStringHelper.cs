namespace LabAcademiaBlazor.Interfaces;

public interface ISystemStringHelper
{
    IEnumerable<Claim> CM_TransformarTokenEmClaims(string p_Token);
    byte[]? CM_AjustarBase64SemPreenchimento(string p_Base64);
}
