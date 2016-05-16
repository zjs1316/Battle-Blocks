using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerDBManager : MonoBehaviour {

	public List<DBDataInput> dbDataInput = new List<DBDataInput> ();
	public string connectionString;
	//public Text text;

	public GameObject textPrefab;
	public Transform parent;

	// Use this for initialization
	void Start () {

		//text.GetComponent<Text> ();

		connectionString = "URI=file:" + Application.dataPath + "/PlayerInfoDB.sqlite";
		//player = GameObject.FindGameObjectWithTag ("Player");
		//playerFunction = player.GetComponent <PlayerFunction> ();
		CreateDataTable ();
		//InsertInfo (playerFunction.name, playerFunction.kills, playerFunction.deaths);
		//DisplayDB ();
		//DeleteEntry (1);
		//UpdateInfo(2,1,1);
		//GetInfo ();

	}

	public void CreateDataTable()	{
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

	public void InsertInfo(string name, int kills, int deaths){
		
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

	public void UpdateInfo(int kills, int deaths, string name){

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {

				string sqlQuerry = String.Format ("UPDATE GameInfo SET Kills = '{0}', Deaths = '{1}' WHERE Name = '{2}'", kills,deaths,name);

				dbCmd.CommandText = sqlQuerry;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	public void GetInfo ()	{
		dbDataInput.Clear ();
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuerry = "SELECT * FROM GameInfo";

				dbCmd.CommandText = sqlQuerry;

				using (IDataReader reader = dbCmd.ExecuteReader ()) {
					while (reader.Read ()) {
						dbDataInput.Add (new DBDataInput (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetInt32 (3), reader.GetDateTime (4)));
						// id - name - kills - deaths - date
						Debug.Log ("	 " + reader.GetString (1) + " 	 " + reader.GetInt32 (2) + " 	" + reader.GetInt32 (3));
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
	}

	public void DeleteEntry(int id){
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

	public void DeleteAllEntries(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {

				string sqlQuerry = String.Format("DELETE FROM GameInfo");

				dbCmd.CommandText = sqlQuerry;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	public void ShowInfo()
	{
		GetInfo ();
		for (int i = 0; i < dbDataInput.Count; i++) 
		{
			GameObject holderObj = Instantiate (textPrefab);

			DBDataInput holderdata = dbDataInput[i];

			holderObj.GetComponent<ScoreManager> ().SetData (holderdata.Name, holderdata.Kills.ToString(), holderdata.Deaths.ToString());
		
			holderObj.transform.SetParent (parent);

			holderObj.GetComponent<RectTransform>().localScale = new Vector3 (0.9f, 0.9f, 0.9f);
		}
	}

	public void ClearInfo()
	{
		foreach (Transform children in parent) {
			Destroy (children.gameObject);
		}
	}
}
