// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Online.API.Requests.Responses;
using OpenTK.Graphics;

namespace osu.Game.Overlays.Changelog
{
    public class HeaderItem : FillFlowContainer
    {
        private static readonly Dictionary<string, Func<OsuColour, Color4>> build_colours = new Dictionary<string, Func<OsuColour, Color4>>
        {
            { "stable", osuColour => osuColour.BlueDarker },
            { "stable40", osuColour => osuColour.Blue },
            { "beta40", osuColour => osuColour.YellowLight },
            { "cuttingedge", osuColour => osuColour.YellowDark },
            { "web", osuColour => osuColour.Purple },
            { "wiki", osuColour => osuColour.Gray9 },
            { "lazer", osuColour => osuColour.Red }
        };

        private readonly APIChangelogBuild build;

        public HeaderItem(APIChangelogBuild build)
        {
            this.build = build;
            Width = build.IsFeatured ? 200 : 100;
            Height = 66.5f;
            Padding = new MarginPadding(5);

            Direction = FillDirection.Vertical;

            Children = new Drawable[]
            {
                new OsuSpriteText
                {
                    Text = build.Stream.DisplayName,
                    TextSize = 15,
                    Font = "Exo2.0-Black"
                },
                new OsuSpriteText
                {
                    Text = build.DisplayVersion,
                    TextSize = 20,
                    Font = "Exo2.0-Medium"

                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            if (build.Users > 0)
                Add(new OsuSpriteText
                {
                    Text = build.Users.ToString("#,### 'users online'"),
                    TextSize = 12.5f,
                    Colour = colour.DarkPurpleLight,
                });

            Circle bar;
            Add(bar = new Circle
            {
                RelativeSizeAxes = Axes.X,
                Height = 6,
                Margin = new MarginPadding { Bottom = 5 },
                Colour = build_colours[build.Stream.Name](colour)
            });

            SetLayoutPosition(bar, -1);
            InvalidateLayout();
        }
    }
}
