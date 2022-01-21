using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tutorial.Matrix;
using OpusSample;

namespace Tutorial
{
    class TriangleMesh
    {
        // 頂点
        VertexPositionColor[] vertexs;
        Matrix3x3[] vertexMatrix;

        public TriangleMesh()
        {
            vertexs = new VertexPositionColor[]{
                new VertexPositionColor(Vector3.Backward, Color.Blue),
                new VertexPositionColor(Vector3.Backward, Color.Blue),
                new VertexPositionColor(Vector3.Backward, Color.Blue),
            };
            vertexMatrix = new Tutorial.Matrix.Matrix3x3[3]{
                Matrix3x3.Identity,
                Matrix3x3.Identity,
                Matrix3x3.Identity
            };
            vertexMatrix[0].M31 = 0.0f;
            vertexMatrix[0].M32 = -0.1f;
            vertexMatrix[1].M31 = -0.05f;
            vertexMatrix[1].M32 = 0.0f;
            vertexMatrix[2].M31 = 0.05f;
            vertexMatrix[2].M32 = 0.0f;
            
            Game1.DrawingEvent += Draw;
        }
        public void VertexUpdate(Matrix3x3 parentMatrix)
        {
            for (int i = 0; i < vertexs.Length; i++)
			{
                vertexs[i].Position = Vector3.Backward * vertexMatrix[i] * parentMatrix;
			} 
        }
        public void Draw(GraphicsDevice gra)
        {
            gra.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertexs,
                0,
                1
            );
        }
    }
}
