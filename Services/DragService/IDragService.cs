using instech_blazor_coding_task.Models;
using Microsoft.AspNetCore.Components.Web;

namespace instech_blazor_coding_task.Services;

public interface IDragService
{
    void OnDragStart(Vessel vessel, MouseEventArgs e);

    void OnDragMove(MouseEventArgs e);

    void OnDragEnd(IEnumerable<Vessel> allVessels, IOverlapService overlapService);
}