using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nhl_stenden_cafe.Pages
{
    public class Process_LoginModel : PageModel
    {
        public IActionResult OnGet(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (StaticUserRepository.GetUserExistanceUserName(username))
                {
                    var user = StaticUserRepository.GetUserByUserName(username);

                    if (user.Password == password)
                    {
                        HttpContext.Session.SetString("GUID", StaticUserRepository.GetUserByUserName(username).UniqueGuid.ToString());
                        return Redirect("/accountoverview");
                    }
                    else
                    {
                        return Redirect("/login?triggerpoint=2");
                    }
                }
                else
                {
                    return Redirect("/login?triggerpoint=1");
                }

            }
            else
            {
                return Redirect("/index");
            }

        }
    }
}
