using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Generator
    {
        /// <summary>
        /// Generates Fibonacci numbers
        /// </summary>
        /// <param name="count">Count of Fibonacci numbers</param>
        /// <returns><see cref="IEnumerable{T}"/> collection</returns>
        /// <exception cref="ArgumentException">Throws if count less than 0 or equals 0</exception>
        public static IEnumerable<int> Fibonacci(int count)
        {
            if (count <= 0) throw new ArgumentException("Count must be positive");

            int a1 = 1, a2 = 1;
            for(int i = 0; i < count; i++)
            {
                if (i == 0 || i == 1)
                {
                    yield return 1;
                    continue;
                }
                int result = a1 + a2;
                yield return result;
                a1 = a2;
                a2 = result;
            }
        }
    }
}
