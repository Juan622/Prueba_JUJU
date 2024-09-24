using Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PostEntity = DataAccess.Data.Post;
using API.Validators;
using System.Threading.Tasks;

namespace API.Controllers.Post
{
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private BaseService<PostEntity> PostService;
        public PostController(BaseService<PostEntity> postService)
        {
            PostService = postService;
        }

        [HttpGet]
        public IQueryable<PostEntity> GetPosts(PostEntity entity)
        {
            return PostService.GetAll();
        }

        [HttpPost]
        public async Task<PostEntity> Create([FromBodyAttribute] PostEntity entity, PostValidator postValidator)
        {
            await PostService.UserExisting(0, entity.CustomerId);
            postValidator.ValidatorBody(entity.Body);
            postValidator.ValidatorType(entity.Type, entity.Category);
           return PostService.Create(entity);
        }

        [HttpPut]
        public PostEntity Update([FromBodyAttribute] PostEntity entity)
        {
            return PostService.Update(entity.CustomerId, entity, out bool changed);
        }

        [HttpDelete]
        public PostEntity Delete([FromBodyAttribute] PostEntity entity)
        {
            return PostService.Delete(entity);
        }
    }
}
