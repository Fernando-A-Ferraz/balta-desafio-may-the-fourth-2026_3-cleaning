using CleaningAssistant.Console.Models;

namespace CleaningAssistant.Console.Agents;

public class ScheduleAgent
{
    public DateTime CalculateNextDate(MaintenanceTask task)
    {
        return task.LastExecutionDate
            .AddDays(task.IntervalInDays);
    }
}
