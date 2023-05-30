using AutoMapper;
using Count.App.Models;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Count.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PostController(IPostService service, IUserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Feed()
        {
            var allPosts = await _service.AllPosts();

            return View(allPosts);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostBindingModel model)
        {
            var post = _mapper.Map<Post>(model);

            post.PostedOn = DateTime.Now;
            var author = await _userService.FindUserByUsername(User.Identity.Name);
            post.AuthorId = author.Id;
            if (post.Content.Length > 0)
            {
                if (post.Content.Length <= 20)
                    post.Summary = post.Content[..] + ".....";
                else
                    post.Summary = post.Content[..20] + ".....";
            }

            if (ModelState.IsValid)
            {
                await _service.CreatePost(post);
                return RedirectToAction("Feed", "Post");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            try
            {
                var findPost = await _service.FindPost(id);
                var currentUser = await _userService.FindUserByUsername(User.Identity.Name);
                if (findPost.AuthorId.Equals(currentUser.Id))
                {
                    var postVM = _mapper.Map<Post>(findPost);

                    return View(postVM);
                }
                ViewData["NotTheAuthor"] = "You can not edit this post, because you are not the author!";
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPost([FromForm] PostBindingModel model)
        {
            if (model.Id == null)
            {
                ViewData["NotTheAuthor"] = "You can not edit this post, because you are not the author!";
                return View();
            }

            var post = await _service.FindPost((int)model.Id);
            post.Title = model.Title;
            post.Content = model.Content;

            if (post.Content.Length > 0)
            {
                if (post.Content.Length <= 20)
                    post.Summary = post.Content[..] + ".....";
                else
                    post.Summary = post.Content[..20] + ".....";
            }

            if (ModelState.IsValid)
            {
                await _service.EditPost(post);
                return RedirectToAction("Feed", "Post");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPost(int id)
        {
            var post = await _service.FindPost(id);

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _service.FindPost(id);
            var currentUser = await _userService.FindUserByUsername(User.Identity.Name);
            if (post.AuthorId.Equals(currentUser.Id))
            {
                return View(post);
            }
            ViewData["NotTheAuthor"] = "You can not delete this post, because you are not the author!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost([FromForm] PostBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<Post>(model);
                await _service.DeletePost(post);
                return RedirectToAction("Feed", "Post");
            }
            return View();
        }
    }
}
