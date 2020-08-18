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

13. Adding Nuget Package:
dotnet add package Microsoft.AspNetCore.WebHooks.Receivers.Dropbox --version 1.0.0-preview2-final

14. Add DropBox webhook (AddDropboxWebHooks):
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddNewtonsoftJson()
    .AddGitHubWebHooks()
    .AddDropboxWebHooks();
}

15. Add the DropBox controller and decorate it action method with DropboxWebHook attribute
public class DropBoxController : ControllerBase
{        
    [DropboxWebHook]
    public IActionResult DropboxHandler(string id, JObject data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok();
	}
}

17.
although the Controller is GitHubWebHookReceiverController, 
the webhook url is: http://f23940d9372b.ngrok.io/api/webhooks/incoming/dropbox

18 Create an application inside dropbox: (I called it as NetCoreWebhook)
https://www.dropbox.com/developers/apps

19. after creation of app, Get the app secret and add that to appsettings.json file:
"WebHooks": {
  "DropBox": {
    "SecretKey": {
      "default": "j5gco55if38p0ei"
    }
  }
 
20. Run the application net core.
 
21. Add the webhook url and add it.
example: http://f23940d9372b.ngrok.io/api/webhooks/incoming/dropbox

22. Add a breakpoint to the action method and upload a file to dropbox.


----------