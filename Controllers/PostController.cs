using Microsoft.AspNetCore.Mvc;
using BlogPost.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlogPost.Models.ViewModels;
using BlogPost.DTOs;
using AutoMapper;
using BlogPost.ViewsModels;
using X.PagedList.Extensions;
using BlogPost.PostRequests;
using X.PagedList;
using BlogPost.Services;
using BlogPost.Exceptions;

namespace BlogPost.Controllers
{
    //[Authorize(Roles = "Post")]
    public class PostController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ILogger _logger;

        public PostController(IMapper mapper, IPostService postService, ILogger<PostController> logger)
        {
            _mapper = mapper;
            _postService = postService;
            _logger = logger;
        }
        
         //[AllowAnonymous]
         public IActionResult Index(PostPageRequest pageRequest)
        {
            IQueryable<Post>? posts = _postService.FindAllByPageRequest(pageRequest);
            PostIndexViewModel viewModel = MapViewModel(pageRequest, posts);
            return base.View(viewModel);
        }

        // GET: Post/Related
        public async Task<IActionResult> Related()
        {
            _logger.LogInformation("Fetching All Posts related to UserName at {DT}", DateTime.UtcNow.ToLongTimeString());
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            if (currentUserName == null) return BadRequest("Claims do not contain UserName");
            IEnumerable<Post> posts = await _postService.FindAllByUserName(currentUserName);
            var result = _mapper.Map<IEnumerable<PostViewDto>>(posts);
            return View(result);
        }

        // GET: Post/Details?id=
        public async Task<IActionResult> Details(Guid? id)
        {
             _logger.LogInformation("Handling details request for post {DT}", DateTime.UtcNow.ToLongTimeString());
            var post = await _postService.FindById(id);
            if (post == null) return NotFound();
            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            PostCreateViewModel postCreateViewModel = new PostCreateViewModel
            {
                Topics = _mapper.Map<List<CheckBox>>(_postService.FindAllTopics())
            };
            return View(postCreateViewModel);
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] PostCreateRequest postCreateRequest)
        {
            _logger.LogInformation("Handle Request to create post {DT}", DateTime.UtcNow.ToLongTimeString());
            var postCreateViewModel = _mapper.Map<PostCreateViewModel>(postCreateRequest);
            Post post = _mapper.Map<Post>(postCreateViewModel.Post);
            post.Author = User.FindFirstValue(ClaimTypes.Name);        
            try {
              await _postService.CreatePost(post, postCreateViewModel.Topics); // unrealistic warning, validated for nullability
            } catch (RequestFieldInvalidException requestFieldInvalidException){
                  foreach (KeyValuePair<string, string> error in requestFieldInvalidException.FieldErrors)
                        ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid) return RedirectToAction(nameof(Related));
            return View(postCreateViewModel);
        }

        // GET: Post/Edit?id=
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _postService.FindById(id);
            if (post == null) return NotFound();
            PostEditViewModel postEditViewModel = new PostEditViewModel
            {
                Post = _mapper.Map<PostEditDto>(post),
                Topics = _postService.ReturnRelatedTopics(id) // Calls Procedure to return all topics and related.
            };
            return View(postEditViewModel);
        }

        // POST: Post/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] PostEditRequest postEditRequest)
        {
            _logger.LogInformation("Handling request to edit post {DT}", DateTime.UtcNow.ToLongTimeString());
            var postEditViewModel = _mapper.Map<PostEditViewModel>(postEditRequest);
            try{
               await _postService.UpdatePost(postEditViewModel.Topics, Guid.Parse(postEditViewModel.Post.PostId));
            }catch (NotFoundException notFoundException){
                  return NotFound(notFoundException.Message);
            }catch (RequestFieldInvalidException requestFieldInvalidException){
                 foreach (KeyValuePair<string, string> error in requestFieldInvalidException.FieldErrors)
                        ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Related));
            return View(postEditViewModel);
        }

        // GET: Post/Delete?id=
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = _postService.FindById(id);
            if (post == null)
                return NotFound();
            return View(await post);
        }

        // POST: Post/Delete?id=
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _logger.LogInformation("Handling delete request for post {DT}", DateTime.UtcNow.ToLongTimeString());
            await _postService.DeleteById(id);
            return RedirectToAction(nameof(Related));
        }

        private PostIndexViewModel MapViewModel(PostPageRequest pageRequest, IQueryable<Post> posts)
        {
            var postsDto = _mapper.Map<IEnumerable<PostViewDto>>(posts);
            IPagedList<PostViewDto> items = postsDto.ToPagedList(pageRequest.Page ?? 1, 3);
            PostIndexViewModel viewModel = _mapper.Map<PostIndexViewModel>(pageRequest);
            viewModel.Items = items;
            return viewModel;
        }
    }
}
