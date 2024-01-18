﻿namespace BorroApp.Data.Models;

public class Post {
	public int                      Id           { get; set; }
	public string                   Title        { get; set; }
	public string                   Image        { get; set; }
	public double                   Price        { get; set; }
	public DateTime                 DateFrom     { get; set; }
	public DateTime                 DateTo       { get; set; }
	public string                   Description  { get; set; }
	public string                   Location     { get; set; }
	public int                      CategoryId   { get; set; }
	public Category                 Category     { get; set; }
	public int                      UserId       { get; set; }
	public User                     User         { get; set; }
	public ICollection<Reservation> Reservations { get; set; }
}