using WeddingBells.Models;
using UpdatedLogReg.Models;
using System.ComponentModel.DataAnnotations;

public class RSVP
{
    [Key]
    public int RSVPId {get;set;}
    public int UserId {get;set;}
    public int WeddingID {get;set;}
    public UserObject Guest {get;set;}
    public Wedding Wedding {get;set;}
}