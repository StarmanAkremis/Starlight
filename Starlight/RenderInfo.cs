using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

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

        public void GenerateRenderItems<T, T2, T3>(Shader shader, int vertSize, int texSize, int eboSize, T[] vertData, T2[] texData, T3[] eboData)
            where T : struct where T2 : struct where T3 : struct
        {
            int location = 0;
            VertexAttribPointerType type;

            location = shader.GetAttribLocation(vertAttrib);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertSize * Marshal.SizeOf(typeof(T)), vertData, BufferUsageHint.StaticDraw);
            type = GetVertexAttribPointerType(typeof(T));
            GL.VertexAttribPointer(location, 3, type, false, 3 * Marshal.SizeOf(typeof(T)), 0);
            GL.EnableVertexAttribArray(location);

            location = shader.GetAttribLocation(texAttrib);
            GL.BindBuffer(BufferTarget.ArrayBuffer, texVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texSize * Marshal.SizeOf(typeof(T)), texData, BufferUsageHint.StaticDraw);
            type = GetVertexAttribPointerType(typeof(T));
            GL.VertexAttribPointer(location, 3, type, false, 3 * Marshal.SizeOf(typeof(T)), 0);
            GL.EnableVertexAttribArray(location);
            
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, eboSize * Marshal.SizeOf(typeof(T)), eboData, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);

            indiceSize = eboSize;

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        private static VertexAttribPointerType GetVertexAttribPointerType(Type type)
        {
            return type switch
            {
                Type t when t == typeof(float) => VertexAttribPointerType.Float,
                Type t when t == typeof(int) => VertexAttribPointerType.Int,
                Type t when t == typeof(uint) => VertexAttribPointerType.UnsignedInt,
                _ => throw new NotSupportedException($"Type {type} is not supported")
            };
        }
    }
}
