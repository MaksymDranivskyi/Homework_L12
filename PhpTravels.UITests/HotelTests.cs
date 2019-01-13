using Atata;
using NUnit.Framework;
using PhpTravels.UITests.Components;
using System;
using System.Collections;
namespace PhpTravels.UITests
{
    public class HotelTests : UITestFixture
    {
        [SetUp]
        public void Login()
        {
            LoginAsAdmin();
        }

       
        [Test, Category("FirstTest")]
        [Order(1)]

        public void  Hotel_Add()
        {
            HotelCreate();
        }    
          
        [Test, Category("SecondTest")]
        [Order(2)]
        public void Hotel_Edit()
        {
            string hotelName = HotelCreate();
            Go.To<HotelsPage>().
                Hotels.Rows[x => x.Name == hotelName].Edit.ClickAndGo().
                Location.Set("Washington").
                Submit().
           Hotels.Rows[x => x.Name == hotelName].Location.Should.Contain("Washington");
        }

        [Test]
        [Order(3)]
        public void Hotel_Room_Add()
        {


            string hotelName = HotelCreate();
           Go.To<RoomPage>().
                Add.ClickAndGo().
                Hotel.Set(hotelName).
                RoomType.Set("Two-Bedroom Apartment").
                BasicPrice.SetRandom(out int price).
                Submit().
            Rooms.Rows[x => x.Hotel == hotelName].Price.Should.Equals(price);

        }

        public string HotelCreate()
        {
            Go.To<HotelsPage>().
             Add.ClickAndGo().
                HotelName.SetRandom(out string name).
                HotelDescription.SetRandom(out string description).
                Location.Set("London").
                HotelStars.Set("3").
                HotelType.Set("Motel").
                FromDate.Set("28/12/2018").
                ToDate.Set("31/12/2018").
                Submit().
            Hotels.Rows[x => x.Name == name].Should.BeVisible();
            return name;
        }
    }
}
