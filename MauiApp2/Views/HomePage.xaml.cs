using MauiApp2.Data.Models;
using MauiApp2.Services;

namespace MauiApp2.Views;

public partial class HomePage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public HomePage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadHomeDataAsync();
    }

    private async Task LoadHomeDataAsync()
    {
        int streak = await _databaseService.GetStreakAsync();
        StreakValueLabel.Text = $"{streak} day{(streak == 1 ? "" : "s")}";

        var sessions = await _databaseService.GetAllSessionsAsync();
        var lastSession = sessions.OrderByDescending(s => s.Date).FirstOrDefault();

        if (lastSession == null)
        {
            LastWorkoutTitleLabel.Text = "No sessions yet";
            LastWorkoutCountLabel.Text = "";
            LastWorkoutSetsView.ItemsSource = new List<ExerciseLogDisplay>();
        }
        else
        {
            var logs = await _databaseService.GetLogsForSessionAsync(lastSession.Id);
            var exerciseDisplays = new List<ExerciseLogDisplay>();

            foreach (var log in logs)
            {
                var exercise = await _databaseService.GetExerciseByIdAsync(log.ExerciseId);
                var sets = await _databaseService.GetSetsForLogAsync(log.Id);

                var setLines = sets
                    .Select((s, i) => $"Set {i + 1}: {s.Weight}kg x {s.Reps}")
                    .ToList();

                exerciseDisplays.Add(new ExerciseLogDisplay
                {
                    ExerciseName = exercise.Name,
                    SetLines = setLines
                });
            }

            LastWorkoutTitleLabel.Text = $"{lastSession.Date:MMM d}";
            LastWorkoutCountLabel.Text = $"{logs.Count} exercise{(logs.Count == 1 ? "" : "s")}";
            LastWorkoutSetsView.ItemsSource = exerciseDisplays;
        }

        var prs = await _databaseService.GetRecentPRsAsync();
        var prDisplays = prs.Select(p => new PrDisplay
        {
            ExerciseName = p.Exercise.Name,
            ResultText = $"{p.Set.Weight}kg x {p.Set.Reps}"
        }).ToList();

        RecentPrsView.ItemsSource = prDisplays;
        PrCountLabel.Text = prs.Count.ToString();
    }

    private void OnExerciseRowTapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is ExerciseLogDisplay exercise)
        {
            exercise.IsExpanded = !exercise.IsExpanded;
        }
    }

    private async void OnStartWorkoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}