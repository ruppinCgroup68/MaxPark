using MaxPark.BL;
using System.Data.SqlClient;
using System.Data;

namespace MaxPark.DAL
{
    public class DBServicesEvents
    {

        /// <summary>
        /// DBServices is a class created by me to provides some DataBase Services
        /// </summary>
        public DBServicesEvents()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }


        //--------------------------------------------------------------------------------------------------
        //                            R E A D - A L L  E V E N T S 
        //---------------------------------------------------------------------------------------------------
        public List<Event> getEvents()
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            List<Event> events = new List<Event>();
            cmd = ReadAllUsers(con, "spReadEvents");
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Event e = new Event();
                e.EventId = Convert.ToInt32(dataReader["eventId"]);
                e.UserId = Convert.ToInt32(dataReader["userId"]);
                e.UserCarNum = dataReader["userCarNum"].ToString();
                e.EventType = dataReader["eventType"].ToString();
                events.Add(e);
            }

            if (con != null)
            {
                // close the db connection
                con.Close();
            }

            return events;
        }

        //---------------------------------------------------------------------------------------------------
        SqlCommand ReadAllUsers(SqlConnection con, string spName)
        {
            SqlCommand cmd = new SqlCommand();// create the command object
            cmd.Connection = con;// assign the connection to the command object
            cmd.CommandText = spName;// can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10;// Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        //                            I N S E R T  - E V E N T 
        //---------------------------------------------------------------------------------------------------
        public int addNewEvent(Event ev)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = insertNewUserSP("spInsertnewEvent", con, ev);// create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------
        private SqlCommand insertNewUserSP(String spName, SqlConnection con, Event ev)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = spName;// can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure
            cmd.Parameters.AddWithValue("@userId", ev.UserId);
            cmd.Parameters.AddWithValue("@userCarNum", ev.UserCarNum);
            cmd.Parameters.AddWithValue("@userEvent", ev.EventType);
            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        //                            D E L E T E   -  U S E R 
        //--------------------------------------------------------------------------------------------------  
        public int deleteEvent(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("myProjDB");//create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            cmd = deleteUserProcedure("spDeleteEvent", con, id);//create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery();//execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //---------------------------------------------------------------------------------
        private SqlCommand deleteUserProcedure(String spName, SqlConnection con, int eventId)
        {
            SqlCommand cmd = new SqlCommand();//create the command object
            cmd.Connection = con;// assign the connection to the command object
            cmd.CommandText = spName;//can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10; //Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.StoredProcedure;//the type of the command, can also be text
            cmd.Parameters.AddWithValue("@eventId", eventId);
            return cmd;
        }
    }
}
