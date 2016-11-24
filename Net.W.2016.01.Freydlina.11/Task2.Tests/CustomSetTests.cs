using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Task2.Tests
{
    [TestFixture]
    public class CustomSetTests
    {
        public static IEnumerable<TestCaseData> TestCasesForAdd
        {
            get
            {
                yield return new TestCaseData(new Person("Gregory","Pratt")).Returns(4);
                yield return new TestCaseData(new Person("Mark", "Green")).Returns(3);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAdd))]
        public int TestAdd(Person item)
        {
            var set = new CustomSet<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green"), item};
            return set.Count;
        }

        public static IEnumerable<TestCaseData> TestCasesForRemove
        {
            get
            {
                yield return new TestCaseData(new Person("Luka", "Kovac")).Returns(2);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForRemove))]
        public int TestRemove(Person item)
        {
            var set = new CustomSet<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green")};
            set.Remove(item);
            return set.Count;
        }

        public static IEnumerable<TestCaseData> TestCasesForIntersect
        {
            get
            {
                var set1 = new CustomSet<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var set2 = new CustomSet<Person> { new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var result = new CustomSet<Person> { new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                yield return new TestCaseData(set1,set2,result).Returns(true);
                yield return new TestCaseData(set2,set1,result).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForIntersect))]
        public bool TestInersect(CustomSet<Person> set1, CustomSet<Person> set2, CustomSet<Person> expextedResult)
        {
            CustomSet<Person> result = CustomSet<Person>.Intersect(set1, set2);
            result.Trim();
            return expextedResult.Equals(result);
        }

        public static IEnumerable<TestCaseData> TestCasesForUnion
        {
            get
            {
                var set1 = new CustomSet<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var set2 = new CustomSet<Person> { new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var result = new CustomSet<Person> { new Person("John", "Carter"), new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                yield return new TestCaseData(set1, set2, result).Returns(true);
                yield return new TestCaseData(set2, set1, result).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForUnion))]
        public bool TestUnion(CustomSet<Person> set1, CustomSet<Person> set2, CustomSet<Person> expextedResult)
        {
            CustomSet<Person> result = CustomSet<Person>.Union(set1, set2);
            result.Trim();
            return expextedResult.Equals(result);
        }

        public static IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                var set1 = new CustomSet<Person> { new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var set2 = new CustomSet<Person> { new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                yield return new TestCaseData(set1,set2).Returns(false);
                var set3 = new CustomSet<Person>(set2);
                set3.Remove(new Person("Doug", "Ross"));
                yield return new TestCaseData(set1,set3).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForEquals))]
        public bool TestEquals(CustomSet<Person> set1, CustomSet<Person> set2)
        {
            return set1.Equals(set2);
        }

        public static IEnumerable<TestCaseData> TestCasesForExept
        {
            get
            {
                var set1 = new CustomSet<Person> { new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var set2 = new CustomSet<Person> { new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var result = new CustomSet<Person> {new Person("Doug", "Ross")};
                yield return new TestCaseData(set1, set2,result).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForExept))]
        public bool TestExept(CustomSet<Person> set1, CustomSet<Person> set2, CustomSet<Person> expextedResult)
        {
            CustomSet<Person> result = CustomSet<Person>.Except(set1, set2);
            result.Trim();
            return expextedResult.Equals(result);
        }
    }
}
