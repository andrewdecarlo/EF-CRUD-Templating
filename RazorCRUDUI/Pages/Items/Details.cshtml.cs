using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Data;
using RazorCRUDUI.Models;

namespace RazorCRUDUI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly RazorCRUDUI.Data.ItemsContext _context;

        public DetailsModel(RazorCRUDUI.Data.ItemsContext context)
        {
            _context = context;
        }

        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _context.Items.FirstOrDefaultAsync(m => m.ID == id);
            if (itemmodel == null)
            {
                return NotFound();
            }
            else
            {
                ItemModel = itemmodel;
            }
            return Page();
        }


        // SYNCHRONOUS ONGET METHOD
        //public IActionResult OnGet(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var itemmodel = _context.Items.FirstOrDefault(m => m.ID == id);
        //    if (itemmodel == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        ItemModel = itemmodel;
        //    }
        //    return Page();
        //}
    }
}
