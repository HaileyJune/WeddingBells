using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UpdatedLogReg.Models;

namespace WeddingBells.Models
{
public class Wedding
{
    [Key]
    public int WeddingId {get;set;}

    public int UserId {get;set;}
    public UserObject Creator {get;set;}
    
    [Required]
    [MinLength(2)]
    [Display (Name = "Bride One")]
    public string Bride1 {get;set;}
    
    [Required]
    [MinLength(2)]
    [Display (Name = "Bride Two")]
    public string Bride2 {get;set;}
    
    [Required]
    [Display (Name = "Date")]
    public DateTime Date {get;set;}
    
    [Required]
    [Display (Name = "Wedding Address")]
    public string Address {get;set;}

    public List<RSVP> RSVP {get;set;}
    // public {get;set;}
}
}