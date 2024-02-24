using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using MyMovies.Data;
using MyMovies.Models;

namespace MyMovies.Pages.MovieAdmin
{
    public class CreateModel : PageModel
    {
        private readonly MyMovies.Data.MyMoviesContext _context;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        [DisplayName("Upload Photo")]
        public IFormFile FileUpload { get; set; }

        // Category select options
        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

        public CreateModel(MyMovies.Data.MyMoviesContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

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

        public IActionResult OnGet()
        {
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            // Set the category for the Movie object based on user's selection
            Category selectCategory = _context.Category.Single(m => m.CategoryId == Movie.Category.CategoryId);
            Movie.Category = selectCategory;

            // Set the Publish Date for the Movie
            Movie.PublishDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the Publish Date for the Movie
            Movie.PublishDate = DateTime.Now;

            //
            // Upload file to server
            //

            // Make a unique filename for the Movie
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff_") + FileUpload.FileName;

            // Update Photo object to include the Movie filename
            Movie.FileName = filename;

            // Save the file
            string projectRootPath = _environment.ContentRootPath;
            string fileSavePath = Path.Combine(projectRootPath, "wwwroot","uploads", filename);

            // We use a "using" to ensure the filestream is disposed of when we're done with it
            using (FileStream fileStream = new FileStream(fileSavePath, FileMode.Create))
            {
                FileUpload.CopyTo(fileStream);
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
