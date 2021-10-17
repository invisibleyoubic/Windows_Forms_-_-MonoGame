using Microsoft.Xna.Framework;

namespace BOX_PUSHER
{
    class Blocks
    {
        public int flag;
        public bool prohodimostt;
        public Vector2 blockPosition;

        public Blocks(int i, int j, int flag_)  // person
        {
            blockPosition = new Vector2(50 * i, 50 * j);
            flag = flag_;
        }

        public Blocks(bool prohodimostt_, int i, int j, int flag_)  // boxes, walls, goals and floor
        {
            prohodimostt = prohodimostt_;
            blockPosition = new Vector2(50 * i, 50 * j);
            flag = flag_;
        }
        
        public Blocks(bool prohodimostt_, int flag_)   // empty blocks
        {
            prohodimostt = prohodimostt_;
            flag = flag_;
        }
    }
}
