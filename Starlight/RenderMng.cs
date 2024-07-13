using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Starlight
{
    public static class RenderMng
    {
        static bool initd = false;

        public static Shader? shader;

        static Matrix4 view;
        static Matrix4 projection;

        static Texture texture1 = new("resources\\dummy.jpg");
        static Texture texture2 = new("resources\\awesome.png");

        static Vector2i size;

        static List<RenderInfo> infos = [];

        public static void Init(Shader _shader, Vector2i _size)
        {
            size = _size;
            shader = _shader;

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            view = Matrix4.CreateTranslation(0f, 0f, -3f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), size.X / size.Y, 0.1f, 100f);

            shader.SetUniform("texture1", 0);
            shader.SetUniform("texture2", 1);
            initd = true;
        }

        public static void Render()
        {
            if (!initd || shader is null) throw new Exception("UNINITIALIZED RENDER MANAGER");
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture1.Use(shader, TextureUnit.Texture0);
            texture2.Use(shader, TextureUnit.Texture1);

            shader.SetUniform("view", view);
            shader.SetUniform("projection", projection);

            foreach (var info in infos)
            {
                Console.WriteLine($"VAO: {info.VAO} vertVBO: {info.vertVBO} texVBO: {info.texVBO} EBO: {info.EBO}");
                RenderObj(info);
            }

            infos = [];
        }

        private static void RenderObj(RenderInfo info)
        {
            shader!.SetUniform("model", info.modelMatrix);

            GL.BindVertexArray(info.VAO);
            GL.DrawElements(PrimitiveType.Triangles, info.indiceSize, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }
        public static void AddObj(RenderInfo info)
        {
            infos.Add(info);
        }
    }
}
