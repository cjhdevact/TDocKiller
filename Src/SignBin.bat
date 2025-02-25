::Tips Set the CSIGNCERT as your path.
@echo off
path D:\ProjectsTmp\SignPack;%path%
echo 任意键签名 一键关闭课件小工具（TDocKiller）...
pause > nul
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKiller.exe"
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKiller64.exe"
cmd.exe /c signcmd.cmd "%CSIGNCERT%" "%~dp0TDocKiller-Bin\TDocKillerAdmxs.exe"
echo.
echo 完成！
echo 任意键退出...
pause > nul