<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>


<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuNodeItem.aspx.cs" Inherits="MAPPS.Pages.MenuNodeItem" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <table border="0" cellpadding="5" cellspacing="5" style="width: 100%; border: 1px solid #e7e7e8;">
                    <tr>
                        <td id="tdView" runat="server" class="mapps-ribbon-cell" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonView" runat="server" CssClass="mapps-view-ribbon" OnClick="ibtnRibbonView_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonView" runat="server" Text="View" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonView_Click" CausesValidation="false"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td id="tdEdit" runat="server" class="mapps-ribbon-cell" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonEdit" runat="server" CssClass="mapps-edit-ribbon" OnClick="ibtnRibbonEdit_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonEdit" runat="server" Text="Edit" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonEdit_Click" CausesValidation="false"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td id="tdSave" runat="server" class="mapps-ribbon-cell" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px;">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonSave" runat="server" CssClass="mapps-save-ribbon" OnClick="ibtnRibbonSave_Click" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonSave" runat="server" Text="Save" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonSave_Click"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td id="tdDelete" runat="server" class="mapps-ribbon-cell" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonDelete" runat="server" CssClass="mapps-delete-ribbon" OnClick="ibtnRibbonDelete_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonDelete" runat="server" Text="Delete" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonDelete_Click" CausesValidation="false"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td id="tdCancel" runat="server" class="mapps-ribbon-cell">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonCancel" runat="server" CssClass="mapps-cancel-ribbon" OnClick="ibtnRibbonCancel_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonCancel" runat="server" Text="Cancel" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonCancel_Click" CausesValidation="false"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td width="99%" class="ms-toolbar">
                            <img width="1" height="18" alt=""
                                src="/_layouts/images/blank.gif" />
                        </td>
                    </tr>
                </table>
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
                                MaxLength="50" Width="450px" />
                            <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                ControlToValidate="txtName"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblNameView" runat="server" CssClass="mapps-item" />
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
                                MaxLength="150" Width="450px" />
                            <asp:Label ID="lblDescriptionView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--Parent Node--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Parent Node
                        <asp:Label ID="lblParentNodeRequired" runat="server"
                            CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:DropDownList ID="ddlParentNode" runat="server" CausesValidation="false" />
                            <asp:Label ID="lblParentNodeView" runat="server" CssClass="mapps-item" />
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
                                MaxLength="255" Width="450px" />
                            <asp:RequiredFieldValidator ID="rfvURL" runat="server"
                                ControlToValidate="txtURL"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblURLView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--Target--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Target
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtTarget" runat="server" CssClass="ms-input"
                                MaxLength="50" Width="450px" />
                            <asp:Label ID="lblTargetView" runat="server" CssClass="mapps-item" />
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
                                MaxLength="3" Width="50px" />
                            <asp:RequiredFieldValidator ID="rfvDisplayIndex" runat="server"
                                ControlToValidate="txtDisplayIndex"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblDisplayIndexView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--IsVisible--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Visible
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:CheckBox ID="cbIsVisible" runat="server" CssClass="ms-input" />
                            <asp:Label ID="lblIsVisibleView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--AdminOnly--%>
<%--                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Admin Only
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:CheckBox ID="cbAdminOnly" runat="server" CssClass="ms-input" />
                            <asp:Label ID="lblAdminOnlyView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>--%>

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
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                                            CssClass="ms-ButtonHeightWidth" Text="Save" />
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
            </td>
        </tr>
    </table>
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

