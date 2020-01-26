using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models.Branch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp
{
    public class BranchDetailPageModel : PageModel
    {
        private IlibraryBranch _branch;

        public BranchDetailPageModel(IlibraryBranch branch)
        {
            _branch = branch;
        }
        public BranchDetailModel model { get; set; }
        public void OnGet(int id)
        {
            var branch = _branch.Get(id);

            model = new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Telephone = branch.Telephone,
                OpenDate = branch.OpenDate.ToString("yyy-MM-dd"),
                NumberOfAssets = _branch.GetAssets(id).Count(),
                NumberOfPatrons = _branch.GetPatrons(id).Count(),
                TotalAssetValue = _branch.GetAssets(id).Sum(a => a.Cost),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branch.GetBranchHours(id)
            };
        }
    }
}