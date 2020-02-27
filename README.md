# About the Project


## Requirements
 - Visual Studio 2017 (or higher)

## Project Setup

Below are the necessary steps to be taken to download and execute the backend project (webapi). The steps are:

 - Clone this project to your work space using the comand: `git clone https://github.com/modest-coder/bliss-application-challenge.git`
  - Navigate to the directory "your_workspace/bliss-application-challenge/BackEnd" and open the project solution (BackEnd.sln) with a double click on it
  - Once it is open, build the solution by executing the shortcut "CTRL + SHIFT + B"
  - Before executing the application make sure to select the kestrel web server to run the application. See how you can do that by the image below: ![Kestrel Selected](https://github.com/modest-coder/bliss-application-challenge/documentation-assets/images/select-kestrel.png)
  - After selecting the kestrel web server you only need to click on this to start the application (the first execution will take a while because it makes some setups related to the execution environment but the next executions will be much faster)

#Back-end TODO List:
 - To implement the share route
 - Implement the method to validate the required fields of the entity
 - Put the database connection string as a setting in the appsettings.json
 - Inspect the behaviour/expected response on the mock api (and adapt my api to have the same behaviour/response)
 - Implement route tests to check both api's (my and the provided one)
 - Publish my api in a free host that supports sqlite on it

#Entity Framework Configuration:
 - https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli