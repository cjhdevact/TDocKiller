@echo off
echo �������� һ���رտμ�С���ߣ�TDocKiller��...
pause > nul
if exist %~dp0TDocKiller-Bin rd /s /q %~dp0TDocKiller-Bin
md %~dp0TDocKiller-Bin
copy %~dp0TDocKiller\bin\Release\TDocKiller.exe %~dp0TDocKiller-Bin\TDocKiller.exe
copy %~dp0TDocKiller\bin\x64\Release\TDocKiller.exe %~dp0TDocKiller-Bin\TDocKiller64.exe

copy %~dp0TDocKiller\files\1-��װ.bat %~dp0TDocKiller-Bin\1-��װ.bat
copy %~dp0TDocKiller\files\2-ж��.bat %~dp0TDocKiller-Bin\2-ж��.bat
copy %~dp0TDocKiller\files\3-�Զ���������.bat %~dp0TDocKiller-Bin\3-�Զ���������.bat
copy %~dp0TDocKiller\files\4-���Userinit���Զ������ó���.bat %~dp0TDocKiller-Bin\4-���Userinit���Զ������ó���.bat
copy %~dp0TDocKiller\files\5-ɾ��Userinit���Զ������ó���.bat %~dp0TDocKiller-Bin\5-ɾ��Userinit���Զ������ó���.bat
copy %~dp0TDocKiller\files\TDocKillerAdmxs.exe %~dp0TDocKiller-Bin\TDocKillerAdmxs.exe
copy %~dp0TDocKiller\files\TDocKiller.xml %~dp0TDocKiller-Bin\TDocKiller.xml

echo.
echo ��ɣ�
echo ������˳�...
pause > nul