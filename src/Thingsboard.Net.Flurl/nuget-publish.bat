echo Start publishing Thingsboard.Net.Flurl
del bin\Release\Thingsboard.Net.Flurl.*.nupkg
del bin\Release\Thingsboard.Net.Flurl.*.snupkg
dotnet build -c Release
dotnet nuget push bin\Release\Thingsboard.Net.Flurl.*.nupkg --source https://api.nuget.org/v3/index.json 
