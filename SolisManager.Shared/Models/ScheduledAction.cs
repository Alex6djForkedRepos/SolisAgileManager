using System.ComponentModel.DataAnnotations;

namespace SolisManager.Shared.Models;

public class ScheduledAction
{
    [Required] public TimeSpan? StartTime { get; set; }
    [Required] public SlotAction Action { get; set; }
}