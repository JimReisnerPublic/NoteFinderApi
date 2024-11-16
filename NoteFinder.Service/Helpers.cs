using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoteFinder.Interfaces;
using NoteFinder.Service.Definitions;

namespace NoteFinder.Helpers
{
    public static class DefinitionsHelper
    {
        public static IInterval[] GetChordIntervals(string chordName)
        {
            return chordName.ToLower() switch
            {
                "major" => ChordDefinitions.MajorTriad,
                "minor" => ChordDefinitions.MinorTriad,
                "augmented" => ChordDefinitions.AugmentedTriad,
                "diminished" => ChordDefinitions.DiminishedTriad,
                "sus4" => ChordDefinitions.Sus4Triad,
                "sus2" => ChordDefinitions.Sus2Triad,
                "major7" => ChordDefinitions.Major7,
                "minor7" => ChordDefinitions.Minor7,
                "dominant7" => ChordDefinitions.Dominant7,
                "minor7flat5" => ChordDefinitions.Minor7Flat5,
                "m7flat5" => ChordDefinitions.Minor7Flat5,
                "m7b5" => ChordDefinitions.Minor7Flat5,
                "diminished7" => ChordDefinitions.Diminished7,
                "dim7" => ChordDefinitions.Diminished7,
                "6" => ChordDefinitions.Six,
                "minor6" => ChordDefinitions.MinorSix,
                "major9" => ChordDefinitions.MajorNine,
                "minor9" => ChordDefinitions.MinorNine,
                "9" => ChordDefinitions.Nine,
                "minor11" => ChordDefinitions.Minor11,
                "major11" => ChordDefinitions.Major11,
                "minor13" => ChordDefinitions.Minor13,
                "major13" => ChordDefinitions.Major13,
                "augmented7" => ChordDefinitions.Augmented7,
                "augmentedmajor7" => ChordDefinitions.AugmentedMajor7,
                "minormajor7" => ChordDefinitions.MinorMajor7,
                "7sus4" => ChordDefinitions.SevenSus4,
                "9sus4" => ChordDefinitions.NineSus4,
                "13sus4" => ChordDefinitions.ThirteenSus4,
                "7#5" => ChordDefinitions.SevenSharp5,
                "7b9" => ChordDefinitions.SevenFlat9,
                "7#11" => ChordDefinitions.SevenSharp11,
                "13b9" => ChordDefinitions.ThirteenFlat9,
                "majoradd9" => ChordDefinitions.MajorAdd9,
                "7#9" => ChordDefinitions.Dominant7Sharp9, // Hendrix chord
                "hendrix" => ChordDefinitions.Dominant7Sharp9, // Alternative name for 7#9
                "dominant7sharp9" => ChordDefinitions.Dominant7Sharp9,
                "powerchord" => ChordDefinitions.PowerChord,
                _ => throw new ArgumentException("Invalid chord name")
            };
        }

        public static IInterval[] GetScaleIntervals(string scaleName)
        {
            return scaleName.ToLower() switch
            {
                "ionian" or "major" => ScaleDefinitions.Ionian,
                "dorian" => ScaleDefinitions.Dorian,
                "phrygian" => ScaleDefinitions.Phrygian,
                "lydian" => ScaleDefinitions.Lydian,
                "mixolydian" => ScaleDefinitions.Mixolydian,
                "aeolian" or "minor" => ScaleDefinitions.Aeolian,
                "locrian" => ScaleDefinitions.Locrian,
                "harmonicminor" => ScaleDefinitions.HarmonicMinor,
                "pentatonicminor" => ScaleDefinitions.PentatonicMinor,
                "pentatonicmajor" => ScaleDefinitions.PentatonicMajor,
                _ => throw new ArgumentException("Invalid scale name")
            };
        }
    }
}
