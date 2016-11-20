using System;
using System.Collections.Generic;
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
    }
}
