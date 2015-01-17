using Akka.Actor;
using Bleeter.Actors;
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
    public class AccountController : ApiController
    {
        AuthRepository _repo;
        ActorSystem _system;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        [Route("~/account/register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserRegistrationModel registration)
        {
            var res = await _repo.RegisterUserAsync(registration.Username, registration.DisplayName, registration.Password);
            if (res.Succeeded)
            {
                var props = Props.Create(typeof(UserActor), registration.DisplayName);
                return this.Ok();
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}
