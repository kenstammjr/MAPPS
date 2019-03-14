<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>


<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exceptions.aspx.cs" Inherits="MAPPS.Pages.Exceptions" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 95%; float: left;">
        <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td class="mapps-grid-top-left" style="text-align: left;">
                    <asp:HyperLink ID="lnkTitle" runat="server" Text="Application Exceptions" ForeColor="White"></asp:HyperLink></td>
                <td class="mapps-grid-top-right" style="text-align: right;">
                    <asp:Label ID="lblSearch" runat="server" Text="Search" CssClass="mapps-tool-text" />
                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" CssClass="mapps-tool-input" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnPurge" runat="server" Text="Purge" CssClass="mapps-app-page-button" Visible="false" OnClick="lbtnPurge_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr id="trListView" runat="server">
                <td colspan="2">
                    <SharePoint:SPGridView ID="gvData" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand" AllowSorting="True" OnSorting="gvData_Sorting"
                        Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                        AllowPaging="true" PageSize="30" OnPageIndexChanging="gvData_PageIndexChanging" BackColor="White">
                        <EmptyDataRowStyle CssClass="mapps-grid-text" />
                        <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                        <AlternatingRowStyle CssClass="ms-alternating" />
                        <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                        <PagerSettings Position="Bottom" />
                        <Columns>
                            <%--Class--%>
                            <asp:TemplateField HeaderText="Object Class Name" SortExpression="Class">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnClass" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                        CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.Class") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <%--Class Method--%>
                            <asp:TemplateField HeaderText="Class Method" SortExpression="Method">
                                <ItemTemplate>
                                    <asp:Label ID="lblMethod" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Method") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <%-- Exception Message--%>
                            <asp:TemplateField HeaderText="Exception Message" SortExpression="ExceptionMessage">
                                <ItemTemplate>
                                    <asp:Label ID="lblExceptionMessage" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ExceptionMessage") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <%--Date Occurred--%>
                            <asp:TemplateField HeaderText="Date Occurred" SortExpression="DateOccurred">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateOccurred" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DateOccurred") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left-end" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                        </Columns>
                    </SharePoint:SPGridView>
                    <table id="tablePager" runat="server" style="border-top: 1px solid #808080;" visible="false">
                        <tr>
                            <td style="width: 25%; text-align: left; white-space: nowrap" class="mapps-std-text">Displaying
                            <asp:Label ID="lblPagingItemsRange" runat="server" Font-Bold="true" />
                                of
                            <asp:Label ID="lblPagingItemsTotal" runat="server" Font-Bold="true" />
                                items.
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap;">
                                <asp:ImageButton ID="ibtnPagingFirst" runat="server" OnClick="ibtnPager_Click" ToolTip="First" />
                                <asp:ImageButton ID="ibtnPagingPrevious" runat="server" OnClick="ibtnPager_Click" ToolTip="Previous" />
                            </td>
                            <td style="width: 1%; text-align: center; white-space: nowrap;" class="mapps-std-text">Page:
                                    <asp:DropDownList ID="ddlPagingPages" runat="server" OnSelectedIndexChanged="ddlPagingPages_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 25%; text-align: left; white-space: nowrap;">
                                <asp:ImageButton ID="ibtnPagingNext" runat="server" OnClick="ibtnPager_Click" ToolTip="Next" />
                                <asp:ImageButton ID="ibtnPagingLast" runat="server" OnClick="ibtnPager_Click" ToolTip="Last" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap;" class="mapps-std-text">Items per page:
                                    <asp:DropDownList ID="ddlItemsPerPage" runat="server" OnSelectedIndexChanged="ddlItemsPerPage_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="clear: both;"></div>
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
    </div>
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
</asp:Content><asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <%= PAGE_DESCRIPTION %>
</asp:Content>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderBodyAreaClass" runat="server">
    <%= ADDITIONAL_PAGE_HEAD %>
</asp:Content>
<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
    <uc1:NavigationTree runat="server" id="NavigationTree" />
</asp:Content>

