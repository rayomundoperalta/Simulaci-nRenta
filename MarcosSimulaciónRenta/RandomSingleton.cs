using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public sealed class RandomSingleton
{
    private static readonly RandomSingleton instance = new RandomSingleton();
    private static Random _rand;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static RandomSingleton()
    {
    }

    private RandomSingleton()
    {
        _rand = new Random();
    }

    public static int Next(int N)
    {
        return _rand.Next(N);
    }

    public static int Next(int min, int max)
    {
        return _rand.Next(min, max);
    }

    public static double NextDouble()
    {
        return _rand.NextDouble();
    }
}
