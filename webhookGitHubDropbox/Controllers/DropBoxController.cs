using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;
using Dropbox.Api;
using System;

namespace webhookGitHubDropbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DropBoxController : ControllerBase
    {    
        [Route("login")]
        public IActionResult Login()
        {
            string state = "b332bdfad2af48e3914fc181cf13b2c4";
            var redirect = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, "py29t3lsbgqyi1s", new Uri("https://97c9d96e6107.ngrok.io/dropbox/logging"), state);
            return Redirect(redirect.ToString());
            //return Redirect("https://www.dropbox.com/1/oauth2/authorize?client_id=py29t3lsbgqyi1s&response_type=code&redirect_uri=https://97c9d96e6107.ngrok.io/dropbox/logging&state=1");            
        }

        [Route("logging")]
        public IActionResult logging(string code, string state)
        {
            return Ok();
        }
    
        [DropboxWebHook]
        public IActionResult Dropbox(string id, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}