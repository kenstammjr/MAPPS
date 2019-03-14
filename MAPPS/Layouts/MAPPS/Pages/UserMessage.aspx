<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMessage.aspx.cs" Inherits="MAPPS.Pages.UserMessage" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/_layouts/15/MAPPS/styles/mapps.css" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
 <style>
     #sideNavBox {
         display: none;
     }

     #contentBox {
         margin-left: 90px;
     }
 </style>
        <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="ms-vb3">
                <h3>
                    <asp:Label ID="lblHeader" runat="server" ForeColor="Silver"></asp:Label></h3>
            </td>
        </tr>
        <tr>
            <td class="ms-vb3">
                <asp:Label ID="lblMessage" runat="server" CssClass="ms-vb3" ForeColor="Silver"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ms-descriptiontext">
                <img src="images/blank.gif" width="1" height="10" alt="" />
            </td>
        </tr>
        <tr id="trError" runat="server">
            <td style="padding-top: 0px; vertical-align: top;" class="style7">
                <asp:Label ID="lblErrorMessage" runat="server" Visible="False" CssClass="ms-formvalidation" ForeColor="Red" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <%= PAGE_DESCRIPTION %>
</asp:Content>
<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
</asp:Content>
