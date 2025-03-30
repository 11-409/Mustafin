using ConsoleApp1;

namespace FiboMatrixTest;


public class FiboMatrixTest
{
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 2)]
    [TestCase(4, 3)]
    [TestCase(5, 5)]
    [TestCase(6, 8)]
    [TestCase(30, 832040)]
    public void SimpleFibonacciTest(int n, int expected)
    {
        int result = Solution.FibonacciMatrixPow(n);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(-1)]
    [TestCase(-10)]
    [TestCase(0)]
    public void CheckFibonacciNegativeOrZeroPower(int n)
    {
        int result = Solution.FibonacciMatrixPow(n);

        Assert.That(result, Is.EqualTo(-1));
    }
}
