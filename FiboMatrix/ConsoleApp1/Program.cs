namespace ConsoleApp1;

class Program
{
    public static void Main()
    {
        Console.WriteLine(Solution.FibonacciMatrixPow(6));
    }
}


public class Solution
{
    public static int FibonacciMatrixPow(int p)
    {
        int[,] x = new int[2, 2] 
        {
            {1, 1},
            {1, 0}
        };

        int[,] result = new int[2, 2]
        {
            {1, 0},
            {0, 1}
        };
        
        while (p > 0)
        {
            if (p % 2 == 1)
                result = MultiplayMatrix2x2(result, x);

            x = MultiplayMatrix2x2(x, x);
            p /= 2;
        }

        return result[0, 0];
    }

    public static int[,] MultiplayMatrix2x2(int[,] a, int[,] b)
    {
        return new int[2, 2]
        {
            {a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0], a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1]},
            {a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0], a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1]}
        };
    }
}
