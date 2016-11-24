using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Task2.Tests
{
    class CustomCollectionTests
    {
        public static IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                var col1 = new CustomCollection<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var col2 = new CustomCollection<Person> { new Person("Doug", "Ross"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                var col3 = new CustomCollection<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                yield return new TestCaseData(col1, col2).Returns(false);
                yield return new TestCaseData(col1, col3).Returns(true);
                col3.Add(new Person("Peter", "Benton"));
                col3.Remove(new Person("Peter", "Benton"));
                yield return new TestCaseData(col1, col3).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForEquals))]
        public bool TestEquals(CustomCollection<Person> col1, CustomCollection<Person> col2)
        {
            return col1.Equals(col2);
        }

        public static IEnumerable<TestCaseData> TestCasesForTrim
        {
            get
            {
                var col = new CustomCollection<Person> { new Person("John", "Carter"), new Person("Luka", "Kovac"), new Person("Mark", "Green") };
                int size = col.Size;
                col.Add(new Person("Peter", "Benton"));
                col.Remove(new Person("Peter", "Benton"));
                yield return new TestCaseData(col).Returns(size);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForTrim))]
        public int TestTrim(CustomCollection<Person> col)
        {
            col.Trim();
            return col.Size;
        }
    }
}
