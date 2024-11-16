using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Logging;

using Velopack;
using Velopack.Locators;

namespace AppWithVelopackInstaller;

public partial class MainWindowViewModel : ObservableObject
{
    public string AppVersion { get; } = typeof(MainWindowViewModel).Assembly.GetName().Version.ToString();

    private readonly ILogger<MainWindowViewModel> _logger;
    private readonly ILogger<UpdateManager> _umLogger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, ILogger<UpdateManager> umLogger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _umLogger = umLogger ?? throw new ArgumentNullException(nameof(umLogger)); ;
    }

    [RelayCommand]
    private async Task CheckForUpdatesAsync()
    {
        string updatePath = GetType().Assembly.GetCustomAttributes<AssemblyMetadataAttribute>().Single(a => a.Key == "UpdatePath").Value;

        // Check for updates
        IVelopackLocator locator = null;
#if DEBUG
        locator = new DebugVelopackLocator(updatePath);
#endif
        var mgr = new UpdateManager(updatePath, locator: locator, logger: _umLogger);
        if (!mgr.IsInstalled)
            return;

        // check for new version
        try
        {
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
            {
                MessageBox.Show($"No updates found in: '{updatePath}'");
                return; // no update available
            }

#if DEBUG
            MessageBox.Show($"New version available: '{newVersion.TargetFullRelease.Version}', will not install in debug mode!");
#else
            // download new version
            await mgr.DownloadUpdatesAsync(newVersion);

            // install new version and restart app
            mgr.ApplyUpdatesAndRestart(newVersion);
#endif

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check for updates");
            MessageBox.Show($"Error occurred while updating: '{ex}'");
        }
    }
}
