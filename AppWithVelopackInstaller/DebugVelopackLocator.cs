using System;
using System.Collections.Generic;
using System.IO;

using NuGet.Versioning;

using Velopack;
using Velopack.Locators;

namespace AppWithVelopackInstaller;

internal class DebugVelopackLocator : IVelopackLocator
{
    private readonly string _updatePath;

    public DebugVelopackLocator(string updatePath)
    {
        _updatePath = updatePath;
    }

    public string AppId => GetType().Assembly.GetName().Name;

    public string RootAppDir => throw new NotImplementedException();

    public string PackagesDir => throw new NotImplementedException();

    public string AppContentDir => throw new NotImplementedException();

    public string AppTempDir => throw new NotImplementedException();

    public string UpdateExePath => Path.Join(_updatePath, AppId + ".exe");

    public SemanticVersion CurrentlyInstalledVersion => new(1,0,0);

    public string ThisExeRelativePath => throw new NotImplementedException();

    public string Channel => "win";

    public bool IsPortable => throw new NotImplementedException();

    public VelopackAsset GetLatestLocalFullPackage()
    {
        return new VelopackAsset();
    }

    public List<VelopackAsset> GetLocalPackages()
    {
        throw new NotImplementedException();
    }

    public Guid? GetOrCreateStagedUserId()
    {
        return Guid.NewGuid();
    }
}