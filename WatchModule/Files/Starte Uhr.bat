@echo off 

set pathToWatchModuleFiles="c:\Source\WatchModule\WatchModule\bin\Release\"
set pathToLogorotation="C:\"

echo %pathToWatchModuleFiles%
echo %pathToLogorotation%
cd  %pathToWatchModuleFiles%
start WatchModule.exe

cd %pathToLogorotation%
REM start "%ProgramFiles(x86)%\Windows Media Player\wmplayer.exe" Logorotation_2021.m4v
start Logorotation_2021.m4v

cd "%pathToWatchModuleFiles%\Files"
REM cmdow.exe "Filme & TV" /mov 1500 500
cmdow.exe "Windows Media Player" /mov 1500 500

exit