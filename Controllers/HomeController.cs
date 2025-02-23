using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NotesApp.Models;
using System;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly EncryptionService _encryption;

    public HomeController(AppDbContext context, EncryptionService encryption)
    {
        _context = context;
        _encryption = encryption;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.Include(c => c.Notes).ToList();
        return View(categories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorMessage = TempData["ErrorMessage"] as string
        };
        return View(errorViewModel);
    }

    [HttpPost]
    public IActionResult CreateCategory(string name)
    {
        try
        {
            if (!string.IsNullOrEmpty(name))
            {
                _context.Categories.Add(new Category { Name = name });
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddNote(Guid categoryId, string content)
    {
        try
        {
            if (!string.IsNullOrEmpty(content))
            {
                var category = _context.Categories.Find(categoryId);
                if (category == null)
                {
                    TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
                    return RedirectToAction("Error");
                }

                var encryptedContent = _encryption.Encrypt(content);
                _context.Notes.Add(new Note
                {
                    CategoryId = categoryId,
                    Content = encryptedContent
                });
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");
    }

    public IActionResult ViewNotes(Guid categoryId)
    {
        var category = _context.Categories.Include(c => c.Notes).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
            return RedirectToAction("Error");
        }

        foreach (var note in category.Notes)
        {
            note.Content = _encryption.Decrypt(note.Content);
        }

        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteNote(Guid noteId)
    {
        var note = _context.Notes.Find(noteId);
        if (note == null)
        {
            TempData["ErrorMessage"] = $"Note with ID {noteId} not found.";
            return RedirectToAction("Error");
        }

        _context.Notes.Remove(note);
        _context.SaveChanges();

        return RedirectToAction("ViewNotes", new { categoryId = note.CategoryId });
    }

    [HttpPost]
    public IActionResult DeleteCategory(Guid categoryId)
    {
        var category = _context.Categories.Include(c => c.Notes).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            TempData["ErrorMessage"] = $"Category with ID {categoryId} not found.";
            return RedirectToAction("Error");
        }

        _context.Notes.RemoveRange(category.Notes);
        _context.Categories.Remove(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // Новый экшен для просмотра всех заметок подряд.
    [HttpGet]
    public IActionResult MyNotes()
    {
        // Получаем все заметки из БД.
        var notes = _context.Notes.ToList();

        // Расшифровываем контент каждой заметки.
        foreach (var note in notes)
        {
            note.Content = _encryption.Decrypt(note.Content);
        }

        // Передаём список заметок в представление MyNotes.cshtml.
        return View(notes);
    }

    // Экшен для переключения тем (светлая / темная)
    [HttpGet]
    public IActionResult SetTheme(string theme)
    {
        // Запоминаем тему в куки на год.
        var options = new Microsoft.AspNetCore.Http.CookieOptions
        {
            Expires = DateTime.Now.AddYears(1)
        };
        Response.Cookies.Append("SiteTheme", theme, options);

        // Возвращаемся назад на предыдущую страницу (или на Index, если Referer отсутствует).
        var referer = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(referer))
            return RedirectToAction("Index");
        else
            return Redirect(referer);
    }
}
