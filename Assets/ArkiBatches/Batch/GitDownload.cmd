@echo off
set help1=Usage: GitDownload.cmd [url] [fromDir] [toDir] [tag]
set help2=Example: GitDownload.cmd ssh://git@github.com/GameArki/FPMath.git Assets\com.gamearki.fpmath FPMath 1.3.0

@REM 如果参数总量不等于3, 输出 help 并退出  
set count=0
for %%a in (%*) do set /a count+=1
if %count% neq 4 (
    echo Args count must be 4!
    echo %help1%
    echo %help2%
    exit /b 1
)

@REM 获取第0个参数
set url=%1
set fromDir=%2
set toDir=%3
set tag=%4

set TMPDIR=tttttttmp_repo

@REM git clone %url% %toDir%
git clone %url% %TMPDIR%
git checkout -b %tag% %tag%

@REM 从 %fromDir% 复制到 %toDir%
xcopy %TMPDIR%\%fromDir% %toDir% /E /Y /I /Q

@REM 移除 %TMPDIR%
rmdir /S /Q %TMPDIR%

@echo on
@echo Download %url% to %toDir% success!