using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public class BaseCube : WorldSolid
    {
        public BaseCube()
        {
            vertices =
            [
                -0.5f,  0.5f, 0.5f,
                -0.5f, -0.5f, 0.5f,
                 0.5f,  0.5f, 0.5f
            ];

            texCoords =
            [
                0, 1,
                0, 0,
                1, 1
            ];

            indices =
            [
                1,2,3
            ];
            RenderMng.AddObj(renderInfo);
        }
    }
}
