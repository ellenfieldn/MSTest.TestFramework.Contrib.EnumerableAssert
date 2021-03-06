﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert
{
    public sealed class EnumerableAssert
    {
        private static EnumerableAssert that;

        private EnumerableAssert()
        {
        }

        public static EnumerableAssert That
        {
            get
            {
                if (that == null)
                {
                    that = new EnumerableAssert();
                }
                return that;
            }
        }

        public static void IsNullOrEmpty(IEnumerable enumerable)
        {
            if (enumerable != null && enumerable.GetEnumerator().MoveNext())
            {
                throw new AssertFailedException("EnumerableAssert.IsNullOrEmpty failed. Collection is not null or empty.");
            }
        }

        public static void Contains<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is empty.");
            }
            if (!enumerable.Contains(item))
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection does not contain the expected element.");
            }
        }

        public static void Contains<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is empty.");
            }
            if (!enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection does not contain an element that matches the predicate.");
            }
        }

        public static void ContainsOne<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is empty.");
            }
            var matches = enumerable.Where(i => i.Equals(item)).Count();
            if (matches == 0)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection does not contain the expected element.");
            }
            if (matches > 1)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection contains the expected element more than once.");
            }
        }

        public static void ContainsOne<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is empty.");
            }
            var matches = enumerable.Where(predicate).Count();
            if (matches == 0)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection does not contain an element that matches the predicate.");
            }
            if (matches > 1)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection contains more than one element that matches the predicate.");
            }
        }

        public static void IsTrueForAll<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Collection is empty.");
            }
            if (!enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. No elements match the predicate.");
            }
            if (!enumerable.All(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Not all elements match the predicate.");
            }
        }

        public static void DoesNotContain<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is empty.");
            }
            if (enumerable.Contains(item))
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection contains the unexpected element.");
            }
        }

        public static void DoesNotContain<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is empty.");
            }
            if (enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection contains an element matching the predicate.");
            }
        }

        public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected == null && actual == null || ReferenceEquals(expected, actual))
            {
                return;
            }
            if (expected == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected enumerable is null.");
            }
            if (actual == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Actual enumerable is null.");
            }
            if (expected.Count() != actual.Count())
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.");
            }
            if (expected.Except(actual).Any())
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.");
            }
        }

        public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual, Func<T, T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if(expected == null && actual == null || ReferenceEquals(expected, actual))
            {
                return;
            }
            if (expected == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected enumerable is null.");
            }
            if (actual == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Actual enumerable is null.");
            }
            if(expected.Count() != actual.Count())
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.");
            }
            if (expected.Except(actual, new GenericEqualityComparer<T>(predicate)).Any())
            {
                throw new AssertFailedException("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.");
            }
        }

        public static void AreNotEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected == null && actual == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Both enumerables are null.");
            }
            if (expected == null || actual == null)
            {
                return;
            }
            if (ReferenceEquals(expected, actual))
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Enumerables refer to the same object.");
            }
            if (expected.Count() == 0 && actual.Count() == 0)
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Both enumerables are empty.");
            }
            if (expected.Count() != actual.Count())
            {
                return;
            }
            if (!expected.Except(actual).Any())
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Enumerables are equivalent.");
            }
        }

        public static void AreNotEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual, Func<T, T, bool> predicate)
        {
            if (expected == null && actual == null)
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Both enumerables are null.");
            }
            if (expected == null || actual == null)
            {
                return;
            }
            if (ReferenceEquals(expected, actual))
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Enumerables refer to the same object.");
            }
            if (expected.Count() == 0 && actual.Count() == 0)
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Both enumerables are empty.");
            }
            if (expected.Count() != actual.Count())
            {
                return;
            }
            if (!expected.Except(actual, new GenericEqualityComparer<T>(predicate)).Any())
            {
                throw new AssertFailedException("EnumerableAssert.AreNotEquivalent failed. Enumerables are equivalent.");
            }
        }

        private class GenericEqualityComparer<T> : IEqualityComparer<T>
        {
            private Func<T, T, bool> _predicate;

            public GenericEqualityComparer(Func<T, T, bool> predicate)
            {
                _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            }

            public bool Equals(T x, T y)
            {
                if(x == null && y == null)
                {
                    return true;
                }
                if(x == null || y == null)
                {
                    return false;
                }
                return _predicate(x, y);
            }

            public int GetHashCode(T obj) => 0; //This is bad
        }
    }
}
