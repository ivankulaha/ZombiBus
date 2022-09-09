using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZombiBus.Core;

namespace ZombiBus.Pages;

public class IndexModel : PageModel
{
    private readonly IDeadLetterConnectionRepository _repository;
    private readonly IDeadLetterListenerScheduler _deadLetterListenerScheduler;

    public IndexModel(IDeadLetterConnectionRepository repository, IDeadLetterListenerScheduler deadLetterListenerScheduler)
    {
        _repository = repository;
        _deadLetterListenerScheduler = deadLetterListenerScheduler;
    }

    public IList<DeadLetterConnection>? DeadLetterConnections { get; set; }

    public async Task OnGetAsync()
    {
        DeadLetterConnections = (await _repository.GetAll()).ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var connection = await _repository.Find(id);
        
        if (connection != null)
        {
            _repository.Remove(connection);
            await _repository.SaveChanges();
        }

        return new PageResult();
    }
    
    public async Task<IActionResult> OnPostWatchAsync(int id)
    {
        var contact = await _repository.Find(id);
        await _deadLetterListenerScheduler.Start(contact!);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostStopWatchAsync(int id)
    {
        var contact = await _repository.Find(id);
        await _deadLetterListenerScheduler.Stop(contact!);

        return RedirectToPage();
    }
}