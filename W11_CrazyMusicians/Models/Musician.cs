using System.ComponentModel.DataAnnotations;

namespace W11_CrazyMusicians.Models;

public class Musician
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Occupation is required")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Occupation must be between 4 and 50 characters")]
    public string Occupation { get; set; }
    
    [Required(ErrorMessage = "Fun fact is required")]
    public string FunnyFact { get; set; }
}