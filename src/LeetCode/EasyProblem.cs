namespace LeetCode;

public class EasyProblem
{
    public EasyProblem()
    {
    }

    public int RemoveDuplicatesBestAnswer(int[] nums)
    {
        int indx = 1;
        int cur = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] == nums[i])
            {
                continue;
            }
            else
            {
                nums[indx] = nums[i];
                indx++;
            }
        }
        return indx;
    }

    public int RemoveDuplicatesOne(int[] nums)
    {
        List<int> ints = nums.Distinct().ToList();
        for (int i = 0; i < ints.Count(); i++)
        {
            nums[i] = ints[i];
        }
        return ints.Distinct().Count();
    }

    public int RemoveDuplicatesTwo(int[] nums)
    {
        Span<int> stack = stackalloc int[nums.Count()];
        int stackPointer = 0;

        stack[stackPointer] = nums[0];
        stackPointer++;
        for (int i = 1; i < nums.Count(); i++)
        {
            if (nums[i] == stack[stackPointer - 1])
            {
                continue;
            }
            else
            {
                stack[stackPointer] = nums[i];
                stackPointer++;
            }
        }
        return stackPointer;
    }


    // Did this correctly.  Good time and memory usage.
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode answer = new ListNode();
        if (list1 == null)
        {
            return list2;
        }
        if (list2 == null)
        {
            return list1;
        }

        answer = SortRecursion(list1, list2);

        return answer;
    }

    public ListNode SortRecursion(ListNode node1, ListNode node2)
    {
        ListNode answer = new ListNode();
        if (node1 != null && node2 != null)
        {
            if (node1.val <= node2.val)
            {
                answer = node1;
                if (node1.next == null)
                {
                    answer.next = node2;
                    return answer;
                }
                node1 = node1.next!;
                answer.next = SortRecursion(node1, node2);
            }
            else
            {
                answer = node2;
                if (node2.next == null)
                {
                    answer.next = node1;
                    return answer;
                }
                node2 = node2.next!;
                answer.next = SortRecursion(node1, node2);
            }
        }

        return answer;
    }

    public int[] GetTotalValues(ListNode list)
    {
        List<int> values = new List<int>();
        if (list == null) { return [ 0 ]; }
        values.Add(list.val);
        ListNode next = new ListNode();
        while(list.next != null)
        {
            next = next.next;
            values.Add(next.val);
        }
        return values.ToArray();
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


// Definition for singly-linked list.
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) {
        this.val = val;
        this.next = next;
    }
}

