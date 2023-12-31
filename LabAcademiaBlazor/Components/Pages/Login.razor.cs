﻿namespace LabAcademiaBlazor.Components.Pages;

public partial class Login
{
    public LoginDTO? C_Login { get; set; } = new LoginDTO();
    private bool C_MostrarErros { get; set; }
    private IEnumerable<string> C_Erros { get; set; } = new List<string>();
    public bool C_Carregando { get; set; } = false;

    private async Task cm_Login()
    {
        C_MostrarErros = false;

        try
        {
            C_Carregando = true;
            await C_AuthService.CM_Login(C_Login!);

            C_NavigationManager.NavigateTo("/");
        }
        catch (UnauthorizedAccessException)
        {
            C_MostrarErros = true;
            C_Erros = ["Usuário ou senha incorretos!"];
        }
        catch (Exception ex)
        {
            C_MostrarErros = true;
            C_Erros = new[] { ex.Message };
        }
        finally
        {
            C_Carregando = false;
        }
    }
}
