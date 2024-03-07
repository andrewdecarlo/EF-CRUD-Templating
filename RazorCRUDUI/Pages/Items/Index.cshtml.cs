using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Data;
using RazorCRUDUI.Models;

namespace RazorCRUDUI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly RazorCRUDUI.Data.ItemsContext _context;

        //Pass dbcontext into constructor
        public IndexModel(RazorCRUDUI.Data.ItemsContext context)
        {
            _context = context;
        }

        //Iterable list of items in the database
        public IList<ItemModel> ItemModel { get;set; } = default!;


        //Properties for search bar
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? ItemNames { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ItemName { get; set; }


        public async Task OnGetAsync()
        {
            //select all items from database
            var items = from i in _context.Items select i;

            //if search string is not null or empty, filter by search string
            if (!String.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.Name.Contains(SearchString));
            }

            //load query into ItemModel list asynchronously
            ItemModel = await items.ToListAsync();


            // **THE FOLLOWING ARE CLASS NOTES FOR MYSELF. DISREGARD.**

            //This loads all items in Items database that contain "test" to a list
            //ItemModel = await _context.Items.Where(i => i.Name.Contains("test")).ToListAsync();

            //This also filters for items containing "test", but just in a different syntax
            //Type error is because it needs to be converted to a list to match type
            //ItemModel = from i in _context.Items where i.Name.Contains("test") select i;

            //This fixes the problem of type mismatch by storing the first part in a variable, then converting to list
            //var list = await from i in _context.Items where i.Name.Contains("test") select i);
            //ItemModel = list.ToList();
            //Without intermediate variable
            //ItemModel = await(from i in _context.Items where i.Name.Contains("test") select i).ToListAsync();
        }

        // CHANGE FROM ASYNC METHOD TO NON-ASYNC METHOD
        //public void OnGetAsync()
        //{
        //    //This loads the entire Items database table to a list
        //    //ItemModel = _context.Items.ToListAsync();

        //    //This loads all items in Items database that contain "test" to a list
        //    ItemModel = _context.Items.Where(i => i.Name.Contains("test")).ToList();

        //    //This also filters for items containing "test", but just in a different syntax
        //    //Type error is because it needs to be converted to a list to match type
        //    //ItemModel = from i in _context.Items where i.Name.Contains("test") select i;

        //    //This fixes the problem of type mismatch by storing the first part in a variable, then converting to list
        //    var list = from i in _context.Items where i.Name.Contains("test") select i;
        //    ItemModel = list.ToList();
        //}
    }
}
