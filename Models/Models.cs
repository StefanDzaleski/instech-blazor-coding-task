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
}