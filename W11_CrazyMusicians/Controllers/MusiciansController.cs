using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using W11_CrazyMusicians.Models;

namespace W11_CrazyMusicians.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusiciansController : Controller
{
    private static List<Musician> _musicians = new List<Musician>()
    {
        new Musician { Id = 1, Name = "Ahmet Çalgı", Occupation = "Famous Instrument Player", FunnyFact = "Always plays the wrong note, but he's funny." },
        new Musician { Id = 2, Name = "Zeynep Melodi", Occupation = "Popular Melody Writer", FunnyFact = "Her songs are misunderstood but very popular." },
        new Musician { Id = 3, Name = "Cemil Akor", Occupation = "Crazy Cordist", FunnyFact = "Changes chords often, but surprisingly skillful." },
        new Musician { Id = 4, Name = "Fatma Nota", Occupation = "Surprise Note Producer", FunnyFact = "Constantly prepares surprises while producing notes." },
        new Musician { Id = 5, Name = "Hasan Ritim", Occupation = "Rhythm monster", FunnyFact = "Makes every beat in his own way, it never fits, but it's funny." },
        new Musician { Id = 6, Name = "Elif Armoni", Occupation = "Master of Harmony", FunnyFact = "Sometimes plays her harmonies wrong, but she's very creative." },
        new Musician { Id = 7, Name = "Ali Perde", Occupation = "Pitch Practitioner", FunnyFact = "Plays each pitch differently, always surprising." },
        new Musician { Id = 8, Name = "Ayşe Rezonans", Occupation = "Resonance Expert", FunnyFact = "Specialises in resonance, but sometimes she makes a lot of noise." },
        new Musician { Id = 9, Name = "Murat Ton", Occupation = "Intonation Enthusiast", FunnyFact = "Differences in intonation are sometimes funny, but quite interesting." },
        new Musician { Id = 10, Name = "Selin Akor", Occupation = "Chord Wizard", FunnyFact = "When she changes the chords, she sometimes creates a magical atmosphere." }
    };
    
    [HttpGet]
    public IEnumerable<Musician> GetAll()
    {
        return _musicians;
    }
    
    [HttpGet("{id:min(1)}")]
    public ActionResult<Musician> Get(int id)
    {
        var musician = _musicians.FirstOrDefault(m => m.Id == id);
        if (musician == null)
        {
            return NotFound($"Musician with ID: {id} was not found.");
        }

        return Ok(musician);
    }
    
    [HttpGet]
    [Route("search")]
    public IEnumerable<Musician> Search([FromQuery] string? name, [FromQuery] string? occupation)   
    {
        // returns all musicians if no search parameters are provided, otherwise returns musicians that match the search criteria ignoring case
        return _musicians.Where(m => string.IsNullOrEmpty(name) || m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .Where(m => string.IsNullOrEmpty(occupation) || m.Occupation.Contains(occupation, StringComparison.OrdinalIgnoreCase));
    }
    
    [HttpPost("add")]
    public ActionResult<Musician> AddMusician([FromBody] Musician musician)
    {
        var id = _musicians.Max(m => m.Id) + 1;
        musician.Id = id;
        _musicians.Add(musician);
        
        return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
    }
    
    [HttpDelete("delete/{id:min(1)}")]
    public IActionResult Delete(int id)
    {
        var musician = _musicians.FirstOrDefault(m => m.Id == id);
        if (musician == null)
        {
            return NotFound($"Musician with ID: {id} was not found.");
        }
        
        _musicians.Remove(musician);
        
        return NoContent();
    }
    
    [HttpPut("update/{id:min(1)}")]
    public IActionResult Update(int id, [FromBody] Musician musician)
    {
        var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
        if (existingMusician == null)
        {
            return NotFound($"Musician with ID: {id} was not found.");
        }
        
        existingMusician.Name = musician.Name;
        existingMusician.Occupation = musician.Occupation;
        existingMusician.FunnyFact = musician.FunnyFact;
        
        return NoContent();
    }

    [HttpPatch("patch-update/{id:min(1)}")]
    public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<Musician> patchDoc)
    {
        var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
        if (existingMusician == null)
        {
            return NotFound($"Musician with ID: {id} was not found.");
        }
        
        // it cannot change the static list musician objects created for testing purposes
        patchDoc.ApplyTo(existingMusician);
        return NoContent();
    }
}