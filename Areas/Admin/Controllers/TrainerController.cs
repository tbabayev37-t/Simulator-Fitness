using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulator16TB.Contexts;
using Simulator16TB.Helpers;
using Simulator16TB.Models;
using Simulator16TB.ViewModels;

namespace Simulator16TB.Areas.Admin.Controllers;
[Area("Admin")]

public class TrainerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string FolderPath;
    public TrainerController(AppDbContext appDbContext, IWebHostEnvironment environment)
    {
       _context = appDbContext;
         _environment = environment;
        FolderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }
    public async Task<IActionResult> Index()
    {
        var trainers = await _context.Trainers.Select(x => new TranierGetVM()
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            Description = x.Description,
            Experience = x.Experience,
            ImagePath = x.ImagePath,
            CategoryName = x.Category.Name
        }).ToListAsync();
        return View(trainers);
    }
    public async Task<IActionResult> Create()
    {
        await SendCategoriesWithViewBag();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TrainerCreateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var IsExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!IsExistCategory)
        {
            ModelState.AddModelError("","Category is not valid");
            return View(vm);
        }
        if(vm.Image.Length > 2 * 1024 * 1024)
        {
            ModelState.AddModelError("Image", "Image size must be less than 2MB");
            return View(vm);
        }
        if (!vm.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "File must be an image");
            return View(vm);
        }
        string uniqueFileName = await vm.Image.FileUploadAsync(FolderPath);

        Trainer trainer = new()
        {
            Name = vm.Name,
            Surname = vm.Surname,
            Description = vm.Description,
            Experience = vm.Experience,
            ImagePath = uniqueFileName,
            CategoryId = vm.CategoryId
        };
        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        if (trainer == null)
        {
            return NotFound();
        }
        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync();

        string fullPath = Path.Combine(FolderPath, trainer.ImagePath);
        FileHelpers.FileDelete(fullPath);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var trainer =await _context.Trainers.FindAsync(id);
        if(trainer == null)
        {
            return NotFound();
        }
        TrainerUpdateVM vm = new()
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Surname = trainer.Surname,
            Description = trainer.Description,
            Experience = trainer.Experience,
            CategoryId = trainer.CategoryId

        };
        await SendCategoriesWithViewBag();
        return View(trainer);
    }
    [HttpPost]
    public async Task<IActionResult> Update(TrainerUpdateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var trainer = await _context.Trainers.FindAsync(vm.Id);
        if(trainer is null)
        {
            return BadRequest();
        }

        var isExistTrainer = await _context.Trainers.AnyAsync(x => x.Id == vm.CategoryId);
        if (!isExistTrainer)
        {
            ModelState.AddModelError("", "Category is not valid");
            return View(vm);
        }

        return RedirectToAction("Index");
    }
    private async Task SendCategoriesWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;
    }
}
