namespace instech_blazor_coding_task.Models;

public class Anchorage
{
    public double Width { get; set; }
    public double Height { get; set; }
}
public class Vessel
{
    public double Width { get; set; }

    public double Height { get; set; }

    public double PositionX { get; set; }

    public double PositionY { get; set; }

    public double PreviousPositionX { get; set; }

    public double PreviousPositionY { get; set; }

    public double Rotation { get; set; }

    public string? ShipDesignation { get; set; }
}

public class Fleet
{
    public SingleShipDimensions singleShipDimensions { get; set; } = new();
    public string? shipDesignation { get; set; }

    public int shipCount { get; set; }
}

public class SingleShipDimensions
{
    public int width { get; set; }
    public int height { get; set; }
}

public class ApiResponse
{
    public Anchorage anchorageSize { get; set; } = new(); 
    public List<Fleet> fleets { get; set; } = new(); 
}
