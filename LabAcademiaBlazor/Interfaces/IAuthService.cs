namespace LabAcademiaBlazor.Interfaces;

public interface IAuthService
{
    Task CM_Login(LoginDTO p_Login);
    Task CM_Logout();
    Task CM_RegistrarProfessor(RegistrarUsuarioDTO p_Registro);
    Task CM_RegistrarAluno(RegistrarUsuarioDTO p_Registro);
}
