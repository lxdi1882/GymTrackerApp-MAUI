using MauiApp2.Data;
using MauiApp2.Data.Models;
using MauiApp2.Services;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    
    private WorkoutService workoutService = new WorkoutService();
    private DatabaseService databaseService = new DatabaseService();
    public MainPage()
    {
        
        InitializeComponent();
       
        TestDatabase();
    }

    private async void TestDatabase()
    {
        await databaseService.SeedExercisesAsync();

        var exercises = await databaseService.GetExercisesAsync();
        ExercisePicker.ItemsSource = exercises.Select(e => e.Name).ToList();
        
        System.Diagnostics.Debug.WriteLine($"Count: {exercises.Count}");
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

    private void OnFinishWorkoutClicked(object sender, EventArgs e)
    {
        int exerciseCount = workoutService.FinishWorkout();

        SetsLabel.Text = $"Workout Session: Saved! {exerciseCount} exercises logged.";
    }
    

    private void OnViewHistoryClicked(object sender, EventArgs e)
    {
        HistoryLabel.Text = workoutService.GetHistory();
    }

    
}