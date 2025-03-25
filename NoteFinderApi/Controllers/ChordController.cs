using Microsoft.AspNetCore.Mvc;
using NoteFinder.Helpers;
using NoteFinder.Interfaces;
using NoteFinder.Service;
using NoteFinder.Service.Definitions;
using Swashbuckle.AspNetCore.Filters;

namespace NoteFinderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChordController : ControllerBase
    {
        public class ChordOfScaleDegreeResponse
        {
            public string Key { get; set; }
            public string Scale { get; set; }
            public int Degree { get; set; }
            public string ChordRoot { get; set; }
            public string ChordType { get; set; }
            public string RomanNumeral { get; set; }
            public string[] ChordNotes { get; set; }
        }
        /// <summary>
        /// Retrieves the chord of a specific scale degree in a given key and scale.
        /// </summary>
        /// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
        /// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
        /// <param name="degree">The scale degree (1-7) to get the chord for </param>
        /// <returns>Information about the chord, including its root, type, and notes</returns>
        /// <response code="200">Returns the chord information</response>
        /// <response code="400">If the parameters are invalid</response>
        [HttpGet("chord-of-scale-degree")]
        [ProducesResponseType(typeof(ChordOfScaleDegreeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetChordOfScaleDegree([FromQuery] string key, [FromQuery] string scaleName, [FromQuery] int degree)
        {
            try 
            {
                var (chord, chordName) = ChordDefinitions.GetChordOfScaleDegree(key, scaleName, degree);
                var chordRoot = chord.NotesAndIntervals[0].Note.Note;

                // Get the Roman numeral representation
                string romanNumeral = ChordDefinitions.GetRomanNumeral(scaleName, degree, chordName);

                return Ok(new
                {
                    Key = key,
                    Scale = scaleName,
                    Degree = degree,
                    ChordRoot = chordRoot,
                    ChordType = chordName,
                    RomanNumeral = romanNumeral,
                    ChordNotes = chord.GetProperlyNamedNotes()
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the notes of a specific chord in a given key.
        /// </summary>
        /// <param name="key">The root note of the chord (e.g., C, F#, Bb)</param>
        /// <param name="chordName">The name of the chord (e.g., Major, Minor, Dominant7)</param>
        /// <returns>A collection of notes in the specified chord</returns>
        /// <response code="200">Returns the collection of notes in the chord</response>
        /// <response code="400">If the key or chord name is invalid</response>
        [HttpGet("notes")]
        [ProducesResponseType(typeof(NoteCollection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetChordNotes([FromQuery] string key, [FromQuery] string chordName)
        {
            try
            {
                IInterval[] chordIntervals = DefinitionsHelper.GetChordIntervals(chordName);
                NoteCollection noteCollection = new NoteCollection(key.ToUpper(), chordIntervals);
                return Ok(noteCollection);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }


}
