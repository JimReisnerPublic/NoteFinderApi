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


    public class NoteCollection : INoteCollection
    {
        List<INoteAndInterval> m_NotesAndIntervals = new List<INoteAndInterval>();
        Intervals m_Intervals = new Intervals();
        ChromaticNotes m_ChromaticNotes = new ChromaticNotes();

        public List<INoteAndInterval> NotesAndIntervals
        {
            get { return m_NotesAndIntervals; }
            set { m_NotesAndIntervals = value; }
        }


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
        }

        //This assumes that the scale does not have a step beyond a M2
        public NoteCollection(string key, string signature)
        {

            //TODO: Should I make this an array of characters?
            //TODO: Exception if note T or s
            //string[] sigSteps = Regex.Split(signature, string.Empty);
            var sigSteps = signature.Select(x => new string(x, 1)).ToArray();
            int root = 0;
            int curInt = root;
            List<int> intervalNumbers = new List<int>();
            AddRootToNoteCollection(key);

            foreach (string step in sigSteps)
            {
                if (step.ToLower() == "s")
                {
                    curInt += 1;
                }
                else if (step.ToLower() == "t")
                {
                    curInt += 2;
                }
                else
                {
                    throw new Exception("Each part of signature must be a 's' (semitone) or 'T' (Tone)");
                }

                intervalNumbers.Add(curInt);
            }

            foreach (int thisIntervalNum in intervalNumbers)
            {
                IInterval thisInterval =
                    m_Intervals.IntervalList.Where(x => x.SemitonesFromRoot == thisIntervalNum).Single();

                SingleNote thisNote = m_ChromaticNotes.ReturnInterval(key, thisInterval.SemitonesFromRoot);
                NoteAndInterval thisNoteAndInterval = new NoteAndInterval();
                thisNoteAndInterval.Note = thisNote;
                thisNoteAndInterval.Interval = thisInterval;
                m_NotesAndIntervals.Add(thisNoteAndInterval);
            }

        }
        public void SetProperlyNamedNotes()
        {
            string[] noteOrder = { "C", "D", "E", "F", "G", "A", "B" };
            string previousNoteName = m_NotesAndIntervals[0].Note.Note;

            for (int i = 0; i < m_NotesAndIntervals.Count; i++)
            {
                var currentNote = m_NotesAndIntervals[i].Note;
                string properNoteName = GetProperNoteName(currentNote, previousNoteName, noteOrder);

                // Update the Note property with the properly named note
                m_NotesAndIntervals[i].Note = new SingleNote(properNoteName, (byte)currentNote.ChromaticPosition, currentNote.FlatAlternativeName);

                previousNoteName = properNoteName;
            }
        }
        public List<string> GetProperlyNamedNotes()
        {
            List<string> properNotes = new List<string>();
            string[] noteOrder = { "C", "D", "E", "F", "G", "A", "B" };

            // Add the root note
            properNotes.Add(m_NotesAndIntervals[0].Note.Note);

            for (int i = 1; i < m_NotesAndIntervals.Count; i++)
            {
                var currentNote = m_NotesAndIntervals[i].Note;
                string previousNoteName = properNotes[i - 1];
                string properNoteName = GetProperNoteName(currentNote, previousNoteName, noteOrder);
                properNotes.Add(properNoteName);
            }

            return properNotes;
        }

        private string GetProperNoteName(INote note, string previousNoteName, string[] noteOrder)
        {
            int previousIndex = Array.IndexOf(noteOrder, previousNoteName[0].ToString().ToUpper());
            int currentIndex = Array.IndexOf(noteOrder, note.Note[0].ToString().ToUpper());
            int distance = (currentIndex - previousIndex + 7) % 7;

            // If the distance is 0 or 1, we prefer the next letter name
            if (distance <= 1)
            {
                int nextLetterIndex = (previousIndex + 1) % 7;
                string nextLetter = noteOrder[nextLetterIndex];

                // If there's a flat alternative and it matches the next letter, use it
                if (!string.IsNullOrEmpty(note.FlatAlternativeName) &&
                    note.FlatAlternativeName[0].ToString().ToUpper() == nextLetter)
                {
                    return note.FlatAlternativeName;
                }
                // Otherwise, use the sharp name
                return note.Note;
            }
            // For larger intervals, prefer the original note name
            else
            {
                return note.Note;
            }
        }

        private void AddRootToNoteCollection(string key)
        {
            NoteAndInterval rootNoteAndIterval = new NoteAndInterval();
            rootNoteAndIterval.Interval = m_Intervals.IntervalList.Where(x => x.SemitonesFromRoot == 0).Single();
            rootNoteAndIterval.Note = m_ChromaticNotes.SingleNotes.Where(y => y.Note == key).Single();
            m_NotesAndIntervals.Add(rootNoteAndIterval);
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

        public SingleNote ReturnInterval(string rootNoteName, int interval)
        {
            var rootNote = m_SingleNotes.Where(n => n.Note == rootNoteName).Single();

            int destChromPosition = rootNote.ChromaticPosition + interval;

            if (destChromPosition > 12)
            {
                destChromPosition = destChromPosition - 12;
            }

            var destinationNote = m_SingleNotes.Where(d => d.ChromaticPosition == destChromPosition).Single();

            return destinationNote;
        }
    }
}
