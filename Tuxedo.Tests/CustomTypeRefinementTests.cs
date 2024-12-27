using FluentAssertions;
using Tuxedo.Tests.Custom;
using Xunit;

namespace Tuxedo.Tests
{
    namespace Custom
    {
        public sealed record Widget(int Id, string Name);
    }

    public static class CustomTypeRefinement
    {
        [Refinement(
            "The widget must have a valid Id and Name",
            isInternal: false,
            dropTypeFromName: true
        )]
        public static bool ValidWidget(Widget widget) =>
            widget.Id > 0 && !string.IsNullOrWhiteSpace(widget.Name);
    }

    public sealed class CustomTypeRefinementTests
    {
        [Fact(DisplayName = "A widget can be refined to a valid widget")]
        public void Case1()
        {
            var widget = new Widget(1, "Widget");
            var refined = (ValidWidget)widget;
            refined.Value.Should().Be(widget);
            refined.Value.Id.Should().Be(1);
            refined.Value.Name.Should().Be("Widget");
        }

        [Fact(DisplayName = "An invalid widget should fail the refinement")]
        public void Case2()
        {
            var widget = new Widget(0, "Widget");
            Assert
                .Throws<InvalidOperationException>(() => (ValidWidget)widget)
                .Message.Should()
                .Be("The widget must have a valid Id and Name");
            ValidWidget.TryParse(widget, out _, out _).Should().BeFalse();
        }
    }
}
