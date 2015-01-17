using Akka.Actor;
using Bleeter.Messages;
using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bleeter.Controllers
{
    [RoutePrefix("bleet")]
    public class BleetController : ApiController
    {
        private ActorSystem _system;

        public BleetController()
        {
            _system = BleeterSystem.ActorSystem;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostBleet(PostBleetModel bleet)
        {
            var message = new PostBleetMessage(DateTime.UtcNow, bleet.Content);
            var username = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var actor = await _system.ActorSelection(String.Format("akka://bleeter/user/{0}", username)).ResolveOne(TimeSpan.FromSeconds(10.0));
            //var actor = _system.ActorSelection(String.Format("akka://bleeter/user/{0}", username));
            actor.Tell(message);
            return this.Ok();
        }

        [HttpGet]
        [Route("{bleetid}")]
        public IHttpActionResult RetrieveBleet(string bleetid)
        {
            return this.Ok();
        }

        [HttpDelete]
        [Route("{bleetid}")]
        public IHttpActionResult DeleteBleet(string bleetid)
        {
            return this.Ok();
        }
    }
}
