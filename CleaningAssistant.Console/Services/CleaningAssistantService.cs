using System.Text.Json;
using CleaningAssistant.Console.Agents;
using CleaningAssistant.Console.Models;

namespace CleaningAssistant.Console.Services;

public class CleaningAssistantService
{
    private readonly MaintenanceAgent _maintenanceAgent = new();
    private readonly AlertAgent _alertAgent = new();

    private readonly string _filePath = "Data/tasks.json";
    private readonly List<MaintenanceTask> _tasks;

    public CleaningAssistantService()
    {
        _tasks = LoadTasks();
    }

    public bool HasTasks()
    {
        return _tasks.Any();
    }

    public void AddTask(
        string taskName,
        int intervalInDays,
        DateTime lastExecutionDate)
    {
        var task = _maintenanceAgent.CreateTask(
            taskName,
            intervalInDays,
            lastExecutionDate);

        _tasks.Add(task);
        SaveTasks();
    }

    public List<MaintenanceTask> GetAllTasks()
    {
        return _tasks;
    }

    public List<MaintenanceTask> GetDueTasks()
    {
        return _alertAgent.GetDueTasks(_tasks);
    }

    private List<MaintenanceTask> LoadTasks()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return [];

        return JsonSerializer.Deserialize<List<MaintenanceTask>>(json) ?? [];
    }

    private void SaveTasks()
    {
        Directory.CreateDirectory("Data");

        var json = JsonSerializer.Serialize(
            _tasks,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }
}
