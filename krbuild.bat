@echo off
chcp 65001 >nul
echo ==============================================
echo 加载 VS2022 编译环境
echo ==============================================
call "D:\VS2022\Community\Common7\Tools\VsDevCmd.bat"
if errorlevel 1 (
    echo 错误：未找到VsDevCmd.bat，请检查VS安装路径！
    pause
    exit /b 1
)

echo.
echo ==============================================
echo 切换至源码目录
echo ==============================================
cd /d D:\Dev\KinoveaSrc\Kinovea
if errorlevel 1 (
    echo 错误：源码目录不存在！
    pause
    exit /b 1
)

echo.
echo ==============================================
echo 清理旧编译产物 Clean
echo ==============================================
msbuild Kinovea.VS2019.sln /t:Clean /p:Platform=x64;Configuration=Debug
if errorlevel 1 (
    echo 清理操作异常，继续执行后续流程...
)

echo.
echo ==============================================
echo 完整重新编译 Rebuild x64 Debug
echo ==============================================
msbuild Kinovea.VS2019.sln /t:Rebuild /p:Platform=x64;Configuration=Debug /v:normal
set build_result=%errorlevel%

echo.
if %build_result% equ 0 (
    echo ✅ 编译全部成功，即将启动 Kinovea Debug 程序
    echo ==============================================
    start "" "D:\Dev\KinoveaSrc\Kinovea\Kinovea\bin\x64\Debug\Kinovea.exe"
) else (
    echo ❌ 编译出现错误，无法启动程序，请查看上方日志修复问题
)

echo.
echo ==============================================
echo 脚本执行完毕
echo ==============================================
pause