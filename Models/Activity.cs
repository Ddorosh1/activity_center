using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace activity_center.Models
{
  public class Activity
  {
    [Key]
    public int ActivityId { get; set; }

    [Required(ErrorMessage = "Title must be provided")]
    public string Title { get; set; }

    [Required(ErrorMessage = "You must provide a date")]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "You must provide a time")]
    public DateTime Time { get; set; }

    [Required(ErrorMessage = "You must provide how long the activity is")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "You must provide how long the activity is")]
    public string DurationUnit { get; set; }

    [Required(ErrorMessage = "Description must be provided")]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }

    public string Coordinator { get; set; }

    //nav properties
    public List<Association> Participants {get; set;}

  }
}