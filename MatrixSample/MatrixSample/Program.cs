using System;
using MatrixSample.Tutorial.Matrix;

namespace MatrixSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix3x3 translation = new Matrix3x3(1f, 0f, 0f,
                                                  0f, 1f, 0f,
                                                  2f, 3f, 1f);
            Matrix3x3 scale = new Matrix3x3(1f, 3f, 0f,
                                            1f, 2f, 0f,
                                            0f, 0f, 1f);
            float rag = MathF.PI * 0.5f;
            Matrix3x3 rotation = new Matrix3x3(MathF.Cos(rag), MathF.Sin(-rag), 0,
                                               MathF.Sin(rag), MathF.Cos(rag), 0,
                                               0,              0,              1);
            Console.WriteLine(rotation.Text);

            //Console.WriteLine("\n Vector");
            //Vector3 vec = new Vector3(1, 1, 1);
            //// Vector * 変換行列
            //Vector3 printV = vec;
            //for (int i = 0; i < 5; i++)
            //{
            //    printV = Matrix3x3.Multiply(printV, rotation);
            //    Console.WriteLine(printV.Text + "\n");
            //}

            Console.WriteLine("\n 階層構造");
            var parentPos = new Vector3(0, -1, 1);
            Matrix3x3 parentMat = Matrix3x3.Identity;

            Matrix3x3 chaild1Mat = new Matrix3x3(1, 0, 0,
                                                 0, 1, 0,
                                                 0, -1, 1);
            Matrix3x3 chaild2Mat = new Matrix3x3(1, 0, 0,
                                                 0, 1, 0,
                                                 0, -1, 1);

            parentMat = parentMat * rotation;

            chaild1Mat = chaild1Mat * parentMat;

            chaild2Mat = chaild2Mat * chaild1Mat;

            Console.WriteLine("親  ：" + Matrix3x3.Multiply(parentPos, parentMat).Text + "\n");
            Console.WriteLine("子１：" + Matrix3x3.Multiply(new Vector3(0,-1, 1), chaild1Mat).Text + "\n");
            Console.WriteLine("子２：" + Matrix3x3.Multiply(new Vector3(0, -2, 1), chaild2Mat).Text + "\n");
            Console.WriteLine("親  ：\n" + parentMat.Text + "\n");
            Console.WriteLine("子１：\n" + chaild1Mat.Text + "\n");
            Console.WriteLine("子２：\n" + chaild2Mat.Text + "\n");
        }
    }

}


