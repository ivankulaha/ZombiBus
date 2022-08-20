using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;

namespace ZombiBus.Pages;

public class EditModel : PageModel
{
    private readonly IDeadLetterConnectionRepository _repository;

    public EditModel(IDeadLetterConnectionRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public DeadLetterConnection? Connection { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Connection = await _repository.Find(id.Value);
        
        if (Connection == null)
        {
            return NotFound();
        }
        
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Connection != null)
        {
            _repository.Update(Connection);

            try
            {
                await _repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repository.Find(Connection.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }
        }

        return RedirectToPage("./Index");
    }
}