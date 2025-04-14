namespace LeetCode;

public class EasyProblem
{
    public EasyProblem()
    {
    }

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
