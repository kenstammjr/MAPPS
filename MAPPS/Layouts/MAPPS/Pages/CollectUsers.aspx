<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectUsers.aspx.cs" Inherits="MAPPS.Pages.CollectUsers" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 95%; float: left;">
        <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding: 15px 15px 15px 15px;">
            <tr id="trListView" runat="server">
                <td>
                    <SharePoint:SPGridView ID="gvData" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand" AllowSorting="True" OnSorting="gvData_Sorting"
                        Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                        AllowPaging="true" PageSize="30" OnPageIndexChanging="gvData_PageIndexChanging" BackColor="White">
                        <EmptyDataRowStyle CssClass="ms-vb" />
                        <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                        <AlternatingRowStyle CssClass="ms-alternating" />
                        <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                        <PagerSettings Position="Bottom" />
                        <Columns>
                            <%-- DisplayName --%>
                            <asp:TemplateField HeaderText="Name&nbsp;">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DisplayName") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left-none" />
                                <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%-- Activity --%>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivity" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Action") %>' />
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Visible="False" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="mapps-grid-header-left" />
                                <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                    </SharePoint:SPGridView>
                    <table id="tablePager" runat="server" class="ms-menutoolbar" visible="false">
                        <tr>
                            <td style="width: 25%; text-align: left; white-space: nowrap" class="mapps-breadcrumb">Displaying
                            <asp:Label ID="lblPagingItemsRange" runat="server" Font-Bold="true" />
                                of
                            <asp:Label ID="lblPagingItemsTotal" runat="server" Font-Bold="true" />
                                items.
                            </td>
                            <td style="width: 25%; text-align: right;">
                                <asp:ImageButton ID="ibtnPagingFirst" runat="server" OnClick="ibtnPager_Click" ToolTip="First" />
                                <asp:ImageButton ID="ibtnPagingPrevious" runat="server" OnClick="ibtnPager_Click" ToolTip="Previous" />

                            </td>
                            <td style="width: 1%; text-align: center; white-space: nowrap;" class="mapps-breadcrumb">Page:
                                    <asp:DropDownList ID="ddlPagingPages" runat="server" OnSelectedIndexChanged="ddlPagingPages_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 25%; text-align: left;">
                                <asp:ImageButton ID="ibtnPagingNext" runat="server" OnClick="ibtnPager_Click" ToolTip="Next" />
                                <asp:ImageButton ID="ibtnPagingLast" runat="server" OnClick="ibtnPager_Click" ToolTip="Last" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap;" class="mapps-breadcrumb">Items per page:
                                    <asp:DropDownList ID="ddlItemsPerPage" runat="server" OnSelectedIndexChanged="ddlItemsPerPage_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" style="width: 100%; margin-top: 5px;">
                        <tr>
                            <td valign="middle" class="ms-splitbuttontext" style="padding-top: 10px;">
                                <asp:ImageButton CssClass="ms-toolbar" ImageUrl="/_layouts/15/Images/CALADD.GIF"
                                    runat="server" ID="ibtnNewItem" Height="10" Width="10" Visible="False" />
                                <asp:LinkButton CssClass="mapps-breadcrumb" ID="lbtnNewItem" runat="server" Text="&nbsp;Add new item" Visible="False" />
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
</asp:Content>
<asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <%= PAGE_DESCRIPTION %>
</asp:Content>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderBodyAreaClass" runat="server">
    <%= ADDITIONAL_PAGE_HEAD %>
</asp:Content>
<asp:Content ID="LeftNavBar" ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
     <asp:PlaceHolder ID="PlaceHolderLeftNavBar" runat="server">
         <uc1:NavigationTree runat="server" id="NavigationTree" />
     </asp:PlaceHolder>
</asp:Content>

