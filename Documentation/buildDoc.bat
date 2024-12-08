@echo off  
REM 切换到包含 docfx.json 的目录  
cd /d "J:/UnityWorks/RTCubeExtensions/Documentation"  

REM 运行 docfx 并启动本地服务器  
docfx docfx.json --serve --logLevel verbose 

REM 暂停窗口，防止自动关闭  
pause  