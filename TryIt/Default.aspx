﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TryIt._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>TryIt Page to test services</h1>
    </div>

    <div class="row">
        <div >
            <p>
                Click the tabs above or links below to go to the services. </p>
            
            <p>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://webstrar4.fulton.asu.edu/page0/NearestStore">Nearest Store Service</asp:HyperLink>
            </p>
    </div>

    </div>
</asp:Content>
