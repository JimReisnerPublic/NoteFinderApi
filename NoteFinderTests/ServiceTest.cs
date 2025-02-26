using NoteFinder.Service;
using NoteFinder.Service.Definitions;
using Xunit;
using NoteFinder.Interfaces;
using System.Collections.Generic;
using System.Linq;
using NoteFinder.Helpers;
using System;

namespace NoteFinder.Tests
{
    public class ChordDefinitionsTests
    {
        private static readonly Intervals m_Intervals = new Intervals();

        [Fact]
        public void MajorTriad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MajorTriad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
        }

        [Fact]
        public void MinorTriad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MinorTriad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
        }

        [Fact]
        public void AugmentedTriad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.AugmentedTriad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Minor 6th", intervals[1].IntervalName);
        }

        [Fact]
        public void DiminishedTriad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.DiminishedTriad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Tritone", intervals[1].IntervalName);
        }

        [Fact]
        public void Sus4Triad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Sus4Triad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Perfect 4th", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
        }

        [Fact]
        public void Sus2Triad_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Sus2Triad;
            Assert.Equal(2, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.Equal("Major 2nd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
        }

        [Fact]
        public void Major7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Major7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void Minor7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Minor7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void Dominant7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Dominant7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void Minor7Flat5_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Minor7Flat5;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Tritone", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void Diminished7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Diminished7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Tritone", intervals[1].IntervalName);
            Assert.Equal("Major 6th", intervals[2].IntervalName);
        }

        [Fact]
        public void Six_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Six;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 6th", intervals[2].IntervalName);
        }

        [Fact]
        public void MinorSix_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MinorSix;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 6th", intervals[2].IntervalName);
        }

        [Fact]
        public void MajorNine_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MajorNine;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
        }

        [Fact]
        public void MinorNine_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MinorNine;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
        }

        [Fact]
        public void Nine_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Nine;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
        }

        [Fact]
        public void Dominant11_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Dominant11;
            Assert.Equal(5, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
        }

        [Fact]
        public void Major13_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Major13;
            Assert.Equal(6, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.IsType<SingleInterval>(intervals[5]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
            Assert.Equal("Major 6th", intervals[5].IntervalName);
        }

        [Fact]
        public void Dominant7b5_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Dominant7b5;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Tritone", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void Dominant7Sharp9_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Dominant7Sharp9;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Minor 3rd", intervals[3].IntervalName);
        }

        [Fact]
        public void MajorAdd9_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MajorAdd9;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 2nd", intervals[2].IntervalName);
        }

        [Fact]
        public void PowerChord_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.PowerChord;
            Assert.Equal(1, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.Equal("Perfect 5th", intervals[0].IntervalName);
        }

        [Fact]
        public void Minor11_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Minor11;
            Assert.Equal(5, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
        }

        [Fact]
        public void Major11_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Major11;
            Assert.Equal(5, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
        }

        [Fact]
        public void Minor13_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Minor13;
            Assert.Equal(6, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.IsType<SingleInterval>(intervals[5]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
            Assert.Equal("Major 6th", intervals[5].IntervalName);
        }

        [Fact]
        public void Augmented7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.Augmented7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Minor 6th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void AugmentedMajor7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.AugmentedMajor7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Minor 6th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void MinorMajor7_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.MinorMajor7;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Minor 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Major 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void SevenSus4_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.SevenSus4;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Perfect 4th", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void NineSus4_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.NineSus4;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Perfect 4th", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
        }

        [Fact]
        public void ThirteenSus4_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.ThirteenSus4;
            Assert.Equal(5, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.Equal("Perfect 4th", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Major 2nd", intervals[3].IntervalName);
            Assert.Equal("Major 6th", intervals[4].IntervalName);
        }

        [Fact]
        public void SevenSharp5_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.SevenSharp5;
            Assert.Equal(3, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Minor 6th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
        }

        [Fact]
        public void SevenFlat9_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.SevenFlat9;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Minor 2nd", intervals[3].IntervalName);
        }

        [Fact]
        public void SevenSharp11_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.SevenSharp11;
            Assert.Equal(4, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Tritone", intervals[3].IntervalName);
        }

        [Fact]
        public void ThirteenFlat9_ReturnsCorrectIntervals()
        {
            var intervals = ChordDefinitions.ThirteenFlat9;
            Assert.Equal(6, intervals.Length);
            Assert.IsType<SingleInterval>(intervals[0]);
            Assert.IsType<SingleInterval>(intervals[1]);
            Assert.IsType<SingleInterval>(intervals[2]);
            Assert.IsType<SingleInterval>(intervals[3]);
            Assert.IsType<SingleInterval>(intervals[4]);
            Assert.IsType<SingleInterval>(intervals[5]);
            Assert.Equal("Major 3rd", intervals[0].IntervalName);
            Assert.Equal("Perfect 5th", intervals[1].IntervalName);
            Assert.Equal("Minor 7th", intervals[2].IntervalName);
            Assert.Equal("Minor 2nd", intervals[3].IntervalName);
            Assert.Equal("Perfect 4th", intervals[4].IntervalName);
            Assert.Equal("Major 6th", intervals[5].IntervalName);
        }

        [Theory]
        [InlineData("ionian", 1, "Major", "I")]
        [InlineData("ionian", 2, "Minor", "ii")]
        [InlineData("ionian", 3, "Minor", "iii")]
        [InlineData("ionian", 4, "Major", "IV")]
        [InlineData("ionian", 5, "Major", "V")]
        [InlineData("ionian", 6, "Minor", "vi")]
        [InlineData("ionian", 7, "Diminished", "vii°")]
        [InlineData("aeolian", 1, "Minor", "i")]
        [InlineData("aeolian", 2, "Diminished", "ii°")]
        [InlineData("aeolian", 3, "Major", "III")]
        [InlineData("aeolian", 4, "Minor", "iv")]
        [InlineData("aeolian", 5, "Minor", "v")]
        [InlineData("aeolian", 6, "Major", "VI")]
        [InlineData("aeolian", 7, "Major", "#vii")]
        public void GetRomanNumeral_ReturnsCorrectNumeral(string scaleName, int degree, string chordType, string expectedNumeral)
        {
            Assert.Equal(expectedNumeral, ChordDefinitions.GetRomanNumeral(scaleName, degree, chordType));
        }

        [Fact]
        public void GetRomanNumeral_InvalidScaleName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => ChordDefinitions.GetRomanNumeral("invalidscale", 1, "Major"));
        }

        [Fact]
        public void GetChordOfScaleDegree_ReturnsChordAndName()
        {
            var (chord, chordName) = ChordDefinitions.GetChordOfScaleDegree("C", "ionian", 1);
            Assert.NotNull(chord);
            Assert.NotEmpty(chordName);
        }

        [Fact]
        public void GetChordName_ReturnsCorrectName()
        {
            IInterval[] majorIntervals = { m_Intervals.Major3rd, m_Intervals.Perfect5th };
            IInterval[] minorIntervals = { m_Intervals.Minor3rd, m_Intervals.Perfect5th };

            Assert.Equal("Major", ChordDefinitions.GetChordName(majorIntervals));
            Assert.Equal("Minor", ChordDefinitions.GetChordName(minorIntervals));
        }

        [Fact]
        public void GetChordName_UnknownChord_ReturnsUnknown()
        {
            IInterval[] unknownIntervals = { m_Intervals.Major2nd, m_Intervals.Perfect4th };
            Assert.Equal("Unknown", ChordDefinitions.GetChordName(unknownIntervals));
        }

        [Fact]
        public void GetChordName_HalfDiminished7_ReturnsCorrectName()
        {
            IInterval[] halfDiminishedIntervals = { m_Intervals.Minor3rd, m_Intervals.Tritone, m_Intervals.Minor7th };
            //Create Interval List
            var intervalSet = new HashSet<int>(halfDiminishedIntervals.Select(i => i.SemitonesFromRoot));
            if (intervalSet.SetEquals(new[] { 3, 6, 10 }))
            {
                Assert.Equal("Half-Diminished7", ChordDefinitions.GetChordName(halfDiminishedIntervals));
            }
        }

        [Fact]
        public void GetChordName_Diminished7_ReturnsCorrectName()
        {
            IInterval[] diminishedIntervals = { m_Intervals.Minor3rd, m_Intervals.Tritone, m_Intervals.Diminished7 };
            var intervalSet = new HashSet<int>(diminishedIntervals.Select(i => i.SemitonesFromRoot));
            if (intervalSet.SetEquals(new[] { 3, 6, 9 }))
            {
                Assert.Equal("Diminished7", ChordDefinitions.GetChordName(diminishedIntervals));
            }
        }
    }

    public class DefinitionsHelperTests
    {
        [Fact]
        public void GetChordIntervals_Major_ReturnsCorrectIntervals()
        {
            var intervals = DefinitionsHelper.GetChordIntervals("major");
            Assert.NotNull(intervals);
            Assert.NotEmpty(intervals);
        }

        [Fact]
        public void GetChordIntervals_InvalidChordName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => DefinitionsHelper.GetChordIntervals("invalidchord"));
        }

        [Fact]
        public void GetScaleIntervals_Ionian_ReturnsCorrectIntervals()
        {
            var intervals = DefinitionsHelper.GetScaleIntervals("ionian");
            Assert.NotNull(intervals);
            Assert.NotEmpty(intervals);
        }

        [Fact]
        public void GetScaleIntervals_InvalidScaleName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => DefinitionsHelper.GetScaleIntervals("invalidscale"));
        }

        [Theory]
        [InlineData("major", 2)]
        [InlineData("minor", 2)]
        [InlineData("augmented", 2)]
        [InlineData("diminished", 2)]
        [InlineData("sus4", 2)]
        [InlineData("sus2", 2)]
        [InlineData("major7", 3)]
        [InlineData("minor7", 3)]
        [InlineData("dominant7", 3)]
        [InlineData("minor7flat5", 3)]
        [InlineData("diminished7", 3)]
        [InlineData("6", 3)]
        [InlineData("minor6", 3)]
        [InlineData("major9", 4)]
        [InlineData("minor9", 4)]
        [InlineData("9", 4)]
        [InlineData("dominant11", 5)]
        [InlineData("major13", 6)]
        [InlineData("dominant7b5", 3)]
        [InlineData("dominant7sharp9", 4)]
        [InlineData("majoradd9", 3)]
        [InlineData("powerchord", 1)]
        [InlineData("minor11", 5)]
        [InlineData("major11", 5)]
        [InlineData("minor13", 6)]
        [InlineData("augmented7", 3)]
        [InlineData("augmentedmajor7", 3)]
        [InlineData("minormajor7", 3)]
        [InlineData("7sus4", 3)]
        [InlineData("9sus4", 4)]
        [InlineData("13sus4", 5)]
        [InlineData("7#5", 3)]
        [InlineData("7b9", 4)]
        [InlineData("7#11", 4)]
        [InlineData("13b9", 6)]
        public void GetChordIntervals_ReturnsCorrectNumberOfIntervals(string chordName, int expectedNumberOfIntervals)
        {
            var intervals = DefinitionsHelper.GetChordIntervals(chordName);
            Assert.Equal(expectedNumberOfIntervals, intervals.Length);
        }
    }

    public class NoteCollectionTests
    {
        [Fact]
        public void NoteCollection_Creation_AddsNotesAndIntervals()
        {
            Intervals intervals = new Intervals();
            IInterval[] scaleIntervals = { intervals.Major2nd, intervals.Major3rd, intervals.Perfect5th };
            NoteCollection collection = new NoteCollection("C", scaleIntervals);

            Assert.NotNull(collection.NotesAndIntervals);
            Assert.NotEmpty(collection.NotesAndIntervals);
        }

        [Fact]
        public void NoteCollection_GetProperlyNamedNotes_ReturnsCorrectNames()
        {
            Intervals intervals = new Intervals();
            IInterval[] scaleIntervals = { intervals.Major2nd, intervals.Major3rd, intervals.Perfect5th };
            NoteCollection collection = new NoteCollection("C", scaleIntervals);

            List<string> noteNames = collection.GetProperlyNamedNotes();

            Assert.Contains("D", noteNames);
            Assert.Contains("E", noteNames);
            Assert.Contains("G", noteNames);
        }

        [Fact]
        public void NoteCollection_Creation_IncludesRoot()
        {
            Intervals intervals = new Intervals();
            IInterval[] scaleIntervals = { intervals.Major2nd, intervals.Major3rd }; // No root interval
            NoteCollection collection = new NoteCollection("C", scaleIntervals);

            Assert.Contains(collection.NotesAndIntervals, ni => ni.Interval.IntervalName == "Root");
        }

        [Fact]
        public void NoteCollection_InvalidKey_ThrowsException()
        {
            Intervals intervals = new Intervals();
            IInterval[] scaleIntervals = { intervals.Major2nd, intervals.Major3rd };

            Assert.Throws<ArgumentException>(() => new NoteCollection("InvalidKey", scaleIntervals));
        }
    }

    public class SingleNoteTests
    {
        [Fact]
        public void SingleNote_Creation_SetsProperties()
        {
            SingleNote note = new SingleNote("C", 1);
            Assert.Equal("C", note.Note);
            Assert.Equal((byte)1, note.ChromaticPosition);
        }

        [Fact]
        public void SingleNote_FlatAlternativeName_SetsCorrectly()
        {
            SingleNote note = new SingleNote("Db", 2, "C#");
            Assert.Equal("Db", note.Note);
            Assert.Equal("C#", note.FlatAlternativeName);
        }

        [Fact]
        public void SingleNote_DefaultConstructor_Works()
        {
            SingleNote note = new SingleNote();
            Assert.Null(note.Note); // Or any other default assertion
        }
    }

    public class IntervalsTests
    {
        [Fact]
        public void Intervals_GetIntervalByName_ReturnsCorrectInterval()
        {
            Intervals intervals = new Intervals();
            IInterval minor3rd = intervals.GetIntervalByName("Minor 3rd");
            Assert.Equal("Minor 3rd", minor3rd.IntervalName);
            Assert.Equal("m3", minor3rd.Abbreviation);
            Assert.Equal(3, minor3rd.SemitonesFromRoot);
        }

        [Fact]
        public void Intervals_GetIntervalByAbbreviation_ReturnsCorrectInterval()
        {
            Intervals intervals = new Intervals();
            IInterval major7th = intervals.GetIntervalByAbbreviation("M7");
            Assert.Equal("Major 7th", major7th.IntervalName);
            Assert.Equal("M7", major7th.Abbreviation);
            Assert.Equal(11, major7th.SemitonesFromRoot);
        }
    }

    public class ScaleDefinitionsTests
    {
        [Fact]
        public void Ionian_ReturnsCorrectIntervals()
        {
            var intervals = ScaleDefinitions.Ionian;
            Assert.Equal(6, intervals.Length);
        }
    }

    public class RomanNumeralsTests
    {
        [Fact]
        public void GetIonianRomanNumeral_ReturnsCorrectNumerals()
        {
            Assert.Equal("I", ChordRomanNumerals.GetIonianRomanNumeral(1, "Major"));
            Assert.Equal("ii", ChordRomanNumerals.GetIonianRomanNumeral(2, "Minor"));
            Assert.Equal("iii", ChordRomanNumerals.GetIonianRomanNumeral(3, "Minor"));
            Assert.Equal("IV", ChordRomanNumerals.GetIonianRomanNumeral(4, "Major"));
            Assert.Equal("V", ChordRomanNumerals.GetIonianRomanNumeral(5, "Major"));
            Assert.Equal("vi", ChordRomanNumerals.GetIonianRomanNumeral(6, "Minor"));
            Assert.Equal("vii°", ChordRomanNumerals.GetIonianRomanNumeral(7, "Diminished"));
        }

        [Fact]
        public void GetAeolianRomanNumeral_ReturnsCorrectNumerals()
        {
            Assert.Equal("i", ChordRomanNumerals.GetAeolianRomanNumeral(1, "Minor"));
            Assert.Equal("ii°", ChordRomanNumerals.GetAeolianRomanNumeral(2, "Diminished"));
            Assert.Equal("III", ChordRomanNumerals.GetAeolianRomanNumeral(3, "Major"));
            Assert.Equal("iv", ChordRomanNumerals.GetAeolianRomanNumeral(4, "Minor"));
            Assert.Equal("v", ChordRomanNumerals.GetAeolianRomanNumeral(5, "Minor"));
            Assert.Equal("VI", ChordRomanNumerals.GetAeolianRomanNumeral(6, "Major"));
            Assert.Equal("#vii", ChordRomanNumerals.GetAeolianRomanNumeral(7, "Major"));
        }
    }
}