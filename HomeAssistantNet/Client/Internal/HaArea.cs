using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaAreaCreate : HaWsCommand
{
    public HaAreaCreate(string name, string? picture)
        : base("config/area_registry/create")
    {
        Name = name;
        Picture = picture;
    }
    public string Name { get; init; }
    public string? Picture { get; init; }
}

internal sealed record HaAreaUpdate : HaWsCommand
{
    public HaAreaUpdate(string areaId, string? name, string? picture)
        : base("config/area_registry/update")
    {
        AreaId = areaId;
        Name = name;
        Picture = picture;
    }
    public string AreaId { get; init; }
    public string? Name { get; init; }
    public string? Picture { get; init; }
}

internal sealed record HaAreaDelete : HaWsCommand
{
    public HaAreaDelete(string areaId)
        : base("config/area_registry/delete")
    {
        AreaId = areaId;
    }
    public string AreaId { get; init; }
}


