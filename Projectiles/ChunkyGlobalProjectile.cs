using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chunky;
using Chunky.NPCs;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Laugicality.Projectiles
{
    public class ChunkyGlobalProjectile : GlobalProjectile
    {
        public float xV = 0f;
        public float yV = 0f;
        public int ai = 0;
        public float xP = 0;
        public float yP = 0;
        public bool stopped = false;

        public override void SetDefaults(Projectile projectile)
        {
            ai = projectile.aiStyle;
            xV = 0f;
            yV = 0f;
        }

        public override bool PreAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            Player projOwner = Main.player[projectile.owner];

            //King Crimson
            if (ChunkyWorld.TimeSkipping > 0)
            {
                //This erases the AI and makes the velocity permanently equal to 75% the initial velocity
                if (xV == 0 && yV == 0)
                {
                    xV = projectile.velocity.X;
                    yV = projectile.velocity.Y;
                }
                projectile.velocity.X = xV * 0.75f;
                projectile.velocity.Y = yV * 0.75f;
                return false;
            }
            else
            {
                if (xV != 0 || yV != 0)
                {
                    projectile.velocity.X = xV;
                    projectile.velocity.Y = yV;
                }
            }

            //Stopping time
            if (ChunkyWorld.TOKIWOTOMARE > 0)
            {
                projectile.timeLeft++;
                if (!stopped)
                {
                    xV = projectile.velocity.X;
                    yV = projectile.velocity.Y;
                    stopped = true;
                    xP = 0;
                    yP = 0;
                }
                if (stopped)
                {
                    projectile.velocity.X *= 0.01f;
                    projectile.velocity.Y *= 0.01f;
                    if (xP == 0 || yP == 0)
                    {
                        xP = projectile.position.X;
                        yP = projectile.position.Y;
                    }
                    else
                    {
                        projectile.position.X = xP;
                        projectile.position.Y = yP;
                    }
                }
                return false;
            }
            else
            {
                if (stopped)
                {
                    projectile.velocity.X = xV;
                    projectile.velocity.Y = yV;
                    stopped = false;
                }
                if (!stopped)
                {
                    xV = 0f;
                    yV = 0f;
                }
                return true;
            }
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
    }
}