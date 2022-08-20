using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZombiBus.Core;
using ZombiBus.Persistance;

namespace ZombiBus.Pages;

public class CreateModel : PageModel
{
    private readonly IDeadLetterConnectionRepository _repository;

    public CreateModel(IDeadLetterConnectionRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public DeadLetterConnection? Connection { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Connection != null) _repository.Add(Connection);
        await _repository.SaveChanges();

        return RedirectToPage("./Index");
    }
}