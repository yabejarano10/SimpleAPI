using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp
{
    public class DetailModel : PageModel
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;
        public DetailModel(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        public AssetDetailModel modelDetail { get; set; }
        public void OnGet(int id)
        {
            var asset = _assets.GetById(id);
            var currentHolds = _checkouts.GetCurrentHolds(id)
                .Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id).ToString("d"),
                    PatronName = _checkouts.GetCurrentHoldPatronName(a.Id)
                });

            modelDetail = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.GetAuthorOrDirector(id),
                CurrentLocation = _assets.GetCurrentLocation(id).Name,
                DeweyCallNumber = _assets.GetDeweyIndex(id),
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                ISBN = _assets.GetIsbn(id),
                PatronName = _checkouts.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds
                
            };
        }

        public IActionResult OnGetCheckIn(int assetid)
        {
            _checkouts.CheckInItem(assetid);
            return RedirectToPage("/Catalog/Detail", new { id = assetid });
        }
    }
}