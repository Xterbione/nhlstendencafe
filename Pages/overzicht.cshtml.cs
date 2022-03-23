using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repository;
using System.Text.Json;

namespace nhl_stenden_cafe.Pages
{
    public class overzichtModel : PageModel
    {

        //properties:
   
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public int Tablenumber { get; set; }
        public Dictionary<Product, int> OrderItems { get; set; } = new Dictionary<Product, int>();

        //binding get properties

        [BindProperty(SupportsGet = true)]
        public int Categoryid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int incproductid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int decproductid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int delproductid { get; set; }


        public IActionResult OnGet()
        {


            var GUID = HttpContext.Session.GetString("GUID");



            if (GUID == null || GUID == "")
            {
                return Redirect("login");
            }

            //gets raw session string in json
            var raworder = HttpContext.Session.GetString("order");

            //gets products by catid 2 to initialize product page with some default selections
            Products = new ProductRepository().GetByCatid(2);


            //checks if table is selected
            if (raworder == null || raworder == "")
            {
                return Redirect("tablenumber");
            }

            //converts back to object
            Order sessionvalue = JsonSerializer.Deserialize<Order>(raworder);
            Tablenumber = sessionvalue.tafelnr;

            if (sessionvalue.isplaced == true)
            {
                return Redirect("betalen");
            }
            //for incrementing existing
            if (incproductid != 0)
            {
                //checks if product is already in list and executes correct assignment
                if (sessionvalue.items.ContainsKey(incproductid))
                {
                    sessionvalue.items[incproductid] = (sessionvalue.items[incproductid] + 1);

                }

                //sets session again and reloads page
                HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(sessionvalue));
            }

            //for decrement existing
            if (decproductid != 0)
            {
                //checks if product is already in list and executes correct assignment
                if (sessionvalue.items.ContainsKey(decproductid))
                {
                    if (sessionvalue.items[decproductid] > 1)
                    {
                        sessionvalue.items[decproductid] = (sessionvalue.items[decproductid] - 1);
                    }
                    else
                    {
                        sessionvalue.items.Remove(decproductid);
                    }

                }

                //sets session again and reloads page
                HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(sessionvalue));
            }


            //for deleting:
            if (delproductid != 0)
            {
                //checks if product is already in list and executes correct assignment
                if (sessionvalue.items.ContainsKey(delproductid))
                {
                    sessionvalue.items.Remove(delproductid);
                }

                //sets session again and reloads page
                HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(sessionvalue));
            }

            //gets all products from session and puts them in the model's property
            foreach (KeyValuePair<int, int> item in sessionvalue.items)
            {
                if (item.Key != null || item.Key != 0)
                    OrderItems.Add(new ProductRepository().Get(item.Key), item.Value);
            }

            
            return Page();
        }
    }
}
