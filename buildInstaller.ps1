param([Version]$version)

dotnet tool install -g vpk
vpk pack -u AppWithVelopackInstaller -v $version -p ./publish -o ./installer