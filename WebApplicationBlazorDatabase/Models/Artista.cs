using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WebApplicationBlazorDatabase.Models
{
	public class Artista
	{
		public Artista(int ID)
		{
			//aprire il db
			//rappresenta il db fisico
			SqliteConnection myConnection = new SqliteConnection(@"Data Source = Data/chinook.db"); //instaura la connessione col db
			string sqlString = "SELECT * FROM tblArtists WHERE idArtist = @Parametro1"; //stringa della query che manderemo in esecuzione
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
			IDArtista = Convert.ToInt32(myDataReader["idArtist"].ToString());
			Nome = myDataReader["Name"].ToString();


			myConnection.Close();
		}

		public int IDArtista { get; set; }
		public string Nome { get; set; }

		public static List<Artista> ListaArtisti
		{
			get
			{
				List<Artista> listaTemp = new List<Artista>();

				//aprire il db
				//rappresenta il db fisico
				SqliteConnection myConnection = new SqliteConnection(@"Data Source = Data/chinook.db"); //instaura la connessione col db
				string sqlString = "SELECT * FROM tblArtists ORDER by NAME ASC"; //stringa della query che manderemo in esecuzione
																					  //tabella in ram che ospita i risultati pullati
				SqliteDataReader myDataReader;
				//esecuzione query sql
				SqliteCommand myCommand = new SqliteCommand(sqlString);
				myCommand.Connection = myConnection;

				myConnection.Open();

				myDataReader = myCommand.ExecuteReader();

				while (myDataReader.Read())
				{
					int id = Convert.ToInt32(myDataReader["idArtist"].ToString());
					Artista ArtistaTemp = new Artista(id);
					listaTemp.Add(ArtistaTemp);
				}


				myConnection.Close();

				return listaTemp;
			}
		}

		public List<Album> AlbumsArtista
		{
			get
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
		}
	}
}
