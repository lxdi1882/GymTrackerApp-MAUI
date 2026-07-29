using MauiApp2.Data;
using MauiApp2.Data.Models;
using MauiApp2.Services;

namespace MauiApp2.Views;

public partial class MainPage : ContentPage
{
    
    private DatabaseService databaseService = new DatabaseService();
    private WorkoutService workoutService;
    public MainPage()
    {
        
        InitializeComponent();
        workoutService = new WorkoutService(databaseService);
        TestDatabase();
    }

    private async void TestDatabase()
    {
        await databaseService.SeedExercisesAsync();

        var exercises = await databaseService.GetExercisesAsync();
        ExercisePicker.ItemsSource = exercises.Select(e => e.Name).ToList();

        System.Diagnostics.Debug.WriteLine($"Count: {exercises.Count}");

        // sanity check the new methods
        int streak = await databaseService.GetStreakAsync();
        System.Diagnostics.Debug.WriteLine($"Streak: {streak}");

        var prs = await databaseService.GetRecentPRsAsync();
        System.Diagnostics.Debug.WriteLine($"PR count: {prs.Count}");
        foreach (var pr in prs)
            System.Diagnostics.Debug.WriteLine($"PR: {pr.Exercise.Name} - {pr.Set.Weight}kg x {pr.Set.Reps} on {pr.Date:d}");
    }
  

    private void OnAddSetClicked(object sender, EventArgs e)
    {
        string selectedName = ExercisePicker.SelectedItem?.ToString();
        if (selectedName == null)
        {
            SetsLabel.Text = " please select an exercise first.";
            return;
        }

        bool weightValid = double.TryParse(WeightInput.Text, out double weight);
        bool repsValid = int.TryParse(RepsInput.Text, out int reps);

        if (!weightValid || !repsValid)
        {
            SetsLabel.Text = " please enter a valid numbers for weight and reps.";
            return;
        }
        
        workoutService.AddSet(selectedName, weight, reps);
        SetsLabel.Text = workoutService.GetWorkoutSummary();
        
        RepsInput.Text = "";
    
    }

    private async void OnFinishWorkoutClicked(object sender, EventArgs e)
    {
        int exerciseCount = await workoutService.FinishWorkoutAsync();
       
        SetsLabel.Text = $"Workout Session: Saved! {exerciseCount} exercises logged.";
    }
    
    
    

    // private async void OnViewHistoryClicked(object sender, EventArgs e)
    // {
    //     
    //     HistoryLabel.Text = await workoutService.GetHistoryAsync();
    // }

    
}