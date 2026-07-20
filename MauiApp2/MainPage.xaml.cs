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
}