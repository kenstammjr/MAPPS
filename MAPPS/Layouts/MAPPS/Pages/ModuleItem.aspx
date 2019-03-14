<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleItem.aspx.cs" Inherits="MAPPS.Pages.ModuleItem" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 40%; float: left;">
        <table class="ms-formtable" border="0" cellpadding="0" cellspacing="0" width="100%">
            <%--Name--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Name
                                <asp:Label ID="lblNameRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtName" runat="server" CssClass="ms-input"
                        MaxLength="50" />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                        ControlToValidate="txtName"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblNameView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--Description--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Description
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="ms-input"
                        MaxLength="250" Width="500px" />
                    <asp:Label ID="lblDescriptionView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--Directory--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Directory
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtDirectory" runat="server" CssClass="ms-input"
                        MaxLength="50" />
                    <asp:Label ID="lblDirectoryView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--URL--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">URL
                                <asp:Label ID="lblURLRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtURL" runat="server" CssClass="ms-input"
                        MaxLength="255" Width="500px" />
                    <asp:RequiredFieldValidator ID="rfvURL" runat="server"
                        ControlToValidate="txtURL"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblURLView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--AdminURL--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Admin URL
                                <asp:Label ID="lblAdminURLRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtAdminURL" runat="server" CssClass="ms-input"
                        MaxLength="255" Width="500px" />
                    <asp:RequiredFieldValidator ID="rfvAdminURL" runat="server"
                        ControlToValidate="txtAdminURL"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblAdminURLView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--ImageURL--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Image URL
                                <asp:Label ID="lblImageURLRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtImageURL" runat="server" CssClass="ms-input"
                        MaxLength="255" Width="500px" />
                    <asp:RequiredFieldValidator ID="rfvImageURL" runat="server"
                        ControlToValidate="txtImageURL"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblImageURLView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--DBVersion--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Database Version
                                <asp:Label ID="lblDBVersionRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtDBVersion" runat="server" CssClass="ms-input"
                        MaxLength="50" />
                    <asp:RequiredFieldValidator ID="rfvDBVersion" runat="server"
                        ControlToValidate="txtDBVersion"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblDBVersionView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--DisplayIndex--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Display Index
                                <asp:Label ID="lblDisplayIndexRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtDisplayIndex" runat="server" CssClass="ms-input"
                        MaxLength="50" />
                    <asp:RequiredFieldValidator ID="rfvDisplayIndex" runat="server"
                        ControlToValidate="txtDisplayIndex"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblDisplayIndexView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--Active--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Active
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody2">
                    <asp:CheckBox ID="cbActive" runat="server" CssClass="ms-input" />
                    <asp:Label ID="lblActiveView" runat="server" CssClass="ms-vb2" />
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
                                <asp:Button ID="btnSave" runat="server"
                                    CssClass="ms-ButtonHeightWidth" Text="Save" />
                            </td>
                            <td style="padding-left: 5px;" class="ms-toolbar">
                                <asp:Button ID="btnCancel" runat="server"
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
    <%= GetModulesURL(true) %>
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

