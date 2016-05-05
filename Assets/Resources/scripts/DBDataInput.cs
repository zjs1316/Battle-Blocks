using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DBDataInput
{
	public string Name { get; set; }

	public int Kills { get; set; }

	public int Deaths { get; set; }

	public DateTime Date { get; set; }

	public int Id { get; set; }


	public DBDataInput(int id, string name, int kills, int deaths, DateTime date)	{
		this.Id = id;
		this.Name = name;
		this.Kills = kills;
		this.Deaths = deaths;
		this.Date = date;

	}

}
