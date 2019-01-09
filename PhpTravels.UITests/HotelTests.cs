using Atata;
using NUnit.Framework;
using PhpTravels.UITests.Components;

namespace PhpTravels.UITests
{
    public class HotelTests : UITestFixture
    {
        [Test]
        public void Hotel_Add()
        {
            LoginAsAdmin();

            Go.To<HotelsPage>().
                Add.ClickAndGo().
                    HotelName.SetRandom(out string name).
                    HotelDescription.SetRandom(out string description).
                    Location.Set("London").
                    Submit().
                Hotels.Rows[x => x.Name == name].Should.BeVisible();
        }
    }
}
