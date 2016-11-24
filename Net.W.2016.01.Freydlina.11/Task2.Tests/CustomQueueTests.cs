using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Task2.Tests
{
    [TestFixture]
    public class CustomQueueTests
    {
        public static IEnumerable<TestCaseData> TestCasesForPush
        {
            get
            {
                yield return new TestCaseData(new int[] {}).Returns(1);
                yield return new TestCaseData(new int[] {18, 48, 6, 1}).Returns(5);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForPush))]
        public int TestPush(int[] elements)
        {
            CustomQueue<int> queue = new CustomQueue<int>(elements);
            queue.Push(1);
            return queue.Count();
        }

        public static IEnumerable<TestCaseData> TestCasesForPop
        {
            get
            {
                yield return new TestCaseData(new int[] { 1 }).Returns(1);
                yield return new TestCaseData(new int[] { 18, 48, 6, 1 }).Returns(48);
                yield return new TestCaseData(new int[] { 18, 48, 6, 1, 3 }).Returns(48);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForPop))]
        public int TestPop(int[] elements)
        {
            CustomQueue<int> queue = new CustomQueue<int>(elements);
            queue.Pop();
            queue.Push(1);
            return queue.Pop();
        }

        [Test, TestCaseSource(nameof(TestCasesForPop))]
        public int TestPeek(int[] elements)
        {
            CustomQueue<int> queue = new CustomQueue<int>(elements);
            queue.Pop();
            queue.Push(1);
            return queue.Peek();
        }

        public static IEnumerable<TestCaseData> TestCasesForEnumenator
        {
            get
            {
                string result = "1 ";
                yield return new TestCaseData(new int[] { 2 }, result).Returns(true);
                result = "48 6 1 1 ";
                yield return new TestCaseData(new int[] { 18, 48, 6, 1 },result).Returns(true);
                result = "48 6 1 3 1 ";
                yield return new TestCaseData(new int[] { 18, 48, 6, 1, 3 },result).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForEnumenator))]
        public bool TestGetEnumenator(int[] elements, string expectedResult)
        {
            CustomQueue<int> queue = new CustomQueue<int>(elements);
            queue.Pop();
            queue.Push(1);
            string result = "";
            foreach (var i in queue)
                result += i + " ";
            return expectedResult.Equals(result);
            //return queue.Aggregate("", (current, i) => current + (i + " "));
        }
    }
}

