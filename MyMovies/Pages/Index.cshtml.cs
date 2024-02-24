using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyMovies.Data;
using MyMovies.Models;

namespace MyMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data.MyMoviesContext _context;
        public IList<Movie> Movies { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, MyMoviesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {

            Movies = await _context.Movie.ToListAsync();
        }
    }
}
