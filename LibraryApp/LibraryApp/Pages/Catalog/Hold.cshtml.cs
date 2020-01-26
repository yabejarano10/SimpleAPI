using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp
{
    public class HoldModel : PageModel
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;
        public HoldModel(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }
        public Models.Checkout.CheckoutModel modelHold { get; set; }
        public void OnGet(int id)
        {
            var asset = _assets.GetById(id);
            modelHold = new Models.Checkout.CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetCurrentHolds(id).Count()
            };
        }
        public IActionResult OnPostPlaceHold(int assetId, int libraryCardId)
        {
            _checkouts.PlaceHold(assetId, libraryCardId);
            return RedirectToPage("/Catalog/Detail", new { id = assetId });
        }
    }
}