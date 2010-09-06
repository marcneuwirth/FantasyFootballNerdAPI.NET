<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FFN._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FantasyFootballNerd.com API Test for ASP.NET</title>
</head>
<body>
    <h2>
        FantasyFootballNerd.com API Test</h2>
    <ul>
        <li><a href="?display=schedule">Get Season Schedule</a></li>
        <li><a href="?display=players">Get All NFL Players</a></li>
        <li><a href="?display=playerDetails">Get Player Details</a></li>
        <li><a href="?display=draftRankings">Get Draft Rankings</a></li>
        <li><a href="?display=injuries">Get Injuries</a></li>
        <li><a href="?display=weeklyRankings">Get Weekly Rankings</a></li>
    </ul>
    <div style="height: 10px; border-top: 1px solid #3366CC;">
    </div>
    <form id="form1" runat="server">
    <div>
        <h4>
            <asp:Literal runat="server" ID="Heading" /></h4>
        <div runat="server" id="PlayerDetails" visible="false">
            <p>
                FFN playerId:
                <asp:TextBox runat="server" ID="playerId" />
            </p>
            <p>
                <asp:Literal runat="server" ID="Literal1" /></p>
            <p>
                <asp:Literal runat="server" ID="Literal2" /></p>
            <p>
                <asp:Literal runat="server" ID="Literal3" /></p>
            <p>
                <strong>
                    <asp:Literal runat="server" ID="Literal4" /></strong></p>
        </div>
        <p runat="server" id="pPosition" visible="false">
            Position:
            <asp:DropDownList runat="server" ID="Position">
                <asp:ListItem Value="ALL" Selected="True">ALL</asp:ListItem>
                <asp:ListItem Value="QB">QB</asp:ListItem>
                <asp:ListItem Value="RB">RB</asp:ListItem>
                <asp:ListItem Value="WR">WR</asp:ListItem>
                <asp:ListItem Value="TE">TE</asp:ListItem>
                <asp:ListItem Value="DEF">DEF</asp:ListItem>
                <asp:ListItem Value="K">K</asp:ListItem>
            </asp:DropDownList>
        </p>
        <div runat="server" id="DraftRankings" visible="false">
            <p>
                # of Results:
                <asp:TextBox runat="server" ID="Limit" />
                (1 - 1000)</p>
            <p>
                Include Strength of Schedule?
                <asp:DropDownList runat="server" ID="SOS">
                    <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </p>
        </div>
        <p runat="server" visible="false" id="pGameWeek">
            Week:
            <asp:DropDownList runat="server" ID="GameWeek">
                <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p runat="server" id="pSubmit" visible="false">
            <asp:Button runat="server" ID="submit" Text="Submit" />
        </p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
