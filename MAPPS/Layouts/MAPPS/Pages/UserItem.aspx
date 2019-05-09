<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/UserRoles.ascx" TagPrefix="uc1" TagName="UserRoles" %>
<%@ Register Src="~/_controltemplates/15/MAPPS/NavigationTree.ascx" TagPrefix="uc1" TagName="NavigationTree" %>


<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserItem.aspx.cs" Inherits="MAPPS.Pages.UserItem" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<div style="width: 40%; float: left;">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <table border="0" cellpadding="5" cellspacing="5" style="width: 100%; border: 1px solid #e7e7e8;">
                    <tr>
                        <td id="tdNew" runat="server" class="mapps-ribbon-cell" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 32px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnRibbonNew" runat="server" CssClass="mapps-new-ribbon" OnClick="ibtnRibbonNew_Click" CausesValidation="false" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnRibbonNew" runat="server" Text="New" CssClass="mapps-ribbon-link" OnClick="lbtnRibbonNew_Click" CausesValidation="false"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
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
                    <%--Lookup--%>
                    <tr id="trLookup" runat="server" visible="true">
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">User Lookup
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <SharePoint:PeopleEditor runat="server" ID="spePickUser" AutoPostBack="true"
                                MultiSelect="false" ShowEntityDisplayTextInTextBox="true" AllowEmpty="true" SelectionSet="User"
                                BorderWidth="0" Rows="1" BrowseButtonToolTip="Select name from authorized users" DoPostBackOnResolve="true" />
                        </td>
                    </tr>
                    <%--UserName--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">User Name
                                <asp:Label ID="lblUserNameRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="ms-input"
                                MaxLength="150" Width="300px" />
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                ControlToValidate="txtUserName"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblUserNameView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--Email--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Email
                                <asp:Label ID="lblEmailRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="ms-input"
                                MaxLength="150" Width="300px" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblEmailView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>

                    <%--LastName--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Last Name
                                <asp:Label ID="lblLastNameRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="ms-input"
                                MaxLength="150" Width="300px" />
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                                ControlToValidate="txtLastName"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblLastNameView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--FirstName--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">FirstName
                                <asp:Label ID="lblFirstNameRequired" runat="server"
                                    CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="ms-input"
                                MaxLength="150" Width="300px" />
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                                ControlToValidate="txtFirstName"
                                ErrorMessage="<br/>You can't leave this blank."
                                Display="Dynamic"
                                ForeColor="Red" />
                            <asp:Label ID="lblFirstNameView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--MiddleInitial--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Middle Initial
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="ms-input"
                                MaxLength="50" Width="75px" />
                            <asp:Label ID="lblMiddleInitialView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--GenerationalQualifier--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Generational Qualifier
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtGenerationalQualifier" runat="server" CssClass="ms-input"
                                MaxLength="50" Width="75px" />
                            <asp:Label ID="lblGenerationalQualifierView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--PreferredName--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Preferred Name
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:TextBox ID="txtPreferredName" runat="server" CssClass="ms-input"
                                MaxLength="50" Width="250px" />
                            <asp:Label ID="lblPreferredNameView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>

                    <%--SeniorStaff--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Senior Staff
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:CheckBox ID="ckbxSeniorStaff" runat="server" />
                            <asp:Label ID="lblSeniorStaffView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--ITAdmin--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">IT Admin
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:CheckBox ID="ckbxITAdmin" runat="server" />
                            <asp:Label ID="lblITAdminView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--Roles--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">Roles
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <uc1:UserRoles runat="server" id="UserRoles" />
                        </td>
                    </tr>
                    <%--ADObjectGuid--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">ADObjectGuid
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:Label ID="lblADObjectGuidView" runat="server" CssClass="mapps-item" />
                        </td>
                    </tr>
                    <%--SPObjectGuid--%>
                    <tr>
                        <td nowrap="true" valign="top" width="113px" class="ms-formlabel">
                            <h3 class="ms-standardheader">SPObjectGuid
                            </h3>
                        </td>
                        <td valign="middle" width="350px" class="mapps-formbody">
                            <asp:Label ID="lblSPObjectGuidView" runat="server" CssClass="mapps-item" />
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
    </div>
    <div style="clear: both;"></div>
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
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
    <uc1:NavigationTree runat="server" id="NavigationTree" />
    <asp:PlaceHolder ID="PlaceHolderLeftNavBar" runat="server"></asp:PlaceHolder>
</asp:Content>

