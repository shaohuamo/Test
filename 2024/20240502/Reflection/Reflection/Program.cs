using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Reflection
{
    public class Program
    {
        public interface IVehicle
        {
            void Run();
        }

        public class Car : IVehicle
        {
            public void Run()
            {
                Console.WriteLine("Car is running");
            }
        }

        public interface IWeapon
        {
            void Fire();
        }
        public interface ITank:IWeapon,IVehicle
        {
        }

        public class Tank : ITank
        {
            public void Run()
            {
                Console.WriteLine("Tank is running");
            }

            public void Fire()
            {
                Console.WriteLine("Tank is firing");
            }
        }

        public class Driver
        {
            public IVehicle Vehicle;

            public Driver(IVehicle vehicle)
            {
                Vehicle = vehicle;
            }
            public void Run()
            {
                Vehicle.Run();
            }
        }

        public static int Sum(IEnumerable collection)
        {
            int sum = 0;
            foreach (var o in collection)
            {
                sum = sum + (int)o;
            }
            return sum;
        }


        static void Main(string[] args)
        {
            //创建IOC
            var sc = new ServiceCollection();
            sc.AddScoped(typeof(ITank),typeof(Tank));
            //sc.AddScoped(typeof(IVehicle), typeof(Car));
            sc.AddScoped<Driver>();
            var sp = sc.BuildServiceProvider();
            //=================================================================
            //从IOC中创建Tank的实例
            var driver = sp.GetService<Driver>();
            driver?.Run();












            ////=================================
            ////获取tank的type information
            //var t = tank.GetType();
            ////创建t类型的实例
            ////CreateInstance的返回类型是object?
            //object? o = Activator.CreateInstance(t);
            ////获取t类型中Fire与Run方法的详细信息
            //MethodInfo? fireMi = t.GetMethod("Fire");
            //MethodInfo? runeMi = t.GetMethod("Run");
            ////调用o中的Fire与Run方法
            //fireMi?.Invoke(o, null);
            //runeMi?.Invoke(o, null);



            //========================================
            //var wk = new WarmKiller();
            //wk.Love();
            //var killer = (IKiller)wk;
            //killer.Kill();

            //=======================================
            //int[] array1 = { 1, 2, 3, 4, 5 };
            //var array2 = new ArrayList() { 1, 2, 3, 4, 5 };
            //var array3 = new ReadOnlyCollection(array1);

            //Console.WriteLine(Sum(array1));
            //Console.WriteLine(Sum(array2));
            //Console.WriteLine(Sum(array3));

            //=======================================
            //var driver1 = new Driver(new Tank());
            //driver1.Run();
            //violate interface segregation principle 待确认
        }
    }
}
