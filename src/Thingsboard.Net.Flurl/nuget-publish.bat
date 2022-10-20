@echo off
echo Start publishing...

echo *** Delete old packages **************************************************
del bin\Release\*.* /q

echo *** Restore packages *****************************************************
dotnet restore
if %errorlevel% neq 0 goto :error

echo *** Building *************************************************************
dotnet build -c Release
if %errorlevel% neq 0 goto :error

echo *** Packaging ************************************************************
dotnet pack -c Release
if %errorlevel% neq 0 goto :error

echo *** Publishing ***********************************************************
dotnet nuget push bin\Release\*.nupkg --source https://api.nuget.org/v3/index.json 

echo *** Done *****************************************************************
exit 0

:error
echo *** Error ****************************************************************