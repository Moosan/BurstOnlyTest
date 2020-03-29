using System.ComponentModel;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
public class BurstSample : MonoBehaviour
{
    public void CalcDefault()
    {
        Debug.Log(Fibonacci.Calc40ThFibonacci());
    }

    public void CalcBurst()
    {
        Debug.Log(Fibonacci.Calc40ThFibonacciBurst());
    }
}

[BurstCompile]
public static class Fibonacci
{
    private delegate int CalcDelegate();

    private static CalcDelegate _calcDelegate;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<CalcDelegate>(CalcBurstImpl);
        _calcDelegate = funcPtr.Invoke;
    }

    [BurstCompile]
    private static int CalcBurstImpl() => Calc40ThFibonacci();
    public static int Calc40ThFibonacciBurst()
    {
        return _calcDelegate();
    }
    public static int Calc40ThFibonacci()
    {
        return CalcFibonacci(40);
    }
    private static int CalcFibonacci(int n)
    {
        switch (n)
        {
            case 0:
                return 0;
            case 1:
                return 1;
            default:
                return CalcFibonacci(n - 1) + CalcFibonacci(n - 2);
        }
    }
}