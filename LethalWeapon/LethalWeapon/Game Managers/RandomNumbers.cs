using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    public class RandomNumbers
    {
        Random randomDirection, randomX, randomY;
        public int randDirect;
        public int posX, posY;

        public void RandomDirection()
        {
            randomDirection = new Random();
            randDirect = randomDirection.Next(0, 100);
        }
        public void RandomPOsition()
        {
            randomX = new Random();
            randomY = new Random();
            posX = randomX.Next(2, 950);
            posY = randomY.Next(2, 735);
        }
        public void Update()
        {
            RandomDirection();
            RandomPOsition();
        }

    }
}
