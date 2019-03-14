﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettingItem.aspx.cs" Inherits="MAPPS.Pages.SettingItem" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width: 40%; float: left;">
        <table class="ms-formtable" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="mapps-grid-top-left" style="text-align: left;">
                    <asp:HyperLink ID="lnkTitle" runat="server" Text="Setting" ForeColor="White"></asp:HyperLink></td>
                <td class="mapps-grid-top-right" style="text-align: right;">
                    <asp:LinkButton ID="lbtnNew" runat="server" Text="New" CssClass="mapps-app-page-button" Visible="false" ></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="mapps-grid-header-left" style="text-align: left;">&nbsp;</td>
                <td class="mapps-grid-header-left-end" style="text-align: right;">
                    <asp:Label ID="lblReqMsg" runat="server" Text="<font color=red>*</font> Required Field" CssClass="ms-errorn"></asp:Label></td>
            </tr>
            <%--Key--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Key
                                <asp:Label ID="lblKeyRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtKey" runat="server" CssClass="ms-input"
                        MaxLength="50" Width="350px" />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                        ControlToValidate="txtKey"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblKeyView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--Value--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Value
                                <asp:Label ID="lblValueRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtValue" runat="server" CssClass="ms-input"
                        MaxLength="255" Width="500px" />
                    <asp:RequiredFieldValidator ID="rfvValue" runat="server"
                        ControlToValidate="txtValue"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblValueView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--Description--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Description
                                <asp:Label ID="lblDescriptionRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="ms-input"
                        MaxLength="255" Width="500px" />
                    <asp:RequiredFieldValidator ID="rfvDescriptionURL" runat="server"
                        ControlToValidate="txtDescription"
                        ErrorMessage="<br/>You can't leave this blank."
                        Display="Dynamic"
                        ForeColor="Red" />
                    <asp:Label ID="lblDescriptionView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--IsPassword--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Password
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody2">
                    <asp:CheckBox ID="cbPassword" runat="server" CssClass="ms-input" />
                    <asp:Label ID="lblPasswordView" runat="server" CssClass="ms-vb2" />
                </td>
            </tr>
            <%--IsMultiline--%>
            <tr>
                <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                    <h3 class="ms-standardheader">Multiline
                    </h3>
                </td>
                <td valign="middle" width="350px" class="mapps-formbody2">
                    <asp:CheckBox ID="cbMultiline" runat="server" CssClass="ms-input" />
                    <asp:Label ID="lblMultilineView" runat="server" CssClass="ms-vb2" />
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

