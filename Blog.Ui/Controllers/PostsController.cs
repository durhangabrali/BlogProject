using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Models.Concrete;
using Blog.Business.Repositories;

namespace Blog.Ui.Controllers
{
    public class PostsController : Controller
    {        
        private readonly IPostRepository _postRepository;

        private readonly ICategoryRepository _categoryRepository;

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {           
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }        

        // GET: PostsController
        public IActionResult Index()
        {
            var posts = _postRepository.GetAll().AsQueryable().Include(p => p.Category);
            return View(posts);
        }

        // GET: PostsController/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: PostsController/Create
        public IActionResult Create()
        {
            // <option value="Id"> Name </option>
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll(),"Id","Name");
            return View();
        }

        // POST: PostsController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content,CreateDate,FullName,CategoryId,Id")] Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Add(post);
                _postRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll(),"Id","Name", post.CategoryId);
            return View(post);
        }

        // GET: PostsController/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll(),"Id","Name", post.CategoryId);
            return View(post);
        }

        // POST: PostsController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Content,CreateDate,FullName,CategoryId,Id")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _postRepository.Update(post);
                    _postRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_postRepository.Any(x=>x.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll(),"Id","Name", post.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: PostsController/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: PostsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _postRepository.Delete(id);
            _postRepository.Save();
            return RedirectToAction(nameof(Index));
        }       
    }
}
