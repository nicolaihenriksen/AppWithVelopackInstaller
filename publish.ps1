param([Version]$version)

dotnet publish AppWithVelopackInstaller/AppWithVelopackInstaller.csproj --configuration Release -p:Version=$($version) -o ./publish
Remove-Item -Path ./publish/*.pdb