using NoteFinder.Helpers;
using NoteFinder.Interfaces;
using NoteFinder.Service.Definitions;

namespace NoteFinder.Service
{
    public class SingleNote : INote
    {
        string m_Note;
        byte m_ChromaticPosition;
        string m_FlatAlternativeName;

        //Default contructor for JSON serialization
        public SingleNote()
        {
        }

        public SingleNote(string noteName, byte notePosition)
        {
            m_Note = noteName;
            m_ChromaticPosition = notePosition;
        }

        public SingleNote(string noteName, byte notePosition, string flatName)
        {
            m_Note = noteName;
            m_ChromaticPosition = notePosition;
            m_FlatAlternativeName = flatName;
        }

        public string Note
        {
            get { return m_Note; }
            set { m_Note = value; }
        }

        public string FlatAlternativeName
        {
            get { return m_FlatAlternativeName; }
            set { m_FlatAlternativeName = value; }
        }

        public byte ChromaticPosition
        {
            get { return m_ChromaticPosition; }
            set { m_ChromaticPosition = value; }
        }
    }

    public class NoteAndInterval : INoteAndInterval
    {
        INote m_Note;
        IInterval m_Interval;

        public INote Note
        {
            get { return m_Note; }
            set { m_Note = value; }
        }

        public IInterval Interval
        {
            get { return m_Interval; }
            set { m_Interval = value; }
        }
    }

    public class NoteToStringFretList
    {
        private INote m_OpenStringNote;
        private int m_NumberOfFrets;
        private ChromaticNotes m_ChromaticNotes = new ChromaticNotes();
        private int m_StringNumber;

        public NoteToStringFretList(string openString, int stringNumber, int numberOfFrets)
        {

            m_OpenStringNote = (INote)m_ChromaticNotes.SingleNotes.Where(x => x.Note == openString).Single();
            m_NumberOfFrets = numberOfFrets;
            m_StringNumber = stringNumber;
        }

        public List<INoteToStringFret> GetStringNotes()
        {
            List<INoteToStringFret> noteToStringFretList = new List<INoteToStringFret>();

            int chromaticPosition = m_OpenStringNote.ChromaticPosition;

            for (int i = 0; i < m_NumberOfFrets; i++)
            {
                NoteToStringFret thisNoteStringFret = new NoteToStringFret();
                INote thisNote = (INote)m_ChromaticNotes.SingleNotes.Where(x => x.ChromaticPosition == chromaticPosition).Single();
                thisNoteStringFret.Note = thisNote;
                thisNoteStringFret.InstrumentString = m_StringNumber;
                thisNoteStringFret.Fret = i;
                noteToStringFretList.Add(thisNoteStringFret);
                chromaticPosition++;

                if (chromaticPosition > 12)
                {
                    chromaticPosition = 1;
                }
            }

            return noteToStringFretList;
        }

    }

    //todo: struct?
    public class NoteToStringFret : INoteToStringFret
    {
        private int m_Fret;
        private int m_InstrumentString;
        private INote m_Note;

        #region INoteToStringFret Members

        public INote Note
        {
            get
            {
                return m_Note;
            }
            set
            {
                m_Note = value;
            }
        }

        public int InstrumentString
        {
            get
            {
                return m_InstrumentString;
            }
            set
            {
                m_InstrumentString = value;
            }
        }

        public int Fret
        {
            get
            {
                return m_Fret;
            }
            set
            {
                m_Fret = value;
            }
        }

        #endregion
    }


    public class NoteCollection
    {
        private List<NoteAndInterval> m_NotesAndIntervals = new List<NoteAndInterval>();
        private ChromaticNotes m_ChromaticNotes = new ChromaticNotes();

        public NoteCollection(string key, IInterval[] intervals)
        {
            if (!intervals.Any(x => x.IntervalName == "Root"))
            {
                AddRootToNoteCollection(key);
            }

            foreach (SingleInterval thisInterval in intervals)
            {
                SingleNote thisNote = m_ChromaticNotes.ReturnInterval(key, thisInterval.SemitonesFromRoot);
                m_NotesAndIntervals.Add(new NoteAndInterval { Note = thisNote, Interval = thisInterval });
            }

            SetProperlyNamedNotes(key);
        }

