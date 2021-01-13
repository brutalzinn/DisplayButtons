using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendAPI.Views
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public static void setButtonImage(int slot, DeckItemMisc item)

        {

            Debug.WriteLine("Clicado.");
        }
        
}
