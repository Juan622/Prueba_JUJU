using System;
using Business;
using Microsoft.AspNetCore.Mvc;
using PostEntity = DataAccess.Data.Post;
using System.Collections.Generic;
using API.Validators;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    public class NewApiController : ControllerBase
    {
        private BaseService<PostEntity> PostService;

        public NewApiController(BaseService<PostEntity> postService)
        {
            PostService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultiplePosts([FromBody] List<PostEntity> entity, PostValidator postValidator)
        {
            for (int i = 0; i < entity.Count; i++)
            {
                await PostService.UserExisting(0, entity[i].CustomerId);
                entity[i].Body = postValidator.ValidatorBody(entity[i].Body);
                entity[i].Category = postValidator.ValidatorType(entity[i].Type, entity[i].Category);
            }
            var result = PostService.CreateMultiplePosts(entity);

            return Ok(result);
        }
    }
}

