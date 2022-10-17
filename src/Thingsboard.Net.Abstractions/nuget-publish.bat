echo Start publishing Thingsboard.Net.Abstractions
del bin\Release\Thingsboard.Net.Abstractions.*.nupkg
del bin\Release\Thingsboard.Net.Abstractions.*.snupkg
dotnet build -c Release
dotnet nuget push bin\Release\Thingsboard.Net.Abstractions.*.nupkg --source https://api.nuget.org/v3/index.json 
