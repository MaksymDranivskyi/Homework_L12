using Atata;
using OpenQA.Selenium;

namespace PhpTravels.UITests.Components
{
    [ControlDefinition("div", ContainingClass = "select2-container", ComponentTypeName = "auto-complete")]
    public class AutoCompleteSelect<TOwner> : EditableField<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        [FindByClass("select2-focused", ScopeSource = ScopeSource.Page)]
        public TextInput<TOwner> AssociatedInput { get; private set; }

        [FindByClass("select2-chosen")]
        public Text<TOwner> SelectedText { get; private set; }

        [FindByClass("select2-results", ScopeSource = ScopeSource.Page)]
      
        public UnorderedList<DropDownItem, TOwner> DropDownItems { get; private set; }

        protected override string GetValue()
        {
            string value = SelectedText.Value;
            return ConvertStringToValue(value);
        }

        protected override void SetValue(string value)
        {
            Click();

            string valueAsString = ConvertValueToString(value);
            AssociatedInput.Set(valueAsString);
            //AssociatedInput.Set("London");
            DropDownItems.Items.Count.Should.BeGreater(0);

            Owner.Press(Keys.Enter);
        }

        [ControlDefinition("li", ContainingClass = "select2-result-selectable", ComponentTypeName = "item")]
        public class DropDownItem : ListItem<TOwner>
        {
        }
    }
}
