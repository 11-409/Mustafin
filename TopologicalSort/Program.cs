using System.Diagnostics;
using OfficeOpenXml;

namespace TopologicalSort;

class Program
{
    public static void Main()
    {
        var random = new Random();

        var countIteration = 100;

        var generator = new DAGGenerator();

        using var package = new ExcelPackage("result.xlsx");
        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Results1");

        worksheet.Cells[1, 1].Value = "Count elements";
        worksheet.Cells[1, 2].Value = "Execution time";
        worksheet.Cells[1, 3].Value = "Count operations";

        for (int i = 0; i < countIteration; i++)
        {
            var stopwatch = new Stopwatch();

            int vertexCount = random.Next(100, 10_000);

            var graph = generator.GenerateRandomDAG(vertexCount);

            var topologicSort = new TopologicalSorter(graph, vertexCount);
            
            stopwatch.Start();
            topologicSort.TopologicalSort();
            stopwatch.Stop();

            TimeSpan elapsedTime = stopwatch.Elapsed;

            int operations = topologicSort.operations;

            worksheet.Cells[i + 2, 1].Value = vertexCount;
            worksheet.Cells[i + 2, 2].Value = elapsedTime.TotalMilliseconds;
            worksheet.Cells[i + 2, 3].Value = operations;
        }
        package.Save();
    }
}

public class DAGGenerator
{
    private readonly Random _random = new();

    readonly double edgeProbability = 0.3; // Вероятность наличия ребра между двумя вершинами

    public List<int>[] GenerateRandomDAG(int vertexCount)
    {
        // Инициализация графа
        var graph = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
            graph[i] = [];

        // 1. Генерируем случайный порядок вершин (топологическая сортировка)
        var topOrder = Enumerable.Range(0, vertexCount).ToList();
        Shuffle(topOrder);

        // 2. Добавляем рёбра только в направлении топологической сортировки
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = i + 1; j < vertexCount; j++)
            {
                if (_random.NextDouble() < edgeProbability)
                {
                    int u = topOrder[i];
                    int v = topOrder[j];
                    graph[u].Add(v); // Ребро u → v (но не v → u!)
                }
            }
        }
        return graph;
    }

    // Перемешивает список (алгоритм Фишера-Йетса)
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}

public class TopologicalSorter
{
    private readonly bool[] used;
    private readonly Stack<int> stack = [];
    private readonly List<int>[] Graph;
    private readonly int VertexCount;
    public int operations = 0;

    public TopologicalSorter(List<int>[] graph, int vertexCount)
    {
        Graph = graph;
        VertexCount = vertexCount;
        used = new bool[VertexCount];
    }

    private void Dfs(int v)
    {
        used[v] = true;
        operations++;
        foreach (int u in Graph[v])
        {
            operations++;
            if (!used[u])
            {
                Dfs(u);
            }
        }
        operations++;
        stack.Push(v);
    }

    public List<int> TopologicalSort()
    {
        for (int v = 0; v < VertexCount; v++)
        {
            operations++;
            if (!used[v])
            {
                Dfs(v);
            }
        }

        var result = new List<int>();
        operations++;

        while (stack.Count > 0)
        {
            operations++;
            result.Add(stack.Pop());
        }

        return result;
    }
}
