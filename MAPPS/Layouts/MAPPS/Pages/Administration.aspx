<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="MAPPS.Pages.Administration" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(
            function (sender, args) {
                $get("<%=updPnlMain.ClientID %>").style.display = "none";
            }
        );
            prm.add_endRequest(
                function (sender, args) {
                    $get("<%=updPnlMain.ClientID %>").style.display = "";
            }
        );
    </script>
    <asp:UpdateProgress ID="updPrgMain" AssociatedUpdatePanelID="updPnlMain" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <%= PROCESSING_TEMPLATE %>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updPnlMain" runat="server">
        <ContentTemplate>
            <div style="width: 65%; float: left;">
                <table border="0" cellpadding="4" cellspacing="4">
                    <tr>
                        <td valign="top" style="width: 50%;">
                            <asp:Repeater ID="rptLeft" runat="server">
                                <HeaderTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                </HeaderTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <tr class="ms-linksection-level1">
                                        <td valign="top" style="width: 60px; height: 32px;">
                                            <img style="border: 0;" alt="" src="<%# DataBinder.Eval(Container,"DataItem.Icon") %>" />
                                        </td>
                                        <td valign="top">
                                            <h3><%# DataBinder.Eval(Container,"DataItem.Name") %></h3>
                                            <ul><%# DataBinder.Eval(Container,"DataItem.Links") %></ul>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="width: 16px;" />
                        <td valign="top" style="width: 50%;">
                            <asp:Repeater ID="rptRight" runat="server">
                                <HeaderTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                </HeaderTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <tr class="ms-linksection-level1">
                                        <td valign="top" style="width: 60px; height: 32px;">
                                            <img style="border: 0;" alt="" src="<%# DataBinder.Eval(Container,"DataItem.Icon") %>" />
                                        </td>
                                        <td valign="top">
                                            <h3><%# DataBinder.Eval(Container,"DataItem.Name") %></h3>
                                            <ul><%# DataBinder.Eval(Container,"DataItem.Links") %></ul>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="ms-pageinformation" style="width: 30%; float: right;">
                <table width="100%" class="ms-pageinformation" id="idItemHoverTable">
                    <tr>
                        <th style="vertical-align: top;" colspan="2" scope="col">
                            <div class="ms-linksectionheader">
                                <h3 class="ms-standardheader">
                                    <asp:Label ID="lblAppName" runat="server" />
                                </h3>
                            </div>
                        </th>
                    </tr>
                    <tr>
                        <th style="vertical-align: top; white-space: nowrap;" scope="row">
                            <asp:Label ID="lblAppInfo" runat="server" ForeColor="Silver" />
                        </th>
                    </tr>
                </table>
            </div>
            <div style="clear: both;"></div>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

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
<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
    <uc1:NavigationTree runat="server" id="NavigationTree" />
</asp:Content>

