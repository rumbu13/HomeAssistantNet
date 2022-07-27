namespace HomeAssistantNet.Api;

public sealed record HaSelector
{
    public HaEntitySelector? Entity { get; init; }
    public HaDeviceSelector? Device { get; init; }
    public HaActionSelector? Action { get; init; }
    public HaAddonSelector? Addon { get; init; }
    public HaAreaSelector? Area { get; init; }
    public HaAttributeSelector? Attribute { get; init; }
    public HaBooleanSelector? Boolean { get; init; }
    public HaColorRgbSelector? ColorRgb { get; init; }
    public HaColorTempSelector? ColorTemp { get; init; }    
    public HaDateSelector? Date { get; init; }
    public HaDateTimeSelector? Datetime { get; init; }
    public HaDurationSelector? Duration { get; init; }
    public HaIconSelector? Icon { get; init; }
    public HaLocationSelector? Location { get; init; }
    public HaMediaSelector? Media { get; init; }    
    public HaNumberSelector? Number { get; init; }
    public HaObjectSelector? Object { get; init; }
    public HaSelectSelector? Select { get; init; }
    public HaTargetSelector? Target { get; init; }
    public HaTemplateSelector? Template { get; init; }
    public HaTextSelector? Text { get; init; }
    public HaThemeSelector? Theme { get; init; }
    public HaTimeSelector? Time { get; init; }

}
