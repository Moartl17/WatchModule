@echo off 

cd "c:\Users\Martin.Eckart.ext\OneDrive\Dokumente\Source\WatchModule\WatchModule\bin\Debug\"
start WatchModule.exe

cd "c:\Users\Martin.Eckart.ext\Downloads\"
start "%ProgramFiles(x86)%\Windows Media Player\wmplayer.exe" Logorotation_2021.m4v

cd "c:\Users\Martin.Eckart.ext\OneDrive\Dokumente\Source\WatchModule\WatchModule\bin\Debug\Files"
cmdow.exe "Windows Media Player" /mov 1500 500

exit