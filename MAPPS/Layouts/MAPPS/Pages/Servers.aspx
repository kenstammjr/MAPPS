﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>




<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Servers.aspx.cs" Inherits="MAPPS.Pages.Servers" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 95%; float: left;">
        <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td class="mapps-grid-top-left" style="text-align: left;">
                    <asp:HyperLink ID="lnkTitle" runat="server" Text="Servers" ForeColor="White"></asp:HyperLink></td>
                <td class="mapps-grid-top-right" style="text-align: right;">
                    <asp:Label ID="lblSearch" runat="server" Text="Search" CssClass="mapps-tool-text" />
                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" CssClass="mapps-tool-input" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnNew" runat="server" Text="New" CssClass="mapps-app-page-button" Visible="false" OnClick="lbtnNew_Click"></asp:LinkButton>
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
                            <%-- Edit --%>
                            <asp:TemplateField HeaderText="" Visible="True">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                        CommandName="EditItem" ToolTip="Edit" ImageUrl="~/_layouts/Images/EditItem.gif" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="5px" />
                            </asp:TemplateField>
                            <%-- Name --%>
                            <asp:TemplateField HeaderText="Name&nbsp;" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnName" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                        CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left-none" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- TypeName --%>
                            <asp:TemplateField HeaderText="Type" SortExpression="TypeName">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TypeName") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- FunctionName --%>
                            <asp:TemplateField HeaderText="Function" SortExpression="FunctionName">
                                <ItemTemplate>
                                    <asp:Label ID="lblFunctionName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FunctionName") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- EnvironmentName --%>
                            <asp:TemplateField HeaderText="Environment" SortExpression="EnvironmentName">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnvironmentName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EnvironmentName") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- Description --%>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                             <%-- VersionName --%>
                            <asp:TemplateField HeaderText="OS Version" SortExpression="VersionName">
                                <ItemTemplate>
                                    <asp:Label ID="lblVersionName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VersionName") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- Purpose --%>
                            <asp:TemplateField HeaderText="Purpose&nbsp;" SortExpression="Purpose">
                                <ItemTemplate>
                                    <asp:Label ID="lblPurpose" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Purpose") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                             <%-- CPU --%>
                            <asp:TemplateField HeaderText="CPU&nbsp;" SortExpression="CPU">
                                <ItemTemplate>
                                    <asp:Label ID="lblCPU" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CPU") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- Memory --%>
                            <asp:TemplateField HeaderText="Memory">
                                <ItemTemplate>
                                    <asp:Label ID="lblMemory" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Memory") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- IPAddresses --%>
                            <asp:TemplateField HeaderText="IP Addresses" SortExpression="IPAddresses" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIPAddresses" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IPAddresses") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- Contacts --%>
                            <asp:TemplateField HeaderText="Contacts&nbsp;" SortExpression="Contacts" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblContacts" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Contacts") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
                            </asp:TemplateField>
                            <%-- StatusName --%>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StatusName") %>' />
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Visible="False" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left-end" />
                                <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" Width="5px" />
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

