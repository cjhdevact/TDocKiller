::/*****************************************************\
::
::     TDocKiller - 1-安装.bat
::
::     版权所有(C) 2023-2024 CJH。
::
::     安装批处理
::
::\*****************************************************/
@echo off
cls
title 一键关闭课件小工具安装程序
if "%1" == "/noadm" goto main
if "%1" == "/?" goto hlp
fltmc 1>nul 2>nul&& goto main
echo 正在获取管理员权限...
echo.
echo 可以使用 %0 /noadm 跳过Bat提权，但请手动以管理员身份运行
echo 如果当前窗口无限循环出现，或者未成功获取管理员权限，请注销当前用户或重启电脑，
echo 然后以管理员用户账号运行或手动以管理员身份运行。
if "%1" == "/mshtaadm" goto mshtaAdmin
if "%1" == "/psadm" goto powershellAdmin
ver | findstr "10\.[0-9]\.[0-9]*" >nul && goto powershellAdmin
:mshtaAdmin
rem 原理是利用mshta运行vbscript脚本给bat文件提权
rem 这里使用了前后带引号的%~dpnx0来表示当前脚本，比原版的短文件名%~s0更可靠
rem 这里使用了两次Net session，第二次是检测是否提权成功，如果提权失败则跳转到failed标签
rem 这有效避免了提权失败之后bat文件继续执行的问题
::Net session >nul 2>&1 || mshta vbscript:CreateObject("Shell.Application").ShellExecute("cmd.exe","/c ""%~dpnx0""","","runas",1)(window.close)&&exit
set parameters=
:parameter
@if not "%~1"=="" ( set parameters=%parameters% %~1& shift /1& goto :parameter)
set parameters="%parameters:~1%"
mshta vbscript:createobject("shell.application").shellexecute("%~dpnx0",%parameters%,"","runas",1)(window.close)&exit
cd /d "%~dp0"
Net session >nul 2>&1 || goto failed
goto main

:powershellAdmin
rem 原理是利用powershell给bat文件提权
rem 这里使用了两次Net session，第二次是检测是否提权成功，如果提权失败则跳转到failed标签
rem 这有效避免了提权失败之后bat文件继续执行的问题
Net session >nul 2>&1 || powershell start-process \"%0\" -argumentlist \"%1 %2\" -verb runas && exit
Net session >nul 2>&1 || goto failed
goto main

:failed
cls
echo.
echo 当前未以管理员身份运行。请手动以管理员身份运行本程序。
echo.
echo 任意键关闭... & pause > NUL
goto enda

:hlp
title 一键关闭课件小工具安装程序
cls
echo.
echo ====================================================
echo               一键关闭课件小工具安装程序
echo ====================================================
echo.
echo 你可以使用以下参数：
echo 1-安装.bat [/noadm ^| /mshtaadm ^| /psadm]
echo.
echo /noadm 当检测到无管理员权限跳过自动提权。
echo /mshtaadm 强制使用mshta.exe自动提权。
echo /psadm 强制使用Powershell.exe自动提权。
echo.
goto enda

:main
cd /d "%~dp0"
title 一键关闭课件小工具安装程序
cls
echo.
echo ====================================================
echo               一键关闭课件小工具安装程序
echo ====================================================
echo.
echo 版权所有(C) 2023-2024 CJH。
echo.
echo 安装前建议关闭杀毒软件以及在UAC设置中设置UAC等级为最低，否则在安装主程序或如果选择写入自动启动项会被拦截导致安装失败。
echo.
echo 任意键开始安装... & pause >nul

cls
echo.
echo ====================================================
echo               一键关闭课件小工具安装程序
echo ====================================================
echo.
echo 正在安装中...
echo.
taskkill /f /im TDocKiller.exe
::ver|findstr "\<10\.[0-9]\.[0-9][0-9]*\>" > nul && (set netv=4)
ver|findstr "\<6\.[0-1]\.[0-9][0-9]*\>" > nul && (set netv=4c)
ver|findstr "\<6\.[2-9]\.[0-9][0-9]*\>" > nul && (set netv=4c)
ver|findstr "\<5\.[0-9]\.[0-9][0-9]*\>" > nul && (set netv=4c)

