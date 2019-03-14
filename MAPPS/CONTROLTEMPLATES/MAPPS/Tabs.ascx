<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tabs.ascx.cs" Inherits="MAPPS.ControlTemplates.Tabs" %>
<div style="border-bottom: 1px solid #c0c0c0;">
    <asp:Menu ID="TopNavigationMenu" EnableViewState="false" Orientation="Horizontal"
        StaticDisplayLevels="1" StaticSubMenuIndent="0" CssClass="mapps-topNavContainer"
        MaximumDynamicDisplayLevels="5" DynamicHorizontalOffset="2"
        SkipLinkText="" runat="server" StaticMenuStyle-HorizontalPadding="2px" Visible="false">
        <StaticMenuStyle />
        <StaticMenuItemStyle CssClass="mapps-topnav" ItemSpacing="0px" />
        <StaticSelectedStyle CssClass="mapps-topnavselected" />
        <StaticHoverStyle CssClass="mapps-topNavHover" />
        <DynamicMenuItemStyle CssClass="mapps-childNav" />
    </asp:Menu>
</div>
