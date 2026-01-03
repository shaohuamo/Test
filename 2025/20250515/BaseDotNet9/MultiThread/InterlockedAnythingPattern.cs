using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    public delegate Int32 Morpher<TResult, TArgument>(Int32 startValue, TArgument argument,
        out TResult morphResult);
    public static class InterlockedAnythingPattern
    {
        public static TResult Morph<TResult, TArgument>(ref Int32 originalValue, TArgument argument,
            Morpher<TResult, TArgument> morpher)
        {
            TResult morphResult;
            Int32 actualOriginalValue = originalValue,expectedOriginalValue,desiredVal;
            do
            {
                expectedOriginalValue = actualOriginalValue;
                desiredVal = morpher(expectedOriginalValue, argument, out morphResult);
                actualOriginalValue = Interlocked.CompareExchange(ref originalValue, desiredVal, expectedOriginalValue);
            } while (expectedOriginalValue !=actualOriginalValue);

            //morphResult 的作用是：在保证原子性修改数值的同时，
            //允许返回与修改操作相关的任意业务结果，实现了数值修改与业务逻辑结果的分离。
            return morphResult;
        }
    }
}
