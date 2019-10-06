using Microsoft.AspNetCore.Mvc;

namespace StoryServer
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {
        private readonly WelcomeMessage welcomeMessage;
        public WelcomeController(WelcomeMessage message){
            welcomeMessage = message;
        }
        
        [HttpGet]
        public string Get() => welcomeMessage.message;
    }
}