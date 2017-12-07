using RS1_P120_MobitelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Helper
{
    public class Autorizacija: FilterAttribute, IAuthorizationFilter
    {
        private readonly KorisnickeUloge[] _ulogeKorisnika;

        public Autorizacija(params KorisnickeUloge[] ulogeKorisnika)
        {
            _ulogeKorisnika = ulogeKorisnika;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if(k==null)
            {
                filterContext.HttpContext.Response.Redirect("/Autentifikacija");
                return;
            }
            foreach (KorisnickeUloge x in _ulogeKorisnika)
            {
                if (x == KorisnickeUloge.Administrator && k.Administrator != null)
                    return;
                if (x == KorisnickeUloge.Klijent && k.Klijent != null)
                    return;
            }
            filterContext.HttpContext.Response.Redirect("/Autentifikacija");
        }
    }

    public enum KorisnickeUloge
    {
        Administrator,
        Klijent
    }
}