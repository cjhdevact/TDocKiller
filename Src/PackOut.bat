@echo off
echo 任意键打包 一键关闭课件小工具（TDocKiller）...
pause > nul
if exist %~dp0TDocKiller-Bin rd /s /q %~dp0TDocKiller-Bin
md %~dp0TDocKiller-Bin
copy %~dp0TDocKiller\bin\Release\TDocKiller.exe %~dp0TDocKiller-Bin\TDocKiller.exe
copy %~dp0TDocKiller\bin\x64\Release\TDocKiller.exe %~dp0TDocKiller-Bin\TDocKiller64.exe

copy %~dp0TDocKiller\files\1-安装.bat %~dp0TDocKiller-Bin\1-安装.bat
copy %~dp0TDocKiller\files\2-卸载.bat %~dp0TDocKiller-Bin\2-卸载.bat
copy %~dp0TDocKiller\files\3-自动启动管理.bat %~dp0TDocKiller-Bin\3-自动启动管理.bat
copy %~dp0TDocKiller\files\4-添加Userinit级自动启动该程序.bat %~dp0TDocKiller-Bin\4-添加Userinit级自动启动该程序.bat
copy %~dp0TDocKiller\files\5-删除Userinit级自动启动该程序.bat %~dp0TDocKiller-Bin\5-删除Userinit级自动启动该程序.bat
copy %~dp0TDocKiller\files\TDocKillerAdmxs.exe %~dp0TDocKiller-Bin\TDocKillerAdmxs.exe
copy %~dp0TDocKiller\files\TDocKiller.xml %~dp0TDocKiller-Bin\TDocKiller.xml

echo.
echo 完成！
echo 任意键退出...
pause > nul