using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Starlight
{
    public class RenderInfo
    {
        public string vertAttrib = "aPos";
        public string texAttrib = "aTexCoord";
        public int indiceSize = 0;

        public int vertVBO = GL.GenBuffer();
        public int texVBO = GL.GenBuffer();
        public int EBO = GL.GenBuffer();
        public int VAO = GL.GenVertexArray();

        public Matrix4 modelMatrix = Matrix4.Identity;

        public void GenerateRenderItems(Shader shader, int vertSize, int texSize, int eboSize, float[] vertData, float[] texData, int[] eboData)
        {
            GL.BindVertexArray(VAO);

            // Bind and set vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertSize, vertData, BufferUsageHint.StaticDraw);
            int vertAttribLocation = shader.GetAttribLocation(vertAttrib);
            GL.VertexAttribPointer(vertAttribLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(vertAttribLocation);

            // Bind and set texture buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, texVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texSize, texData, BufferUsageHint.StaticDraw);
            int texAttribLocation = shader.GetAttribLocation(texAttrib);
            GL.VertexAttribPointer(texAttribLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(texAttribLocation);

            // Bind and set element buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, eboSize, eboData, BufferUsageHint.StaticDraw);

            // Unbind VAO
            GL.BindVertexArray(0);

            indiceSize = eboSize;
        }
    }
}
