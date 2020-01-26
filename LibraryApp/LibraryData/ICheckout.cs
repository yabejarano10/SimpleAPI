using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryData
{
    public interface ICheckout
    {
        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);
        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int assetId);

        void Add(Checkout newCheckout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);

        string GetCurrentCheckoutPatron(int assetId);
        string GetCurrentHoldPatronName(int id);
        bool IsCheckedOut(int id);
        DateTime GetCurrentHoldPlaced(int id);


    }
}
