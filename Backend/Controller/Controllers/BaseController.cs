using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int? GetUserId()
        {
            int id;

            if (int.TryParse(User.Claims.First(i => i.Type == "UserId").Value, out id))
                return id;

            return null;
        }
    }
}
