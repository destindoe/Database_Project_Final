using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dream_Home
{
    public class ConnectionManager
    {
        private static SqlConnection _default;
        private static Dictionary<int, SqlConnection> connections = new Dictionary<int, SqlConnection>();
        
        /// <summary>
        /// Call this to open a new connection.
        /// Will not create a new connection if the same connection string is passed!
        /// For these situations just call, Last() instead. Its quicker!
        /// </summary>
        /// <param name="connectionString">Your ConnectionStrings.<Name> </Name></param>
        /// <returns></returns>
        public static SqlConnection Open(string connectionString)
        {
            if (connections.ContainsKey(connectionString.GetHashCode()))
                return connections[connectionString.GetHashCode()];
            try
            {
                SqlConnection db = new SqlConnection(connectionString);
                db.Open();

                connections.Add(connectionString.GetHashCode(), db);

                if (_default == null)
                    _default = db;

                return connections[connectionString.GetHashCode()];

                
            }catch(Exception e)
            {
                MessageBox.Show("Failed to open connection!", "Error");
                Application.Current.Shutdown(1);
                return null;
            }
        }

        /// <summary>
        /// Call this to reopen a connection.
        /// </summary>
        /// <returns>Previously opened SqlConnection.</returns>
        public static SqlConnection Last()
        {
            if (_default != null)
                return _default;
            else
                throw new Exception("Cannot return unset default! Call Open() first!");
        }
    }
}
