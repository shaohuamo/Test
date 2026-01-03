namespace SpanLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = -1;
            var value = (uint)a;

            int[] array =Enumerable.Range(0, 1000).ToArray();
            Sum(array,0);
            string[]? objectArray = new string[10];
            Span<object> testSpan = new Span<object>(objectArray);
        }

        static int Sum(int[] array, int offset)
        {
            int sum = 0;
            foreach (var i in array)
            {
                sum += i;
            }
            return sum;
        }
    }
}
