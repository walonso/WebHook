using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;

namespace webhookGitHubDropbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DropBoxController : ControllerBase
    {        
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
