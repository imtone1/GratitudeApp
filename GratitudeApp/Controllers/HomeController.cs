using GratitudeApp.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GratitudeApp.Controllers
{
    public class HomeController : Controller
    {
        private gratitudeEntities db = new gratitudeEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // Kirjautuminen ja sessioiden luominen
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kayttajat LoginModel)
        {
            try
            {
                //salasanan hash
                var crpwd = "";
                var salt = Hmac.GenerateSalt();
                var hmac1 = Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(LoginModel.password), salt);
                crpwd = (Convert.ToBase64String(hmac1));

                //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
                var LoggedUser = db.Kayttajat.SingleOrDefault(x => x.username == LoginModel.username && x.password == crpwd);

                if (LoggedUser != null)
                {

                    Session["UserName"] = LoggedUser.username;
                    
                    Session["LoggedUser"] = LoggedUser.username;

                    ViewBag.LoginMessage = "Successfull login";
                    ViewBag.LoggedStatus = "In";
                    ViewBag.LoginError = 0;

                    return RedirectToAction("Create", "Kayttajats"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa


                }
                else
                {
                    ViewBag.LoginMessage = "Login unsuccessfull";
                    ViewBag.LoggedStatus = "Out";
                    ViewBag.LoginError = 1;
                    return RedirectToAction("Create", "Kayttajats");

                    //return View("Login", LoginModel);
                }
            }
            catch
            {
                TempData["Errori"] = "Kirjautuminen epäonnistui!";
                TempData["BodyText1"] = "Tarkista käyttäjätunnus ja salasana.";

                return RedirectToAction("Create", "Kayttajats");
            }
        }
    }
}