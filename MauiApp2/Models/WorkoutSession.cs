using MauiApp2.Data.Models;
using SQLite;

namespace MauiApp2.Data.Models;

public class WorkoutSession
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    
    public int ProgramId { get; set; }
    public int TrainingDayId { get; set; }
    public DateTime Date{ get; set; }
    //public List<ExerciseEntry> Exercises{ get; set; }
}