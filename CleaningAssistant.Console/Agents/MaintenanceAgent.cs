using CleaningAssistant.Console.Models;

namespace CleaningAssistant.Console.Agents;

public class MaintenanceAgent
{
    public MaintenanceTask CreateTask(
        string taskName,
        int intervalInDays,
        DateTime lastExecutionDate)
    {
        return new MaintenanceTask
        {
            Name = taskName.Trim(),
            IntervalInDays = intervalInDays,
            LastExecutionDate = lastExecutionDate
        };
    }
}
