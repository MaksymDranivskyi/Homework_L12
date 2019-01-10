using Atata;

namespace PhpTravels.UITests.Components
{
    using _ = RoomAddPage;

    public class RoomAddPage : Page<_>
    {
        [FindByName(TermCase.LowerMerged)]
        public NumberInput<_> BasicPrice { get; private set; }

        [FindById("s2id_autogen3")]
        public AutoCompleteSelect<_> Hotel { get; private set; }

        [FindById("s2id_autogen1")]
        public AutoCompleteSelect<_> RoomType { get; private set; }

        public ButtonDelegate<RoomPage, _> Submit { get; private set; }
    }
}
