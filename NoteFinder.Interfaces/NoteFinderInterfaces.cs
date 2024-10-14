namespace NoteFinder.Interfaces
{
    public interface IInterval
    {
        string IntervalName
        {
            get;
            set;
        }

        string Abbreviation
        {
            get;
            set;
        }

        int SemitonesFromRoot
        {
            get;
            set;
        }
    }

    public interface INote
    {
        string Note
        {
            get;
            set;
        }

        string FlatAlternativeName
        {
            get;
            set;
        }

        int ChromaticPosition
        {
            get;
            set;
        }
    }

    public interface INoteAndInterval
    {
        INote Note
        {
            get;
            set;
        }

        IInterval Interval
        {
            get;
            set;
        }
    }

    public interface INoteCollection
    {
        List<INoteAndInterval> NotesAndIntervals
        {
            get;
            set;
        }
    }

    public interface INoteToStringFret
    {
        INote Note
        {
            get;
            set;
        }

        int InstrumentString
        {
            get;
            set;
        }

        int Fret
        {
            get;
            set;
        }
    }
}