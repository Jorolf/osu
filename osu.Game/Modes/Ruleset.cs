﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Game.Modes.UI;
using System;
using System.Collections.Concurrent;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Overlays.Mods;

namespace osu.Game.Modes
{
    public class BeatmapStatistic
    {
        public FontAwesome Icon;
        public string Content;
        public string Name;
    }

    public abstract class Ruleset
    {
        private static ConcurrentDictionary<int, Type> availableRulesets = new ConcurrentDictionary<int, Type>();

        public abstract ScoreOverlay CreateScoreOverlay();

        public virtual IEnumerable<BeatmapStatistic> GetBeatmapStatistics(WorkingBeatmap beatmap) => new BeatmapStatistic[] { };

        public abstract IEnumerable<Mod> GetModsFor(ModType type);

        public abstract ScoreProcessor CreateScoreProcessor(int hitObjectCount);

        public abstract HitRenderer CreateHitRendererWith(Beatmap beatmap);

        public abstract HitObjectParser CreateHitObjectParser();

        public abstract DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap);

        public static void Register(Ruleset ruleset)
        {
            availableRulesets.TryAdd(ruleset.PlayMode, ruleset.GetType());
            Modes.PlayMode.Description.Add(ruleset.PlayMode, ruleset.Description);
        }

        protected abstract int PlayMode { get; }

        public virtual FontAwesome Icon => FontAwesome.fa_question_circle;

        protected abstract string Description { get; }

        public static Ruleset GetRuleset(int mode)
        {
            Type type;

            if (!availableRulesets.TryGetValue(mode, out type))
                return null;

            return Activator.CreateInstance(type) as Ruleset;
        }
    }
}
