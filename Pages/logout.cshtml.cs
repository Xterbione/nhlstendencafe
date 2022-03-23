using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nhl_stenden_cafe.Pages;

public class LogoutModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public LogoutModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
