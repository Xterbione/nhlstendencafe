using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Repository;
using System.Text.Json;

namespace nhl_stenden_cafe.Pages
{
    public class order_processModel : PageModel
    {
        //properties and bindings
        [BindProperty (SupportsGet = true)]
        public int direction { get; set; }  
        public int Tablenumber { get; private set; }


        //functionality
        public IActionResult OnGet()
        {
            //retrieve session info and deserialize
            var raworder = HttpContext.Session.GetString("order");
            var OrderRepo = new OrderRepository();



            //converts back to object
            Order sessionvalue = JsonSerializer.Deserialize<Order>(raworder);
            Tablenumber = sessionvalue.tafelnr;


            if (sessionvalue.items.Count < 1 || sessionvalue == null)
            {
                return Redirect("/");
            }
            else if (sessionvalue.isplaced == true)
            {
                return Redirect("betalen");
            }
            else
            {
                OrderRepo.AddOrder(sessionvalue.orderid.ToString(), sessionvalue.tafelnr);
                foreach (var item in sessionvalue.items)
                {
                    for (int i = 1; i <= item.Value; i++)
                    {
                        OrderRepo.AddEntry(item.Key, sessionvalue.orderid.ToString());
                    }
                }
                sessionvalue.isplaced = true;
                HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(sessionvalue));
            }
            if (direction == 1 || direction == 0)
            {
                
                return Redirect("pbetalen");
            }
            else
            {
                return Redirect("betalen");
            }
            
        }
    }
}
