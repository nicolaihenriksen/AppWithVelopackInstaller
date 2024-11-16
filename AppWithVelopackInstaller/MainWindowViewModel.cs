using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppWithVelopackInstaller;

public partial class MainWindowViewModel : ObservableObject
{
    public string AppVersion { get; } = typeof(MainWindowViewModel).Assembly.GetName().Version.ToString();

    [RelayCommand]
    private void CheckForUpdates()
    {
        // Check for updates
    }
}
