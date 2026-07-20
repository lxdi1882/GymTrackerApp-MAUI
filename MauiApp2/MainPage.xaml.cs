namespace MauiApp2;

public partial class MainPage : ContentPage
{
    private int count = 0;

    public MainPage()
    {
        InitializeComponent();
        ExercisePicker.ItemsSource = ExerciseLibrary.All.Select(e => e.Name).ToList();
    }


    // private void OnAddExerciseClicked(object? sender, EventArgs e)
    // {
    //     var exerciseName = ExerciseInput.Text;
    //
    //     var exerciseLabel = new Label { Text = $"Exercise: {exerciseName}" };
    //     var logButton = new Button { Text = $"Log : {exerciseName}" };
    //
    //     logButton.Clicked += (s, args) =>
    //     {
    //         exerciseLabel.Text = $"{exerciseName} - logged!";
    //         logButton.IsEnabled = false;
    //     };
    //
    //     MainStack.Children.Add(exerciseLabel);
    //     MainStack.Children.Add(logButton);
    //
    //     ExerciseInput.Text = "";
    // }
    
    private Dictionary<string, ExerciseEntry> workoutInProgress = new Dictionary<string, ExerciseEntry>();

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
        
        var newSet = new SetEntry{Weight = weight, Reps = reps};
        
        if (!workoutInProgress.ContainsKey(selectedName))
        {
            var exercise = ExerciseLibrary.All.First(ex => ex.Name == selectedName);
            workoutInProgress[selectedName] = new ExerciseEntry
            {
                Exercise = exercise,
                Sets = new List<SetEntry>()
            };
        }

        workoutInProgress[selectedName].Sets.Add(newSet);

        // rebuild the display from everything tracked so far
        string summary = "";
        foreach (var entry in workoutInProgress.Values)
        {
            summary += $"\n{entry.Exercise.Name} ({entry.Exercise.MuscleGroup}):\n";
            foreach (var set in entry.Sets)
            {
                summary += $"  {set.Weight}kg x {set.Reps} reps\n ";
            }
        }
        SetsLabel.Text = summary;

        WeightInput.Text = "";
        RepsInput.Text = "";
    
    }
    
        

    
}