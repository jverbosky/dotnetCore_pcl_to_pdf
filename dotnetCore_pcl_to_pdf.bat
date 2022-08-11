set _timestring=%TIME: =0%
set _timestamp=%date:~-4,4%-%date:~-10,2%-%date:~-7,2%_%_timestring:~0,2%-%_timestring:~3,2%-%_timestring:~6,2%

C:
cd C:\Prototypes\dotnetCore_pcl_to_pdf
dotnet run > .\debug\dotnetCore_pcl_to_pdf-debug_%_timestamp%.txt

:: get last backup file in .\debug\
cd debug
for /f %%i in ('dir /b/a-d/od/t:w') do set lastDebugLog=%%i >NUL

:: open file in Notepad for review
notepad %lastDebugLog%

:: return to ./ directory
cd ..