::Tips Set the CSIGNCERT as your path.
@echo off
path D:\ProjectsTmp\SignPack;%path%
echo �����ǩ�� һ���رտμ�С���ߣ�TDocKiller��...
pause > nul
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKiller.exe"
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKiller64.exe"
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKillerAdmxs.exe"
echo.
echo ��ɣ�
echo ������˳�...
pause > nul