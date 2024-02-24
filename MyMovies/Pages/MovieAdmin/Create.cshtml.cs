﻿using System;
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

        public CreateModel(MyMovies.Data.MyMoviesContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the Publish Date for the photo
            Movie.PublishDate = DateTime.Now;

            //
            // Upload file to server
            //

            // Make a unique filename for the photo
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff_") + FileUpload.FileName;

            // Update Photo object to include the photo filename
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
