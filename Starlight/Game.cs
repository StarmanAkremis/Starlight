using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection;
using System.Resources;

namespace Starlight
{
    public class Game : GameWindow
    {
        BaseCube? cube;

        Shader? shader;

        private static List<Entity> ents = [];

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (width, height), Title = title
        }) {  }

        protected override void OnLoad()
        {
            base.OnLoad();

            KeyRun esctoquit = new(this, Keys.Escape, () => Close());
            ents.Add(esctoquit);

            Sound sound = new("resources\\archives.mp3");
            sound.Play(true);

            shader = new("resources\\shaders\\vertex.vert", "resources\\shaders\\fragment.frag");

            RenderMng.Init(shader, Size);

            cube = new BaseCube(shader);

            ents.Add(cube);
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Entity.MegaUpdate(ents);
            RenderMng.Render();

            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }


        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        { 
            base.OnUpdateFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
