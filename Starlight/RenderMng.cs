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
    public enum RenderItemType
    {
        vertVBO,
        texVBO,
        EBO
    }

    public static class RenderMng
    {
        static List<WorldObject> worldObjects = new();

        static Shader? shader = Shader.global;

        //static int VertexBufferObject;
        //static int VertexArrayObject;
        //static int ElementBufferObject;

        static Matrix4 view;
        static Matrix4 projection;

        static Texture texture1 = new("resources\\dummy.jpg");
        static Texture texture2 = new("resources\\awesome.png");

        static Vector2i size;

        static RenderInfo[] infos = [];

        public static void Init(string vertex, string fragment, Vector2i _size)
        {
            size = _size;

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Generate and bind the Vertex Array Object
            var VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Generate and bind the Element Buffer Object
            

            view = Matrix4.CreateTranslation(0f, 0f, -3f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), size.X / size.Y, 0.1f, 100f);

            shader?.SetUniform("texture1", 0);
            shader?.SetUniform("texture2", 1);
        }

        public static void Render()
        {
            if(shader is null) throw new NullReferenceException();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture1.Use(shader, TextureUnit.Texture0);
            texture2.Use(shader, TextureUnit.Texture1);

            shader.SetUniform("view", view);
            shader.SetUniform("projection", projection);

            for(int i = 0; i <= infos.Length - 1; i++)
            {
                RenderObj(infos[i]);
            }

            //GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        private static void RenderObj(RenderInfo info)
        {
            GL.BindVertexArray(info.VAO);
            shader?.SetUniform("model", info.modelMatrix);
            GL.DrawElements(PrimitiveType.Triangles, info.indiceSize, DrawElementsType.UnsignedInt, 0);
        }

        public static void AddObj(RenderInfo info)
        {
            infos.Append(info);
        }

        static void Dispose()
        {
            shader?.Dispose();
        }

    }
}