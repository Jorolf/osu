// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Backgrounds;
using osu.Game.Rulesets.Scoring;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;

namespace osu.Game.Users
{
    public abstract class Grade : Container
    {
        protected abstract Color4 BackgroundColourLight(OsuColour colour);
        protected abstract Color4 BackgroundColourDark(OsuColour colour);
        private OsuColour colour;

        protected abstract int BackgroundCount { get; }

        private const float shear = 1f;

        protected override void LoadComplete()
        {
            Size = new Vector2(140, 140);

            for (int i = 0; i < BackgroundCount; i++)
            {
                Add(new GradeBackground(BackgroundColourLight(colour), BackgroundColourDark(colour))
                {
                    RelativeSizeAxes = Axes.Both,
                    X = 30 * i,
                    Depth = i,
                });
            }
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            this.colour = colour;
        }

        private class GradeBackground : Container
        {
            public GradeBackground(Color4 light, Color4 dark)
            {
                BorderThickness = 15;
                BorderColour = Color4.White;
                Masking = true;

                EdgeEffect = new EdgeEffect
                {
                    Type = EdgeEffectType.Shadow,
                    Radius = 15,
                    Colour = new Color4(0,0,0,63),
                };

                CornerRadius = 40;

                Shear = new Vector2(shear, 0);

                Children = new Drawable[] {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        ColourInfo = new ColourInfo
                        {
                            TopLeft = dark,
                            TopRight = dark,
                            BottomLeft = light,
                            BottomRight = light,
                        },
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Shear = Shear * -1,
                        BorderColour = Color4.White,
                        Children = new Drawable[]
                        {
                            new GradeTriangles
                            {
                                RelativePositionAxes = Axes.X,
                                RelativeSizeAxes = Axes.Both,
                                X = -shear,
                                Width = shear * 2,
                                ColourDark = dark,
                                ColourLight = light,
                            }
                        }
                    },
                };
            }
        }

        private class GradeTriangles : Triangles
        {
            protected override float SpawnRatio => 0.5f;
        }

        public static Grade FromScoreRank(ScoreRank rank)
        {
            switch (rank)
            {
                case ScoreRank.A:
                    return new GradeA();
                default:
                    throw new ArgumentException();
            }
        }

        private class GradeA : Grade
        {
            protected override Color4 BackgroundColourLight(OsuColour colour) => colour.Green;

            protected override Color4 BackgroundColourDark(OsuColour colour) => colour.GreenDark;

            protected override int BackgroundCount => 2;

            [BackgroundDependencyLoader]
            private void load(FontStore font)
            {
                Add(new Container
                {
                    Height = 52,
                    RelativePositionAxes = Axes.X,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Depth = float.MinValue,
                    X = -shear / 2,
                    Children = new[]
                    {
                        new Sprite
                        {
                            Texture = font.Get("Venera/a"),
                            FillMode = FillMode.Fill,
                        }
                    },
                });
            }
        }
    }
}
