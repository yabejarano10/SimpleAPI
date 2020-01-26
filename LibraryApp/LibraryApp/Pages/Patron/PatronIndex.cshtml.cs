using LibraryApp.Models.Patron;
using LibraryData;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace LibraryApp
{
    public class PatronIndex : PageModel
    {
        private IPatron _patron;
        public PatronIndex(IPatron patron)
        {
            _patron = patron;
        }
        public PatronIndexModel patrons { get; set; }
        public void OnGet()
        {
            var allPatrons = _patron.GetAll();
            var patronModels = allPatrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardID = p.LibraryCard.Id,
                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name
            }).ToList();

            patrons = new PatronIndexModel
            {
                patrons = patronModels
            };
        }
    }
}