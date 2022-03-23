using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;

namespace nhl_stenden_cafe.Pages
{
    public class AccountOverviewModel : PageModel
    {
       
        public string name { get; set; }   
        public string guid { get; set; }    
       
        public IActionResult OnGet()
        {
            string sessionvalue = HttpContext.Session.GetString("GUID");
            if (sessionvalue != null)
            {
                Guid userguid = new Guid(sessionvalue);
                CafeUser user = StaticUserRepository.GetUserByID(userguid);
                name = user.UserName;
                guid = user.UniqueGuid.ToString();
                return Page();
            }
            else
            {
                return Redirect("login"); 
            }

        }
    }
}
