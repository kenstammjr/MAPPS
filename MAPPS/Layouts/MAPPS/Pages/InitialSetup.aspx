<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitialSetup.aspx.cs" Inherits="MAPPS.Pages.InitialSetup" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 40%; float: left;">
        <table class="ms-formtable" border="0"
            cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="mapps-grid-top-full" style="text-align: left;" colspan="2">
                    <asp:HyperLink ID="lnkTitle" runat="server" Text="Initial Setup" ForeColor="White"></asp:HyperLink></td>
            </tr>
            <%--AccountName--%>
            <tr id="trAccountName" runat="server"  class="mapps-item-row">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">Account Name
                    </h3>
                </td>
                <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblAccountNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
            <%--LastName--%>
            <tr id="trLastName" runat="server"  class="mapps-item-row">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">Last Name
                    </h3>
                </td>
                 <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblLastNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--FirstName--%>
            <tr id="trFirstName" runat="server"  class="mapps-item-row">
                 <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">First Name
                    </h3>
                </td>
                <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblFirstNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--PreferredName--%>
            <tr id="trPreferredName" runat="server"  class="mapps-item-row">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">Preferred Name
                    </h3>
                </td>
                <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblPreferredNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--UserProfile_GUID--%>
            <tr id="trUserProfile_GUID" runat="server"  class="mapps-item-row">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">User Profile GUID
                    </h3>
                </td>
               <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblUserProfileGuidView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--UserPrincipalName--%>
            <tr id="trUserPrincipalName" runat="server"  class="mapps-item-row">
                 <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">User Principal Name
                    </h3>
                </td>
                <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblUserPrincipalNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--DistinguishedName--%>
            <tr id="trDistinguishedName" runat="server"  class="mapps-item-row">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">Distinguished Name
                    </h3>
                </td>
                 <td valign="middle" class="mapps-formbody" style="white-space: nowrap; width: 350px;">
                    <asp:Label ID="lblDistinguishedNameView" runat="server" CssClass="mapps-item" />
                </td>
            </tr>
             <%--Message--%>
            <tr id="trMessage" runat="server" class="mapps-item-row" visible="true">
                <td valign="top" class="ms-formlabel" style="white-space: nowrap; width: 113px;">
                    <h3 class="ms-standardheader">Message
                    </h3>
                </td>
                 <td valign="middle" class="mapps-formbody" style="width: 350px;">
                    <asp:Label ID="lblMessageView" runat="server" CssClass="ms-error" Text="Add this user to the application as an administrator" />
                </td>
            </tr>
            <tr>
                <td colspan="2" width="100%">
                    <table width="100%" class="ms-formtoolbar" cellspacing="0"
                        cellpadding="2">
                        <tr>
                            <td width="99%" class="ms-toolbar">
                                &nbsp;
                            </td>
                            <td style="padding-left: 5px;" class="ms-toolbar">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                                    CssClass="mapps-button" Text="Add" />
                            </td>
                            <td style="padding-left: 5px;" class="ms-toolbar">
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                    CssClass="mapps-button" Text="Cancel" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both;"></div>
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
    <asp:Label ID="lblMessage" runat="server" CssClass="ms-error"></asp:Label>
    <input id="hfUserProfileRecordID" runat="server" type="hidden" />
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

</asp:Content>
