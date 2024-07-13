namespace Starlight
{
	public abstract class Entity
	{

		/// <summary>
		/// Parent of
		/// </summary>
		public Entity? Parent { get; private set; }

		/// <summary>
		/// Updates every frame, do not run, the engine already does that.
		/// </summary>
		protected virtual void Update() { }

		/// <summary>
		/// Like update, but runs after all entities have run update, again, do not run, the engine already does that.
		/// </summary>
		protected virtual void LateUpdate() { }

		/// <summary>
		/// Like update, but does't run asynchronously, usefull when using OpenGL functions.
		/// </summary>
		protected virtual void SyncUpdate() { }

		private List<Entity> Children = [];

		/// <summary>
		/// Add entity to this object's children
		/// </summary>
		/// <param name="entity"></param>
		/// <exception cref="InvalidOperationException">If the entity already is a child of another entity</exception>
		public void AddChild(Entity entity, bool replace)
		{
			
			ArgumentNullException.ThrowIfNull(nameof(entity));

			if (entity.Parent != null && !replace)
			{
				throw new InvalidOperationException("The entity already has a parent.");
			}

			entity.Parent = this;
			Children.Add(entity);
		}

		/// <summary>
		/// Remove entity from this object's children
		/// </summary>
		/// <param name="entity"></param>
		public void RemoveChild(Entity entity) 
		{
			ArgumentNullException.ThrowIfNull(nameof(entity));

			entity.Parent = null;
			Children.Remove(entity);
		}

		/// <summary>
		/// Get this object's children
		/// </summary>
		/// <returns>A copy of the object's children list</returns>
		public List<Entity> GetChildren() { return new List<Entity>(Children); }

		internal static void SyncMegaUpdate(List<Entity> ents)
		{
            foreach (Entity entity in ents)
            {
                UpdateChildren(entity);
            }
        }

		internal static async void MegaUpdate(List<Entity> ents)
		{
			List<Task> updateTasks = new();

            List<Task> childUpdateTasks = new();

            foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity, childUpdateTasks)));
			}

			await Task.WhenAll(updateTasks);

			childUpdateTasks.Clear();

			foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity, childUpdateTasks, true)));
			}

			await Task.WhenAll(updateTasks);
		}

		internal static async void UpdateChildren(Entity entity, List<Task> childUpdateTasks, bool late = false)
		{
			if (!late) entity.Update();
			else entity.LateUpdate();

			List<Entity> children = entity.GetChildren();

			foreach (Entity child in children)
			{
				childUpdateTasks.Add(Task.Run(() => UpdateChildren(child, childUpdateTasks, late)));
			}

			await Task.WhenAll(childUpdateTasks.ToArray());
		}
        internal static void UpdateChildren(Entity entity)
        {
            entity.SyncUpdate();

            List<Entity> children = entity.GetChildren();

            foreach (Entity child in children)
            {
                UpdateChildren(child);
            }
        }

    }
}
