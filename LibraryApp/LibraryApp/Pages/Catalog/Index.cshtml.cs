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
    public class IndexModel : PageModel
    {
        private ILibraryAsset _assets;
        public IndexModel(ILibraryAsset assets)
        {
            _assets = assets;
        }
        public AssetIndexModel model { get; set; }
        public void OnGet()
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id)
                });

            model = new AssetIndexModel()
            {
                Assets = listingResult
            };

        }

       
    }
}