using NoteFinder.Interfaces;
using NoteFinder.Service.Definitions;

namespace NoteFinder.Service
{
    public class SingleNote : INote
    {
        string m_Note;
        int m_ChromaticPosition;
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

        public SingleNote(string noteName, int notePosition, string flatName)
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

        public int ChromaticPosition
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
            var keyInterval = intervals.Where(x => x.IntervalName == "Root");

            if (keyInterval.Count() == 0)
            {
                AddRootToNoteCollection(key);
            }

            foreach (SingleInterval thisInterval in intervals)
            {
                SingleNote thisNote = m_ChromaticNotes.ReturnInterval(key, thisInterval.SemitonesFromRoot);
                NoteAndInterval thisNoteAndInterval = new NoteAndInterval();
                thisNoteAndInterval.Note = thisNote;
                thisNoteAndInterval.Interval = thisInterval;
                m_NotesAndIntervals.Add(thisNoteAndInterval);
            }

            SetProperlyNamedNotes(key);
        }

        private string NormalizeToSharp(string note)
        {
            var flatToSharp = new Dictionary<string, string>
        {
            {"Db", "C#"}, {"Eb", "D#"}, {"Gb", "F#"}, {"Ab", "G#"}, {"Bb", "A#"}
        };

            return flatToSharp.ContainsKey(note) ? flatToSharp[note] : note;
        }

        private bool UseSharps(string key)
        {
            string[] sharpKeys = { "G", "D", "A", "E", "B", "F#", "C#" };
            return sharpKeys.Contains(key) || key.Contains("#");
        }

        public void SetProperlyNamedNotes(string key)
        {
            bool useFlats = key.Contains("b");
            string[] noteOrder = useFlats
                ? new[] { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" }
                : new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

            int keyIndex = Array.IndexOf(noteOrder, key);

            for (int i = 0; i < m_NotesAndIntervals.Count; i++)
            {
                var currentNote = m_NotesAndIntervals[i].Note;
                int noteIndex = (keyIndex + m_NotesAndIntervals[i].Interval.SemitonesFromRoot) % 12;
                string properNoteName = noteOrder[noteIndex];

                m_NotesAndIntervals[i].Note = new SingleNote(properNoteName, currentNote.ChromaticPosition, currentNote.FlatAlternativeName);
            }
        }

        private string GetProperNoteName(SingleNote note, string previousNoteName, string[] noteOrder, bool useSharps)
        {
            int previousIndex = Array.IndexOf(noteOrder, previousNoteName[0].ToString());
            int currentIndex = (previousIndex + 1) % 7;

            string baseName = noteOrder[currentIndex];

            if (note.ChromaticPosition != Array.IndexOf(noteOrder, baseName))
            {
                if (useSharps)
                {
                    return baseName + "#";
                }
                else
                {
                    int flatIndex = (currentIndex + 1) % 7;
                    return noteOrder[flatIndex] + "b";
                }
            }

            return baseName;
        }

        public List<NoteAndInterval> NotesAndIntervals
        {
            get { return m_NotesAndIntervals; }
        }

        private void AddRootToNoteCollection(string key)
        {
            SingleNote rootNote = new SingleNote(key, 0, "");
            SingleInterval rootInterval = new SingleInterval("Root", "I", 0);
            NoteAndInterval rootNoteAndInterval = new NoteAndInterval();
            rootNoteAndInterval.Note = rootNote;
            rootNoteAndInterval.Interval = rootInterval;
            m_NotesAndIntervals.Add(rootNoteAndInterval);
        }

        public List<string> GetProperlyNamedNotes()
        {
            return m_NotesAndIntervals.Select(ni => ni.Note.Note).ToList();
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
        //SingleNote singleNoteA = new SingleNote("A", 1);
        //SingleNote singleNoteASharp = new SingleNote("A#", 2, "Bb");
        //SingleNote singleNoteB = new SingleNote("B", 3);
        //SingleNote singleNoteC = new SingleNote("C", 4);
        //SingleNote singleNoteCSharp = new SingleNote("C#", 5, "Db");
        //SingleNote singleNoteD = new SingleNote("D", 6);
        //SingleNote singleNoteDSharp = new SingleNote("D#", 7, "Eb");
        //SingleNote singleNoteE = new SingleNote("E", 8);
        //SingleNote singleNoteF = new SingleNote("F", 9);
        //SingleNote singleNoteFSharp = new SingleNote("F#", 10, "Gb");
        //SingleNote singleNoteG = new SingleNote("G", 11);
        //SingleNote singleNoteGSharp = new SingleNote("G#", 12, "Ab");

        ////Generic lists are automatically in the order of insertion
        //m_SingleNotes = new List<SingleNote>();
        //m_SingleNotes.Add(singleNoteA);
        //m_SingleNotes.Add(singleNoteASharp);
        //m_SingleNotes.Add(singleNoteB);
        //m_SingleNotes.Add(singleNoteC);
        //m_SingleNotes.Add(singleNoteCSharp);
        //m_SingleNotes.Add(singleNoteD);
        //m_SingleNotes.Add(singleNoteDSharp);
        //m_SingleNotes.Add(singleNoteE);
        //m_SingleNotes.Add(singleNoteF);
        //m_SingleNotes.Add(singleNoteFSharp);
        //m_SingleNotes.Add(singleNoteG);
        //m_SingleNotes.Add(singleNoteGSharp);
    }


}
