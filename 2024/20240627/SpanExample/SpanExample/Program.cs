namespace SpanExample
{
    public readonly struct CustomMemory
    {
        //public readonly Span<int> _spanField;

        //Span<int>可以作为非自动实现的属性
        public Span<int> Span => Span<int>.Empty;
    }
    internal class Program
    {
        private const string _dateAsText = "08 07 2024";

        public static (int day, int month, int year) DateWithStringAndSubstring()
        {
            var dayAsText = _dateAsText.Substring(0, 2);
            var monthAsText = _dateAsText.Substring(3, 2);
            var yearAsText = _dateAsText.Substring(6);

            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day,month,year);
        }

        public static ReadOnlySpan<char> DateWithStringAndSpan()
        {
            ReadOnlySpan<char> dateAsSpan = _dateAsText;
            Console.WriteLine(dateAsSpan.Length);

            var dayAsText = dateAsSpan.Slice(0,2);

            //返回Span,而不是返回String，这样在整个方法内部都没有在Heap上分配内存
            return dayAsText;
        }

        static void Main(string[] args)
        {

            var array = new[] { 4, 2, 5, 1, 3, 10, 8 };
            var spanArray = array.AsSpan();
            spanArray.Sort();
            foreach (var VARIABLE in spanArray)
            {
                Console.WriteLine(VARIABLE);
            }

            //在caller处决定要如何使用返回的Span
            //Console.WriteLine(DateWithStringAndSpan().ToString());


            //Console.WriteLine(DateWithStringAndSubstring());
        }

        public void test()
        {
            Span<byte> span = stackalloc byte[100];
        }
    }
}
