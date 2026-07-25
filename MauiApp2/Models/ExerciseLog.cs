using SQLite;

namespace MauiApp2.Models;

public class ExerciseLog
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Indexed]
    public int WorkoutSessionId{ get; set; }
    [Indexed]
    public int ExerciseId { get; set; }
    
}