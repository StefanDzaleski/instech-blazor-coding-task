namespace instech_blazor_coding_task.Models;

public class Anchorage
{
    public double Width { get; init; }
    public double Height { get; init; }
}
public class Vessel
{
    public double Width { get; init; }

    public double Height { get; init; }

    public double PositionX { get; set; }

    public double PositionY { get; set; }

    public double PreviousPositionX { get; set; }

    public double PreviousPositionY { get; set; }

    public double Rotation { get; set; }

    public string? ShipDesignation { get; init; }
}

public class Fleet
{
    public SingleShipDimensions SingleShipDimensions { get; set; } = new();
    public string? ShipDesignation { get; set; }

    public int ShipCount { get; set; }
}

public class SingleShipDimensions
{
    public int Width { get; set; }
    public int Height { get; set; }
}

public class ApiResponse
{
    public Anchorage? AnchorageSize { get; init; } = new(); 
    public List<Fleet> Fleets { get; init; } = []; 
}
