using Atata;

namespace PhpTravels.UITests.Components
{
    using _ = LoginPage;

    public class LoginPage : Page<_>
    {
        [FindByName]
        public TextInput<_> Email { get; private set; }

        [FindByName]
        public PasswordInput<_> Password { get; private set; }

        public Button<_> Login { get; private set; }
    }
}
