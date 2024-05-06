using System.Runtime.Loader;
using BabyStroller.SDK;

//H:\source\Practice\Test\2024\20240502\Reflection\BabyStroller\bin\Debug\net8.0\Animals
var folder = Path.Combine(Environment.CurrentDirectory,"Animals");
var files =  Directory.GetFiles(folder);
var animalsTypes = new List<Type>();

foreach (var file in files)
{
    //加载dll文件到内存中
    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
    var types = assembly.GetTypes();
    foreach (var type in types)
    {
        //判断type是否实现了IAnimal接口
        var isContains = type.GetInterfaces().Contains(typeof(IAnimal));
        if (isContains)
        {
            //判断type是否使用了自定义的UnfinishedAttribute修饰
            //inherit参数表示该Attribute是否可以是继承来的
            var isUnfinished = type.GetCustomAttributes(false)
                .Any(a => a.GetType() == typeof(UnfinishedAttribute));

            if (isUnfinished) continue;
            animalsTypes.Add(type);
        }
    }
}

while (true)
{
    for (int i = 0; i < animalsTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}.{animalsTypes[i].Name}");
    }


    Console.WriteLine("==========================");
    Console.WriteLine("Please choose animal");
    int index = int.Parse(Console.ReadLine());
    if (index > animalsTypes.Count || index<1)
    {
        Console.WriteLine("No such an animal, Try Again!");
        continue;
    }

    Console.WriteLine("How many times?");
    int times = int.Parse(Console.ReadLine());

    //反射处理
    var t = animalsTypes[index - 1];
    //创建实例
    var o = Activator.CreateInstance(t);
    //将实例转换了IAnimal接口类型
    var a = (IAnimal)o;
    a.Voice(times);
}