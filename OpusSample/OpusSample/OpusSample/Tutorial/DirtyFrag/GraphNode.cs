using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial.DirtyFrag

{
    class GraphNode
    {
        private Transform local_ = Transform.origin();
        //セッタに更新フラグ
        public Transform Local{
            get{return local_;}
            set{
                dirty_ = true;
                local_ = value;
            }
        }

        //pubric Mesh mesh_;

        private Transform world_;
        private bool dirty_=true;
        
        List<GraphNode> children_ = new List<GraphNode>();

        public void render(Transform parentWorld, bool dirty)
        {
            dirty |= dirty_;
            if (dirty) {
                world_ = local_.combine(parentWorld);
                dirty_ = false;
            }

            /*if(mesh_)*/renderMesh();

            for (int i = 0; i < children_.Count; i++)
            {
                children_[i].render(world_, dirty);
            }
        }

        void renderMesh()
        {
            //描画
        }
    }
}
