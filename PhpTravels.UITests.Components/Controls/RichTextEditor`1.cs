using Atata;

namespace PhpTravels.UITests.Components
{
    [ControlDefinition("iframe", ContainingClass = "cke_wysiwyg_frame")]
    [ControlFinding(typeof(FindFirstAttribute))]
    public class RichTextEditor<TOwner> : EditableField<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        [UseParentScope]
        [TraceLog]
        protected Frame<OrdinaryPage, TOwner> ContentFrame { get; set; }

        protected override string GetValue()
        {
            string content = null;
            ContentFrame.DoWithin(x => content = x.Content);
            return ConvertStringToValue(content);
        }

        protected override void SetValue(string value)
        {
            string valueAsString = ConvertValueToString(value);
            ContentFrame.DoWithin(x => x.Scope.FillInWith(valueAsString));
        }
    }
}
