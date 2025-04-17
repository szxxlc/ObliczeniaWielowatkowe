using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObliczeniaWielowatkowe
{
    internal class MatrixMultiplier
    {
        public static Matrix MultiplyParallel(Matrix matrixA, Matrix matrixB, int maxThreads)
        {
            Matrix result = new Matrix(matrixA.GetRows(), matrixB.GetColumns());
            ParallelOptions opt = new ParallelOptions() { MaxDegreeOfParallelism = maxThreads };

            Parallel.For(0, matrixA.GetRows() * matrixB.GetColumns(), opt, x =>
            {
                int row = x / matrixB.GetColumns();
                int column = x % matrixB.GetColumns();
                double sum = 0;
                for (int k = 0; k < matrixA.GetColumns(); k++)
                    sum += matrixA.GetValue(row, k) * matrixB.GetValue(k, column);
                result.SetValue(row, column, sum);
            });

            return result;
        }

        public static void MultiplyCell(Matrix matrixA, Matrix matrixB, Matrix result, int maxThreads, int threadIndex)
        {
            for (int i = threadIndex; i < matrixA.GetRows(); i = i + maxThreads)
            {
                int row = i;

                for (int j = 0; j < matrixB.GetColumns(); j++)
                {
                    double sum = 0;
                    for (int k = 0; k < matrixA.GetColumns(); k++)
                        sum += matrixA.GetValue(i, k) * matrixB.GetValue(k, j);
                    result.SetValue(row, j, sum);
                }
            }
        }

        public static Matrix MultiplyThread(Matrix matrixA, Matrix matrixB, int maxThreads)
        {
            Matrix result = new Matrix(matrixA.GetRows(), matrixB.GetColumns());

            Thread[] threads = new Thread[maxThreads];

            for (int i = 0; i < maxThreads; i++)
            {
                int threadIndex = i;
                threads[i] = new Thread(() => MultiplyCell(matrixA, matrixB, result, maxThreads, threadIndex));
                threads[i].Name = $"Thread-{i + 1}";
                threads[i].Start();
            }

            foreach (Thread thread in threads)
                thread.Join();

            return result;
        }
    }
}
