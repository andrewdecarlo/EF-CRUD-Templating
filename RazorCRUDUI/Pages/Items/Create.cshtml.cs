using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorCRUDUI.Data;
using RazorCRUDUI.Models;

namespace RazorCRUDUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly RazorCRUDUI.Data.ItemsContext _context;

        public CreateModel(RazorCRUDUI.Data.ItemsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Items.Add(ItemModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
