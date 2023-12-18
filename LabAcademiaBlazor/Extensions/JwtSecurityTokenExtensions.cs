namespace LabAcademiaBlazor.Extensions;

public static class JwtSecurityTokenExtensions
{
    public static bool CMX_ValidarToken(this JwtSecurityToken p_Token)
    {
        try
        {
            return p_Token.ValidTo > DateTime.UtcNow;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
