// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using NUnit.Framework;
using osu.Game.Online.API.Requests;
using osu.Game.Online.API.Requests.Responses;
using osu.Game.Overlays.Changelog;

namespace osu.Game.Tests.Visual
{
    [TestFixture]
    public class TestCaseChangelogHeader : OsuTestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new[] { typeof(APIChangelogBuild), typeof(HeaderItem), typeof(GetChangelogRequest) };

        public TestCaseChangelogHeader()
        {
            AddStep("show stable", () => Child = new HeaderItem(new APIChangelogBuild
            {
                DisplayVersion = "19180626.-1",
                IsFeatured = true,
                Users = 12342,
                Stream = new APIChangelogBuild.UpdateStream
                {
                    DisplayName = "Stable",
                    Name = "stable40"
                }
            }));
        }
    }
}
