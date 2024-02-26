using System;

namespace Aiirh.Basic.Sounds;

public static class SoundsCollection {

    private static readonly Pause Pause10 = new Pause(TimeSpan.FromMilliseconds(10));
    private static readonly Pause Pause20 = new Pause(TimeSpan.FromMilliseconds(20));
    private static readonly Pause Pause50 = new Pause(TimeSpan.FromMilliseconds(50));
    private static readonly Pause Pause500 = new Pause(TimeSpan.FromMilliseconds(500));

    private static readonly Sound SuccessNote = new Sound(1000, TimeSpan.FromMilliseconds(100));
    private static readonly Sound SuccessNoteExtra = new Sound(800, TimeSpan.FromMilliseconds(200));
    private static readonly Sound WarningNote = new Sound(207, TimeSpan.FromMilliseconds(200));
    private static readonly Sound ErrorNote = new Sound(165, TimeSpan.FromMilliseconds(100));

    public static SoundSequence Error => new SoundSequence(ErrorNote, Pause10, ErrorNote, Pause10, ErrorNote, Pause10, ErrorNote, Pause10, ErrorNote, Pause10, ErrorNote, Pause10, ErrorNote);
    public static SoundSequence Warning => new SoundSequence(WarningNote, Pause20, WarningNote, Pause20, WarningNote);
    public static SoundSequence Success => new SoundSequence(SuccessNote);
    public static SoundSequence Success1 => new SoundSequence(SuccessNote, Pause500, SuccessNoteExtra);
    public static SoundSequence Success2 => new SoundSequence(SuccessNote, Pause500, SuccessNoteExtra, Pause50, SuccessNoteExtra);
    public static SoundSequence Success3 => new SoundSequence(SuccessNote, Pause500, SuccessNoteExtra, Pause50, SuccessNoteExtra, Pause50, SuccessNoteExtra);
}