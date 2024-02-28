using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class ConnectionStringProvider
	{
		private static readonly string _connectionString;

		static ConnectionStringProvider()
		{

			// Initialize _connectionString from configuration or other source

			//_connectionString = "Server=DESKTOP-VUIIR3S;Database=JD_FC_VD_RecipesProject;Integrated Security=True;"; //Torre
			_connectionString = "Server=DESKTOP-QHK0NI6;Database=JD_FC_VD_RecipesProject;Integrated Security=True;"; //Portatil
			//private static string _connectionString = "Data Source=db.assembly.pt;Initial Catalog=JD_FC_FC_TtodoManagement; User Id=Staff; Password=Cyb3rAdmin;";
		}

		public static string GetConnectionString()
		{
			return _connectionString;
		}

	}
}
