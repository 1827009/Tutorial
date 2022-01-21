using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorial.Matrix;

namespace Tutorial
{
    class MeshGraphNode
    {
        public static uint updateCount = 0;

        private MeshTransform local_ = MeshTransform.origin;

        /// <summary>
        /// 更新フラグを立てつつ変更
        /// </summary>
        public MeshTransform Local
        {
            get { return local_; }
            set
            {
                local_ = value;
                dirty_ = true;
            }
        }
        public MeshTransform GetLocal()
        {
            dirty_ = true;
            return local_;
        }

        private MeshTransform world_;
        private bool dirty_ = true;
        public bool Dirty { get { return dirty_; } }

        TriangleMesh mesh = new TriangleMesh();

        List<MeshGraphNode> children_ = new List<MeshGraphNode>();

        public MeshGraphNode() { }
        public MeshGraphNode(MeshGraphNode parent)
        {
            parent.children_.Add(this);
        }

        public void render(MeshTransform parentWorld, bool dirty)
        {
            dirty |= dirty_;
            if (dirty)
            {
                world_ = local_.combine(parentWorld);
                dirty_ = false;
                Console.WriteLine("更新:" + MeshGraphNode.updateCount++);

                mesh.VertexUpdate(world_.positionMatrix);
            }

            for (int i = 0; i < children_.Count; i++)
            {
                children_[i].render(world_, dirty);
            }
        }
    }
}
