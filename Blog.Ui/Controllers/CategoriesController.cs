using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Models.Concrete;
using Blog.Business;
using Blog.Business.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Ui.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BlogContext _context;

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(BlogContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        public IActionResult Details(Guid? id)
        {
            //if(!id.HasValue)  de kullanılabilir.
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {               
                _categoryRepository.Add(category);
                //Burada try-catch kullanmakta fayda var. Try-catch kullanıldığında if ile kontrol etmeye gerek yok
                try
                {
                    _categoryRepository.Save();
                     return RedirectToAction("Index");
                    //return View("Index", _categoryRepository.GetAll());
                }
                catch
                {
                    return BadRequest();
                }                
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _categoryRepository.Update(category);
                   _categoryRepository.Save();                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryRepository.Any(x=>x.Id == id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);  
                          
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //var category = _categoryRepository.GetById(id);    
            _categoryRepository.Delete(id);
            _categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // private bool CategoryExists(Guid id)
        // {
        //     return _context.Categories.Any(e => e.Id == id);
        // }
    }
}
