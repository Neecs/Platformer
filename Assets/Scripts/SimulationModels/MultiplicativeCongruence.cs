using System;

public class MultiplicativeCongruence
{
    private int a;
    private int g;
    private int m;
    private int c;
    private long xi;

    public MultiplicativeCongruence()
    {
        g = 31;
        a = 48271;
        m = (int)(Math.Pow(2, g)) - 1;
        c = 0;
        xi = DateTime.Now.Ticks % m;
    }


    public float RandomNumber()
    {
        xi = ((a * xi) + c) % m;
        float ri = (float)xi / (m - 1);
        return ri;
    }

    public float RandomNumberRange(int min, int max)
    {
        xi = ((a * xi) + c) % m;
        float ri = (float)xi / (m - 1);
        float Ni = min + ((max - min) * ri);
        return Ni;
    }
}
