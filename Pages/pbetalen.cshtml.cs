using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Repository;
using nhl_stenden_cafe.Pages.Repositorys;
using System.Text.Json;

namespace nhl_stenden_cafe.Pages
{
    public class pbetalenModel : PageModel
    {






        [BindProperty(SupportsGet = true)]
        public List<ToPay> toPay { get; set; } 
        //init order list
        public IEnumerable<CafeCount> cafeCounts { get; set; } = Enumerable.Empty<CafeCount>();

        [BindProperty(SupportsGet = true)]
        public bool ending { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool pay { get; set; } = false;
        //init repo
        OrderRepository orderRepo = new OrderRepository();

        //for calculating how many products still have to be paid for exit validation
        public int toPayTotal { get; set; } = 0;


        /// <summary>
        /// all functionality
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet(List<ToPay> toPay)
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


            //deserialize the raw json order to an object
            Order sessionvalue = JsonSerializer.Deserialize<Order>(raworder);

            //checks if order is placed
            if (sessionvalue.isplaced == false)
            {
                return Redirect("/");
            }
            if (pay == true)
            {
               
            }
            if (ending == true)
            {
                HttpContext.Session.Remove("order");
                return Redirect("/");
            }

            //getting cafecount 
            var rawcafeCount = HttpContext.Session.GetString("CafeCount");

            if (rawcafeCount == null || rawcafeCount == "")
            {

            }
            else
            {

            }

            if (toPay.Any()) 
            {
                foreach (var single in toPay)
                {
                    if (single.prodid > 0 && single.amount > 0)
                    {
                        orderRepo.PaySpecific(sessionvalue.orderid, single.prodid, single.amount);
                    }                  
                }
            }

            cafeCounts = orderRepo.GetOrder(sessionvalue.orderid.ToString());
            //checks if out of topay



            return Page();




            //from here i increment and decrement the value of order;

        }
    }
}
