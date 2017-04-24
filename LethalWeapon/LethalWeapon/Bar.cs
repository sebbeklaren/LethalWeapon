using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalWeapon
{
    class Bar : Gui
    {

        public Bar(ContentManager content,  int maxHealth, int maxEnergy, Vector2 targetPosition)
            : base(content, maxHealth, maxEnergy)
        {
            health = maxHealth;
            energy = maxEnergy;
            
        }
    }
}
