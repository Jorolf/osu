// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Lists;

namespace osu.Game.Beatmaps.ControlPoints
{
    public class ControlPointInfo
    {
        /// <summary>
        /// All timing points.
        /// </summary>
        public readonly SortedList<TimingControlPoint> TimingPoints = new SortedList<TimingControlPoint>(Comparer<TimingControlPoint>.Default);

        /// <summary>
        /// All difficulty points.
        /// </summary>
        public readonly SortedList<DifficultyControlPoint> DifficultyPoints = new SortedList<DifficultyControlPoint>(Comparer<DifficultyControlPoint>.Default);

        /// <summary>
        /// All sound points.
        /// </summary>
        public readonly SortedList<SoundControlPoint> SoundPoints = new SortedList<SoundControlPoint>(Comparer<SoundControlPoint>.Default);

        /// <summary>
        /// All effect points.
        /// </summary>
        public readonly SortedList<EffectControlPoint> EffectPoints = new SortedList<EffectControlPoint>(Comparer<EffectControlPoint>.Default);

        /// <summary>
        /// Finds the difficulty control point that is active at <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time to find the difficulty control point at.</param>
        /// <returns>The difficulty control point.</returns>
        public DifficultyControlPoint DifficultyPointAt(double time) => binarySearch(DifficultyPoints, time);

        /// <summary>
        /// Finds the effect control point that is active at <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time to find the effect control point at.</param>
        /// <returns>The effect control point.</returns>
        public EffectControlPoint EffectPointAt(double time) => binarySearch(EffectPoints, time);

        /// <summary>
        /// Finds the sound control point that is active at <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time to find the sound control point at.</param>
        /// <returns>The sound control point.</returns>
        public SoundControlPoint SoundPointAt(double time) => binarySearch(SoundPoints, time);

        /// <summary>
        /// Finds the timing control point that is active at <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time to find the timing control point at.</param>
        /// <returns>The timing control point.</returns>
        public TimingControlPoint TimingPointAt(double time) => binarySearch(TimingPoints, time);

        /// <summary>
        /// Finds the maximum BPM represented by any timing control point.
        /// </summary>
        public double BPMMaximum =>
            60000 / (TimingPoints.OrderBy(c => c.BeatLength).FirstOrDefault() ?? new TimingControlPoint()).BeatLength;

        /// <summary>
        /// Finds the minimum BPM represented by any timing control point.
        /// </summary>
        public double BPMMinimum =>
            60000 / (TimingPoints.OrderByDescending(c => c.BeatLength).FirstOrDefault() ?? new TimingControlPoint()).BeatLength;

        /// <summary>
        /// Finds the mode BPM (most common BPM) represented by the control points.
        /// </summary>
        public double BPMMode =>
            60000 / (TimingPoints.GroupBy(c => c.BeatLength).OrderByDescending(grp => grp.Count()).FirstOrDefault()?.FirstOrDefault() ?? new TimingControlPoint()).BeatLength;

        private T binarySearch<T>(SortedList<T> list, double time)
            where T : ControlPoint, new()
        {
            if (list.Count == 0)
                return new T();

            if (time < list[0].Time)
                return list[0];

            int index = list.BinarySearch(new T() { Time = time });

            // Check if we've found an exact match (t == time)
            if (index >= 0)
                return list[index];

            index = ~index;

            if (index == list.Count)
                return list[list.Count - 1];
            return list[index - 1];
        }
    }
}