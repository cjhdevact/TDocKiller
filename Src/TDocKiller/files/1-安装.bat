::/*****************************************************\
::
::     TDocKiller - 1-��װ.bat
::
::     ��Ȩ����(C) 2023-2024 CJH��
::
::     ��װ������
::
::\*****************************************************/
@echo off
cls
title һ���رտμ�С���߰�װ����
if "%1" == "/noadm" goto main
if "%1" == "/?" goto hlp
fltmc 1>nul 2>nul&& goto main
echo ���ڻ�ȡ����ԱȨ��...
echo.
echo ����ʹ�� %0 /noadm ����Bat��Ȩ�������ֶ��Թ���Ա�������
echo �����ǰ��������ѭ�����֣�����δ�ɹ���ȡ����ԱȨ�ޣ���ע����ǰ�û����������ԣ�
echo Ȼ���Թ���Ա�û��˺����л��ֶ��Թ���Ա������С�
if "%1" == "/mshtaadm" goto mshtaAdmin
if "%1" == "/psadm" goto powershellAdmin
ver | findstr "10\.[0-9]\.[0-9]*" >nul && goto powershellAdmin
:mshtaAdmin
rem ԭ��������mshta����vbscript�ű���bat�ļ���Ȩ
rem ����ʹ����ǰ������ŵ�%~dpnx0����ʾ��ǰ�ű�����ԭ��Ķ��ļ���%~s0���ɿ�
rem ����ʹ��������Net session���ڶ����Ǽ���Ƿ���Ȩ�ɹ��������Ȩʧ������ת��failed��ǩ
rem ����Ч��������Ȩʧ��֮��bat�ļ�����ִ�е�����
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
rem ԭ��������powershell��bat�ļ���Ȩ
rem ����ʹ��������Net session���ڶ����Ǽ���Ƿ���Ȩ�ɹ��������Ȩʧ������ת��failed��ǩ
rem ����Ч��������Ȩʧ��֮��bat�ļ�����ִ�е�����
Net session >nul 2>&1 || powershell start-process \"%0\" -argumentlist \"%1 %2\" -verb runas && exit
Net session >nul 2>&1 || goto failed
goto main

:failed
cls
echo.
echo ��ǰδ�Թ���Ա������С����ֶ��Թ���Ա������б�����
echo.
echo ������ر�... & pause > NUL
goto enda

:hlp
title һ���رտμ�С���߰�װ����
cls
echo.
echo ====================================================
echo               һ���رտμ�С���߰�װ����
echo ====================================================
echo.
echo �����ʹ�����²�����
echo 1-��װ.bat [/noadm ^| /mshtaadm ^| /psadm]
echo.
echo /noadm ����⵽�޹���ԱȨ�������Զ���Ȩ��
echo /mshtaadm ǿ��ʹ��mshta.exe�Զ���Ȩ��
echo /psadm ǿ��ʹ��Powershell.exe�Զ���Ȩ��
echo.
goto enda

:main
cd /d "%~dp0"
title һ���رտμ�С���߰�װ����
cls
echo.
echo ====================================================
echo               һ���رտμ�С���߰�װ����
echo ====================================================
echo.
echo ��Ȩ����(C) 2023-2024 CJH��
echo.
echo ��װǰ����ر�ɱ������Լ���UAC����������UAC�ȼ�Ϊ��ͣ������ڰ�װ����������ѡ��д���Զ�������ᱻ���ص��°�װʧ�ܡ�
echo.
echo �������ʼ��װ... & pause >nul

cls
echo.
echo ====================================================
echo               һ���رտμ�С���߰�װ����
echo ====================================================
echo.
echo ���ڰ�װ��...
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
if exist "%windir%\TDocKiller.exe" choice /C YN /T 5 /D Y /M "��⵽��ǰϵͳ���ھɰ汾һ���رտμ�С���ߡ���(Y)��(N)Ҫɾ���ɰ�һ���رտμ�С���ߣ�5����Զ�ѡ��Y��"
if errorlevel 1 set a=1
if errorlevel 2 set a=2
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" Reg delete HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /f
if exist "%windir%\TDocKiller.exe" if "%a%" == "1" del /q "%windir%\TDocKiller.exe"

