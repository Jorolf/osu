// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Users;
using osu.Game.Rulesets.Replays;
using System.Linq;

namespace osu.Game.Rulesets.Scoring
{
    public class Score
    {
        public ScoreRank Rank { get; set; }

        [JsonProperty(@"score")]
        public double TotalScore { get; set; }

        public double Accuracy { get; set; }

        public double Health { get; set; } = 1;

        [JsonProperty(@"max_combo")]
        public int MaxCombo { get; set; }

        public int Combo { get; set; }

        [JsonProperty(@"mods")]
        protected string[] ModStrings { get; set; }

        public RulesetInfo Ruleset { get; set; }

        private Mod[] mods;

        public Mod[] Mods
        {
            get
            {
                if (mods != null)
                    return mods;
                mods = new Mod[ModStrings.Length];
                Ruleset ruleset = (Ruleset ?? Beatmap.Ruleset).CreateInstance();
                List<Mod> rulesetMods = new List<Mod>(ruleset.GetAllMods());
                System.Diagnostics.Debug.WriteLine(rulesetMods.Select(mod => mod.ShortName).Aggregate((s1, s2) => s1 + s2));
                for (int i = 0; i < ModStrings.Length; i++)
                {
                    mods[i] = rulesetMods.Find(mod => mod.ShortName == ModStrings[i]);
                    if(mods[i] == null)
                        throw new ArgumentNullException(ModStrings[i]);
                }
                return mods;
            }
        }

        [JsonProperty(@"user")]
        public User User;

        [JsonProperty(@"replay_data")]
        public Replay Replay;

        public BeatmapInfo Beatmap;

        [JsonProperty(@"score_id")]
        public long OnlineScoreID;

        [JsonProperty(@"created_at")]
        public DateTimeOffset Date;

        [JsonProperty(@"statistics")]
        private Dictionary<string, dynamic> jsonStats
        {
            set
            {
                foreach (var kvp in value)
                {
                    string key = kvp.Key;
                    switch (key)
                    {
                        case @"count_300":
                            key = @"300";
                            break;
                        case @"count_100":
                            key = @"100";
                            break;
                        case @"count_50":
                            key = @"50";
                            break;
                        case @"count_miss":
                            key = @"x";
                            break;
                        default:
                            continue;
                    }

                    Statistics.Add(key, kvp.Value);
                }
            }
        }

        public Dictionary<string, dynamic> Statistics = new Dictionary<string, dynamic>();
    }
}
