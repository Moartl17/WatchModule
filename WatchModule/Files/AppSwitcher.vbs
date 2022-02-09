Option Explicit 
Dim WshShell 
Set WshShell = WScript.CreateObject("WScript.Shell") 

REM WshShell.Run("""C:\Program Files (x86)\Windows Media Player\wmplayer.exe""")


Do 
    
    
    WshShell.AppActivate("Video Würfel") 
    REM WshShell.SendKeys "% x" 
    WScript.Sleep 15000
	
	WshShell.AppActivate("Windows Media Player") 
    REM WshShell.SendKeys "% r" 
	WshShell.SendKeys "%{ENTER}"
    WScript.Sleep 25000
Loop 
