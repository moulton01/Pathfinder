using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Pathfinder
{
    class AiBotSimple2 : AiBotBase 
    {
        public AiBotSimple2(int x, int y) : base(x,y)
        {

        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            Coord2 CurrentPos;
            bool blocked = false; 
            CurrentPos = GridPosition;
           
            if (plr.GridPosition.X > CurrentPos.X)
            {
                CurrentPos.X += 1;
            }
            else if (plr.GridPosition.X < CurrentPos.X)
            {
                CurrentPos.X -= 1; 
            }
            else if (plr.GridPosition.Y < CurrentPos.Y)
            {
                CurrentPos.Y -= 1;
            }
            else if (plr.GridPosition.Y > CurrentPos.Y)
            {
                CurrentPos.Y += 1;
            }
            SetNextGridPosition(CurrentPos, level);

            if (!level.ValidPosition(CurrentPos))
            {
                blocked = true; 
            }
            if (blocked)
            {
                if (plr.GridPosition.Y < CurrentPos.Y)
                {
                    CurrentPos = GridPosition;
                    CurrentPos.Y -= 1;
                    SetNextGridPosition(CurrentPos, level);
                }
                if (plr.GridPosition.Y > CurrentPos.Y)
                {
                    CurrentPos = GridPosition;
                    CurrentPos.Y += 1;
                    SetNextGridPosition(CurrentPos, level);
                }
                else
                {
                    CurrentPos = GridPosition;
                    CurrentPos.Y += 1;
                    SetNextGridPosition(CurrentPos, level);
                }
            }
            
        }
    }
}
