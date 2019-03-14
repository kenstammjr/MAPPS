<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionItem.aspx.cs" Inherits="MAPPS.Pages.ExceptionItem" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 40%; float: left;">
        <table class="ms-formtable" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="mapps-grid-top-full" style="text-align: left;" colspan="2">
                    <asp:HyperLink ID="lnkTitle" runat="server" Text="Application Exception" ForeColor="White"></asp:HyperLink></td>
            </tr>
            <tr>
                <td class="mapps-grid-header-left" style="text-align: left;">&nbsp;</td>
                <td class="mapps-grid-header-left-end" style="text-align: right;">
                    <asp:Label ID="lblReqMsg" runat="server" Text="<font color=red>*</font> Required Field" CssClass="ms-errorn"></asp:Label></td>
            </tr>
            <%--Object Class Name--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblObjectClassName" class="ms-standardheader" Text="Object Class Name"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblObjectClassNameView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Class Method--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblClassMethod" class="ms-standardheader" Text="Class Method" runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblClassMethodView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Record ID--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblRecordID" class="ms-standardheader" Text="Record ID" runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblRecordIDView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--UserName--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblUserName" class="ms-standardheader" Text="UserName" runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblUserNameView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--UserMachineIP--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblUserMachineIP" class="ms-standardheader" Text="User Machine IP"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblUserMachineIPView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Server Name--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblServerName" class="ms-standardheader" Text="Server Name" runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblServerNameView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Exception Message--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblExceptionMessage" class="ms-standardheader" Text="Exception Message"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblExceptionMessageView" runat="server" BorderWidth="0px" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Exception Type--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblExceptionType" class="ms-standardheader" Text="Exception Type"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblExceptionTypeView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Exception Source--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblExceptionSource" class="ms-standardheader" Text="Exception Source"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblExceptionSourceView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Exception Stack--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblExceptionStackTrace" class="ms-standardheader" Text="Exception Stack Trace"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblExceptionStackTraceView" runat="server" BorderWidth="0px" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Comment--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblComment" class="ms-standardheader" Text="Comment" runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblCommentView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <%--Date Occurred--%>
            <tr>
                <td class="ms-formlabel" style="width: 190px; vertical-align: top;">
                    <asp:Label ID="lblDateOccurred" class="ms-standardheader" Text="Date Occured"
                        runat="server" />
                </td>
                <td valign="top" class="ms-formbody">
                    <asp:Label ID="lblDateOccurredView" runat="server" CssClass="ms-formbody" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" class="ms-formline">
                    <img alt="" src="/_layouts/15/images/blank.gif" width="1"
                        height="1"></td>
            </tr>
            <tr>
                <td colspan="2" width="100%">
                    <table width="100%" class="ms-formtoolbar" cellspacing="0"
                        cellpadding="2">
                        <tr>
                            <td class="ms-toolbar" nowrap="">
                                <table cellpadding="0" cellspacing="0">
                                    <tr id="trCreatedInfo">
                                        <td class="ms-descriptiontext">
                                            <asp:Label ID="lblCreatedInfo"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="trUpdatedInfo">
                                        <td class="ms-descriptiontext">
                                            <asp:Label ID="lblUpdatedInfo"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="99%" class="ms-toolbar">
                                <img width="1" height="18" alt=""
                                    src="/_layouts/images/blank.gif" />
                            </td>
                            <td style="padding-left: 5px;" class="ms-toolbar">
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                    CssClass="ms-ButtonHeightWidth" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
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
    <%= GetItemsURL(true) %>
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

