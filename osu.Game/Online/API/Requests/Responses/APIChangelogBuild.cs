// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;

namespace osu.Game.Online.API.Requests.Responses
{
    public class APIChangelogBuild
    {
        [JsonProperty("id")]
        public int ID;

        [JsonProperty("version")]
        public string Version;

        [JsonProperty("display_version")]
        public string DisplayVersion;

        [JsonProperty("users")]
        public int Users;

        [JsonProperty("is_featured")]
        public bool IsFeatured;

        [JsonProperty("update_stream")]
        public UpdateStream Stream;

        public struct UpdateStream
        {
            [JsonProperty("id")]
            public int ID;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("display_name")]
            public string DisplayName;
        }
    }
}
