using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectronsLibrary;

namespace SystemInfo.WPF.Settings;

/// <summary>
/// Settings for fonts.
/// </summary>
public class FontSettings : ObservableObject
{
    private Color color;
    private FontFamily family;
    private double size;
    private FontStretch stretch;
    private FontStyle style;
    private FontWeight weight;

    /// <summary>
    /// Initializes a new instance of the <see cref="FontSettings"/> class.
    /// </summary>
    public FontSettings()
    {
        Color = Colors.White;
        family = new FontFamily("Microsoft Sans Serif");
        size = 9;
        Stretch = (FontStretch)Control.FontStretchProperty.DefaultMetadata.DefaultValue;
        Style = (FontStyle)Control.FontStyleProperty.DefaultMetadata.DefaultValue;
        Weight = (FontWeight)Control.FontWeightProperty.DefaultMetadata.DefaultValue;
    }

    /// <summary>
    /// Gets or sets the font color.
    /// </summary>
    public Color Color
    {
        get => color;
        set => SetField(ref color, value);
    }

    /// <summary>
    /// Gets or sets the font family.
    /// </summary>
    public FontFamily Family
    {
        get => family;
        set => SetField(ref family, value);
    }

    /// <summary>
    /// Gets or sets the font size.
    /// </summary>
    public double Size
    {
        get => size;
        set => SetField(ref size, value);
    }

    /// <summary>
    /// Gets or sets the font stretch.
    /// </summary>
    public FontStretch Stretch
    {
        get => stretch;
        set => SetField(ref stretch, value);
    }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    public FontStyle Style
    {
        get => style;
        set => SetField(ref style, value);
    }

    /// <summary>
    /// Gets or sets the font weight.
    /// </summary>
    public FontWeight Weight
    {
        get => weight;
        set => SetField(ref weight, value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is FontSettings settings &&
        Color.Equals(settings.Color) &&
        EqualityComparer<FontFamily>.Default.Equals(Family, settings.Family) &&
        Size == settings.Size &&
        EqualityComparer<FontStretch>.Default.Equals(Stretch, settings.Stretch) &&
        EqualityComparer<FontStyle>.Default.Equals(Style, settings.Style) &&
        EqualityComparer<FontWeight>.Default.Equals(Weight, settings.Weight);

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Color, Family, Size, Stretch, Style, Weight);
}
