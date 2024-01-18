namespace BorroApp.Data.Models;

public class User {
	public int                      Id           { get; set; }
	public string                   Email        { get; set; }
	public string                   Password     { get; set; }
	public ICollection<Reservation> Reservations { get; set; }
	public ICollection<Post>        Posts        { get; set; }
}