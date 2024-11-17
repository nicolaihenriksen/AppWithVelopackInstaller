param([Version]$version)

dotnet publish AppWithVelopackInstaller/AppWithVelopackInstaller.csproj --configuration Release -p:Version=$($version) -o ./publish
Copy-Item -Path ./AppWithVelopackInstaller/appsettings.json -Destination ./publish
Remove-Item -Path ./publish/*.pdb