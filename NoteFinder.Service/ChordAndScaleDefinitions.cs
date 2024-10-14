using NoteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteFinder.Service.Definitions
{
    public static class ChordDefinitions
    {
        public static Intervals m_Intervals = new Intervals();

        public static IInterval[] MajorTriad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Major3rd, m_Intervals.Perfect5th };
            }
        }

        public static IInterval[] MinorTriad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Minor3rd, m_Intervals.Perfect5th };
            }
        }

        public static IInterval[] AugmentedTriad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Major3rd, m_Intervals.Minor6th };
            }
        }

        public static IInterval[] DiminishedTriad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Minor3rd, m_Intervals.Tritone };
            }
        }

        public static IInterval[] Sus4Triad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Perfect4th, m_Intervals.Perfect5th };
            }
        }

        public static IInterval[] Sus2Triad
        {
            get
            {
                return new IInterval[2] { m_Intervals.Major2nd, m_Intervals.Perfect5th };
            }
        }

        public static IInterval[] Major7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Major7th };
            }
        }

        public static IInterval[] Minor7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Minor3rd,
                    m_Intervals.Perfect5th, m_Intervals.Minor7th };
            }
        }

        public static IInterval[] Dominant7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Minor7th };
            }
        }

        public static IInterval[] Minor7Flat5
        {
            get
            {
                return new IInterval[3] { m_Intervals.Minor3rd,
                    m_Intervals.Tritone, m_Intervals.Minor7th };
            }
        }

        public static IInterval[] Diminished
        {
            get
            {
                return new IInterval[3] { m_Intervals.Minor3rd,
                    m_Intervals.Tritone, m_Intervals.Major6th };
            }
        }

        public static IInterval[] Six
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Major6th };
            }
        }

        public static IInterval[] MinorSix
        {
            get
            {
                return new IInterval[3] { m_Intervals.Minor3rd,
                    m_Intervals.Perfect5th, m_Intervals.Major6th };
            }
        }

        public static IInterval[] MajorNine
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Major7th,
                    m_Intervals.Major2nd};
            }
        }

        public static IInterval[] MinorNine
        {
            get
            {
                return new IInterval[4] { m_Intervals.Minor3rd,
                    m_Intervals.Perfect5th, m_Intervals.Minor7th,
                    m_Intervals.Major2nd};
            }
        }

        public static IInterval[] Nine
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Minor7th,
                    m_Intervals.Major2nd};
            }
        }

        public static IInterval[] Dominant11
        {
            get
            {
                return new IInterval[5] { m_Intervals.Major3rd,
            m_Intervals.Perfect5th, m_Intervals.Minor7th,
            m_Intervals.Major2nd, m_Intervals.Perfect4th };
            }
        }

        public static IInterval[] Major13
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major3rd,
            m_Intervals.Perfect5th, m_Intervals.Major7th,
            m_Intervals.Major2nd, m_Intervals.Perfect4th,
            m_Intervals.Major6th };
            }
        }

        public static IInterval[] Dominant7b5
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd,
                    m_Intervals.Tritone, m_Intervals.Minor7th };
            }
        }

        public static IInterval[] Dominant7Sharp9
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Minor7th,
                    m_Intervals.Minor3rd }; // The sharp 9 is enharmonically equivalent to a minor 3rd
            }
        }

        public static IInterval[] MajorAdd9
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd,
                    m_Intervals.Perfect5th, m_Intervals.Major2nd };
            }
        }

        public static IInterval[] PowerChord
        {
            get
            {
                return new IInterval[1] { m_Intervals.Perfect5th };
            }
        }

        public static IInterval[] Minor11
        {
            get
            {
                return new IInterval[5] { m_Intervals.Minor3rd, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Major2nd, m_Intervals.Perfect4th };
            }
        }

        public static IInterval[] Minor13
        {
            get
            {
                return new IInterval[6] { m_Intervals.Minor3rd, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Major2nd, m_Intervals.Perfect4th,
                m_Intervals.Major6th };
            }
        }

        public static IInterval[] Augmented7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd, m_Intervals.Minor6th,
                m_Intervals.Minor7th };
            }
        }

        public static IInterval[] AugmentedMajor7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd, m_Intervals.Minor6th,
                m_Intervals.Major7th };
            }
        }

        public static IInterval[] MinorMajor7
        {
            get
            {
                return new IInterval[3] { m_Intervals.Minor3rd, m_Intervals.Perfect5th,
                m_Intervals.Major7th };
            }
        }

        public static IInterval[] SevenSus4
        {
            get
            {
                return new IInterval[3] { m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Minor7th };
            }
        }

        public static IInterval[] NineSus4
        {
            get
            {
                return new IInterval[4] { m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Major2nd };
            }
        }

        public static IInterval[] ThirteenSus4
        {
            get
            {
                return new IInterval[5] { m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Major2nd, m_Intervals.Major6th };
            }
        }

        public static IInterval[] SevenSharp5
        {
            get
            {
                return new IInterval[3] { m_Intervals.Major3rd, m_Intervals.Minor6th,
                m_Intervals.Minor7th };
            }
        }

        public static IInterval[] SevenFlat9
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major3rd, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Minor2nd };
            }
        }

        public static IInterval[] SevenSharp11
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major3rd, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Tritone };
            }
        }

        public static IInterval[] ThirteenFlat9
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major3rd, m_Intervals.Perfect5th,
                m_Intervals.Minor7th, m_Intervals.Minor2nd, m_Intervals.Perfect4th,
                m_Intervals.Major6th };
            }
        }

    }

    public class Intervals
    {
        List<IInterval> m_IntervalList = new List<IInterval>();
        SingleInterval m_Tonic = new SingleInterval("Root", "I", 0);

        public IInterval Tonic
        {
            get { return m_Tonic; }
        }

        IInterval m_Minor2nd = new SingleInterval("Minor 2nd", "m2", 1);

        public IInterval Minor2nd
        {
            get { return m_Minor2nd; }
        }

        IInterval m_Major2nd = new SingleInterval("Major 2nd", "M2", 2);

        public IInterval Major2nd
        {
            get { return m_Major2nd; }
        }

        IInterval m_Minor3rd = new SingleInterval("Minor 3rd", "m3", 3);

        public IInterval Minor3rd
        {
            get { return m_Minor3rd; }
        }

        IInterval m_Major3rd = new SingleInterval("Major 3rd", "M3", 4);

        public IInterval Major3rd
        {
            get { return m_Major3rd; }
        }

        IInterval m_Perfect4th = new SingleInterval("Perfect 4th", "P4", 5);

        public IInterval Perfect4th
        {
            get { return m_Perfect4th; }
        }

        IInterval m_Tritone = new SingleInterval("Tritone", "TRI", 6);

        public IInterval Tritone
        {
            get { return m_Tritone; }
        }

        IInterval m_Perfect5th = new SingleInterval("Perfect 5th", "P5", 7);

        public IInterval Perfect5th
        {
            get { return m_Perfect5th; }
        }

        IInterval m_Minor6th = new SingleInterval("Minor 6th", "m6", 8);

        public IInterval Minor6th
        {
            get { return m_Minor6th; }
        }

        IInterval m_Major6th = new SingleInterval("Major 6th", "M6", 9);

        public IInterval Major6th
        {
            get { return m_Major6th; }
        }

        IInterval m_Minor7th = new SingleInterval("Minor 7th", "m7", 10);

        public IInterval Minor7th
        {
            get { return m_Minor7th; }
        }

        IInterval m_Major7th = new SingleInterval("Major 7th", "M7", 11);

        public IInterval Major7th
        {
            get { return m_Major7th; }
        }

        IInterval m_Octave = new SingleInterval("Octave", "VII", 12);

        public IInterval Octave
        {
            get { return m_Octave; }
        }


        public List<IInterval> IntervalList
        {
            get { return m_IntervalList; }
            set { m_IntervalList = value; }
        }

        public Intervals()
        {
            InitializeList();
        }

        public IInterval GetIntervalByName(string intervalName)
        {
            var thisInterval = m_IntervalList.Where(x => x.IntervalName == intervalName).Single();
            return thisInterval;
        }

        public IInterval GetIntervalByAbbreviation(string intervalAbbreviation)
        {
            var thisInterval = m_IntervalList.Where(x => x.Abbreviation == intervalAbbreviation).Single();
            return thisInterval;
        }

        private void InitializeList()
        {


            m_IntervalList.Add(m_Tonic);
            m_IntervalList.Add(m_Minor2nd);
            m_IntervalList.Add(m_Major2nd);
            m_IntervalList.Add(m_Minor3rd);
            m_IntervalList.Add(m_Major3rd);
            m_IntervalList.Add(m_Perfect4th);
            m_IntervalList.Add(m_Tritone);
            m_IntervalList.Add(m_Perfect5th);
            m_IntervalList.Add(m_Minor6th);
            m_IntervalList.Add(m_Major6th);
            m_IntervalList.Add(m_Minor7th);
            m_IntervalList.Add(m_Major7th);
            m_IntervalList.Add(m_Octave);

        }
    }

    public class SingleInterval : IInterval
    {
        string m_IntervalName;
        string m_Abbreviation;
        int m_SemitonesFromRoot;

        public string IntervalName
        {
            get { return m_IntervalName; }
            set { m_IntervalName = value; }
        }

        public string Abbreviation
        {
            get { return m_Abbreviation; }
            set { m_Abbreviation = value; }
        }

        public int SemitonesFromRoot
        {
            get { return m_SemitonesFromRoot; }
            set { m_SemitonesFromRoot = value; }
        }

        public SingleInterval(string intervalName, string abbreviation, int semitonesFromRoot)
        {
            m_IntervalName = intervalName;
            m_Abbreviation = abbreviation;
            m_SemitonesFromRoot = semitonesFromRoot;
        }
    }

    public static class ScaleDefinitions
    {
        private static Intervals m_Intervals = new Intervals();

        public static IInterval[] Ionian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major2nd, m_Intervals.Major3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Major6th, m_Intervals.Major7th};
            }
        }

        public static IInterval[] Dorian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major2nd, m_Intervals.Minor3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Major6th, m_Intervals.Minor7th};
            }
        }

        public static IInterval[] Phrygian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Minor2nd, m_Intervals.Minor3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Minor6th, m_Intervals.Minor7th};
            }
        }

        public static IInterval[] Lydian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major2nd, m_Intervals.Major3rd,
                    m_Intervals.Tritone, m_Intervals.Perfect5th,
                m_Intervals.Major6th, m_Intervals.Major7th};
            }
        }

        public static IInterval[] Mixolydian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major2nd, m_Intervals.Major3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Major6th, m_Intervals.Minor7th};
            }
        }

        public static IInterval[] Aeolian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Major2nd, m_Intervals.Minor3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                m_Intervals.Minor6th, m_Intervals.Minor7th};
            }
        }

        public static IInterval[] Locrian
        {
            get
            {
                return new IInterval[6] { m_Intervals.Minor2nd, m_Intervals.Minor3rd,
                    m_Intervals.Perfect4th, m_Intervals.Tritone,
                m_Intervals.Minor6th, m_Intervals.Minor7th};
            }
        }

        public static IInterval[] PentatonicMinor
        {
            get
            {
                return new IInterval[4] { m_Intervals.Minor3rd,
                    m_Intervals.Perfect4th, m_Intervals.Perfect5th,
                    m_Intervals.Minor7th};
            }
        }

        public static IInterval[] PentatonicMajor
        {
            get
            {
                return new IInterval[4] { m_Intervals.Major2nd, m_Intervals.Major3rd,
                    m_Intervals.Perfect5th,  m_Intervals.Major6th};
            }
        }
    }
}
