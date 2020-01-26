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
    public class BranchIndexModel : PageModel
    {
        private IlibraryBranch _branch;

        public BranchIndexModel(IlibraryBranch branch)
        {
            _branch = branch;
        }
        public LibraryApp.Models.Branch.BranchIndexModel allBranches;
        public void OnGet()
        {
            var branches = _branch.GetAll().Select(branch => new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                IsOpen = _branch.IsBranchOpen(branch.Id),
                NumberOfAssets = _branch.GetAssets(branch.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(branch.Id).Count()
            });

            allBranches = new LibraryApp.Models.Branch.BranchIndexModel
            {
                Branches = branches
            };


        }
    }
}