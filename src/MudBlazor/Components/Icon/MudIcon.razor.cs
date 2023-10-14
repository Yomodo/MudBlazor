using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace MudBlazor
{
#nullable enable
    public partial class MudIcon : MudComponentBase
    {
        protected string Classname =>
            new CssBuilder("mud-icon-root")
                .AddClass("mud-icon-default", IconData.HasDefaultColor())
                .AddClass("mud-svg-icon", IconData.IsSvg())
                .AddClass($"mud-{IconData.Color.ToDescriptionString()}-text", IconData.HasCustomColor())
                .AddClass($"mud-icon-size-{IconData.Size.ToDescriptionString()}")
                .AddClass(IconData.Class, IconData.HasClass())
                .Build();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (IconProperties is not null)
            {
                IconData = IconProperties;
                
                // Backwards compatibility

                if (IconData.HasIcon()) Icon = IconData.Icon;
                if (IconData.HasTitle()) Title = IconData.Title;
                if (IconData.HasStyle()) Style = IconData.Style;
            }
            else
            {
                IconData.Icon = Icon;
                IconData.Title = Title;
                IconData.Size = Size;
                IconData.Color = Color;
                IconData.Style = Style;
                IconData.ViewBox = ViewBox;
            }
        }

        /// <summary>
        /// The icon properties.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Behavior)]
        public IconProperties? IconProperties { get; set; }

        /// <summary>
        /// Icon to be used can either be svg paths for font icons.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Behavior)]
        public string? Icon { get; set; }

        /// <summary>
        /// Title of the icon used for accessibility.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Behavior)]
        public string? Title { get; set; }

        /// <summary>
        /// The Size of the icon.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Appearance)]
        public Size Size { get; set; } = Size.Medium;

        /// <summary>
        /// The color of the component. It supports the theme colors.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Appearance)]
        public Color Color { get; set; } = Color.Inherit;

        /// <summary>
        /// The viewbox size of an svg element.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Behavior)]
        public string ViewBox { get; set; } = "0 0 24 24";

        /// <summary>
        /// Child content of component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Icon.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