        private void AddRootToNoteCollection(string key)
        {
            SingleNote rootNote = new SingleNote(key, 0, "");
            SingleInterval rootInterval = new SingleInterval("Root", "I", 0);
            m_NotesAndIntervals.Add(new NoteAndInterval { Note = rootNote, Interval = rootInterval });
        }

        public List<NoteAndInterval> NotesAndIntervals
        {
            get { return m_NotesAndIntervals; }
        }

        public List<string> GetProperlyNamedNotes()
        {
            return m_NotesAndIntervals.Select(ni => ni.Note.Note).ToList();
        }

        private void SetProperlyNamedNotes(string key)
        {
            bool useFlats = ShouldUseFlats(key);
            string[] noteOrder = useFlats
                ? new[] { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" }
                : new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

            string normalizedKey = useFlats ? NormalizeToFlat(key) : NormalizeToSharp(key);
            int keyIndex = Array.IndexOf(noteOrder, normalizedKey);

            if (keyIndex == -1)
            {
                throw new ArgumentException($"Invalid key: {key}");
            }

            for (int i = 0; i < m_NotesAndIntervals.Count; i++)
            {
                int noteIndex = (keyIndex + m_NotesAndIntervals[i].Interval.SemitonesFromRoot) % 12;
                string properNoteName = noteOrder[noteIndex];
                m_NotesAndIntervals[i].Note = new SingleNote(properNoteName, m_NotesAndIntervals[i].Note.ChromaticPosition);
            }
        }

        private bool ShouldUseFlats(string key)
        {
            string[] flatKeys = { "F", "Bb", "Eb", "Ab", "Db", "Gb", "Cb" };
            return flatKeys.Contains(key) || key.Contains("b") || HasFlatThird() || HasFlatSeventh();
        }

        private bool HasFlatThird() => m_NotesAndIntervals.Any(ni => ni.Interval.SemitonesFromRoot == 3);

        private bool HasFlatSeventh() => m_NotesAndIntervals.Any(ni => ni.Interval.SemitonesFromRoot == 10);

        private string NormalizeToSharp(string note)
        {
            var flatToSharp = new Dictionary<string, string>
        {
            {"Db", "C#"}, {"Eb", "D#"}, {"Gb", "F#"}, {"Ab", "G#"}, {"Bb", "A#"}
        };

            return flatToSharp.ContainsKey(note) ? flatToSharp[note] : note;
        }

        private string NormalizeToFlat(string note)
        {
            var sharpToFlat = new Dictionary<string, string>
        {
            {"C#", "Db"}, {"D#", "Eb"}, {"F#", "Gb"}, {"G#", "Ab"}, {"A#", "Bb"}
        };

            return sharpToFlat.ContainsKey(note) ? sharpToFlat[note] : note;
        }
    }

    public class ChromaticNotes
    {
        private List<SingleNote> m_SingleNotes;

        public List<SingleNote> SingleNotes
        {
            get { return m_SingleNotes; }
            set { m_SingleNotes = value; }
        }

        public ChromaticNotes()
        {
            InitializeList();
        }

        private void InitializeList()
        {
            m_SingleNotes = new List<SingleNote>
            {
                new SingleNote("C", 1),
                new SingleNote("C#", 2, "Db"),
                new SingleNote("D", 3),
                new SingleNote("D#", 4, "Eb"),
                new SingleNote("E", 5),
                new SingleNote("F", 6),
                new SingleNote("F#", 7, "Gb"),
                new SingleNote("G", 8),
                new SingleNote("G#", 9, "Ab"),
                new SingleNote("A", 10),
                new SingleNote("A#", 11, "Bb"),
                new SingleNote("B", 12)
            };
        }

        public SingleNote ReturnInterval(string rootNoteName, int interval)
        {
            var rootNote = m_SingleNotes.FirstOrDefault(n => n.Note == rootNoteName || n.FlatAlternativeName == rootNoteName);
            if (rootNote == null)
            {
                throw new ArgumentException($"Invalid root note: {rootNoteName}");
            }

            int destChromPosition = ((rootNote.ChromaticPosition - 1 + interval) % 12) + 1;

            var destinationNote = m_SingleNotes.First(d => d.ChromaticPosition == destChromPosition);

            return destinationNote;
        }

    }

}
