using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IVesselLayoutService responsible for positioning vessels in multiple columns.
/// </summary>
public class VesselLayoutService : IVesselLayoutService
{
    /// <summary>
    /// Horizontal spacing between columns in pixels
    /// </summary>
    private const int ColumnSpacing = 30;
    
    /// <summary>
    /// Vertical spacing between vessels in the same column in pixels
    /// </summary>
    private const int VesselSpacing = 20;

    /// <summary>
    /// Converts the fleets from the API to a list of vessels in multiple columns.
    /// </summary>
    /// <param name="fleets">List of fleets</param>
    /// <param name="anchorageWidth">Width of the anchorage, so that the columns start to the right</param>
    /// <param name="numberOfColumns">Number of columns to distribute vessels across (default: 3)</param>
    /// <returns>List of vessels with calculated positions, widths, and heights</returns>
    public List<Vessel> GenerateVesselLayout(List<Fleet> fleets, double anchorageWidth, int numberOfColumns = 3)
    {
        var vessels = new List<Vessel>();
        
        if (fleets.Count == 0)
        {
            return vessels;
        }

        // Start vessels at the same Y position as the anchorage (below header)
        const double anchorageTopPosition = 162;
        
        // First pass: calculate maximum width needed for each column
        var columnMaxWidths = new double[numberOfColumns];
        var currentColumn = 0;

        foreach (var fleet in fleets)
        {
            for (var i = 0; i < fleet.ShipCount; i++)
            {
                var vesselWidth = fleet.SingleShipDimensions.Width * 20;
                columnMaxWidths[currentColumn] = Math.Max(columnMaxWidths[currentColumn], vesselWidth);
                currentColumn = (currentColumn + 1) % numberOfColumns;
            }
        }

        // Calculate X position for each column based on cumulative widths
        var columnXPositions = new double[numberOfColumns];
        columnXPositions[0] = anchorageWidth + 50;
        for (var i = 1; i < numberOfColumns; i++)
        {
            columnXPositions[i] = columnXPositions[i - 1] + columnMaxWidths[i - 1] + ColumnSpacing;
        }

        // Initialize Y offsets for each column
        var columnYOffsets = new double[numberOfColumns];
        for (var i = 0; i < numberOfColumns; i++)
        {
            columnYOffsets[i] = anchorageTopPosition;
        }
        
        // Second pass: position vessels
        currentColumn = 0;

        foreach (var fleet in fleets)
        {
            for (var i = 0; i < fleet.ShipCount; i++)
            {
                var vesselWidth = fleet.SingleShipDimensions.Width * 20;
                var vesselHeight = fleet.SingleShipDimensions.Height * 20;

                vessels.Add(new Vessel
                {
                    Width = vesselWidth,
                    Height = vesselHeight,
                    PositionX = columnXPositions[currentColumn],
                    PositionY = columnYOffsets[currentColumn],
                    ShipDesignation = fleet.ShipDesignation
                });

                columnYOffsets[currentColumn] += vesselHeight + VesselSpacing;

                currentColumn = (currentColumn + 1) % numberOfColumns;
            }
        }

        return vessels;
    }
}

