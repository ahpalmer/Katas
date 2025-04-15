namespace LeetCode;

public class EasyProblem
{
    public EasyProblem()
    {
    }

    // Don't use Linq.  It's fairly slow in these situations
    public string LongestCommonPrefix(string[] strs)
    {
        if (strs.Count() == 0) { return ""; }
        if (strs.Count() == 1) { return strs[0]; }
        var stringList = strs.ToList();
        int shortestString = stringList.Select(s => s.Length).Min();
        int finalCount = shortestString;
        for (int i = 0; i < shortestString; i++)
        {
            var list = stringList.Select(s => s[i]).Distinct().ToList();
            if (list.Count > 1)
            {
                finalCount = i;
                break;
            }
        }

        return stringList[0].Substring(0, finalCount);
    }

    //Best version of LongestCommonPrefix:
    //public string LongestCommonPrefix(string[] strs)
    //{
    //    if (strs == null || strs.Length == 0)
    //        return "";

    //    string prefix = strs[0];

    //    for (int i = 1; i < strs.Length; i++)
    //    {
    //        while (!strs[i].StartsWith(prefix))
    //        {
    //            prefix = prefix.Substring(0, prefix.Length - 1);

    //            if (prefix == "")
    //                return "";
    //        }
    //    }

    //    return prefix;
    //}

    // Slower than most people's code.  Try to use the Span<T> and ReadOnlySpan<T> classes next time to improve memory usage.
    // Look at this line of code: Span<char> span = stackalloc char[s.Length];
    // This creates a span (a very memory-cheap object) that targets a block of memory directly on the stack memory.  It acts as a stack (?) object
    // Memory and time sensitive applications benefit greatly from understanding low-level data structures.  LinkedList was the wrong object to use for this problem, stack would have been better.
    public bool IsValid(string s)
    {
        LinkedList<char> charValues = new LinkedList<char>(s.Select(s => (char)s).ToList());
        LinkedListNode<char> currentNode = charValues.First;

        while (currentNode != null)
        {
            if (currentNode.Value == '{' || currentNode.Value == '[' || currentNode.Value == '(')
            {
                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    continue;
                }
                else { return false; }
            }
            else
            {
                if (currentNode.Previous != null && equivalentParenthesis(currentNode.Value, currentNode.Previous.Value))
                {
                    charValues.Remove(currentNode.Previous);
                    if (currentNode.Next == null)
                    {
                        charValues.Remove(currentNode);
                        break;
                    }
                    currentNode = currentNode.Next;
                    charValues.Remove(currentNode.Previous!);
                }
                else { return false; }

            }
        }

        if (charValues.Count == 0)
        {
            return true;
        }
        return false;
    }

    public bool equivalentParenthesis(char currentNodeValue, char previousNodeValue)
    {
        if (currentNodeValue == ')' && previousNodeValue == '(') { return true; }
        else if (currentNodeValue == '}' && previousNodeValue == '{') { return true; }
        else if (currentNodeValue == ']' && previousNodeValue == '[') { return true; }
        else { return false; }
    }

    // Done but the efficiency is pretty bad.  Test efficiency with competitive coding test.
    public int CountSymmetricIntegers(int low, int high)
    {
        List<int> answers = new List<int>();
        for (int i = low; i <= high; i++)
        {
            if (i.ToString().Count() % 2 == 1)
            {
                continue;
            }
            else
            {
                string leftChars = i.ToString().Substring(0, i.ToString().Count() / 2);
                string rightChars = i.ToString().Substring(i.ToString().Count() / 2);
                var leftCharTotal = leftChars.ToCharArray().ToList().Select(x => Int32.Parse(x.ToString())).Sum();
                var rightCharTotal = rightChars.ToCharArray().ToList().Select(y => Int32.Parse(y.ToString())).Sum();
                if (leftCharTotal == rightCharTotal)
                {
                    answers.Add(i);
                }
            }
        }

        return answers.Count;
    }
}
