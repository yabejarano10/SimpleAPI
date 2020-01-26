using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models.Patron;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp
{
    public class PatronDetailPageModel : PageModel
    {
        private IPatron _patron;
        public PatronDetailPageModel(IPatron patron)
        {
            _patron = patron;
        }
        public PatronDetailModel patron;
        public void OnGet(int id)
        {
            var p = _patron.Get(id);

            patron = new PatronDetailModel
            {
                LastName = p.LastName,
                FirstName = p.FirstName,
                Address = p.Address,
                HomeLibraryBranch = p.HomeLibraryBranch.Name,
                MermberSince = p.LibraryCard.Created,
                OverdueFees = p.LibraryCard.Fees,
                LibraryCardID = p.LibraryCard.Id,
                Telephone = p.TelephoneNumber,
                AssetsCheckedOut = _patron.GetCheckouts(id).ToList() ?? new List<LibraryData.Models.Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistory(id),
                Holds = _patron.GetHolds(id)
            };
        }
    }
}