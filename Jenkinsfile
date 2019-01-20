properties([
    parameters([
        string (name: 'branchName', defaultValue: 'master', description: 'Branch to get the tests from')
    ])
])

def isFailed = false
def branch = params.branchName
def pathFirst = "C:/Program Files (x86)/Jenkins/workspace/Student Pipeline/PhpTravels.UITests/bin/Debug"
def buildArtifactsFolder = "C:/BuildPackagesFromPipeline/$BUILD_ID"
currentBuild.description = "Branch: $branch"

def RunNUnitTests(String pathToDll, String condition, String reportName)
{
    try
    {
        bat "E:/NUnit.Console-3.9.0/nunit3-console.exe $pathToDll $condition --result=$reportName"
    }
    finally
    {
        stash name: reportName, includes: reportName
    }
}

node('master') 
{
    stage('Checkout')
    {
		git 'https://github.com/MaksymDranivskyi/Homework_L12.git'
    }
    
		stage('Restore NuGet')
    {
       powershell ".\\build.ps1 RestorePackages"
    }

    stage('Build Solution')
    {
        powershell ".\\build.ps1 Build"
    }

    stage('Copy Artifacts')
    {
         powershell ".\\build.ps1 CopyArtifacts -BuildArtifactsFolder $buildArtifactsFolder"
    }
	
}

catchError
{
    isFailed = true
    stage('Run Tests')
    {
        parallel FirstTest: {
            node('Slave') {
                RunNUnitTests("$buildArtifactsFolder/PhpTravels.UITests.dll", "--where cat==FirstTest", "TestResult1.xml")
            }
        }, SecondTest: {
            node('master') {
                RunNUnitTests("$buildArtifactsFolder/PhpTravels.UITests.dll", "--where cat==SecondTest", "TestResult2.xml")
            }
        }
    }

    isFailed = false
}

node()
{
    stage('Reporting')
    {
        unstash "TestResult1.xml"
        unstash "TestResult2.xml"

         archiveArtifacts '*.xml'

        nunit testResultsPattern: 'TestResult2.xml,TestResult1.xml'

    }
}

