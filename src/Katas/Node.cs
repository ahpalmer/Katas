using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

public class Node
{
    public Node Left;
    public Node Right;
    public int Value;

    public Node(Node l, Node r, int v)
    {
        Left = l;
        Right = r;
        Value = v;
    }
}