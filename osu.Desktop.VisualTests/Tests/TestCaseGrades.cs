using osu.Framework.Testing;
using osu.Framework.Graphics;
using osu.Game.Users;
using OpenTK;
using OpenTK.Graphics;
using System;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Scoring;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseGrades : TestCase
    {
        public override string Description => "To show how good you are";

        public override void Reset()
        {
            base.Reset();

            FlowContainer<Grade> grades = new FillFlowContainer<Grade>()
            {
                Direction = FillDirection.Full,
                RelativeSizeAxes = Axes.Both,
                Margin = new MarginPadding { Left = 80, Top = 25 },
            };
            Add(grades);

            grades.Add(Grade.FromScoreRank(ScoreRank.A));
        }
    }
}
