using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZombiBus.Core;

namespace ZombiBus.Pages;

public class IndexModel : PageModel
{
    private readonly IDeadLetterConnectionRepository _repository;
    
    public IndexModel(IDeadLetterConnectionRepository repository)
    {
        _repository = repository;
    }

    public IList<DeadLetterConnection>? DeadLetterConnections { get; set; }

    public async Task OnGetAsync()
    {
        DeadLetterConnections = (await _repository.GetAll()).ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // var contact = await _context.Customer.FindAsync(id);
        //
        // if (contact != null)
        // {
        //     _context.Customer.Remove(contact);
        //     await _context.SaveChangesAsync();
        // }

        await Task.CompletedTask;

        return RedirectToPage();
    }
}