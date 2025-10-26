using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service responsible for positioning vessels in multiple columns.
/// Because the items are positioned absolutely, it's necessary to calculate the position of each vessel based on the anchorage width and the number of columns.
/// Therefore we need to know the anchorage width and the widths of all vessels to calculate how many columns are needed
/// and what are the widths of the columns.
/// The vessels and the anchorage are scaled * 20 so that it is better displayed on the UI
/// </summary>
public interface IVesselLayoutService
{
    /// <summary>
    /// Converts the fleets from the API to a list of vessels in multiple columns.
    /// </summary>
    /// <param name="fleets">List of fleets</param>
    /// <param name="anchorageWidth">Width of the anchorage, so that the columns start to the right</param>
    /// <param name="numberOfColumns">Number of columns to distribute vessels across (default: 3)</param>
    /// <returns>List of vessels with calculated positions, widths, and heights</returns>
    List<Vessel> GenerateVesselLayout(List<Fleet> fleets, double anchorageWidth, int numberOfColumns = 3);
}

