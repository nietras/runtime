// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
    internal interface IArraySortHelper<TKey>
    {
        void Sort<TComparer>(Span<TKey> keys, TComparer comparer) where TComparer : IComparer<TKey>?;
        int BinarySearch<TComparer>(TKey[] keys, int index, int length, TKey value, TComparer comparer) where TComparer : IComparer<TKey>?;
    }

    [TypeDependency("System.Collections.Generic.GenericArraySortHelper`1")]
    internal partial class ArraySortHelper<T>
        : IArraySortHelper<T>
    {
        private static readonly IArraySortHelper<T> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<T> Default => s_defaultArraySortHelper;

        [DynamicDependency("#ctor", typeof(GenericArraySortHelper<>))]
        private static IArraySortHelper<T> CreateArraySortHelper()
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                return (IArraySortHelper<T>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string>).TypeHandle.Instantiate(new Type[] { typeof(T) }));
            }
            else
            {
                return new ArraySortHelper<T>();
            }
        }
    }

    internal partial class GenericArraySortHelper<T>
        : IArraySortHelper<T>
    {
    }

    internal interface IArraySortHelper<TKey, TValue>
    {
        void Sort<TComparer>(Span<TKey> keys, Span<TValue> values, TComparer comparer) where TComparer : IComparer<TKey>?;
    }

    [TypeDependency("System.Collections.Generic.GenericArraySortHelper`2")]
    internal partial class ArraySortHelper<TKey, TValue>
        : IArraySortHelper<TKey, TValue>
    {
        private static readonly IArraySortHelper<TKey, TValue> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<TKey, TValue> Default => s_defaultArraySortHelper;

        [DynamicDependency("#ctor", typeof(GenericArraySortHelper<,>))]
        private static IArraySortHelper<TKey, TValue> CreateArraySortHelper()
        {
            IArraySortHelper<TKey, TValue> defaultArraySortHelper;

            if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
            {
                defaultArraySortHelper = (IArraySortHelper<TKey, TValue>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string, string>).TypeHandle.Instantiate(new Type[] { typeof(TKey), typeof(TValue) }));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<TKey, TValue>();
            }
            return defaultArraySortHelper;
        }
    }

    internal partial class GenericArraySortHelper<TKey, TValue>
        : IArraySortHelper<TKey, TValue>
    {
    }
}
