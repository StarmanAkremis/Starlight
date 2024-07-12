using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class WorldObject : Entity
    {
        protected Vector3 position;
        protected Vector3 rotation;
    }
}
