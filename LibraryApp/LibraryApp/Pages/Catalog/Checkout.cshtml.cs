using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models.Checkout;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp
{
    public class CheckoutModel : PageModel
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;
        public CheckoutModel(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        public Models.Checkout.CheckoutModel modelCheck { get; set; }
        
        public void OnGet(int id)
        {
            var asset = _assets.GetById(id);
            modelCheck = new Models.Checkout.CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };
           
        }


        public IActionResult OnPostPlaceCheckout(int assetId, int libraryCardId)
        {
            _checkouts.CheckOutItem(assetId, libraryCardId);
            return RedirectToPage("/Catalog/Detail", new { id = assetId });
        }

        public IActionResult OnPostMarkLost(int assetId, int libraryCardId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToPage("/Catalog/Detail", new { id = assetId });
        }
        public IActionResult OnPostMarkFound(int assetId, int libraryCardId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToPage("/Catalog/Detail", new { id = assetId });
        }
    }
}