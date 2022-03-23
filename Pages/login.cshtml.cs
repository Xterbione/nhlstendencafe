using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nhl_stenden_cafe.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public int Triggerpoint { get; set; } = 0;

        public void OnGet()
        {
        }
    }
}
