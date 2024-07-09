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

		internal static async void MegaUpdate(List<Entity> ents)
		{
			List<Task> updateTasks = new();
			foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity)));
			}

			await Task.WhenAll(updateTasks);

			foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity, true)));
			}

			await Task.WhenAll(updateTasks);

		}

		internal static async void UpdateChildren(Entity entity, bool late = false)
		{
			if (!late) entity.Update();
			else entity.LateUpdate();

			List<Entity> children = entity.GetChildren();
			List<Task> childUpdateTasks = new();

			foreach (Entity child in children)
			{
				childUpdateTasks.Add(Task.Run(() => UpdateChildren(child, late)));
			}

			await Task.WhenAll(childUpdateTasks.ToArray());
		}

	}
}
