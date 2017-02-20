﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens;
using osu.Game.Screens.Backgrounds;
using OpenTK.Graphics;

namespace osu.Game.Screens.Play
{
    class ModSelect : ScreenWhiteBox
    {
        protected override BackgroundMode CreateBackground() => new BackgroundModeCustom(@"Backgrounds/bg4");

        protected override void OnEntering(Screen last)
        {
            base.OnEntering(last);
            Background.Schedule(() => Background.FadeColour(Color4.DarkGray, 500));
        }

        protected override bool OnExiting(Screen next)
        {
            Background.Schedule(() => Background.FadeColour(Color4.White, 500));
            return base.OnExiting(next);
        }
    }
}
