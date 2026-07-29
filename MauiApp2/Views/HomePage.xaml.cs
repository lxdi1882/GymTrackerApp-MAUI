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
        StreakLabel.Text = $"Streak: {streak} day{(streak == 1 ? "" : "s")}";

        var sessions = await _databaseService.GetAllSessionsAsync();
        var lastSession = sessions.OrderByDescending(s => s.Date).FirstOrDefault();

        if (lastSession == null)
        {
            LastWorkoutLabel.Text = "No sessions yet";
        }
        else
        {
            string summary = $"{lastSession.Date:MMM d}\n";
            var logs = await _databaseService.GetLogsForSessionAsync(lastSession.Id);

            foreach (var log in logs)
            {
                var exercise = await _databaseService.GetExerciseByIdAsync(log.ExerciseId);
                var sets = await _databaseService.GetSetsForLogAsync(log.Id);
                string setsText = string.Join(", ", sets.Select(s => $"{s.Weight}kg x {s.Reps}"));
                summary += $"{exercise.Name}: {setsText}\n";
            }

            LastWorkoutLabel.Text = summary;
        }

        var prs = await _databaseService.GetRecentPRsAsync();

        RecentPrsLabel.Text = prs.Count == 0
            ? "No PRs yet"
            : string.Join("\n", prs.Select(p => $"{p.Exercise.Name}: {p.Set.Weight}kg x {p.Set.Reps}"));
    }

    private async void OnStartWorkoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}