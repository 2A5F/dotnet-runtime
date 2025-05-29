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

    internal interface IArraySortHelperSpecialization<TKey, TComparer> where TComparer : IComparer<TKey>?
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
            ArraySortHelperSpecialization<T, IComparer<T>?>.Sort(keys, comparer);
        }

        int IArraySortHelper<T>.BinarySearch(T[] array, int index, int length, T value, IComparer<T>? comparer)
        {
            return ArraySortHelperSpecialization<T, IComparer<T>?>.BinarySearch(array, index, length, value, comparer);
        }
    }

    internal sealed partial class ArraySortHelperSpecialization<T, TComparer>
        : IArraySortHelperSpecialization<T, TComparer>
        where TComparer : IComparer<T>?
    {
        private static readonly IArraySortHelperSpecialization<T, TComparer> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelperSpecialization<T, TComparer> Default => s_defaultArraySortHelper;

        private static IArraySortHelperSpecialization<T, TComparer> CreateArraySortHelper()
        {
            IArraySortHelperSpecialization<T, TComparer> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (IArraySortHelperSpecialization<T, TComparer>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperSpecialization<string, IComparer<string>>), (RuntimeType)typeof(T), (RuntimeType)typeof(TComparer));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperSpecialization<T, TComparer>();
            }
            return defaultArraySortHelper;
        }

        void IArraySortHelperSpecialization<T, TComparer>.Sort(Span<T> keys, TComparer comparer)
        {
            Sort(keys, comparer);
        }
        int IArraySortHelperSpecialization<T, TComparer>.BinarySearch(T[] array, int index, int length, T value, TComparer comparer)
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

    internal sealed partial class GenericArraySortHelperSpecialization<T, TComparer>
        : IArraySortHelperSpecialization<T, TComparer>
        where TComparer : IComparer<T>?
        where T : IComparable<T>
    {
        void IArraySortHelperSpecialization<T, TComparer>.Sort(Span<T> keys, TComparer comparer)
        {
            Sort(keys, comparer);
        }

        int IArraySortHelperSpecialization<T, TComparer>.BinarySearch(T[] keys, int index, int length, T value, TComparer comparer)
        {
            return BinarySearch(keys, index, length, value, comparer);
        }
    }

    internal interface IArraySortHelper<TKey, TValue>
    {
        void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey>? comparer);
    }

    internal sealed partial class ArraySortHelper<TKey, TValue>
        : IArraySortHelper<TKey, TValue>
    {
        private static readonly IArraySortHelper<TKey, TValue> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<TKey, TValue> Default => s_defaultArraySortHelper;

        private static IArraySortHelper<TKey, TValue> CreateArraySortHelper()
        {
            IArraySortHelper<TKey, TValue> defaultArraySortHelper;

            if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
            {
                defaultArraySortHelper = (IArraySortHelper<TKey, TValue>)RuntimeType.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<,>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<TKey, TValue>();
            }
            return defaultArraySortHelper;
        }
    }

    internal sealed partial class GenericArraySortHelper<TKey, TValue>
        : IArraySortHelper<TKey, TValue>
    {
    }
}
