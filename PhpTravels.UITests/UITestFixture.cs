using System;
using Atata;
using NUnit.Framework;
using PhpTravels.UITests.Components;

namespace PhpTravels.UITests
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            // Find information about AtataContext set-up on https://atata-framework.github.io/getting-started/#set-up.
            AtataContext.Configure().
                ApplyJsonConfig<AppConfig>().
                UseBaseRetryTimeout(TimeSpan.FromSeconds(15)).
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        protected void LoginAsAdmin()
        {
            Go.To<LoginPage>().
                Email.Set(AppConfig.Current.AdminEmail).
                Password.Set(AppConfig.Current.AdminPassword).
                Login.Click();
        }
    }
}
