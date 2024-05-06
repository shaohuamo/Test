using BabyStroller.SDK;

namespace Animal.Lib1
{
    public class Cat:IAnimal
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Meow!");
            }
        }
    }
}
