using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HW_Backend.Models;
using HW_Backend.Models.DTO;


public class DBservices
{
    public DBservices()
    {

    }

    // This method creates a connection to the database according to the connectionString name in the appsettings.json 
    protected static SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString(conString);
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    // Create the SqlCommand
    protected static SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object
        cmd.Connection = con;              // assign the connection to the command object
        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 
        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }
        return cmd;
    }

    protected static Movies CreateMovieFromReader(SqlDataReader reader)
    {
        return new Movies(
            Convert.ToInt32(reader["id"]),
            reader["url"].ToString(),
            reader["primaryTitle"].ToString(),
            reader["description"].ToString(),
            reader["primaryImage"].ToString(),
            Convert.ToInt32(reader["year"]),
            Convert.ToDateTime(reader["releaseDate"]),
            reader["language"].ToString(),
            Convert.ToDouble(reader["budget"]),
            Convert.ToDouble(reader["grossWorldwide"]),
            reader["genres"].ToString(),
            Convert.ToBoolean(reader["isAdult"]),
            Convert.ToInt32(reader["runtimeMinutes"]),
            Convert.ToSingle(reader["averageRating"]),
            Convert.ToInt32(reader["numVotes"]),
            Convert.ToInt32(reader["priceToRent"])
         );
    }
}
