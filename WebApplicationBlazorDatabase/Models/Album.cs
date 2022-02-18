using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WebApplicationBlazorDatabase.Models
{
	public class Album
	{
		public Album(int ID)
		{
			//aprire il db
			//rappresenta il db fisico
			SqliteConnection myConnection = new SqliteConnection(@"Data Source = Data/chinook.db"); //instaura la connessione col db
			string sqlString = "SELECT * FROM tblAlbums WHERE idAlbum = @Parametro1"; //stringa della query che manderemo in esecuzione
			SqliteParameter myParameter = new SqliteParameter("@Parametro1", ID); //
																				  //tabella in ram che ospita i risultati pullati
			SqliteDataReader myDataReader;
			//esecuzione query sql
			SqliteCommand myCommand = new SqliteCommand(sqlString);
			myCommand.Parameters.Add(myParameter);
			myCommand.Connection = myConnection;

			myConnection.Open();

			myDataReader = myCommand.ExecuteReader();

			myDataReader.Read();
			IDAlbum = Convert.ToInt32(myDataReader["idAlbum"].ToString());
			Titolo = myDataReader["Title"].ToString();
			ArtistaID = Convert.ToInt32(myDataReader["ArtistId"].ToString());


			myConnection.Close();
		}

		public static List<Album> GetAlbumsByArtistID(int IDArtista)
		{
			List<Album> listaTemp = new List<Album>();

			//aprire il db
			//rappresenta il db fisico
			SqliteConnection myConnection = new SqliteConnection(@"Data Source = Data/chinook.db"); //instaura la connessione col db
			string sqlString = "SELECT * FROM tblAlbums WHERE ArtistId = @Parametro1"; //stringa della query che manderemo in esecuzione
			SqliteParameter myParameter = new SqliteParameter("@Parametro1", IDArtista); //
																				  //tabella in ram che ospita i risultati pullati
			SqliteDataReader myDataReader;
			//esecuzione query sql
			SqliteCommand myCommand = new SqliteCommand(sqlString);
			myCommand.Parameters.Add(myParameter);
			myCommand.Connection = myConnection;

			myConnection.Open();

			myDataReader = myCommand.ExecuteReader();

			while (myDataReader.Read())
			{
				int id = Convert.ToInt32(myDataReader["idAlbum"].ToString());
				Album AlbumTemp = new Album(id);
				listaTemp.Add(AlbumTemp);
			}

			myConnection.Close();

			return listaTemp;
		}

		public int IDAlbum { get; set; }
		public string Titolo { get; set; }
		public int ArtistaID { get; set; }

		public static List<Album> ListaAlbum
		{
			get
			{
				List<Album> listaTemp = new List<Album>();

				//aprire il db
				//rappresenta il db fisico
				SqliteConnection myConnection = new SqliteConnection(@"Data Source = Data/chinook.db"); //instaura la connessione col db
				string sqlString = "SELECT * FROM tblAlbums ORDER by TITLE ASC"; //stringa della query che manderemo in esecuzione
																				 //tabella in ram che ospita i risultati pullati
				SqliteDataReader myDataReader;
				//esecuzione query sql
				SqliteCommand myCommand = new SqliteCommand(sqlString);
				myCommand.Connection = myConnection;

				myConnection.Open();

				myDataReader = myCommand.ExecuteReader();

				while (myDataReader.Read())
				{
					int id = Convert.ToInt32(myDataReader["idAlbum"].ToString());
					Album AlbumTemp = new Album(id);
					listaTemp.Add(AlbumTemp);
				}


				myConnection.Close();

				return listaTemp;
			}
		}
	}
}
