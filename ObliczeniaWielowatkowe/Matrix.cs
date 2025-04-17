using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObliczeniaWielowatkowe
{
    internal class Matrix
    {
        private int Id { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private double[,] Data { get; set; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }
        public Matrix(int rows, int columns, int seed)
        {
            Rows = rows;
            Columns = columns;
            Data = new double[rows, columns];
            Random random = new Random(seed);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Data[i, j] = Math.Round(random.NextDouble() * 10, 2); //decimal values
                    //Data[i, j] = random.Next(0, 10); //integer values
                }
            }
        }
        public Matrix(int rows, int columns, double[,] data)
        {
            Rows = rows;
            Columns = columns;
            Data = data;
        }
        public double SetValue(int row, int column, double value)
        {
            Data[row, column] = value;
            return Data[row, column];
        }
        public int GetRows()
        {
            return Rows;
        }
        public int GetColumns()
        {
            return Columns;
        }
        public double GetValue(int row, int column)
        {
            return Data[row, column];
        }
        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write($"{Data[i, j]:0.00}\t"); //for decimal values
                    //Console.Write($"{Data[i, j]:0}\t"); //for integer values
                }
                Console.WriteLine();
            }
        }


    }
}
