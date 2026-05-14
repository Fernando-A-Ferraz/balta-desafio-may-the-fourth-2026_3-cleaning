using CleaningAssistant.Console.Services;

Console.Title = "Cleaning Assistant - May The Fourth 2026";

ExibirCabecalho();

var cleaningService = new CleaningAssistantService();

if (!cleaningService.HasTasks())
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("🧹 Quantas tarefas deseja cadastrar? ");
    Console.ResetColor();

    var quantityInput = Console.ReadLine();

    if (!int.TryParse(quantityInput, out var totalTasks) || totalTasks <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("❌ Quantidade inválida.");
        Console.ResetColor();
        return;
    }

    for (int i = 1; i <= totalTasks; i++)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"🧹 Tarefa {i}");
        Console.ResetColor();

        Console.Write("Nome da tarefa: ");
        var taskName = Console.ReadLine() ?? string.Empty;

        Console.Write("Periodicidade em dias: ");
        var intervalInput = Console.ReadLine();

        if (!int.TryParse(intervalInput, out var intervalInDays))
        {
            Console.WriteLine("❌ Intervalo inválido.");
            continue;
        }

        Console.Write("Data da última execução (dd/MM/yyyy): ");
        var dateInput = Console.ReadLine();

        if (!DateTime.TryParse(dateInput, out var lastExecutionDate))
        {
            Console.WriteLine("❌ Data inválida.");
            continue;
        }

        cleaningService.AddTask(
            taskName,
            intervalInDays,
            lastExecutionDate);
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Tarefas cadastradas com sucesso!");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Tarefas carregadas do arquivo tasks.json.");
    Console.ResetColor();
}

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("📋 Verificando tarefas pendentes...");
Console.ResetColor();

var dueTasks = cleaningService.GetDueTasks();

Console.WriteLine();

if (dueTasks.Count == 0)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Nenhuma manutenção pendente.");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("⚠️ Tarefas que precisam de atenção:");
    Console.ResetColor();

    foreach (var task in dueTasks)
    {
        Console.WriteLine();
        Console.WriteLine($"🧹 {task.Name}");
        Console.WriteLine($"📅 Próxima manutenção: {task.NextExecutionDate:dd/MM/yyyy}");
    }
}

Console.WriteLine();
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();

static void ExibirCabecalho()
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("======================================");
    Console.WriteLine(" 🌌 MAY THE FOURTH 2026");
    Console.WriteLine("   CLEANING ASSISTANT");
    Console.WriteLine("======================================");

    Console.ResetColor();
    Console.WriteLine();
}