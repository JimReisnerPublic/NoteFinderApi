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
                "diminished7" => ChordDefinitions.Diminished,
                "6" => ChordDefinitions.Six,
                "minor6" => ChordDefinitions.MinorSix,
                "major9" => ChordDefinitions.MajorNine,
                "minor9" => ChordDefinitions.MinorNine,
                "9" => ChordDefinitions.Nine,
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
                "pentatonicminor" => ScaleDefinitions.PentatonicMinor,
                "pentatonicmajor" => ScaleDefinitions.PentatonicMajor,
                _ => throw new ArgumentException("Invalid scale name")
            };
        }
    }
}
