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

    internal sealed partial class ArraySortHelper<T> : IArraySortHelper<T>
    {
        private static readonly IArraySortHelper<T> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<T> Default => s_defaultArraySortHelper;

        private static IArraySortHelper<T> CreateArraySortHelper()
        {
            IArraySortHelper<T> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (IArraySortHelper<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<string>), (RuntimeType)typeof(T));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<T>();
            }
            return defaultArraySortHelper;
        }
    }

    internal sealed partial class ArraySortHelper<T, TComparer>
        : IArraySortHelper<T, TComparer>
        where TComparer : IComparer<T>?
        // not allows ref struct yet, because ref struct cannot be boxed in ThrowHelper.ThrowArgumentException_BadComparer(comparer);
    {
        private static readonly IArraySortHelper<T, TComparer> s_defaultArraySortHelper = CreateArraySortHelper();

        public static IArraySortHelper<T, TComparer> Default => s_defaultArraySortHelper;

        private static IArraySortHelper<T, TComparer> CreateArraySortHelper()
        {
            IArraySortHelper<T, TComparer> defaultArraySortHelper;

            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                defaultArraySortHelper = (IArraySortHelper<T, TComparer>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelper<string, IComparer<string>>), (RuntimeType)typeof(T), (RuntimeType)typeof(TComparer));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelper<T, TComparer>();
            }
            return defaultArraySortHelper;
        }
    }

    internal sealed partial class GenericArraySortHelper<T>
        : IArraySortHelper<T>
    {
    }

    internal sealed partial class GenericArraySortHelper<T, TComparer>
        : IArraySortHelper<T, TComparer>
        where TComparer : IComparer<T>?
        where T : IComparable<T>
    {
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
                defaultArraySortHelper = (IArraySortHelperPaired<TKey, TValue>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperPaired<string, string>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperPaired<TKey, TValue>();
            }
            return defaultArraySortHelper;
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
                defaultArraySortHelper = (IArraySortHelperPaired<TKey, TValue, TComparer>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericArraySortHelperPaired<string, string, IComparer<string>>), (RuntimeType)typeof(TKey), (RuntimeType)typeof(TValue), (RuntimeType)typeof(TComparer));
            }
            else
            {
                defaultArraySortHelper = new ArraySortHelperPaired<TKey, TValue, TComparer>();
            }
            return defaultArraySortHelper;
        }
    }

    internal sealed partial class GenericArraySortHelperPaired<TKey, TValue>
        : IArraySortHelperPaired<TKey, TValue>
    {
    }

    internal sealed partial class GenericArraySortHelperPaired<TKey, TValue, TComparer>
        : IArraySortHelperPaired<TKey, TValue, TComparer>
    {
    }
}
