using Bbing.Domain.Model;
using Bbing.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bbing.WebApi.Controllers
{
    public class TestController : BaseController
    {
        IPostsService postsService;
        public TestController(IPostsService _postsService) {
            postsService = _postsService;
        }
        public void TestEF()
        {
            Thread th = new Thread(() => {
                var posta = new Posts
                {

                };
                postsService.CurrentRepository.Insert(posta);
                postsService.CurrentRepository.Commit();
            });
            th.Start();
            var post = new Posts
            {

            };
            postsService.CurrentRepository.Insert(post);
            post = new Posts
            {

            };
            postsService.CurrentRepository.Insert(post);
            postsService.CurrentRepository.Commit();
        }
    }
}
