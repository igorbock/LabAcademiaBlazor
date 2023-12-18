namespace LabAcademiaBlazor.Interfaces;

public interface IAuthService
{
    Task CM_Login(LoginDTO p_Login);
    Task CM_Logout();
}
