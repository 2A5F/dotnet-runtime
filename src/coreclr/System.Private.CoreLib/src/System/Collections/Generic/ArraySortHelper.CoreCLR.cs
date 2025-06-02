// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    internal abstract class AArraySortHelper<TKey>
    {
        public abstract void SortFallback(Span<TKey> keys);
        public abstract int BinarySearchFallback(TKey[] keys, int index, int length, TKey value);
    }

    internal sealed partial class ArraySortHelper<T> : AArraySortHelper<T>
    {
        private static readonly AArraySortHelper<T> s_defaultArraySortHelper = CreateArraySortHelper();

        public static AArraySortHelper<T> Default => s_defaultArraySortHelper;

        private static AArraySortHelper<T> CreateArraySortHelper()
        {
            AArraySortHelper<T> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (AArraySortHelper<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<string>), (RuntimeType)typeof(T));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<T>();
            }
            return defaultArraySortHelper;
        }

        public override void SortFallback(Span<T> keys)
        {
            IntrospectiveSort(keys, Comparer<T>.Default);
        }

        public override int BinarySearchFallback(T[] array, int index, int length, T value)
        {
            return InternalBinarySearch(array, index, length, value, Comparer<T>.Default);
        }
    }

    internal sealed partial class GenericArraySortHelper<T>
        : AArraySortHelper<T>
    {
        public override void SortFallback(Span<T> keys)
        {
            Sort(keys);
        }

        public override int BinarySearchFallback(T[] array, int index, int length, T value)
        {
            return BinarySearch(array, index, length, value);
        }
    }

    internal abstract class AArraySortHelperPaired<TKey, TValue>
    {
        public abstract void SortFallBack(Span<TKey> keys, Span<TValue> values);
    }

    internal sealed partial class ArraySortHelperPaired<TKey, TValue>
        : AArraySortHelperPaired<TKey, TValue>
    {
        private static readonly AArraySortHelperPaired<TKey, TValue> s_defaultArraySortHelper = CreateArraySortHelper();

        public static AArraySortHelperPaired<TKey, TValue> Default => s_defaultArraySortHelper;

        private static AArraySortHelperPaired<TKey, TValue> CreateArraySortHelper()
        {
            AArraySortHelperPaired<TKey, TValue> defaultArraySortHelper;

            if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
            {
                defaultArraySortHelper = (AArraySortHelperPaired<TKey, TValue>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperPaired<string, string>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperPaired<TKey, TValue>();
            }
            return defaultArraySortHelper;
        }

        public override void SortFallBack(Span<TKey> keys, Span<TValue> values)
        {
            IntrospectiveSort(keys, values, Comparer<TKey>.Default);
        }
    }

    internal sealed partial class GenericArraySortHelperPaired<TKey, TValue>
        : AArraySortHelperPaired<TKey, TValue>
    {
        public override void SortFallBack(Span<TKey> keys, Span<TValue> values)
        {
            Sort(keys, values);
        }
    }
}
