using EtheralWeb.Data;
using EtheralWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EtheralWeb.Areas.admin.Controllers
{
    [Area("admin")]
    public class TesimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManger;


        public TesimonialController(AppDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManger = userManger;
        }
        public IActionResult Index()
        {
            return View(_context.Testimonials.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Testimonial model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType=="image/jpeg" || model.ImageFile.ContentType=="image/png")
                {
                    if (model.ImageFile.Length<= 3145728)
                    {
                        string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                        using (var stream=new FileStream(filePath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }

                        model.Image = fileName;
                        _context.Testimonials.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Sekilin yaddasi 3 mb cox olmaz");
                        return View(model);
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "You can only upload image file!");
                    return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "You can only upload image file!");
                return View(model);
            }

            return View();
        }

        public IActionResult Update(int? id)
        {
            Testimonial testimonial = _context.Testimonials.Find(id);
            return View(testimonial);
        }

        [HttpPost]
        public IActionResult Update(Testimonial model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType=="image/jpeg" || model.ImageFile.ContentType=="image/png")
                {
                    if (model.ImageFile.Length<= 3145728)
                    {
                        string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", model.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }
                        model.Image = fileName;
                        _context.Testimonials.Update(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can only upload max 3 mb file!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You can only upload image file!");
                    return View(model);
                 }
            }
            
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Testimonial testimonial = _context.Testimonials.Find(id);
            if (testimonial==null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", testimonial.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
