# About the Project

This project aims to solve the challenge proposed by Bliss Applications so that my technical skills can be evaluated by the company.

## Project Setup

### Requirements
 - Dotnet Core 3.1+ (https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x64-installer)
 - Visual Studio 2017 or higher (https://visualstudio.microsoft.com/vs/older-downloads/)

Before doing the steps below make sure that you meet the requirements showed right above. Below are the necessary steps to be taken to download and execute the backend project (webapi). The steps are:

 - Clone this project to your work space using the comand: `git clone https://github.com/modest-coder/bliss-application-challenge.git`
  - Navigate to the directory "{your_workspace}/bliss-application-challenge/BackEnd" and open the project solution (BackEnd.sln) with a double click on it
  - Once it is open, build the solution by executing the shortcut "CTRL + SHIFT + B"
  - Before executing the application make sure to select the kestrel web server to run the application. See how you can do that by the image below:<br/>![Kestrel Selected](https://raw.githubusercontent.com/modest-coder/bliss-application-challenge/develop/documentation-assets/images/select-kestrel.png)

  - After selecting the kestrel web server you only need to click on this to start the application. The first execution will take a while because it makes some setups related to the execution environment but the next executions will be much faster.

### Interesting implementations on the back-end project
 - Custom Implementation of snake_case Naming Convention: https://stackoverflow.com/questions/58570189/is-there-a-built-in-way-of-using-snake-case-as-the-naming-policy-for-json-in-asp
 - Global Exception Filter: https://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/

# Back-end TODO List:
 - Publish my api in a free host that supports sqlite on it
 - Implement a convertion from snake case to pascal case and vice versa using Newtonsoft