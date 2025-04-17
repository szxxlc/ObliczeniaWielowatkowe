using System;
using System.Runtime.CompilerServices;

namespace ObliczeniaWielowatkowe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Parallel - Simple Test");
            Console.WriteLine("2. Thread - Simple Test");
            Console.WriteLine("3. Parallel and Thread - Full Test\n");
            option = int.Parse(Console.ReadLine());

            if (option == 1)
            {
                int dim = 0;
                Console.WriteLine("Enter the dimension of the matrix");
                option = int.Parse(Console.ReadLine());
                Matrix matrixA = new Matrix(option, option, 123456789);
                Matrix matrixB = new Matrix(option, option, 987654321);

                Console.WriteLine("Matrix A:");
                matrixA.Print();
                Console.WriteLine("Matrix B:");
                matrixB.Print();

                Matrix matrixC = MatrixMultiplier.MultiplyParallel(matrixA, matrixB, 4);
                Console.WriteLine("Matrix C:");
                matrixC.Print();

            }
            else if (option == 2)
            {
                int dim = 0;
                Console.WriteLine("Enter the dimension of the matrix");
                option = int.Parse(Console.ReadLine());
                Matrix matrixA = new Matrix(option, option, 123456789);
                Matrix matrixB = new Matrix(option, option, 987654321);

                Console.WriteLine("Matrix A:");
                matrixA.Print();
                Console.WriteLine("Matrix B:");
                matrixB.Print();

                Matrix matrixC = MatrixMultiplier.MultiplyThread(matrixA, matrixB, 4);
                Console.WriteLine("Matrix C:");
                matrixC.Print();
            }
            else if (option == 3)
            {
                int minThreads = 1;
                int maxThreads = 12;
                int testRepetitions = 5;
                int[] sizes = { 250, 500, 1000 };

                string csvFilePath = "results.csv";
                using (StreamWriter writer = new StreamWriter(csvFilePath))
                {
                    writer.WriteLine("Method,MatrixSize,Threads,AverageTime(ms)");

                    for (int i = 0; i < sizes.Length; i++)
                    {
                        int size = sizes[i];

                        Matrix matrixA = new Matrix(size, size, 123456789);
                        Matrix matrixB = new Matrix(size, size, 123456789);

                        Console.WriteLine($"\n size = {size}");

                        Console.WriteLine("\nParallel");
                        Console.WriteLine("Thread\tAverage Time [ms]");
                        Console.WriteLine("-------------------------");

                        for (int threads = minThreads; threads <= maxThreads; threads++)
                        {
                            long totalTime = 0;

                            for (int rep = 0; rep < testRepetitions; rep++)
                            {
                                var watch = System.Diagnostics.Stopwatch.StartNew();

                                Matrix matrixC = MatrixMultiplier.MultiplyParallel(matrixA, matrixB, threads);

                                watch.Stop();
                                totalTime += watch.ElapsedMilliseconds;
                            }

                            double avgTime = totalTime / (double)testRepetitions;
                            Console.WriteLine($"{threads}\t{avgTime:F2}");

                            writer.WriteLine($"Parallel,{size},{threads},{avgTime:F2}");
                        }

                        Console.WriteLine("\nThread");
                        Console.WriteLine("Thread\tAverage Time [ms]");
                        Console.WriteLine("-------------------------");

                        for (int threads = minThreads; threads <= maxThreads; threads++)
                        {
                            long totalTime = 0;

                            for (int rep = 0; rep < testRepetitions; rep++)
                            {
                                var watch = System.Diagnostics.Stopwatch.StartNew();

                                Matrix matrixC = MatrixMultiplier.MultiplyThread(matrixA, matrixB, threads);

                                watch.Stop();
                                totalTime += watch.ElapsedMilliseconds;
                            }

                            double avgTime = totalTime / (double)testRepetitions;
                            Console.WriteLine($"{threads}\t{avgTime:F2}");

                            writer.WriteLine($"Thread,{size},{threads},{avgTime:F2}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid option :(");
            }
        }
    }
}
