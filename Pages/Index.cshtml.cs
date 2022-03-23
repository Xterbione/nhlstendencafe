using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repository;
using System.Text.Json;

namespace nhl_stenden_cafe.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }





    //properties:
    public IEnumerable<Category> Categories { get; set; } = new CategoryRepository().Get();
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



    /// <summary>
    /// does initial get requists. checks for session existance and all other initial tasks
    /// </summary>
    /// <returns></returns>
    /// 
    //de reden dat ik alles in get doe is omdat hij het anders niet default uitvoert. met post moet je
    //data aan leveren anders voert hij hem niet uit. ik weet dat het officieel met get hoort.
    public IActionResult OnGet()
    {




        //gets products by catid 2 to initialize product page with some default selections
        Products = new ProductRepository().GetByCatid(2);
        var GUID = HttpContext.Session.GetString("GUID");



        if (GUID == null || GUID == "")
        {
            return Redirect("login");
        }

        //gets raw session string in json
        var raworder = HttpContext.Session.GetString("order");



        if (raworder == null || raworder == "")
        {
            return Redirect("tablenumber");
        }





        //converts back to object
        Order sessionvalue = JsonSerializer.Deserialize<Order>(raworder);
        Tablenumber = sessionvalue.tafelnr;





        //checks if order has already been placed
        if (sessionvalue.isplaced == true)
        {
            return Redirect("betalen");
        }








        //checks product selected or not from menu
        if (ProductId != 0)
        {
            //checks if product is already in list and executes correct assignment
            if (sessionvalue.items.ContainsKey(ProductId))
            {
                sessionvalue.items[ProductId] = (sessionvalue.items[ProductId] + 1);

            }
            else
            {
                sessionvalue.items.Add(ProductId, 1);
            }
            HttpContext.Session.SetString("order", JsonSerializer.Serialize<Order>(sessionvalue));
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
            if (item.Key != 0)
                OrderItems.Add(new ProductRepository().Get(item.Key), item.Value);
        }






        //sets category to selected cat
        if (Categoryid != 0)
        {
            Products = new ProductRepository().GetByCatid(Categoryid);
        }

        return Page();
    }

}
