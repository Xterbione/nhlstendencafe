using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repositorys;
using System.Text.Json;
namespace nhl_stenden_cafe.Pages
{
    public class tablenumberModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Tablenumber { get; set; }



        public IActionResult OnGet()
        {
            string sessionvalue = HttpContext.Session.GetString("order");
            if (sessionvalue != null)
            {
                return Redirect("/index");
            }

            if (Tablenumber == 0)
            {
                return Page();
            }
            else
            {
                var order = new Order();
                order.orderid = GuidRepository.RandomString(12);
                order.tafelnr = Tablenumber;
                HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(order));
                return Redirect("/index");
            }   

        }
    }
}
