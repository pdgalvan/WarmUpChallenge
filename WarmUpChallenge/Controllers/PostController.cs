using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarmUpChallenge.Data;
using WarmUpChallenge.Models;
using System.IO;
namespace WarmUpChallenge.Controllers
{
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly WarmUpChallengeDbContext _context;

        public PostController(WarmUpChallengeDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _enviroment = webHostEnvironment;
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Posts
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreationDate)
                .ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewBag.ListOfCategory = GetCategoryList();
            return View();
        }

        

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostVm postVm)
        {
            
            if (ModelState.IsValid)
            {
                var fileName = Path.Combine(_enviroment.ContentRootPath,
                    "Uploads", postVm.Title);

                await postVm.Image.CopyToAsync(new FileStream(fileName, FileMode.Create));
                
                Post post = new Post();
                post.Title = postVm.Title;
                post.Content = postVm.Content;
                post.CreationDate = postVm.CreationDate;
                post.Category = _context.Categories.FirstOrDefault(c => c.CategoryId == postVm.CategoryId);

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postVm);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewBag.ListOfCategory = GetCategoryList();
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }

        private List<SelectListItem> GetCategoryList()
        {
            var categoryList = (from category in _context.Categories
                                select new SelectListItem()
                                {
                                    Text = category.Name,
                                    Value = category.CategoryId.ToString()
                                }).ToList();
            categoryList.Insert(0, new SelectListItem()
            {
                Text = "--Seleccione--",
                Value = string.Empty
            });
            return categoryList;
        }
    }
}
