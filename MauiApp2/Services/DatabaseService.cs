using MauiApp2.Data.Models;
using SQLite;
using MauiApp2.Data;

namespace MauiApp2.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _db;

    private async Task Init()
    {
        if (_db is not null)
            return;
        
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "gymtracker.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        
        await _db.CreateTableAsync<Exercise>();
        await _db.CreateTableAsync<WorkoutSession>();
        await _db.CreateTableAsync<ExerciseLog>();
        await _db.CreateTableAsync<SetEntry>();

    }

    public async Task<List<Exercise>> GetExercisesAsync()
    {
        await Init();
        return await _db.Table<Exercise>().ToListAsync();
    }

    public async Task<int> AddExerciseAsync(Exercise exercise)
    {
        await Init();
        return await _db.InsertAsync(exercise);
    }
    
    public async Task SeedExercisesAsync()
    {
        await Init();

        var existing = await _db.Table<Exercise>().ToListAsync();
        if (existing.Count > 0)
            return; // already seeded, don't duplicate

        foreach (var ex in ExerciseLibrary.All)
        {
            await _db.InsertAsync(new Exercise
            {
                Name = ex.Name,
                MuscleGroup = ex.MuscleGroup,
                SubTarget = ex.SubTarget
            });
        }
    }
    
    public async Task AddWorkoutSessionAsync(WorkoutSession session)
    {
        await Init();
        await _db.InsertAsync(session);
    }
    
    public async Task<Exercise> GetExerciseByNameAsync(string name)
    {
        await Init();
        return await _db.Table<Exercise>().FirstOrDefaultAsync(e => e.Name == name);
    }
    
    public async Task AddExerciseLogAsync(ExerciseLog log)
    {
        await Init();
        await _db.InsertAsync(log);
    }
    
    public async Task AddSetEntryAsync(SetEntry set)
    {
        await Init();
        await _db.InsertAsync(set);
    }
    
}