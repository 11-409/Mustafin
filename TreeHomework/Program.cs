namespace TreeHomeork;

class Program
{
    public static void Main()
    {
        Node root = new(1)
        {
            Left = new(1),
            Right = new(1),
        };
        root.Left.Left = new Node(1);
        root.Left.Right = new Node(1);
        root.Right.Left = new Node(1);
        root.Right.Right = new Node(1);
        root.Left.Left.Left = new Node(1);
        root.Left.Left.Left.Left = new Node(1);
        root.Left.Left.Left.Left.Left = new Node(1);

        Console.WriteLine(LongNode(root));
    }

    public static int SumNode(Node? node)
    {
        if (node is null)
            return 0;
        return node.Data + SumNode(node.Right) + SumNode(node.Left);
    }

    public static int LongNode(Node? node)
    {
        if (node is null)
            return 0;
        return 1 + Math.Max(LongNode(node.Right), LongNode(node.Left));
    }
}

public class Node(int data)
{
    public int Data = data;
    public Node? Left;
    public Node? Right;
}