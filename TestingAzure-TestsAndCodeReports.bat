 REM Path variables
 SET FolderPath=%~dp0
 SET ResultsPath=%FolderPath%\UnitTestResults
 SET CoverageHistoryPath=%FolderPath%\CoverageHistory
 
 SET NunitPath=%FolderPath%\packages\NUnit.ConsoleRunner.3.8.0\tools
 SET ReportUnitPath=%FolderPath%\packages\ReportUnit.1.2.1\tools
 SET OpenCoverPath=%FolderPath%\packages\OpenCover.4.6.519\tools
 SET ReportGeneratorPath=%FolderPath%\packages\ReportGenerator.3.1.2\tools
 SET ConverterCoverToCobertura=%FolderPath%\packages\OpenCoverToCoberturaConverter.0.2.6.0\tools
 
 SET UnitTestProj=%FolderPath%\TestingAzure.Tests\bin\Debug\net461\TestingAzure.Tests.dll
 
 REM Recreate Results Folder
 rd /S /Q %ResultsPath%
 md %ResultsPath%
 
 REM Create coverage history folder if not exists
 if not exist "%CoverageHistoryPath%" mkdir %CoverageHistoryPath%
 
 REM Run Nunit3 for tests, it produces TestResult.xml report
 %NunitPath%\nunit3-console.exe %UnitTestProj% --result=%ResultsPath%\TestResult.xml
 
 REM Get nunit command errorlevel
 SET NunitError=%ERRORLEVEL%
 
 REM Run ReportUnit to create HTML Report from Nunit XML report
 %ReportUnitPath%\ReportUnit.exe %ResultsPath%\TestResult.xml
 
 REM Run OpenCover to create coverage XML report
 %OpenCoverPath%\OpenCover.Console.exe -register:user -register:user -target:%NunitPath%\nunit3-console.exe -targetargs:%UnitTestProj% -filter:"+[*]* -[FluentNHibernate*]*" -output:%ResultsPath%\opencovertests.xml
 
 REM Run ReportGenerator to create coverage HTML report from coverage XML
 %ReportGeneratorPath%\ReportGenerator.exe -reports:%ResultsPath%\opencovertests.xml -targetDir:%ResultsPath% -historydir:%CoverageHistoryPath%
 
 %ConverterCoverToCobertura%\OpenCoverToCoberturaConverter.exe -input:%ResultsPath%\opencovertests.xml -output:%ResultsPath%\Cobertura.xml -sources:%ResultsPath%
 
 
 REM Fail if Nunit has found an error on tests
 exit /b %NunitError%