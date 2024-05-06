using BabyStroller.SDK;

namespace Animal.Lib2
{
    //使用Unfinished decorate了Dog Class
    //因此在主体程序中会bypass Dog Class
    [Unfinished]
    public class Dog:IAnimal
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Wang!");
            }
        }
    }
}
