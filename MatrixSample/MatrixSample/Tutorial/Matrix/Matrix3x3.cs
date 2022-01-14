﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixSample.Tutorial.Matrix
{
    struct Matrix3x3
    {
        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;
        public String Text
        {
            get
            {
                String output = this.M11 + ",\t" + this.M12 + ",\t" + this.M13 + "\n" +
                                this.M21 + ",\t" + this.M22 + ",\t" + this.M23 + "\n" +
                                this.M31 + ",\t" + this.M32 + ",\t" + this.M33;
                return output;
            }
        }

        private static Matrix3x3 identity = new Matrix3x3(1f, 0f, 0f,
                                                         0f, 1f, 0f,
                                                         0f, 0f, 1f);
        public static Matrix3x3 Identity { get { return identity; } }

        public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }
        public Matrix3x3(Vector3 row1, Vector3 row2, Vector3 row3)
        {
            M11 = row1.x;
            M12 = row1.y;
            M13 = row1.z;
            M21 = row2.x;
            M22 = row2.y;
            M23 = row2.z;
            M31 = row3.x;
            M32 = row3.y;
            M33 = row3.z;
        }

        public Vector2 Left
        {
            get { return new Vector2(-this.M11, -this.M12); }
            set { this.M11 = -value.x; this.M12 = -value.y; }
        }
        public Vector2 Right
        {
            get { return new Vector2(this.M11, this.M12); }
            set
            {
                this.M11 = value.x;
                this.M12 = value.y;
            }
        }
        public Vector2 Translation
        {
            get { return new Vector2(this.M31, this.M32); }
            set {
                M31 = value.x;
                M32 = value.y;
            }
        }
        public Vector2 Up
        {
            get { return new Vector2(this.M21, this.M22); }
            set
            {
                M21 = value.x;
                M22 = value.y;
            }
        }
        public Vector2 Down
        {
            get { return new Vector2(-this.M21, -this.M22); }
            set
            {
                M21 = -value.x;
                M22 = -value.y;
            }
        }

        public static Matrix3x3 Add(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 output;
            output.M11 = matrix1.M11 + matrix2.M11;
            output.M12 = matrix1.M12 + matrix2.M12;
            output.M13 = matrix1.M13 + matrix2.M13;
            output.M21 = matrix1.M21 + matrix2.M21;
            output.M22 = matrix1.M22 + matrix2.M22;
            output.M23 = matrix1.M23 + matrix2.M23;
            output.M31 = matrix1.M31 + matrix2.M31;
            output.M32 = matrix1.M32 + matrix2.M32;
            output.M33 = matrix1.M33 + matrix2.M33;
            return output;
        }

        public static Matrix3x3 Multiply(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 output;
            output.M11 = (matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21) + (matrix1.M13 * matrix2.M31);
            output.M12 = (matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22) + (matrix1.M13 * matrix2.M32);
            output.M13 = (matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23) + (matrix1.M13 * matrix2.M33);

            output.M21 = (matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21) + (matrix1.M23 * matrix2.M31);
            output.M22 = (matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22) + (matrix1.M23 * matrix2.M32);
            output.M23 = (matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23) + (matrix1.M23 * matrix2.M33);

            output.M31 = (matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21) + (matrix1.M33 * matrix2.M31);
            output.M32 = (matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22) + (matrix1.M33 * matrix2.M32);
            output.M33 = (matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23) + (matrix1.M33 * matrix2.M33);

            return output;
        }
        public static Vector3 Multiply(Vector3 vector, Matrix3x3 conversion)
        {
            Vector3 output;
            output.x = conversion.M11 * vector.x + conversion.M12 * vector.y + conversion.M13 * vector.z;
            output.y = conversion.M21 * vector.x + conversion.M22 * vector.y + conversion.M23 * vector.z;
            output.z = conversion.M31 * vector.x + conversion.M32 * vector.y + conversion.M33 * vector.z;

            return output;
        }
        public static Vector2 Multiply(Vector2 vector, Matrix3x3 conversion)
        {
            Vector2 output;
            output.x = conversion.M11 * vector.x + conversion.M12 * vector.y + conversion.M13;
            output.y = conversion.M21 * vector.x + conversion.M22 * vector.y + conversion.M23;

            return output;
        }
    }
}
