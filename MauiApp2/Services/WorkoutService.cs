using MauiApp2.Data;
using MauiApp2.Data.Models;

namespace MauiApp2.Services;


public class WorkoutService
{
    private List<WorkoutSession> completedSessions = new List<WorkoutSession>();
    private Dictionary<string, ExerciseEntry> workoutInProgress = new Dictionary<string, ExerciseEntry>();
    
    public string GetWorkoutSummary()
    {
        string summary = "";

        foreach (var entry in workoutInProgress.Values)
        {
            summary += $"\n{entry.Exercise.Name} ({entry.Exercise.MuscleGroup}):\n";

            foreach (var set in entry.Sets)
            {
                summary += $"{set.Weight}kg x {set.Reps} reps\n";
            }
        }

        return summary;
    }
    
    public bool AddSet(string selectedName, double weight, int reps)
    {
        var newSet = new SetEntry
        {
            Weight = weight,
            Reps = reps
        };

        if (!workoutInProgress.ContainsKey(selectedName))
        {
            var exercise = ExerciseLibrary.All
                .First(ex => ex.Name == selectedName);

            workoutInProgress[selectedName] = new ExerciseEntry
            {
                Exercise = exercise,
                Sets = new List<SetEntry>()
            };
        }
        
        workoutInProgress[selectedName].Sets.Add(newSet);
        
        return true;
    }
    
    public int FinishWorkout()
    {
        var session = new WorkoutSession
        {
            Date = DateTime.Now,
            Exercises = workoutInProgress.Values.ToList()
        };

        completedSessions.Add(session);

        int count = session.Exercises.Count;

        workoutInProgress.Clear();

        return count;
    }
    
    public string GetHistory()
    {
        string summary = "";

        foreach (var session in completedSessions)
        {
            summary += $"\nDate: {session.Date}\n";

            foreach (var entry in session.Exercises)
            {
                summary += $"\n{entry.Exercise.Name}:\n";

                foreach (var set in entry.Sets)
                {
                    summary += $" {set.Weight}kg x {set.Reps} reps\n";
                }
            }
        }

        return summary;
    }
    
    
}

