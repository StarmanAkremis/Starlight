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

        //static int VertexBufferObject;
        //static int VertexArrayObject;
        //static int ElementBufferObject;

        static bool initd = false;

        public static Shader? shader;

        static Matrix4 view;
        static Matrix4 projection;

        static Texture texture1 = new("resources\\dummy.jpg");
        static Texture texture2 = new("resources\\awesome.png");

        static Vector2i size;

        static RenderInfo[] infos = [];

        public static void Init(Shader _shader, Vector2i _size)
        {
            size = _size;
            shader = _shader;

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Generate and bind the Vertex Array Object
            var VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Generate and bind the Element Buffer Object
            

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

            for(int i = 0; i <= infos.Length - 1; i++)
            {
                RenderObj(infos[i]);
            }

            infos = [];
            
        }

        private static void RenderObj(RenderInfo info)
        {
            GL.BindVertexArray(info.VAO);
            shader?.Use();
            shader?.SetUniform("model", info.modelMatrix);
            GL.DrawElements(PrimitiveType.Triangles, info.indiceSize, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, info.indiceSize);
        }

        public static void AddObj(RenderInfo info)
        {
            infos.Append(info);
        }

    }
}