using System;
using System.Data;
using System.Net;

namespace FFN
{
    ///<summary>
    /// A simple C# class to retrieve data from FantasyFootballNerd.com (FFN)
    /// Class is available free of charge, however you will need an API Key to return results.
    /// The class is intended to retrieve data from FFN, but please store the data locally to remain 
    /// bandwidth-friendly.
    ///</summary>
    ///<author>Marc Neuwirth <marc.neuwirth@gmail.com></author>
    ///<version>1.0 2010-09-5</version>
    
    public class FFN
    {
        private string _ApiKey;

        /// <summary>
        /// Fantasy Football Nerd API URL
        /// </summary>
        private string _FFN = "http://api.fantasyfootballnerd.com/";

        /// <summary>
        /// API XML pages
        /// </summary>
        private string _Schedule = "ffnScheduleXML.php";
        private string _Players = "ffnPlayersXML.php";
        private string _PlayerDetails = "ffnPlayerDetailsXML.php";
        private string _Rankings = "ffnRankingsXML.php";
        private string _Injuries = "ffnInjuriesXML.php";
        private string _SitStart = "ffnSitStartXML.php";

        /// <summary>
        /// Your API Key given to you by registering at FantasyFootballNerd.com
        /// </summary>
        public string apiKey { get { return _ApiKey; } }

        /// <summary>
        /// Error Message - a holder for any error that we may encounter
        /// </summary>
        public string ErrorMessage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ApiKey">Your ApiKey</param>
        public FFN(string ApiKey)
        {
            this._ApiKey = ApiKey;
        }

        /// <summary>
        /// Helper class to get FFN Xml from FFN
        /// </summary>
        /// <param name="request">Http Request of the Xml</param>
        /// <returns>Requested Xml Page</returns>
        private DataSet GetDataSet(HttpWebRequest request)
        {
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Load data into a dataset  
                DataSet xml = new DataSet();
                xml.ReadXml(response.GetResponseStream());
                return xml;
            }
        }

        /// <summary>
        /// Returns the Schedule DataTable, the Season and the Timezone
        /// </summary>
        /// <param name="Season">The current season</param>
        /// <param name="Timezone">The timezone</param>
        /// <returns>The schedule datatable returned from FFN API</returns>
        public DataTable GetSchedule(out string Season, out string Timezone)
        {

            HttpWebRequest request = WebRequest.Create(String.Format("{0}{1}?apiKey={2}",
                _FFN, _Schedule, _ApiKey)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);
            Season = xml.Tables["Schedule"].Rows[0]["Season"].ToString();
            Timezone = xml.Tables["Schedule"].Rows[0]["Timezone"].ToString();

            return xml.Tables["Game"];
        }

        /// <summary>
        /// Returns the a DataTable with all of the NFL Players. This does not need to be called
        /// more than once per week as it doesn't change with much frequency.
        /// </summary>
        /// <returns>The Players datatable returned from FFN API</returns>
        public DataTable GetPlayers()
        {

            HttpWebRequest request = WebRequest.Create(String.Format("{0}{1}?apiKey={2}", 
                _FFN, _Players, _ApiKey)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);

            return xml.Tables["Player"];
        }

        /// <summary>
        /// Get the Player Details of the requested Player
        /// </summary>
        /// <param name="PlayerId">The Player Id of the requested Player</param>
        /// <param name="FirstName">The first name of the requested player</param>
        /// <param name="LastName">The last name of the requested player</param>
        /// <param name="Team">The team of the requested player</param>
        /// <param name="Position">The position of the requested player</param>
        /// <returns>The DataTable of Player Details</returns>
        public DataTable GetPlayerDetails(string PlayerId, out string FirstName, out string LastName, 
            out string Team, out string Position)
        {
            HttpWebRequest request = WebRequest.Create(String.Format("{0}{1}?apiKey={2}&playerId={3}", 
                _FFN, _PlayerDetails, _ApiKey, PlayerId)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);

            FirstName = xml.Tables["PlayerDetails"].Rows[0]["FirstName"].ToString();
            LastName = xml.Tables["PlayerDetails"].Rows[0]["LastName"].ToString();
            Team = xml.Tables["PlayerDetails"].Rows[0]["Team"].ToString();
            Position = xml.Tables["PlayerDetails"].Rows[0]["Position"].ToString();

            return xml.Tables["Article"];
        }


        /// <summary>
        /// Get the current preseason draft rankings
        /// </summary>
        /// <param name="Position">The position to retrieve. Options: ALL, QB, RB, WR, TE, DEF, K</param>
        /// <param name="Limit">How many results to return. Pass an integer between 1 and 1000</param>
        /// <param name="SOS">Return the Strength of Schedule for every player? Pass a 1 for yes, 0 for no</param>
        /// <returns>The DataTable for the current Draft Rankings</returns>
        public DataTable GetDraftRankings(string Position, string Limit, string SOS)
        {

            HttpWebRequest request = WebRequest.Create(
                String.Format("{0}{1}?apiKey={2}&position={3}&limit={4}&sos={5}", 
                _FFN, _Rankings, _ApiKey,Position,Limit,SOS)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);

            return xml.Tables["Player"];
        }

        /// <summary>
        /// Get the current injury list
        /// </summary>
        /// <param name="Week">The week number to retrieve injuries for (1-17)</param>
        /// <returns>Returns the Injuries for the selected week</returns>
        public DataTable GetInjuries(string Week)
        {

            HttpWebRequest request = WebRequest.Create(String.Format("{0}{1}?apiKey={2}&week={3}", 
                _FFN, _Injuries, _ApiKey, Week)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);

            return xml.Tables["Injuries"];
        }

        /// <summary>
        /// Gets the selected week's projections
        /// </summary>
        /// <param name="Week">The week to return results for (1-17)</param>
        /// <param name="Position">The position to retrieve. Options: QB, RB, WR, TE, DEF, K</param>
        /// <returns>The DataTable for the selected week's projections</returns>
        public DataTable GetSitStart(string Week, string Position)
        {

            HttpWebRequest request = WebRequest.Create(String.Format("{0}{1}?apiKey={2}&week={3}&position={4}", 
                _FFN, _SitStart, _ApiKey, Week, Position)) as HttpWebRequest;

            DataSet xml = GetDataSet(request);
            DataTable Players = xml.Tables["Player"];
            if (Players != null)
            {
                Players.Columns.Add("PPR_ProjectedPoints");

                DataTable Projections = xml.Tables["Projections"];
                for (int i = 0; i < Players.Rows.Count; i++)
                {
                    Players.Rows[i]["PPR_ProjectedPoints"] = Projections.Rows[i]["PPR"];
                }
            }
            return Players;
        }


    }
}