if "%PROCESSOR_ARCHITECTURE%"=="x86" goto x86
if "%PROCESSOR_ARCHITECTURE%"=="AMD64" goto x64

:x86
echo.
if exist "%windir%\TDocKiller.exe" choice /C YN /T 5 /D Y /M "检测到当前系统存在旧版本一键关闭课件小工具。是(Y)否(N)要删除旧版一键关闭课件小工具（5秒后自动选择Y）"
if errorlevel 1 set a=1
if errorlevel 2 set a=2
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" Reg delete HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /f
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" del /q "%windir%\TDocKiller.exe"

if exist "%programfiles%\CJH\TDocKiller\TDocKiller.exe" del /q "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

if not exist "%programfiles%\CJH\TDocKiller" md "%programfiles%\CJH\TDocKiller"
copy "%~dp0TDocKiller.exe" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"
echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要添加自动启动项（5秒后自动选择Y）"
if errorlevel 1 set aa=1
if errorlevel 2 set aa=2
if "%aa%" == "1" echo.
if "%aa%" == "1" echo 如果长时间停留在此操作，请检测是否被杀毒软件拦截。
if "%aa%" == "1" echo.
if "%aa%" == "1" Reg add HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
echo.
choice /C YN /T 5 /D N /M "是(Y)否(N)要添加任务计划级自动启动项（添加后只能启动到右边，安装完成后你可以随时在开始菜单的一键关闭课件小工具里的自动启动管理开启关闭该启动项）（5秒后自动选择N）"
if errorlevel 1 set bk=1
if errorlevel 2 set bk=2
if "%bk%" == "1" echo.
if "%bk%" == "1" echo 如果长时间停留在此操作，请检测是否被杀毒软件拦截。
if "%bk%" == "1" echo.
if "%bk%" == "1" schtasks.exe /Delete /TN \CJH\TDocKiller /F
if "%bk%" == "1" schtasks.exe /create /tn \CJH\TDocKiller /xml "%~dp0TDocKiller.xml"
echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要安装策略到当前系统（安装后可以使用组策略编辑一键关闭课件小工具的策略）（仅Windows Vista以上版本支持）（5秒后自动选择Y）"
if errorlevel 1 set ac=1
if errorlevel 2 set ac=2
if "%ac%" == "1" if exist "%windir%\PolicyDefinitions\*.admx" call "%~dp0TDocKillerAdmxs.exe"

echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要创建快捷方式到开始菜单（5秒后自动选择Y）"
if errorlevel 1 set ad=1
if errorlevel 2 set ad=2
if "%ad%" == "1" if not exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具" md "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk"
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

copy /y "%~dp02-卸载.bat" "%programfiles%\CJH\TDocKiller\Uninstall.bat"
copy /y "%~dp03-自动启动管理.bat" "%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"
copy /y "%~dp0TDocKiller.xml" "%programfiles%\CJH\TDocKiller\TDocKiller.xml"

if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"":b.IconLocation=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)添加卸载程序列表（5秒后自动选择Y）"
if errorlevel 1 set ae=1
if errorlevel 2 set ae=2
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayIcon /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayName /t REG_SZ /d "一键关闭课件小工具（TDocKiller）" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v Publisher /t REG_SZ /d "CJH" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v UninstallString /t REG_SZ /d "%programfiles%\CJH\TDocKiller\Uninstall.bat" /f

start /d "%programfiles%\CJH\TDocKiller" "" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

echo.
cls

echo.
echo ====================================================
echo               一键关闭课件小工具安装程序
echo ====================================================
echo.
echo 安装完成！任意键退出... & pause > nul
goto enda

:x64
echo.
if exist "%windir%\TDocKiller.exe" choice /C YN /T 5 /D Y /M "检测到当前系统存在旧版本一键关闭课件小工具。是(Y)否(N)要删除旧版一键关闭课件小工具（5秒后自动选择Y）"
if errorlevel 1 set a=1
if errorlevel 2 set a=2
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" Reg delete HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /f
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" del /q "%windir%\TDocKiller.exe"
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" del /q "%windir%\syswow64\TDocKiller.exe"

if exist "%programfiles%\CJH\TDocKiller\TDocKiller.exe" del /q "%programfiles%\CJH\TDocKiller\TDocKiller.exe"
if exist "%programfiles%\CJH\TDocKiller\x86\TDocKiller.exe" del /q "%programfiles%\CJH\TDocKiller\x86\TDocKiller.exe"

if not exist "%programfiles%\CJH\TDocKiller" md "%programfiles%\CJH\TDocKiller"
if not exist "%programfiles%\CJH\TDocKiller\x86" md "%programfiles%\CJH\TDocKiller\x86"
copy "%~dp0TDocKiller64.exe" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"
copy "%~dp0TDocKiller.exe" "%programfiles%\CJH\TDocKiller\x86\TDocKiller.exe"
echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要添加自动启动项（5秒后自动选择Y）"
if errorlevel 1 set aa=1
if errorlevel 2 set aa=2
if "%aa%" == "1" echo.
if "%aa%" == "1" echo 如果长时间停留在此操作，请检测是否被杀毒软件拦截。
if "%aa%" == "1" echo.
if "%aa%" == "1" Reg add HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
echo.
choice /C YN /T 5 /D N /M "是(Y)否(N)要添加任务计划级自动启动项（添加后只能启动到右边，安装完成后你可以随时在开始菜单的一键关闭课件小工具里的自动启动管理开启关闭该启动项）（5秒后自动选择N）"
if errorlevel 1 set bk=1
if errorlevel 2 set bk=2
if "%bk%" == "1" echo.
if "%bk%" == "1" echo 如果长时间停留在此操作，请检测是否被杀毒软件拦截。
if "%bk%" == "1" echo.
if "%bk%" == "1" schtasks.exe /Delete /TN \CJH\TDocKiller /F
if "%bk%" == "1" schtasks.exe /create /tn \CJH\TDocKiller /xml "%~dp0TDocKiller.xml"
echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要安装策略到当前系统（安装后可以使用组策略编辑一键关闭课件小工具的策略）（仅Windows Vista以上版本支持）（5秒后自动选择Y）"
if errorlevel 1 set ac=1
if errorlevel 2 set ac=2
if "%ac%" == "1" if exist "%windir%\PolicyDefinitions\*.admx" call "%~dp0TDocKillerAdmxs.exe"

echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)要创建快捷方式到开始菜单（5秒后自动选择Y）"
if errorlevel 1 set ad=1
if errorlevel 2 set ad=2
if "%ad%" == "1" if not exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具" md "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具（32位）.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具（32位）.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk"
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\一键关闭课件小工具（32位）.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\x86\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller\x86"":b.Save:close")

copy /y "%~dp02-卸载.bat" "%programfiles%\CJH\TDocKiller\Uninstall.bat"
copy /y "%~dp03-自动启动管理.bat" "%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"
copy /y "%~dp0TDocKiller.xml" "%programfiles%\CJH\TDocKiller\TDocKiller.xml"

if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\一键关闭课件小工具\管理自动启动.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"":b.IconLocation=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

echo.
choice /C YN /T 5 /D Y /M "是(Y)否(N)添加卸载程序列表（5秒后自动选择Y）"
if errorlevel 1 set ae=1
if errorlevel 2 set ae=2
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayIcon /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayName /t REG_SZ /d "一键关闭课件小工具（TDocKiller）" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v Publisher /t REG_SZ /d "CJH" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v UninstallString /t REG_SZ /d "%programfiles%\CJH\TDocKiller\Uninstall.bat" /f

start /d "%programfiles%\CJH\TDocKiller" "" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

echo.
cls

echo.
echo ====================================================
echo                一键关闭课件小工具安装程序
echo ====================================================
echo.
echo 安装完成！任意键退出... & pause > nul
goto enda

:enda