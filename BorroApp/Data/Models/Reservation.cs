namespace BorroApp.Data.Models;

public class Reservation {
	public int      Id       { get; set; }
	public DateTime DateFrom { get; set; }
	public DateTime DateTo   { get; set; }
	public Status   Status   { get; set; }
	public double   Price    { get; set; }
	public int      UserId   { get; set; }
	public User     User     { get; set; }
	public int      PostId  { get; set; }
	public Post     Post      { get; set; }
}