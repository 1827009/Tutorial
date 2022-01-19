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
            Matrix3x3 rotation = new Matrix3x3(MathF.Cos(rag), -MathF.Sin(rag), 0,
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
            var parentPos = new Vector2(0, 0);
            Matrix3x3 parentMat = Matrix3x3.Identity;
            Matrix3x3 chaild1Mat = new Matrix3x3(1, 0, 0,
                                                 0, 1, 0,
                                                 0, -5, 1);
            Matrix3x3 chaild2Mat = new Matrix3x3(1, 0, 0,
                                                 0, 1, 0,
                                                 0, -5, 1);

            parentMat = parentMat;
            chaild1Mat = chaild1Mat * rotation * parentMat;
            chaild2Mat = chaild2Mat * rotation * chaild1Mat;

            var pPos = Matrix3x3.Multiply(parentPos, parentMat);
            var c1Pos = Matrix3x3.Multiply(parentPos, chaild1Mat);
            var c2Pos = Matrix3x3.Multiply(parentPos, chaild2Mat);

            Console.WriteLine("親  ：" + pPos.Text + "\n");
            Console.WriteLine("子１：" + c1Pos.Text + "\n");
            Console.WriteLine("子２：" + c2Pos.Text + "\n");
            //Console.WriteLine("それぞれの変換行列");
            //Console.WriteLine("親  ：\n" + parentMat.Text + "\n");
            //Console.WriteLine("子１：\n" + chaild1Mat.Text + "\n");
            //Console.WriteLine("子２：\n" + chaild2Mat.Text + "\n");

            const int NO = 20;
            Console.Write("  ");
            for (int j = -NO; j < NO; j++)
            {
                Console.Write("{0,3:d}", j);
            }
            for (int i = NO; i > -NO; i--)
            {
                Console.Write("{0,3:d}", i);
                for (int j = -NO; j < NO; j++)
                {
                    if (j == (int)MathF.Round(pPos.x) && i == (int)MathF.Round(pPos.y))
                        Console.Write("Ｐ ");
                    else if (j == (int)MathF.Round(c1Pos.x) && i == (int)MathF.Round(c1Pos.y))
                        Console.Write("C1 ");
                    else if (j == (int)MathF.Round(c2Pos.x) && i == (int)MathF.Round(c2Pos.y))
                        Console.Write("C2 ");
                    else
                        Console.Write("　 ");
                }
                Console.WriteLine();
            }
        }
        /*float About(float v)
        {
            if (MathF.Abs(v) % 1 < 0.01f)
                return MathF.Floor(v);
            else if (MathF.Abs(v) % 1 > 0.99f)
                return MathF.Ceiling(v);
            return v;
        }*/
    }

}


