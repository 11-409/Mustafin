namespace BST;

public class Node(int data)
{
    public int Data = data;
    public Node? Left;
    public Node? Right;
}

public class BST
{
    public Node? root;

    public bool Search(int target, Node? node = null)
    {
        if (node is null) return false;
        else if (node.Data == target) return true;

        if (node.Data > target) 
            return Search(target, node.Left);
        else 
            return Search(target, node.Right);
    }

    public void Add(int data)
    {
        Add(data, root);
    }

    private Node Add(int data, Node? node = null)
    {
        if (node is null) 
        {
            node = new Node(data);
            return node;
        }
        if (node.Data > data) node.Left = Add(data, node.Left);
        if (node.Data <= data) node.Right = Add(data, node.Right);

        return node;
    }
}

class Program
{
    public static void Main()
    {
        BST bST = new BST();
        bST.Add(10);
        Console.WriteLine();
    }
}