using Atata;

namespace PhpTravels.UITests.Components
{
    using _ = HotelEditPage;

    public class HotelEditPage : Page<_>
    {
        [FindByName(TermCase.LowerMerged)]
        [RandomizeStringSettings("AT Hotel {0}")]
        public TextInput<_> HotelName { get; private set; }

        public RichTextEditor<_> HotelDescription { get; private set; }

        [FindById("s2id_searching")]
        public AutoCompleteSelect<_> Location { get; private set; }

        public ButtonDelegate<HotelsPage, _> Submit { get; private set; }

        [FindByName(TermCase.LowerMerged)]
        public Select<_> HotelStars { get; private set; }

        [FindByName(TermCase.LowerMerged)]
        public Select<_> HotelType { get; private set; }

        [FindByName("ffrom")]
        public TextInput<_> FromDate { get; private set; }

        [FindByName("fto")]
        public TextInput<_> ToDate { get; private set; }
    }
}
