using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repository;

namespace nhl_stenden_cafe.Pages
{
    public class ProductManagerModel : PageModel
    {
        public IEnumerable<Product> products { get; set; }
        [BindProperty (SupportsGet =true)]
        public int delete { get; set; } 

        public IEnumerable<Category> Categories { get; set; }

        [BindProperty (SupportsGet = true)]
        public string catinsert { get; set; }



        //for new product
        [BindProperty (SupportsGet = true)]
        public int cat {get;set;}
        [BindProperty (SupportsGet = true)]
        public string name { get; set; }  
        [BindProperty(SupportsGet = true)]
        public decimal price { get; set; }


        //for updating product
        [BindProperty(SupportsGet = true)]
        public int eprodid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ecat { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ename { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal eprice { get; set; }


        public IActionResult OnGet()
        {
            var prodrepo = new ProductRepository();
            var catrepo = new CategoryRepository();

            var GUID = HttpContext.Session.GetString("GUID");


            if (GUID == null || GUID == "")
            {
                return Redirect("login");
            }
            if (delete != 0)
            {
                //prodrepo.;
            }
            if (catinsert != "" && catinsert != null)
            {
                //catrepo.Add(catinsert);
            }
            if (delete != 0 && delete != null)
            {
                prodrepo.DeleteSingle(delete); 
            }
            if ( cat != 0 && name != null && name != "" && price != 0)
            {
                prodrepo.Add(name, cat, price);
            }
            if (eprodid != 0 && ename != null && ename != "" && eprice != 0 && eprodid != 0)
            {
                prodrepo.Update(eprodid, ename, ecat, eprice);
            }

            products = prodrepo.Get();
            Categories = catrepo.Get();
            return Page();

        }
    }
}
