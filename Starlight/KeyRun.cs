using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
	/// <summary>
	/// Class that runs functions when a key is pressed, only works if it's accessible from the entity megatable
	/// </summary>
	public class KeyRun : Entity
	{
		private readonly Keys key;
		private readonly GameWindow window;
		public delegate void ActionDelegate();
		private readonly ActionDelegate onPressed;

		/// <summary>
		/// KeyRun Constructor
		/// </summary>
		/// <param name="setwindow">Window to check for the keypress</param>
		/// <param name="setkey">Key that it listens to</param>
		/// <param name="onPressed">Function to run when the key is pressed</param>
		public KeyRun(GameWindow setwindow, Keys setkey, ActionDelegate onPressed)
		{
			this.window = setwindow;
			this.key = setkey;
			this.onPressed = onPressed;
		}
		

		protected override void Update()
		{
			if (window.KeyboardState.IsKeyDown(key))
			{
				onPressed();
			}
		}
	}
}
