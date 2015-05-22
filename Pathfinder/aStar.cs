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
    class aStar
    {
    public bool[,] closed; //whether or not location is closed. 
    public float[,] cost; // cost value for each location. 
    public float[,] heuristic; //heuristic cost of all nodes. 
    public Coord2[,] link; //link for each location = coords of a neighbouring location. 
    public List<Coord2> finalPath;
    public List<Coord2> openList; 
    public bool[,] inPath; //whether or not a location is in the final path. 
       

        public aStar()
        {
            closed = new bool[40, 40];
            cost = new float[40, 40];
            heuristic = new float[40, 40];
            link = new Coord2[40, 40];
            inPath = new bool[40, 40];
            finalPath = new List<Coord2>();
            openList = new List<Coord2>(); 
            

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    
                    closed[i, j] = false;
                    cost[i, j] = 999999;
                    link[i, j] = new Coord2(-1, -1);
                    inPath[i, j] = false;
                }
            }
        }

        public void Build(Level level, AiBotBase bot, Player plr)
        {
            
            closed[bot.GridPosition.X, bot.GridPosition.Y] = false;
            cost[bot.GridPosition.X, bot.GridPosition.Y] = 0;
            openList.Add(bot.GridPosition);
            
            Coord2 lowestCostLoc = new Coord2(-1, -1); 

            for (int i = 0; i < 39; i++)
            {
            for (int j = 0; j < 39; j++)
                {
                    heuristic[i, j] = 10*(Math.Abs(i - plr.GridPosition.X) + Math.Abs(j - plr.GridPosition.Y));
                }
            }
     
            while(!closed[plr.GridPosition.X, plr.GridPosition.Y])
            {   
                float lowestCost = 999999;
                for (int x = 0; x < 40; x++)
                {
                    for (int y = 0; y < 40; y++)
                    {
                        if ((cost[x, y] + heuristic[x, y]) < lowestCost && !closed[x, y])//&& openList.Contains(new Coord2(x,y))
                        {
                            lowestCost = cost[x, y] + heuristic[x, y];
                            lowestCostLoc = new Coord2(x, y);
                        }
                    }
                }
                closed[lowestCostLoc.X, lowestCostLoc.Y] = true;
                //openList.Remove(lowestCostLoc);

                //Coord2 neighbourLocation;
                //calculates the cost for the 8 neighbouring grid squares. 
                for (int nX = -1; nX <= 1; nX++)
                {
                    for (int nY=-1; nY<= 1; nY++)
                    {
                        //neighbourLocation = new Coord2(lowestCostLoc.X + nX, lowestCostLoc.Y + nY); 
                        if (level.ValidPosition(new Coord2(lowestCostLoc.X + nX, lowestCostLoc.Y + nY)))
                        {
                            float newCost = 999999;

                            if (nX != 0 ^ nY != 0)
                                newCost = lowestCost + 1;
                            else if (nX != 0 && nY != 0)
                                newCost = lowestCost + 1.4f;

                            //if (!openList.Contains(neighbourLocation) && neighbourLocation != bot.GridPosition)
                            //{
                            //    openList.Add(neighbourLocation);
                            //}
                        
                            //if the new calculated cost is lower and it is not blocked by a wall, set the cost to the new cost and set the link to the location that was just closed. 
                            if (newCost < cost[lowestCostLoc.X + nX, lowestCostLoc.Y + nY] && level.ValidPosition(new Coord2(lowestCostLoc.X + nX, lowestCostLoc.Y + nY)) && !closed[lowestCostLoc.X + nX, lowestCostLoc.Y + nY])
                            {
                                cost[lowestCostLoc.X + nX, lowestCostLoc.Y + nY] = newCost;// + heuristic[lowestCostLoc.X + nX, lowestCostLoc.Y + nY];
                                link[lowestCostLoc.X + nX, lowestCostLoc.Y + nY] = new Coord2(lowestCostLoc.X, lowestCostLoc.Y);

                            }
                        }
                    }
                }
            } 
            //end of while loop. 

            //set to true when we are back at the bot position. 
            bool done = false; 
            //start of path.
            Coord2 nextclosed = plr.GridPosition;
            while (!done)
            {
                finalPath.Add(nextclosed); 
                inPath[nextclosed.X, nextclosed.Y] = true; 
                nextclosed = link[nextclosed.X, nextclosed.Y];
                if (nextclosed == bot.GridPosition)
                {
                    finalPath.Add(bot.GridPosition);
                    done = true;
                }
            }
            finalPath.Reverse(); 
        }

    }  
}
