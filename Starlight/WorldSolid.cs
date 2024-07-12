using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class WorldSolid : WorldObject
    {
        protected float[] vertices = { };
        protected float[] texCoords = { };
        protected int[] indices = { };

        Shader shader;

        protected RenderInfo renderInfo = new();

        protected WorldSolid(Shader _shader)
        {
            shader = _shader;
        }

        protected override void Update()
        {
            renderInfo.modelMatrix = 
                Matrix4.CreateTranslation(position) *
                Matrix4.CreateRotationX(rotation.X) *
                Matrix4.CreateRotationY(rotation.Y) *
                Matrix4.CreateRotationZ(rotation.Z);
            
            renderInfo.GenerateRenderItems(shader, vertices.Length, texCoords.Length, indices.Length, vertices, texCoords, indices);
        }
    }
}
