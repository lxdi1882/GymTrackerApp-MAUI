namespace MauiApp2.Data.Models;

public class WorkoutSession
{
    public DateTime Date{ get; set; }
    public List<ExerciseEntry> Exercises{ get; set; }
}