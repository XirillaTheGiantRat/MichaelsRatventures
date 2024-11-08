using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michaelsparty
{
    public interface ICharacter
    {
        (int x, int y) Position { get; set; }
        void Move();
        void Turn(string direction);
        void ResetPosition();
        Image RotateCharacterImage(int width, int height);
        public bool IsAtEdge { get; set; }
    }
}
