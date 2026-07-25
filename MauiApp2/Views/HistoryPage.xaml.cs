using MauiApp2.Data;
using MauiApp2.Data.Models;
using MauiApp2.Services;

namespace MauiApp2.Views;

public partial class HistoryPage : ContentPage
{
    private DatabaseService databaseService = new DatabaseService();
    private WorkoutService workoutService;

    public HistoryPage()
    {
        InitializeComponent();
        workoutService = new WorkoutService(databaseService);
       
    }

    private async void LoadHistory()
    {
        var summaries = await workoutService.GetWorkoutDaySummariesAsync();
        BindingContext = summaries;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadHistory();
    }
}