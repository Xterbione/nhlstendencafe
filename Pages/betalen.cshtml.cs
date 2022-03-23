using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Repository;
using nhl_stenden_cafe.Pages.Repositorys;
using System.Text.Json;

namespace nhl_stenden_cafe.Pages
{
    public class betalenModel : PageModel
    {

        //init order list
        public IEnumerable<CafeCount> cafeCounts { get; set; } = Enumerable.Empty<CafeCount>();

        [BindProperty(SupportsGet = true)]
        public bool ending { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool pay { get; set; }
        //init repo
        OrderRepository orderRepo = new OrderRepository();

        /// <summary>
        /// all functionality
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {

            var GUID = HttpContext.Session.GetString("GUID");



            if (GUID == null || GUID == "")
            {
                return Redirect("login");
            }

            //gets raw session string in json
            var raworder = HttpContext.Session.GetString("order");
            if (raworder == null || raworder == "")
            {
                return Redirect("/");
            }
            //deserializing session
            Order sessionvalue = JsonSerializer.Deserialize<Order>(raworder);
            
            //checks if order is placed
            if (sessionvalue.isplaced == false)
            {
                return Redirect("/");
            }
            //for paying
            if (pay)
            {
                orderRepo.PayAll(sessionvalue.orderid);
            }
            if (ending == true)
            {
                HttpContext.Session.Remove("order");
                return Redirect("/");
            }

            cafeCounts = orderRepo.GetOrder(sessionvalue.orderid.ToString());
            System.Threading.Thread.Sleep(1000);
            return Page();


        }
    }
}
