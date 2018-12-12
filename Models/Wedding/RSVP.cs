using WeddingBells.Models;
using UpdatedLogReg.Models;
public class RSVP
{
    public int UserId {get;set;}
    public int WeddingID {get;set;}
    public UserObject Guest {get;set;}
    public Wedding Wedding {get;set;}
}