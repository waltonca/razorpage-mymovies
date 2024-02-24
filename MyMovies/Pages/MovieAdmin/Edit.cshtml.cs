using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovies.Data;
using MyMovies.Models;

namespace MyMovies.Pages.MovieAdmin
{
    public class EditModel : PageModel
    {
        private readonly MyMovies.Data.MyMoviesContext _context;
        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // Category select options
        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

        public EditModel(MyMoviesContext context)
        {
            _context = context;
            //
            // Populate the category select options
            //

            // get all the categories in table
            List<Category> categories = _context.Category.ToList();

            foreach (var category in categories)
            {
                CategoryOptions.Add(new SelectListItem
                {
                    Text = category.Title,
                    Value = category.CategoryId.ToString()
                });
            }
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie =  await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Set the category for the Photo object based on user's selection
            Category selectCategory = _context.Category.Single(m => m.CategoryId == Movie.Category.CategoryId);
            Movie.Category = selectCategory;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.MovieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
