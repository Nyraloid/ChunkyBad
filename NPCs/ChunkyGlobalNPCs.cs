﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace Chunky.NPCs
{
    public class ChunkyGlobalNPCs : GlobalNPC
    {
        public float xV = 0;
        public float yV = 0;
        public float xP = 0;
        public float yP = 0;

        public override void SetDefaults(NPC npc)
        {
            

        }

        public override void ResetEffects(NPC npc)
        {

        }

        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (ChunkyWorld.TOKIWOTOMARE > 1) spawnRate = 0;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0) npc.lifeRegen = 0;
        }

        public override bool PreAI(NPC npc)
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0) return false;
            if (ChunkyWorld.TimeSkipping > 0) return false;
            return true;
        }

        public override void PostAI(NPC npc)
        {
            //Za Warudo
            if (ChunkyWorld.TOKIWOTOMARE > 0)
            {
                if (xV == 0 && yV == 0)
                {
                    xV = npc.velocity.X;
                    yV = npc.velocity.Y;
                }
                npc.velocity.X = 0;
                npc.velocity.Y = 0;

                //keeping the npc in the same place (so gravity doesn't affect it)
                if (xP == 0 && yP == 0)
                {
                    xP = npc.position.X;
                    yP = npc.position.Y;
                }
                else
                {
                    npc.position.X = xP;
                    npc.position.Y = yP;
                }
            }

            //Undoing the effects of time stop
            else
            {
                if (xV != 0 || yV != 0)
                {
                    npc.velocity.X = xV;
                    npc.velocity.Y = yV;
                    xV = 0;
                    yV = 0;
                }
                xP = 0;
                yP = 0;
            }

            //King Crimson
            if (ChunkyWorld.TimeSkipping > 0)
            {
                //This erases the AI and makes the velocity permanently equal to 75% the initial velocity
                if (xV == 0 && yV == 0)
                {
                    xV = npc.velocity.X;
                    yV = npc.velocity.Y;
                }
                npc.velocity.X = xV * 0.75f;
                npc.velocity.Y = yV * 0.75f;
            }
            else
            {
                if (xV != 0 || yV != 0)
                {
                    npc.velocity.X = xV;
                    npc.velocity.Y = yV;
                }
            }
        }

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Golem) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfestedMeteorite"), Main.rand.Next(1, 5));
        }

        //all global things need this
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
    }
}