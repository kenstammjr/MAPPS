<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="MAPPS.Pages.Permissions" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="PageBodyPanel" runat="server">
        <contenttemplate>
        <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                  <tr id="trAppGroupsListView" runat="server">
                    <td>
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td class="mapps-grid-top-full">
                                    <asp:Label ID="lblAppGroupHeader" runat="server" Text="Application Security Groups" CssClass="mapps-grid-top-label"></asp:Label>
                                </td>
                                <td class="mapps-grid-top-full" style="padding-left: 3px;">
                                    <asp:Label ID="lblAppMembershipHeader" runat="server" Text="Group Memberships" CssClass="mapps-grid-top-label"></asp:Label>
                                </td>
                                <td class="mapps-grid-top-full" style="padding-left: 3px;">
                                    <asp:Label ID="lblAppUserLookup" runat="server" Text="User Lookup" CssClass="mapps-grid-top-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 33%;" valign="bottom">
                                    <asp:ListBox ID="lbxAppGroups" runat="server" CssClass="mapps-input" Height="300px" Width="100%"
                                        AutoPostBack="True" OnSelectedIndexChanged="lbxAppGroups_SelectedIndexChanged"></asp:ListBox>
                                </td>
                                <td style="width: 33%;" valign="bottom">
                                    <asp:ListBox ID="lbxAppGroupMembers" runat="server" CssClass="mapps-input" Height="300px"
                                        Width="100%" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td style="width: 33%;" valign="bottom">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Panel ID="pnlAppSearchBox" runat="server" DefaultButton="ibtnAppSearch">
                                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td style="width: 5%; padding-right: 5px; padding-left: 5px;" align="right">
                                                                <asp:Label ID="lblAppSerach" runat="server" Text="Search" CssClass="mapps-std-text"></asp:Label>
                                                            </td>
                                                            <td style="width: 5%">
                                                                <asp:TextBox ID="txtAppSearch" runat="server" CssClass="mapps-input"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 90%; padding-left: 5px;">
                                                                <asp:ImageButton CssClass="ms-toolbar" ImageUrl="~/_layouts/15/images/mapps/addressbook.gif"
                                                                    runat="server" ID="ibtnAppSearch" OnClick="ibtnAppSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="bottom">
                                                <asp:ListBox ID="lbxAppUserLookup" runat="server" CssClass="mapps-input" Width="100%"
                                                    Height="250px" SelectionMode="Multiple"></asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"></td>
                                <td align="right">
                                    <asp:Button ID="btnRemFromAppGroup" runat="server" Text="Remove" Enabled="False"
                                        OnClick="btnRemFromAppGroup_Click" ToolTip="Remove selected users from selected application security group"
                                        CssClass="mapps-button" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnAddToAppGroup" runat="server" CssClass="mapps-button"
                                        Enabled="False" OnClick="btnAddToAppGroup_Click" Text="Add" ToolTip="Add selected users to selected application security group" />
                                    <asp:Button ID="btnAppClose" runat="server" Text="Close" OnClick="btnAppClose_Click"
                                        CssClass="mapps-button" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <%= GetHomeURL(true) %>
    &#32;<SharePoint:ClusteredDirectionalSeparatorArrow runat="server" />
    <%= GetAdminURL(true) %>
    &#32;<SharePoint:ClusteredDirectionalSeparatorArrow runat="server" />
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <%= PAGE_DESCRIPTION %>
</asp:Content>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderBodyAreaClass" runat="server">
    <%= ADDITIONAL_PAGE_HEAD %>
</asp:Content>
<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="mapps-grid-top-full" style="padding-left: 3px;">
                <asp:Label ID="Label4" runat="server" Text="Permisson Types"
                    CssClass="mapps-grid-top-label"></asp:Label>
            </td>
        </tr>
        <tr title="Application Groups" id="trAppGroups">
            <td>
                <table class="ms-navheader" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="width: 100%; padding-top: 5px; padding-bottom: 5px;" class="mapps-noWrap">
                            <img src="/_layouts/15/images/BCCUR.GIF" />&nbsp;
                            <asp:LinkButton ID="lbtnAppGroups" runat="server" Style="border-style: none; font-size: 1em;"
                                class="mapps-nav-link" Text="Application Groups" OnClick="lbtnAppGroups_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="ms-navSubMenu2"
                    id="tblAppGroups" runat="server">
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="phAppGroups" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     </table>
</asp:Content>
