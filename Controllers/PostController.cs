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
    [Authorize(Roles = "Post")]
    public class PostController(IMapper mapper, IPostService postService, ILogger<PostController> logger) : Controller
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPostService _postService = postService;
        private readonly ILogger _logger = logger;

        [AllowAnonymous]
        public IActionResult Index(PostPageRequest pageRequest)
        {
            if (pageRequest is null) return BadRequest("Provide request data");
            IQueryable<Post>? posts = _postService.FindAllByPageRequest(pageRequest);
            PostIndexViewModel viewModel = MapViewModel(pageRequest, posts);
            return base.View(viewModel);
        }

        // GET: Post/Related
        public async Task<IActionResult> Related()
        {
            _logger.LogInformation("Fetching All Posts related to UserName at {DT}", DateTime.UtcNow.ToLongTimeString());
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            IEnumerable<Post> posts = await _postService.FindAllByUserName(currentUserName);
            return View( _mapper.Map<IEnumerable<PostViewDto>>(posts));
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
        public async Task<IActionResult> Create(PostCreateRequest postCreateRequest, IFormFile? imageFile)
        {
            _logger.LogInformation("Handle Request to create post {DT}", DateTime.UtcNow.ToLongTimeString());
            if (IsRequestEmpty(postCreateRequest)) return BadRequest("Request Data not provided");
            Post post = MapPost(postCreateRequest, imageFile);
            try
            {
                await _postService.CreatePost(post, postCreateRequest); // unrealistic warning, validated for nullability
            }
            catch (RequestFieldInvalidException requestFieldInvalidException)
            {
                foreach (KeyValuePair<string, string> error in requestFieldInvalidException.FieldErrors)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid) return RedirectToAction(nameof(Related));
            return View(_mapper.Map<PostCreateViewModel>(postCreateRequest));
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
        public async Task<IActionResult> Edit([Bind] PostEditRequest postEditRequest, IFormFile? ImageFile)
        {
            _logger.LogInformation("Handling request to edit post {DT}", DateTime.UtcNow.ToLongTimeString());
            if (IsEditRequestEmpty(postEditRequest)) return BadRequest("Must set Post data in request");
             postEditRequest.Post.ImageFile = ImageFile;
            try
            {
                await _postService.UpdatePost(postEditRequest);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (RequestFieldInvalidException requestFieldInvalidException)
            {
                foreach (KeyValuePair<string, string> error in requestFieldInvalidException.FieldErrors)
                    ModelState.AddModelError(error.Key, error.Value);
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Related));
            return View(_mapper.Map<PostEditViewModel>(postEditRequest));
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

        private Post MapPost(PostCreateRequest postCreateRequest, IFormFile? imageFile)
        {
            Post post = _mapper.Map<Post>(postCreateRequest.Post);
            post.Author = User.FindFirstValue(ClaimTypes.Name);
            postCreateRequest.Post.ImageFile = imageFile;
            return post;
        }

        private PostIndexViewModel MapViewModel(PostPageRequest pageRequest, IQueryable<Post> posts)
        {
            var postsDto = _mapper.Map<IEnumerable<PostViewDto>>(posts);
            IPagedList<PostViewDto> items = postsDto.ToPagedList(pageRequest.Page ?? 1, 3);
            PostIndexViewModel viewModel = _mapper.Map<PostIndexViewModel>(pageRequest);
            viewModel.Items = items;
            return viewModel;
        }

       private static bool IsEditRequestEmpty(PostEditRequest postEditRequest) => postEditRequest  is null || postEditRequest.Post is null;
       private static bool IsRequestEmpty(PostCreateRequest postCreateRequest) => postCreateRequest is null || postCreateRequest.Post is null;
    }
}
