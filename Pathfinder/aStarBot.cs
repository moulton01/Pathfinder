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
    class aStarBot : AiBotBase
    {
        public aStar aStar;
        int index;
        Coord2 PlayerStartPos; 
        
        public aStarBot(int x, int y)
            : base(x, y)
        {
            aStar = new aStar();
            index = 0;
             
        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            if (aStar.finalPath.Count() == 0) 
            {
                aStar.finalPath.Clear();
                aStar.Build(level, this, plr);
                PlayerStartPos = plr.GridPosition; 
            }
            if (PlayerStartPos != plr.GridPosition)
            {
                aStar.finalPath.Clear();
                index = 0;
                PlayerStartPos = plr.GridPosition; 
                aStar.Build(level, this, plr);
            }
            
            Coord2 CurrentPos;
            CurrentPos = GridPosition;
            if (GridPosition != plr.GridPosition)
            {
                CurrentPos = aStar.finalPath[index];  
            }
            SetNextGridPosition(CurrentPos, level);
            index++;
        }
    }
}