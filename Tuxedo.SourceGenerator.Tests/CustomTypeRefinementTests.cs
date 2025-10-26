using Tuxedo.Tests.Custom;
using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests
{
    namespace Custom
    {
        public sealed record Widget(int Id, string Name);
    }

    public readonly partial struct ValidWidget
    {
        [Refinement("The widget must have a valid Id and Name", Name = nameof(ValidWidget))]
        private static bool Predicate(Widget widget) =>
            widget.Id > 0 && !string.IsNullOrWhiteSpace(widget.Name);
    }

    public sealed class CustomTypeRefinementTests
    {
        [Fact(DisplayName = "A widget can be refined to a valid widget")]
        public void Case1()
        {
            var widget = new Widget(1, "Widget");
            var refined = (ValidWidget)widget;
            Assert.Equal(widget, refined.Value);
            Assert.Equal(1, refined.Value.Id);
            Assert.Equal("Widget", refined.Value.Name);
        }

        [Fact(DisplayName = "An invalid widget should fail the refinement")]
        public void Case2()
        {
            var widget = new Widget(0, "Widget");
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (ValidWidget)widget);
            Assert.StartsWith("The widget must have a valid Id and Name", ex.Message);
            Assert.False(ValidWidget.TryParse(widget, out _, out _));
        }

        [Fact(
            DisplayName = "ValidWidget refinement snapshot is correct and should be name via the attribute"
        )]
        public Task Case3()
        {
            return """
                [Refinement("The widget must have a valid Id and Name", Name = nameof(ValidWidget))]
                private static bool Predicate(Widget widget) =>
                    widget.Id > 0 && !string.IsNullOrWhiteSpace(widget.Name);
                """.VerifyRefinement();
        }
    }
}
