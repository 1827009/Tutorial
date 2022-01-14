using System;
using MatrixSample.Tutorial.Matrix;

namespace MatrixSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix3x3 mat1 = new Matrix3x3(1f, 2f, 3f,
                                           4f, 5f, 6f,
                                           7f, 8f, 9f);
            Matrix3x3 mat2 = new Matrix3x3(1f, 2f, 3f,
                                           4f, 5f, 6f,
                                           7f, 8f, 9f);

            Console.WriteLine(Matrix3x3.Multiply(mat1, mat2).Text);
        }
    }

}


