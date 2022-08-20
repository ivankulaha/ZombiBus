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
        var contact = await _repository.Find(id);
        
        if (contact != null)
        {
            _repository.Remove(contact);
            await _repository.SaveChanges();
        }

        await Task.CompletedTask;

        return RedirectToPage();
    }
}