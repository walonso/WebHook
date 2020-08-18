1. Create the project:
dotnet new webapi -o webhookGitHubDropbox

cd webhookGitHubDropbox

2. Run the project:
There are 2 possibilities:
- in VS Code click ctrl + f5.
- in VS Code go to Run -> start debugging.

Go to this site: https://localhost:5001/WeatherForecast

------------ GitHub ------------------------
https://dotnetthoughts.net/webhooks-in-aspnet-core/

3. Add GitHub nuget package
dotnet add package Microsoft.AspNetCore.WebHooks.Receivers.GitHub --version 1.0.0-preview2-final

4. Add Github method to startup
modify the startup - ConfigureServices method to configure endpoint to receive notifications.
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddGitHubWebHooks();
}

5. Create controller and action:
[GitHubWebHook]
        public IActionResult GitHubHandler(string id, string @event, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

6. Get public URL for the site 
As options, use Heroku (PaaS in cloud) or ngrok (downloadable client which provide a public URL).
If you use ngrok, you have to disable https, because it does not work with https.

- Comment: app.UseHttpsRedirection();
- In the launchSettings.json, remove the property “sslPort” in the “iisSettings”/”iisExpress” section or set its value to 0. Remember, these 2 changes are only required for development environment running on localhost.

open ngrok and put this command: 
ngrok http -host-header=localhost 5000


7. Add to appsettings:
"WebHooks": {
  "GitHub": {
    "SecretKey": {
      "default": "e0f0d18218fbcb031fa17f9fbc638a8be56be3db"
    }
  }
}


7. url:
although the Controller is GitHubWebHookReceiverController, 
the webhook url is: http://a285e4e6062d.ngrok.io/api/webhooks/incoming/github

8. Add MVC Newtonsoft to the project (this allows to capture the json coming from GitHub)
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.1.7

(if package is not added, as soon as github sends a notification, you receive this error:
webhook github The JSON value could not be converted to System.Collections.Generic.IEnumerable`1[Newtonsoft.Json.Linq.JToken])


9. Add Newtonsoft to startup:
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddNewtonsoftJson()
    .AddGitHubWebHooks();
}


10. Configure "Webhook" section on github:
For referece: https://www.talkingdotnet.com/webhooks-with-asp-net-core-dropbox-and-github/

11. run the application and add a breakpoint.

12. Add an issue to your github repository and you will see in vs code the breack point will be fired.
check data property to see all data
Json: (comment -> body)

------------------------- Drop box ---------------------------------------
https://www.talkingdotnet.com/webhooks-with-asp-net-core-dropbox-and-github/




puede ser el newtonsift:
https://dotnetcoretutorials.com/2019/12/19/using-newtonsoft-json-in-net-core-3-projects/
https://thecodebuzz.com/add-newtonsoft-json-support-net-core/

otros links:
https://github.com/dotnet/aspnetcore/issues/13293
https://github.com/graphql-dotnet/graphql-dotnet/issues/1439
https://github.com/readthedocs/readthedocs.org/issues/5426


http://a285e4e6062d.ngrok.io/GitHubWebHookReceiver/incoming/github

http://a285e4e6062d.ngrok.io/GitHubWebHookReceiver/incoming/github


http://a285e4e6062d.ngrok.io/api/GitHubWebHookReceiver/incoming/github


api/webhooks/incoming/<receiver>/

GitHubWebHookReceiver/incoming/github

http://a285e4e6062d.ngrok.io/api/webhooks/incoming/github
e0f0d18218fbcb031fa17f9fbc638a8be56be3db



https://www.talkingdotnet.com/webhooks-with-asp-net-core-dropbox-and-github/
https://dotnetthoughts.net/webhooks-in-aspnet-core/
https://ngohungphuc.wordpress.com/2019/01/02/github-webhook-with-asp-net-core-signalr/
https://devblogs.microsoft.com/aspnet/introducing-microsoft-asp-net-webhooks-preview/

https://www.twilio.com/docs/usage/tutorials/how-to-create-aspnet-mvc-webhook-project
https://blogs.encamina.com/desarrollandosobresharepoint/webhooks-que-son-y-como-utilizarlos-en-nuestros-desarrollos/
https://adamstorr.azurewebsites.net/blog/aspnetcore-webhooks-running-the-github-webhook



7. Configure GitHub webhook in GitHub


7. 