if exist "%programfiles%\CJH\TDocKiller\TDocKiller.exe" del /q "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

if not exist "%programfiles%\CJH\TDocKiller" md "%programfiles%\CJH\TDocKiller"
copy "%~dp0TDocKiller.exe" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"
echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ����Զ������5����Զ�ѡ��Y��"
if errorlevel 1 set aa=1
if errorlevel 2 set aa=2
if "%aa%" == "1" echo.
if "%aa%" == "1" echo �����ʱ��ͣ���ڴ˲����������Ƿ�ɱ��������ء�
if "%aa%" == "1" echo.
if "%aa%" == "1" Reg add HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
echo.
choice /C YN /T 5 /D N /M "��(Y)��(N)Ҫ�������ƻ����Զ��������Ӻ�ֻ���������ұߣ���װ��ɺ��������ʱ�ڿ�ʼ�˵���һ���رտμ�С��������Զ������������رո��������5����Զ�ѡ��N��"
if errorlevel 1 set bk=1
if errorlevel 2 set bk=2
if "%bk%" == "1" echo.
if "%bk%" == "1" echo �����ʱ��ͣ���ڴ˲����������Ƿ�ɱ��������ء�
if "%bk%" == "1" echo.
if "%bk%" == "1" schtasks.exe /Delete /TN \CJH\TDocKiller /F
if "%bk%" == "1" schtasks.exe /create /tn \CJH\TDocKiller /xml "%~dp0TDocKiller.xml"
echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ��װ���Ե���ǰϵͳ����װ�����ʹ������Ա༭һ���رտμ�С���ߵĲ��ԣ�����Windows Vista���ϰ汾֧�֣���5����Զ�ѡ��Y��"
if errorlevel 1 set ac=1
if errorlevel 2 set ac=2
if "%ac%" == "1" if exist "%windir%\PolicyDefinitions\*.admx" call "%~dp0TDocKillerAdmxs.exe"

echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ������ݷ�ʽ����ʼ�˵���5����Զ�ѡ��Y��"
if errorlevel 1 set ad=1
if errorlevel 2 set ad=2
if "%ad%" == "1" if not exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����" md "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk"
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

copy /y "%~dp02-ж��.bat" "%programfiles%\CJH\TDocKiller\Uninstall.bat"
copy /y "%~dp03-�Զ���������.bat" "%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"
copy /y "%~dp0TDocKiller.xml" "%programfiles%\CJH\TDocKiller\TDocKiller.xml"

if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"":b.IconLocation=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)���ж�س����б�5����Զ�ѡ��Y��"
if errorlevel 1 set ae=1
if errorlevel 2 set ae=2
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayIcon /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayName /t REG_SZ /d "һ���رտμ�С���ߣ�TDocKiller��" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v Publisher /t REG_SZ /d "CJH" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v UninstallString /t REG_SZ /d "%programfiles%\CJH\TDocKiller\Uninstall.bat" /f

start /d "%programfiles%\CJH\TDocKiller" "" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

echo.
cls

echo.
echo ====================================================
echo               һ���رտμ�С���߰�װ����
echo ====================================================
echo.
echo ��װ��ɣ�������˳�... & pause > nul
goto enda

