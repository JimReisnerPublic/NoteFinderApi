using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteFinder.Service.Definitions
{
    public static class ChordRomanNumerals
    {
        public static string GetIonianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "I",
                2 => "ii",
                3 => "iii",
                4 => "IV",
                5 => "V",
                6 => "vi",
                _ => chordType.Contains("Diminished") ? "vii°" : "vii"
            };
        }

        public static string GetDorianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "i",
                2 => "ii",
                3 => "III",
                4 => "IV",
                5 => "v",
                6 => chordType.Contains("Diminished") ? "vi°" : "vi",
                _ => "VII"
            };
        }

        public static string GetPhrygianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "i",
                2 => "II",
                3 => "III",
                4 => "iv",
                5 => chordType.Contains("Diminished") ? "v°" : "v",
                6 => "VI",
                _ => "vii"
            };
        }
        public static string GetLydianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "I",
                2 => "II",
                3 => "iii",
                4 => chordType.Contains("Diminished") ? "#iv°" : "#iv",
                5 => "V",
                6 => "vi",
                _ => "vii"
            };
        }
        public static string GetMixolydianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "I",
                2 => "ii",
                3 => chordType.Contains("Diminished") ? "iii°" : "iii",
                4 => "IV",
                5 => "v",
                6 => chordType.Contains("Diminished") ? "vi°" : "vi",
                _ => "VII"
            };
        }
        public static string GetAeolianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => "i",
                2 => chordType.Contains("Diminished") ? "ii°" : "ii",
                3 => "III",
                4 => "iv",
                5 => chordType.Contains("Dominant") ? "V" : (chordType.Contains("Minor") ? "v" : ""),
                6 => "VI",
                _ => chordType.Contains("Diminished") ? "#vii°" : "#vii"
            };
        }
        public static string GetLocrianRomanNumeral(int degree, string chordType)
        {
            return degree switch
            {
                1 => chordType.Contains("Diminished") ? "#i°" : "#i", // Often half-diminished in practice
                2 => "II", // Sometimes minor in practice depending on the context
                3 => chordType.Contains("Minor") ? "#iii" : "#iii", // Often major in practice
                4 => "#iv", // Minor or diminished depending on context
                5 => chordType.Contains("Major") ? "#V" : "#V", // Avoided in traditional harmony
                6 => "#VI", // Major or minor depending on context
                _ => chordType.Contains("Diminished") ? "#vii°" : "#vii"
            };
        }
    }
}
