using System;
using System.Data;

namespace FFN
{


    /// <summary>
    /// This page will create the FFN object and then show you how to call the different methods 
    /// to return the information that you want. The first thing you need to do is register for an 
    /// API Key at FantasyFootballNerd.com.  When you register for one, enter into the ApiKey 
    /// variable below.  You won't be able to get data without an API key.
    /// </summary>
    ///<author>Marc Neuwirth <marc.neuwirth@gmail.com></author>
    ///<version>1.0 2010-09-5</version>
     
    public partial class _Default : System.Web.UI.Page
    {
        private string ApiKey = "000"; //Replace this with your ApiKey

        protected void Page_Load(object sender, EventArgs e)
        {
            
            FFN ffn = new FFN(ApiKey);
            DataTable table = new DataTable();
            string display = Request.Params["display"];

                if (display == "schedule")
                {
                    string Season, Timezone;
                    table = ffn.GetSchedule(out Season, out Timezone);
                    Heading.Text = "Season Schedule";
                }
                else if (display == "players")
                {
                    table = ffn.GetPlayers();
                    Heading.Text = "All NFL Players";
                }
                else if (display == "playerDetails")
                {
                    PlayerDetails.Visible = true;
                    pSubmit.Visible = true;

                    string PlayerId = playerId.Text;

                    if (!string.IsNullOrEmpty(PlayerId))
                    {
                        string FirstName, LastName, Team, position;
                        table = ffn.GetPlayerDetails(PlayerId, out FirstName, out LastName, 
                            out Team, out position);
                        Literal1.Text = string.Format("Name: {0} {1}",FirstName,LastName);
                        Literal2.Text = string.Format("Team: {0}",Team);
                        Literal3.Text = string.Format("Position: {0}",position);
                        Literal4.Text = "Articles";
                    }
                    Heading.Text = "Player Details";
                }
                else if (display == "draftRankings")
                {
                    DraftRankings.Visible = true;
                    pPosition.Visible = true;
                    pSubmit.Visible = true;

                    string position, limit, sos;

                    position = Position.SelectedValue;
                    limit = Limit.Text;
                    sos = SOS.SelectedValue;
                    Heading.Text = "Preseason Draft Rankings";

                    table = ffn.GetDraftRankings(position, limit, sos);

                }
                else if (display == "injuries")
                {
                    pGameWeek.Visible = true;
                    pSubmit.Visible = true;

                    Heading.Text = "Injuries";
                    table = ffn.GetInjuries(GameWeek.SelectedValue);
                }
                else if (display == "weeklyRankings")
                {
                    pGameWeek.Visible = true;
                    pPosition.Visible = true;
                    pSubmit.Visible = true;

                    Heading.Text = "Weekly Rankings";
                    table = ffn.GetSitStart(GameWeek.SelectedValue, Position.SelectedValue);
                }

                GridView1.DataSource = table;
                GridView1.DataBind();
        }
    }
}