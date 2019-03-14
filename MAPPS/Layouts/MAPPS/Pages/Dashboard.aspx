<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MAPPS.Pages.Dashboard" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <style>
        #sideNavBox {
            display: none;
        }

        #contentBox {
            margin-left: 20px;
        }
    </style>
    <table>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="background-color: black; padding: 3px;">
                                        <asp:Label ID="Label3" runat="server" Text="Pending Leave Requests" CssClass="ms-vb2"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <SharePoint:SPGridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="gvLeave_RowCommand" AllowSorting="False"
                                            Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                                            AllowPaging="true" PageSize="30" OnPageIndexChanging="gvLeave_PageIndexChanging" BackColor="White">
                                            <EmptyDataRowStyle CssClass="ms-vb" />
                                            <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                                            <AlternatingRowStyle CssClass="ms-alternating" />
                                            <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                                            <PagerSettings Position="Bottom" />
                                            <Columns>
                                                <%-- Edit --%>
                                                <asp:TemplateField HeaderText="" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEditLeave" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                            CommandName="EditItem" ToolTip="Edit" ImageUrl="~/_layouts/Images/EditItem.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                                                </asp:TemplateField>
                                                <%-- FullName --%>
                                                <asp:TemplateField HeaderText="Name&nbsp;">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnLeaveFullName" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                            CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.FullName") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left-none" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- Start --%>
                                                <asp:TemplateField HeaderText="Start">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveStartDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StartDate", "{0:dd-MMM-yyyy}") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- Finish --%>
                                                <asp:TemplateField HeaderText="Finish">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveEndDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EndDate", "{0:dd-MMM-yyyy}") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </SharePoint:SPGridView>
                                        <table id="tableLeavePager" runat="server" class="ms-menutoolbar" visible="false">
                                            <tr>
                                                <td style="width: 25%; text-align: left; white-space: nowrap" class="mapps-breadcrumb">Displaying
                                                    <asp:Label ID="lblLeavePagingItemsRange" runat="server" Font-Bold="true" />
                                                    of
                                                    <asp:Label ID="lblLeavePagingItemsTotal" runat="server" Font-Bold="true" />
                                                    items.
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:ImageButton ID="ibtnLeavePagingFirst" runat="server" OnClick="ibtnLeavePager_Click" ToolTip="First" />
                                                    <asp:ImageButton ID="ibtnLeavePagingPrevious" runat="server" OnClick="ibtnLeavePager_Click" ToolTip="Previous" />

                                                </td>
                                                <td style="width: 1%; text-align: center; white-space: nowrap;" class="mapps-breadcrumb">Page:
                                                    <asp:DropDownList ID="ddlLeavePagingPages" runat="server" OnSelectedIndexChanged="ddlLeavePagingPages_SelectedIndexChanged" AutoPostBack="true" />
                                                </td>
                                                <td style="width: 25%; text-align: left;">
                                                    <asp:ImageButton ID="ibtnLeavePagingNext" runat="server" OnClick="ibtnLeavePager_Click" ToolTip="Next" />
                                                    <asp:ImageButton ID="ibtnLeavePagingLast" runat="server" OnClick="ibtnLeavePager_Click" ToolTip="Last" />
                                                </td>
                                                <td style="width: 25%; text-align: right; white-space: nowrap;" class="mapps-breadcrumb">Items per page:
                                                    <asp:DropDownList ID="ddlLeaveItemsPerPage" runat="server" OnSelectedIndexChanged="ddlLeaveItemsPerPage_SelectedIndexChanged" AutoPostBack="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="background-color: black; padding: 3px;">
                                        <asp:Label ID="Label2" runat="server" Text="Pending Travel Requests" CssClass="ms-vb2"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <SharePoint:SPGridView ID="gvTravel" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="gvTravel_RowCommand" AllowSorting="False"
                                            Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                                            AllowPaging="true" PageSize="30" OnPageIndexChanging="gvTravel_PageIndexChanging" BackColor="White">
                                            <EmptyDataRowStyle CssClass="ms-vb" />
                                            <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                                            <AlternatingRowStyle CssClass="ms-alternating" />
                                            <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                                            <PagerSettings Position="Bottom" />
                                            <Columns>
                                                <%-- Edit --%>
                                                <asp:TemplateField HeaderText="" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEditTravel" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                            CommandName="EditItem" ToolTip="Edit" ImageUrl="~/_layouts/Images/EditItem.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                                                </asp:TemplateField>
                                                <%-- FullName --%>
                                                <asp:TemplateField HeaderText="Name&nbsp;">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnTravelFullName" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                            CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.FullName") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left-none" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- Start --%>
                                                <asp:TemplateField HeaderText="Start">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTravelStartDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StartDate", "{0:dd-MMM-yyyy}") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- Finish --%>
                                                <asp:TemplateField HeaderText="Finish">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTravelEndDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EndDate", "{0:dd-MMM-yyyy}") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="mapps-grid-header-left" />
                                                    <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </SharePoint:SPGridView>
                                        <table id="tableTravelPager" runat="server" class="ms-menutoolbar" visible="false">
                                            <tr>
                                                <td style="width: 25%; text-align: left; white-space: nowrap" class="mapps-breadcrumb">Displaying
                                                    <asp:Label ID="lblTravelPagingItemsRange" runat="server" Font-Bold="true" />
                                                    of
                                                    <asp:Label ID="lblTravelPagingItemsTotal" runat="server" Font-Bold="true" />
                                                    items.
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:ImageButton ID="ibtnTravelPagingFirst" runat="server" OnClick="ibtnTravelPager_Click" ToolTip="First" />
                                                    <asp:ImageButton ID="ibtnTravelPagingPrevious" runat="server" OnClick="ibtnTravelPager_Click" ToolTip="Previous" />

                                                </td>
                                                <td style="width: 1%; text-align: center; white-space: nowrap;" class="mapps-breadcrumb">Page:
                                                    <asp:DropDownList ID="ddlTravelPagingPages" runat="server" OnSelectedIndexChanged="ddlTravelPagingPages_SelectedIndexChanged" AutoPostBack="true" />
                                                </td>
                                                <td style="width: 25%; text-align: left;">
                                                    <asp:ImageButton ID="ibtnTravelPagingNext" runat="server" OnClick="ibtnTravelPager_Click" ToolTip="Next" />
                                                    <asp:ImageButton ID="ibtnTravelPagingLast" runat="server" OnClick="ibtnTravelPager_Click" ToolTip="Last" />
                                                </td>
                                                <td style="width: 25%; text-align: right; white-space: nowrap;" class="mapps-breadcrumb">Items per page:
                                                     <asp:DropDownList ID="ddlTravelItemsPerPage" runat="server" OnSelectedIndexChanged="ddlTravelItemsPerPage_SelectedIndexChanged" AutoPostBack="true" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td style="background-color: black; padding: 3px;">
                            <asp:Label ID="lblPersonnelHeader" runat="server" Text="Personnel" CssClass="ms-vb2"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <SharePoint:SPGridView ID="gvPersonnel" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="gvPersonnel_RowCommand" AllowSorting="True" OnSorting="gvPersonnel_Sorting"
                                Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                                AllowPaging="true" PageSize="30" OnPageIndexChanging="gvPersonnel_PageIndexChanging" BackColor="White">
                                <EmptyDataRowStyle CssClass="ms-vb" />
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
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                                    </asp:TemplateField>
                                    <%-- FullName --%>
                                    <asp:TemplateField HeaderText="Name&nbsp;" SortExpression="LastName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnFullName" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.FullName") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="mapps-grid-header-left-none" />
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- Section --%>
                                    <asp:TemplateField HeaderText="Section" SortExpression="OrganizationName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrganizationName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OrganizationName") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="mapps-grid-header-left" />
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- RankTitle --%>
                                    <asp:TemplateField HeaderText="Rank" SortExpression="RankTitle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankTitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RankTitle") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="mapps-grid-header-left" />
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- Nationality --%>
                                    <asp:TemplateField HeaderText="Nationality" SortExpression="Nationality">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNationality" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nationality") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="mapps-grid-header-left" />
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- Assigned --%>
                                    <asp:TemplateField HeaderText="Assigned" SortExpression="Assigned">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssigned" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Assigned", "{0:dd-MMM-yyyy}") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="mapps-grid-header-left" />
                                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </SharePoint:SPGridView>
                            <table id="tablePersonnelPager" runat="server" class="ms-menutoolbar" visible="false">
                                <tr>
                                    <td style="width: 25%; text-align: left; white-space: nowrap" class="mapps-breadcrumb">Displaying
                                        <asp:Label ID="lblPersonnelPagingItemsRange" runat="server" Font-Bold="true" />
                                        of
                                        <asp:Label ID="lblPersonnelPagingItemsTotal" runat="server" Font-Bold="true" />
                                        items.
                                    </td>
                                    <td style="width: 25%; text-align: right;">
                                        <asp:ImageButton ID="ibtnPersonnelPagingFirst" runat="server" OnClick="ibtnPersonnelPager_Click" ToolTip="First" />
                                        <asp:ImageButton ID="ibtnPersonnelPagingPrevious" runat="server" OnClick="ibtnPersonnelPager_Click" ToolTip="Previous" />

                                    </td>
                                    <td style="width: 1%; text-align: center; white-space: nowrap;" class="mapps-breadcrumb">Page:
                                        <asp:DropDownList ID="ddlPersonnelPagingPages" runat="server" OnSelectedIndexChanged="ddlPersonnelPagingPages_SelectedIndexChanged" AutoPostBack="true" />
                                    </td>
                                    <td style="width: 25%; text-align: left;">
                                        <asp:ImageButton ID="ibtnPersonnelPagingNext" runat="server" OnClick="ibtnPersonnelPager_Click" ToolTip="Next" />
                                        <asp:ImageButton ID="ibtnPersonnelPagingLast" runat="server" OnClick="ibtnPersonnelPager_Click" ToolTip="Last" />
                                    </td>
                                    <td style="width: 25%; text-align: right; white-space: nowrap;" class="mapps-breadcrumb">Items per page:
                                        <asp:DropDownList ID="ddlPersonnelItemsPerPage" runat="server" OnSelectedIndexChanged="ddlPersonnelItemsPerPage_SelectedIndexChanged" AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="clear: both;"></div>
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
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
     <asp:PlaceHolder ID="PlaceHolderLeftNavBar" runat="server"></asp:PlaceHolder>
</asp:Content>

