using CleaningAssistant.Console.Models;

namespace CleaningAssistant.Console.Agents;

public class AlertAgent
{
    public List<MaintenanceTask> GetDueTasks(
        List<MaintenanceTask> tasks)
    {
        return tasks
            .Where(task => task.IsDue)
            .ToList();
    }
}