:x64
echo.
if exist "%windir%\TDocKiller.exe" choice /C YN /T 5 /D Y /M "��⵽��ǰϵͳ���ھɰ汾һ���رտμ�С���ߡ���(Y)��(N)Ҫɾ���ɰ�һ���رտμ�С���ߣ�5����Զ�ѡ��Y��"
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
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ����Զ������5����Զ�ѡ��Y��"
if errorlevel 1 set aa=1
if errorlevel 2 set aa=2
if "%aa%" == "1" echo.
if "%aa%" == "1" echo �����ʱ��ͣ���ڴ˲����������Ƿ�ɱ��������ء�
if "%aa%" == "1" echo.
if "%aa%" == "1" Reg add HKLM\Software\Microsoft\Windows\CurrentVersion\run /v TDocKiller /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
echo.
choice /C YN /T 5 /D N /M "��(Y)��(N)Ҫ�������ƻ����Զ��������Ӻ�ֻ���������ұߣ���װ��ɺ��������ʱ�ڿ�ʼ�˵���һ���رտμ�С��������Զ������������رո��������5����Զ�ѡ��N��"
if errorlevel 1 set bk=1
if errorlevel 2 set bk=2
if "%bk%" == "1" echo.
if "%bk%" == "1" echo �����ʱ��ͣ���ڴ˲����������Ƿ�ɱ��������ء�
if "%bk%" == "1" echo.
if "%bk%" == "1" schtasks.exe /Delete /TN \CJH\TDocKiller /F
if "%bk%" == "1" schtasks.exe /create /tn \CJH\TDocKiller /xml "%~dp0TDocKiller.xml"
echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ��װ���Ե���ǰϵͳ����װ�����ʹ������Ա༭һ���رտμ�С���ߵĲ��ԣ�����Windows Vista���ϰ汾֧�֣���5����Զ�ѡ��Y��"
if errorlevel 1 set ac=1
if errorlevel 2 set ac=2
if "%ac%" == "1" if exist "%windir%\PolicyDefinitions\*.admx" call "%~dp0TDocKillerAdmxs.exe"

echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)Ҫ������ݷ�ʽ����ʼ�˵���5����Զ�ѡ��Y��"
if errorlevel 1 set ad=1
if errorlevel 2 set ad=2
if "%ad%" == "1" if not exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����" md "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С���ߣ�32λ��.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С���ߣ�32λ��.lnk"
if "%ad%" == "1" if exist "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk" del /q "%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk"
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С����.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")
if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\һ���رտμ�С���ߣ�32λ��.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\x86\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller\x86"":b.Save:close")

copy /y "%~dp02-ж��.bat" "%programfiles%\CJH\TDocKiller\Uninstall.bat"
copy /y "%~dp03-�Զ���������.bat" "%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"
copy /y "%~dp0TDocKiller.xml" "%programfiles%\CJH\TDocKiller\TDocKiller.xml"

if "%ad%" == "1" call mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(""%systemdrive%\ProgramData\Microsoft\Windows\Start Menu\Programs\һ���رտμ�С����\�����Զ�����.lnk""):b.TargetPath=""%programfiles%\CJH\TDocKiller\AutoBootMgr.bat"":b.IconLocation=""%programfiles%\CJH\TDocKiller\TDocKiller.exe"":b.WorkingDirectory=""%programfiles%\CJH\TDocKiller"":b.Save:close")

echo.
choice /C YN /T 5 /D Y /M "��(Y)��(N)���ж�س����б�5����Զ�ѡ��Y��"
if errorlevel 1 set ae=1
if errorlevel 2 set ae=2
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayIcon /t REG_SZ /d "%programfiles%\CJH\TDocKiller\TDocKiller.exe" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v DisplayName /t REG_SZ /d "һ���رտμ�С���ߣ�TDocKiller��" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v Publisher /t REG_SZ /d "CJH" /f
if "%ae%" == "1" Reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\TDocKiller /v UninstallString /t REG_SZ /d "%programfiles%\CJH\TDocKiller\Uninstall.bat" /f

start /d "%programfiles%\CJH\TDocKiller" "" "%programfiles%\CJH\TDocKiller\TDocKiller.exe"

echo.
cls

echo.
echo ====================================================
echo                һ���رտμ�С���߰�װ����
echo ====================================================
echo.
echo ��װ��ɣ�������˳�... & pause > nul
goto enda

:enda