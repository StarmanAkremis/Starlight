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

            RenderMng.Init("resources\\shaders\\vertex.vert", "resources\\shaders\\fragment.frag", Size);
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
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
            Entity.MegaUpdate(ents);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
