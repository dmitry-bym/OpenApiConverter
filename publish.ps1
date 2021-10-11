$releaseFoleder = 'release'
$projectName = 'OpenApiConverter'
$runtimes = @(
    'win-x64'
    'win-x86'
    'linux-x64'
    'linux-arm'
    'osx-x64'
    # 'osx.11.0-arm64'
    )

if(Test-Path -Path $releaseFoleder)
{
    Remove-Item -Path $releaseFoleder -Recurse
}

foreach ( $elem in $runtimes )
{
    dotnet publish -r $elem -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained false -o $releaseFoleder
    Get-ChildItem -Path $releaseFoleder -Filter $projectName* | Rename-Item -NewName { $_.Name.replace($projectName, $elem)}
}