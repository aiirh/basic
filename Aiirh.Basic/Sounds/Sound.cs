using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Sounds
{

    public interface ISound
    {
        double Frequency { get; }

        TimeSpan Time { get; }
    }

    public class Sound : ISound
    {
        public double Frequency { get; }
        public TimeSpan Time { get; }

        public Sound(double frequency, TimeSpan time)
        {
            Frequency = frequency;
            Time = time;
        }
    }

    public class Pause : ISound
    {
        public double Frequency => 0;
        public TimeSpan Time { get; }

        public Pause(TimeSpan time)
        {
            Time = time;
        }
    }

    public class SoundSequence
    {
        public bool PlaySound => Notes?.Any() ?? false;

        public IReadOnlyCollection<SoundNode> Notes { get; }

        public SoundSequence(double frequency, TimeSpan time) : this(new Sound(frequency, time)) { }

        public SoundSequence(params ISound[] sounds) : this(sounds.ToList()) { }

        public SoundSequence(IList<ISound> sounds)
        {
            var soundsCollection = new List<SoundNode>();
            if (sounds.Count > 0)
            {
                soundsCollection.Add(new SoundNode(sounds[0]));
                var currentTotalTime = sounds[0].Time;
                for (var i = 1; i < sounds.Count; i++)
                {
                    soundsCollection.Add(new SoundNode(sounds[i], currentTotalTime));
                    currentTotalTime += sounds[i].Time;
                }
            }
            Notes = soundsCollection;
        }

        public class SoundNode
        {

            public double Frequency { get; }
            public double TimeInSeconds { get; }
            public double DelayInSeconds { get; }

            public SoundNode(ISound sound, TimeSpan delay = default)
            {
                Frequency = sound.Frequency;
                TimeInSeconds = sound.Time.TotalSeconds;
                DelayInSeconds = delay.IsNullOrDefault() ? 0 : delay.TotalSeconds;
            }
        }
    }
}
