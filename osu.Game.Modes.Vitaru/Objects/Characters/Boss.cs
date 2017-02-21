using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using System;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Boss : Character
    {
        public int StartTime { get; set; }
        public Vector2 BossSpeed { get; set; } = new Vector2(0, 0);
        
        

        public Boss(Container parent) : base(parent)
        {
            Children = new[]
            {
                new DrawableBoss()
                {
                    Origin = Anchor.Centre,
                },
            };
            Health = 1000;
            Team = 1;
            Add(hitbox = new Hitbox()
            {
                Alpha = 1,
                HitboxWidth = 32,
                HitboxColor = Color4.Green,
            });
        }
        protected override void Update()
        {
            base.Update();
            Vector2 bossPosition = Position;
            bossPosition.Y += BossSpeed.Y * (float)(Clock.ElapsedFrameTime);
            bossPosition.X += BossSpeed.X * (float)(Clock.ElapsedFrameTime);
            MoveTo(bossPosition);
        }
    }
}