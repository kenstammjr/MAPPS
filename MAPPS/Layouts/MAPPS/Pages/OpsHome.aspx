<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/ChartCourseCompletions.ascx" TagPrefix="uc1" TagName="ChartCourseCompletions" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/Announcements.ascx" TagPrefix="uc1" TagName="Announcements" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/ChartSpecialtyBreakdown.ascx" TagPrefix="uc1" TagName="ChartSpecialtyBreakdown" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/UserAvailability.ascx" TagPrefix="uc1" TagName="UserAvailability" %>



<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpsHome.aspx.cs" Inherits="MAPPS.Pages.OpsHome" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>


<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
</asp:Content>

<asp:Content ID="Search" ContentPlaceHolderID="PlaceHolderSearchArea" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:StyleBlock runat="server">
        #sideNavBox { display: none; }
        #contentBox { margin-left: 20px; }
    </SharePoint:StyleBlock>
    <table width="800px" border="0" cellpadding="2" cellspacing="0" style="margin-right: auto; margin-left: auto;">
        <tr>
            <td style="vertical-align: top;">
                <uc1:Announcements runat="server" id="Announcements" /><br />
                <uc1:UserAvailability runat="server" id="UserAvailability" />
            </td>
            <td style="vertical-align: top; text-align: center; width: 100%;">
                <img src="/_layouts/15/images/jsou/opsSched.jpg" width="400px" />
            </td>
             <td style="vertical-align: top;">
                <uc1:ChartCourseCompletions runat="server" id="ChartCourseCompletions" /><br />
                <uc1:ChartSpecialtyBreakdown runat="server" id="ChartSpecialtyBreakdown" />
               
            </td>
        </tr>
    </table>

    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <%= GetHomeURL(true) %>
    &#32;<SharePoint:ClusteredDirectionalSeparatorArrow runat="server" />
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <%= PAGE_DESCRIPTION %>
</asp:Content>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderBodyAreaClass" runat="server">
    <%= ADDITIONAL_PAGE_HEAD %>
</asp:Content>

