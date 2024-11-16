using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Logging;

using Velopack;

namespace AppWithVelopackInstaller;

public partial class MainWindowViewModel : ObservableObject
{
    public string AppVersion { get; } = typeof(MainWindowViewModel).Assembly.GetName().Version.ToString();

    private readonly ILogger<MainWindowViewModel> _logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [RelayCommand]
    private async Task CheckForUpdatesAsync()
    {
        // Check for updates
        var mgr = new UpdateManager("https://the.place/you-host/updates");

        // check for new version
        try
        {
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
                return; // no update available

            // download new version
            await mgr.DownloadUpdatesAsync(newVersion);

            // install new version and restart app
            mgr.ApplyUpdatesAndRestart(newVersion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check for updates");
        }
    }
}
