using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizApi.Models;
using QuizApi.CommonLib;

namespace QuizApi.Controllers.Api
{
    public class UsersController : ApiController
    {
        private BLL bll; 
        public UsersController()
        {
            this.bll = new BLL();
        }

        public UsersController(BLL bll)
        {
            this.bll = bll;
        }

        // GET /api/users   
        public IEnumerable<User> GetUsers()
        {
            bll = new BLL();
            return bll.GetUsers();
        }

        [Route("api/Users/Register")]
        [HttpPost]
        public void Register(User user)
        {
            bll.Register(user);
        }


        [Route("api/Users/Login")]
        [HttpPost]
        public string Login(User user)
        {
            bll = new BLL();
            return bll.Login(user.Email, user.Password);
        }

    }
}
