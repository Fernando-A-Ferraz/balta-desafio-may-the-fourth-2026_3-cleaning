namespace CleaningAssistant.Console.Models;

public class MaintenanceTask
{
    public string Name { get; set; } = string.Empty;
    public int IntervalInDays { get; set; }
    public DateTime LastExecutionDate { get; set; }

    public DateTime NextExecutionDate =>
        LastExecutionDate.AddDays(IntervalInDays);

    public bool IsDue =>
        DateTime.Today >= NextExecutionDate.Date;
}

