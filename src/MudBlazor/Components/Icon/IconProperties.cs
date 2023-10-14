#nullable enable
using System;
namespace MudBlazor;

public static class IconPropertiesExentions
{
    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Icon"/> property has a value, <c>false</c> otherwise.
    /// </summary>
    public static bool HasIcon(this IconProperties props) => props.Icon.AsSpan().Trim().Length > 0;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Title"/> property has a value, <c>false</c> otherwise.
    /// </summary>
    public static bool HasTitle(this IconProperties props) => props.Title.AsSpan().Trim().Length > 0;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Class"/> property has a value, <c>false</c> otherwise.
    /// </summary>
    public static bool HasClass(this IconProperties props) => props.Class.AsSpan().Trim().Length > 0;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Style"/> property has a value, <c>false</c> otherwise.
    /// </summary>
    public static bool HasStyle(this IconProperties props) => props.Style.AsSpan().Trim().Length > 0;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Color"/> equals <see cref="Color.Default"/>, <c>false</c> otherwise.
    /// </summary>
    public static bool HasDefaultColor(this IconProperties props) => props.Color == Color.Default;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Color"/> does not equal <see cref="Color.Default"/> and <see cref="Color.Inherit"/>, <c>false</c> otherwise.
    /// </summary>
    public static bool HasCustomColor(this IconProperties props) => !props.HasDefaultColor() && props.Color != Color.Inherit;

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Icon"/> is an SVG icon, <c>false</c> otherwise.
    /// </summary>
    public static bool IsSvg(this IconProperties props) => props.Icon.AsSpan().Trim().StartsWith("<", StringComparison.Ordinal);

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Icon"/> property has a value and <see cref="IconProperties.Position"/> equals <see cref="Position.Start"/>, <see cref="Position.Left"/> or <see cref="Position.Top"/> <c>false</c> otherwise.
    /// </summary>
    public static bool StartIcon(this IconProperties props) => props.HasIcon() && (props.Position == Position.Start || props.Position == Position.Left || props.Position == Position.Top);

    /// <summary>
    /// Returns <c>true</c> when <see cref="IconProperties.Icon"/> property has a value and  <see cref="IconProperties.Position"/> equals <see cref="Position.End"/>, <see cref="Position.Right"/> or <see cref="Position.Bottom"/>, <c>false</c> otherwise.
    /// </summary>
    public static bool EndIcon(this IconProperties props) => props.HasIcon() && (props.Position == Position.End || props.Position == Position.Right || props.Position == Position.Bottom);
}


/// <summary>
/// The properties for an SVG- or font icon.
/// </summary>
public class IconProperties
{
    /// <summary>
    /// The default viewbox dimensions for an SVG icon.
    /// </summary>
    /// <remarks>The default is "0 0 24 24"</remarks>
    public const string DefaultViewBox = "0 0 24 24";


    /// <summary>
    /// The icon to use. This can either be an SVG- or font icon.
    /// </summary>
    [Category(CategoryTypes.Icon.Behavior)]
    public string? Icon { get; set; } = string.Empty;

    /// <summary>
    /// The title of the icon, used for accessibility.
    /// </summary>
    [Category(CategoryTypes.Icon.Behavior)]
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// The size of the icon.
    /// </summary>
    /// <remarks>The default is <see cref=" Size.Medium"/></remarks>
    [Category(CategoryTypes.Icon.Appearance)]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// The color of the icon.
    /// </summary>
    /// <remarks>The default is <see cref="Color.Inherit"/></remarks>
    [Category(CategoryTypes.Icon.Appearance)]
    public Color Color { get; set; } = Color.Inherit;

    /// <summary>
    /// The class names to apply to the icon, separated by space.
    /// </summary>
    [Category(CategoryTypes.Button.Appearance)]
    public string? Class { get; set; }

    /// <summary>
    /// The CSS  style to apply to the icon.
    /// </summary>
    [Category(CategoryTypes.ComponentBase.Common)]
    public string? Style { get; set; }

    /// <summary>
    /// The position of the icon.
    /// </summary>
    public Position? Position { get; set; }


    // SVG properties

    /// <summary>
    /// The viewbox dimensions for an SVG icon.
    /// </summary>
    /// <remarks>The default is "0 0 24 24"</remarks>
    [Category(CategoryTypes.Icon.Behavior)]
    public string ViewBox { get; set; } = DefaultViewBox;

    /// <summary>
    /// The focusable attribute for an SVG icon.
    /// </summary>
    public bool Focusable { get; set; }

    /// <summary>
    /// Theattribute to indicates whether the element is exposed to an accessibility API.
    /// </summary>
    /// <remarks>The default is <c>true</c></remarks>
    public bool AriaHidden { get; set; } = true;
}
