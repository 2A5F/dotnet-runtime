// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    internal interface IArraySortHelper<TKey>
    {
        void Sort(Span<TKey> keys, IComparer<TKey>? comparer);
        int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey>? comparer);
    }

    internal interface IArraySortHelper<TKey, TComparer>
        where TComparer : IComparer<TKey>?, allows ref struct
    {
        void Sort(Span<TKey> keys, TComparer comparer);
        int BinarySearch(TKey[] keys, int index, int length, TKey value, TComparer comparer);
    }

    internal sealed partial class ArraySortHelper<T>
        : IArraySortHelper<T>
    {
        private static readonly IArraySortHelper<T> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<T> Default => s_defaultArraySortHelper;

        private static IArraySortHelper<T> CreateArraySortHelper()
        {
            IArraySortHelper<T> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (IArraySortHelper<T>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<>), (RuntimeType)typeof(T));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<T>();
            }
            return defaultArraySortHelper;
        }

        void IArraySortHelper<T>.Sort(Span<T> keys, IComparer<T>? comparer)
        {
            ArraySortHelper<T, IComparer<T>?>.Sort(keys, comparer);
        }

        int IArraySortHelper<T>.BinarySearch(T[] array, int index, int length, T value, IComparer<T>? comparer)
        {
            return ArraySortHelper<T, IComparer<T>?>.BinarySearch(array, index, length, value, comparer);
        }
    }

    internal sealed partial class ArraySortHelper<T, TComparer>
        : IArraySortHelper<T, TComparer>
        where TComparer : IComparer<T>?
    {
        private static readonly IArraySortHelper<T, TComparer> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<T, TComparer> Default => s_defaultArraySortHelper;

        private static IArraySortHelper<T, TComparer> CreateArraySortHelper()
        {
            IArraySortHelper<T, TComparer> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (IArraySortHelper<T, TComparer>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<string, IComparer<string>>), (RuntimeType)typeof(T), (RuntimeType)typeof(TComparer));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<T, TComparer>();
            }
            return defaultArraySortHelper;
        }

        void IArraySortHelper<T, TComparer>.Sort(Span<T> keys, TComparer comparer)
        {
            Sort(keys, comparer);
        }
        int IArraySortHelper<T, TComparer>.BinarySearch(T[] array, int index, int length, T value, TComparer comparer)
        {
            return BinarySearch(array, index, length, value, comparer);
        }
    }

    internal sealed partial class GenericArraySortHelper<T>
        : IArraySortHelper<T>
    {
        void IArraySortHelper<T>.Sort(Span<T> keys, IComparer<T>? comparer)
        {
            Sort(keys, comparer);
        }

        int IArraySortHelper<T>.BinarySearch(T[] array, int index, int length, T value, IComparer<T>? comparer)
        {
            return BinarySearch(array, index, length, value, comparer);
        }
    }

    internal sealed partial class GenericArraySortHelper<T, TComparer>
        : IArraySortHelper<T, TComparer>
        where TComparer : IComparer<T>?
        where T : IComparable<T>
    {
        void IArraySortHelper<T, TComparer>.Sort(Span<T> keys, TComparer comparer)
        {
            Sort(keys, comparer);
        }

        int IArraySortHelper<T, TComparer>.BinarySearch(T[] keys, int index, int length, T value, TComparer comparer)
        {
            return BinarySearch(keys, index, length, value, comparer);
        }
    }

    internal interface IArraySortHelperPaired<TKey, TValue>
    {
        void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey>? comparer);
    }

    internal interface IArraySortHelperPaired<TKey, TValue, TComparer>
        where TComparer : IComparer<TKey>?, allows ref struct
    {
        void Sort(Span<TKey> keys, Span<TValue> values, TComparer comparer);
    }

    internal sealed partial class ArraySortHelperPaired<TKey, TValue>
        : IArraySortHelperPaired<TKey, TValue>
    {
        private static readonly IArraySortHelperPaired<TKey, TValue> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelperPaired<TKey, TValue> Default => s_defaultArraySortHelper;

        private static IArraySortHelperPaired<TKey, TValue> CreateArraySortHelper()
        {
            IArraySortHelperPaired<TKey, TValue> defaultArraySortHelper;

            if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
            {
                defaultArraySortHelper = (IArraySortHelperPaired<TKey, TValue>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperPaired<,>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperPaired<TKey, TValue>();
            }
            return defaultArraySortHelper;
        }

        void IArraySortHelperPaired<TKey, TValue>.Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey>? comparer)
        {
            Sort(keys, values, comparer);
        }
    }

    internal sealed partial class ArraySortHelperPaired<TKey, TValue, TComparer>
        : IArraySortHelperPaired<TKey, TValue, TComparer>
        where TComparer : IComparer<TKey>?
    {
        private static readonly IArraySortHelperPaired<TKey, TValue, TComparer> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelperPaired<TKey, TValue, TComparer> Default => s_defaultArraySortHelper;

        private static IArraySortHelperPaired<TKey, TValue, TComparer> CreateArraySortHelper()
        {
            IArraySortHelperPaired<TKey, TValue, TComparer> defaultArraySortHelper;

            if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
            {
                defaultArraySortHelper = (IArraySortHelperPaired<TKey, TValue, TComparer>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperPaired<string, string, IComparer<string>>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue), (RuntimeType)typeof(TComparer));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperPaired<TKey, TValue, TComparer>();
            }
            return defaultArraySortHelper;
        }

        void IArraySortHelperPaired<TKey, TValue, TComparer>.Sort(Span<TKey> keys, Span<TValue> values, TComparer comparer)
        {
            Sort(keys, values, comparer);
        }
    }

    internal sealed partial class GenericArraySortHelperPaired<TKey, TValue>
        : IArraySortHelperPaired<TKey, TValue>
    {
        void IArraySortHelperPaired<TKey, TValue>.Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey>? comparer)
        {
            Sort(keys, values, comparer);
        }
    }

    internal sealed partial class GenericArraySortHelperPaired<TKey, TValue, TComparer>
        : IArraySortHelperPaired<TKey, TValue, TComparer>
    {
        void IArraySortHelperPaired<TKey, TValue, TComparer>.Sort(Span<TKey> keys, Span<TValue> values, TComparer comparer)
        {
            Sort(keys, values, comparer);
        }
    }
}
