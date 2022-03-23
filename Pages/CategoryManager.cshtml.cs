using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Pages.Models;
using nhl_stenden_cafe.Pages.Repository;

namespace nhl_stenden_cafe.Pages
{
    public class CategoryManagerModel : PageModel
    {
        public IEnumerable<Category> categories { get; set; }
        [BindProperty (SupportsGet = true)]
        public int catid { get; set; }   
        [BindProperty(SupportsGet = true)]
        public string edit { get; set; }
        [BindProperty (SupportsGet =true)]
        public int delete { get; set; } 

        [BindProperty (SupportsGet = true)]
        public string catinsert { get; set; }

        public IActionResult OnGet()
        {
            var catrepo = new CategoryRepository();

            var GUID = HttpContext.Session.GetString("GUID");


            if (GUID == null || GUID == "")
            {
                return Redirect("login");
            }
            if (edit != "" | edit != null)
            {
                if (catid != 0)
                {
                    var cat = new Category();
                    cat.Name = edit;
                    cat.CategoryId = catid;
                    catrepo.Update(cat);
                }
            }
            if (delete != 0)
            {
                catrepo.Delete(delete);
            }
            if (catinsert != "" && catinsert != null)
            {
                catrepo.Add(catinsert);
            }
            if (delete != 0 && delete != null)
            {
                catrepo.Delete(delete);
            }

            categories = catrepo.Get();
            return Page();

        }
    }
}
