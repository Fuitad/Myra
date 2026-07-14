namespace Myra.Mcp;

/// <summary>A single problem found while loading a layout.</summary>
/// <param name="Message">The exact message from Myra's loader.</param>
/// <param name="Kind">Coarse category: unknown-tag, unknown-property, bad-value, xml-syntax, asset, other.</param>
/// <param name="Line">1-based source line, when recoverable (always for XML-syntax errors, best-effort otherwise).</param>
/// <param name="Column">1-based source column, when recoverable.</param>
public sealed record Diagnostic(string Message, string Kind, int? Line = null, int? Column = null);

/// <summary>Result of validating a layout. On success <see cref="WidgetTree"/> is populated and <see cref="Error"/> is null.</summary>
public sealed record ValidationResult(bool Valid, Diagnostic? Error, string? WidgetTree);

/// <summary>Result of reading a layout file: its raw XML plus validation outcome and widget tree.</summary>
public sealed record ReadResult(string Raw, bool Valid, Diagnostic? Error, string? WidgetTree);

/// <summary>Result of a save. <see cref="Saved"/> is whether the file was written; <see cref="Valid"/> reports validation independently (a forced save can write invalid MML).</summary>
public sealed record SaveResult(bool Saved, bool Valid, Diagnostic? Error, string Path);

/// <summary>A widget tag available in MML.</summary>
/// <param name="Name">The MML tag name.</param>
/// <param name="BaseType">The immediate base type name, for orientation.</param>
/// <param name="Role">Whether the widget is a container, single-child container, or leaf widget.</param>
public sealed record WidgetTypeInfo(string Name, string? BaseType, string Role);

/// <summary>A settable property of a widget.</summary>
/// <param name="Name">Property/MML attribute name.</param>
/// <param name="Type">The value type name.</param>
/// <param name="IsAttribute">True when it serializes as an MML attribute; false when it is a child element.</param>
/// <param name="Default">The declared default value, when annotated.</param>
/// <param name="EnumValues">The allowed values when the type is an enum; otherwise null.</param>
public sealed record WidgetProperty(string Name, string Type, bool IsAttribute, string? Default, string[]? EnumValues);

/// <summary>An attached property a child can carry (e.g. Grid.Row), keyed by the container that defines it.</summary>
public sealed record AttachedProperty(string Owner, string Name, string Syntax, string Type);

/// <summary>Full description of a widget: its settable properties, the attached properties available to it, and its style names.</summary>
public sealed record WidgetDescription(string Name, WidgetProperty[] Properties, AttachedProperty[] AttachedProperties, string[] StyleNames);

/// <summary>Read-only inspection of a stylesheet and its atlas.</summary>
/// <param name="Styles">Named styles per widget type, referenceable via a widget's StyleName.</param>
/// <param name="Fonts">Font ids the stylesheet defines.</param>
/// <param name="AtlasRegions">Region (drawable) names in the stylesheet's texture atlas.</param>
public sealed record StylesheetInfo(StyleGroup[] Styles, string[] Fonts, string[] AtlasRegions);

/// <summary>The named styles defined for one widget type (e.g. Button) in a stylesheet.</summary>
public sealed record StyleGroup(string Widget, string[] Names);
