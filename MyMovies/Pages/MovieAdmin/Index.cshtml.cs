using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyMovies.Data;
using MyMovies.Models;

namespace MyMovies.Pages.MovieAdmin
{
    public class IndexModel : PageModel
    {
        private readonly MyMovies.Data.MyMoviesContext _context;

        public IndexModel(MyMovies.Data.MyMoviesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Add an Include() to join the Movie to Category when executing the SQL statement.
            Movies = await _context.Movie.Include("Category").ToListAsync();
        }
    }
}
