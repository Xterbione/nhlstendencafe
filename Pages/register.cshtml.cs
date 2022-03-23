using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhl_stenden_cafe.Models;

namespace nhl_stenden_cafe.Pages;

public class RegisterModel : PageModel
{
    public string stage { get; set; }
    public bool stageset { get; set; } = false;
    public int stagetype { get; set; } = 0;

    private readonly ILogger<IndexModel> _logger;

    public RegisterModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    //IActionResult
    public void OnGet(string username, string password1, string password2)
    {
        //checking if all fields aren't empty to verify a registration attempt
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password1) && string.IsNullOrWhiteSpace(password2)) 
            {
                stage = "";
                stageset = false;
            }
             else
            {
            //checking if one or more fields are empty
            if (string.IsNullOrWhiteSpace(username) || username == "" || string.IsNullOrWhiteSpace(password1) || password1 == "" || string.IsNullOrWhiteSpace(password2) || password2 == "")
            {
                //setting stage & stage message for failing fieldcheck
                stageset = true;
                stagetype = 1;
                stage = "oh oh.. een of meerdere velden zijn leeg";
            }
            else
            {
                //displaying the stage in case of error
                stage = "PASS 1";
                stageset = true;
                stagetype = 2;
                //checking if passwords match
                if (password1 == password2 && password1 != "" && password2 != "")
                {
                    stage = "STAGE 3 PASS";
                    if (password1.Length > 2)
                    {

                        //creating instance of CafeUser
                        CafeUser gebruiker = new CafeUser();
                        //setting all the static properties
                        gebruiker.UserName = username;
                        gebruiker.Password = password1;
                        gebruiker.Location = "NHL Stenden";
                        gebruiker.UniqueGuid = Guid.NewGuid();
                        //adds user to lobs his static user repository
                        var lastCheck = StaticUserRepository.AddUser(gebruiker);
                        if ( lastCheck == StaticUserRepository.AddUserResult.Success)
                        {
                            //setting stage message. stagetype has already been set
                            stage = "yay! registreren gelukt! je kan nu inloggen";
                        }
                        else if (lastCheck == StaticUserRepository.AddUserResult.UserNameIsNotUnique)
                        {
                            //setting stagetype to bad and stagemessage to the error code.
                            stagetype=1;
                            stage = "deze gebruikersnaam is al in gebruik!";
                        }
                        else if (lastCheck == StaticUserRepository.AddUserResult.GuidIsNotUnique)
                        {
                            stagetype = 1;
                            stage = "GuiD is niet uniek.. contacteer admin";

                        }
                        else
                        {
                            stagetype = 1;
                            stage = "huh? :" + lastCheck.ToString();
                        }


                    }
                    else
                    {
                        //setting stagemessage and stagetype
                        stagetype = 1;
                        stage = "je wachtwoord moet minimaal 3 characters lang zijn!";
                    }

                } 
                else
                {
                    //setting error message for password match failing
                    stage = "wachtwoorden zijn niet het zelfde";
                    stageset = true;
                    stagetype = 1;
                }

            }
        }

    }
}
