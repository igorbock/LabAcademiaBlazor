namespace LabAcademiaBlazor.Interfaces;

public interface IExercicioService
{
    Task<ObservableCollection<ExercicioDTO>> CM_ObterExerciciosAsync();
    Task CM_RemoverExercicioAsync(int p_Codigo);
    Task CM_AdicionarNovoExercicioAsync(ExercicioDTO p_Exercicio);
    Task CM_AtualizarExercicioAsync(ExercicioDTO p_Exercicio);
}
