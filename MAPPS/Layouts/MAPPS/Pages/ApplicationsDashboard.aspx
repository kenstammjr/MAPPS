<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>




<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationsDashboard.aspx.cs" Inherits="MAPPS.Pages.ApplicationsDashboard" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>


<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
</asp:Content>

<asp:Content ID="Search" ContentPlaceHolderID="PlaceHolderSearchArea" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        <table border="0" cellpadding="4" cellspacing="4">
        <tr>
            <td valign="top" style="width: 33%;">
                <asp:Repeater ID="rptLeft" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr class="ms-linksection-level1">
                            <td valign="top">
                                <h3><%# DataBinder.Eval(Container,"DataItem.Name") %></h3>
                            </td>
                            <td valign="top">
                                <img src="/_layouts/15/images/kpidefault-0.gif" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td style="width: 16px;" />
            <td valign="top" style="width: 33%;">
                <asp:Repeater ID="rptCenter" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr class="ms-linksection-level1">
                            <td valign="top">
                                <h3><%# DataBinder.Eval(Container,"DataItem.Name") %></h3>
                            </td>
                            <td valign="top">
                                <img src="/_layouts/15/images/kpidefault-0.gif" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td style="width: 16px;" />
            <td valign="top" style="width: 33%;">
                <asp:Repeater ID="rptRight" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr class="ms-linksection-level1">
                            <td valign="top">
                                <h3><%# DataBinder.Eval(Container,"DataItem.Name") %></h3>
                            </td>
                            <td valign="top">
                                <img src="/_layouts/15/images/kpidefault-0.gif" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
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
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderBodyAreaClass" runat="server">
    <%= ADDITIONAL_PAGE_HEAD %>
</asp:Content>

