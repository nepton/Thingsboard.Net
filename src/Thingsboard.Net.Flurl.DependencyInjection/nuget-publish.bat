echo Start publishing Thingsboard.Net.Flurl
del bin\Release\*.nupkg
del bin\Release\*.snupkg
dotnet build -c Release
dotnet nuget push bin\Release\*.nupkg --source https://api.nuget.org/v3/index.json 
