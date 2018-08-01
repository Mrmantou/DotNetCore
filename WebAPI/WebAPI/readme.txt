

A change to the ASP.NET Core project's launch settings may be required to test the HTML page locally. Open launchSettings.json in the Properties directory of the project. Remove the launchUrl property to force the app to open at index.html—the project's default file.

eg:
"IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      //"launchUrl": "api/values",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }