using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;


public class PlayerDBManager : MonoBehaviour {

	private List<DBDataInput> dbDataInput = new List<DBDataInput> ();
	public GameObject player;
	private PlayerFunction playerFunction;
	private string connectionString;

	// Use this for initialization
	void Start () {

		connectionString = "URI=file:" + Application.dataPath + "/PlayerInfoDB.sqlite";
		player = GameObject.FindGameObjectWithTag ("Player");
		playerFunction = player.GetComponent <PlayerFunction> ();
		CreateDataTable ();
		InsertInfo (playerFunction.name, playerFunction.kills, playerFunction.deaths);
		//DisplayDB ();
		//DeleteEntry (1);
		GetInfo ();

	}

	private void CreateDataTable()	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {

				string sqlQuerry = String.Format("CREATE TABLE if not exists GameInfo(PlayerId INTEGER PRIMARY KEY NOT NULL UNIQUE, Name TEXT NOT NULL, Kills INTEGER NOT NULL, Deaths INTEGER NOT NULL, Date INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP)");

				dbCmd.CommandText = sqlQuerry;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	private void InsertInfo(string name, int kills, int deaths){
		
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				
				string sqlQuerry = String.Format("INSERT INTO GameInfo(Name,Kills,Deaths)VALUES('{0}','{1}','{2}');",name,kills,deaths);

				dbCmd.CommandText = sqlQuerry;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	private void GetInfo ()	{
		dbDataInput.Clear ();

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuerry = "SELECT * FROM GameInfo";

				dbCmd.CommandText = sqlQuerry;

				using (IDataReader reader = dbCmd.ExecuteReader ()) {
					while (reader.Read ()) {
						Debug.Log ("Player Name: " + reader.GetString(1) + " - Kills:  "+ reader.GetInt32(2) + " - Deaths: " + reader.GetInt32(3));
						// id - name - kills - deaths - date
						dbDataInput.Add(new DBDataInput(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2),reader.GetInt32(3),reader.GetDateTime(4)));
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
	}

	private void DeleteEntry(int id){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {

				string sqlQuerry = String.Format("DELETE FROM GameInfo WHERE PlayerId = '{0}'", id);

				dbCmd.CommandText = sqlQuerry;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	/*private void DisplayDB(){		//will use later
		for (int i = 0; i < dbDataInput.Count; i++) {
			//Debug.Log (dbDataInput.Count);
			DBDataInput tempStorage = dbDataInput [i];
			InsertInfo(tempStorage.Name,tempStorage.Kills,tempStorage.Deaths);
		}

	}*/
}
