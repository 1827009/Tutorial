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
    class MeshTransform
    {

        public Matrix3x3 positionMatrix;
        
        public MeshTransform(Matrix3x3 pos)
        {
            positionMatrix = pos;
        }

        public static MeshTransform origin{
            get { return new MeshTransform(Matrix3x3.Identity); }
        }

        public MeshTransform combine(MeshTransform parent)
        {            
            return new MeshTransform(this.positionMatrix * parent.positionMatrix);
        }

    }
}
