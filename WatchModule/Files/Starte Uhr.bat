@echo off 

cd "..\"
start WatchModule.exe

cd "Files"
start "%ProgramFiles(x86)%\Windows Media Player\wmplayer.exe" Logorotation_2023.m4v

REM cd "c:\Users\Martin.Eckart.ext\OneDrive\Dokumente\Source\WatchModule\WatchModule\bin\Debug\Files"
cmdow.exe "Windows Media Player" /mov 1500 500

exit