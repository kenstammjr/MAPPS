<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBUtility.aspx.cs" Inherits="MAPPS.Pages.DBUtility" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(
            function (sender, args) {
                $get("<%=updPnlMain.ClientID %>").style.display = "none";
            }
        );
            prm.add_endRequest(
                function (sender, args) {
                    $get("<%=updPnlMain.ClientID %>").style.display = "";
            }
        );
    </script>
    <asp:UpdateProgress ID="updPrgMain" AssociatedUpdatePanelID="updPnlMain" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <%= PROCESSING_TEMPLATE %>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updPnlMain" runat="server">
        <ContentTemplate>
            <table id="tblFormContent" runat="server" border="0" cellpadding="0" cellspacing="0" width="400px" style="padding: 15px 15px 15px 15px;">
                <tr>
                    <td class="mapps-grid-top-full" style="text-align: left;">
                        <asp:HyperLink ID="lnkTitle" runat="server" Text="Database Utility" ForeColor="White"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td>
                        <table class="ms-formtable" border="0" cellpadding="0" cellspacing="0" width="100%" style="border-bottom: 1px solid #d8d8d8;">
                            <%--Database Status--%>
                            <tr>
                                <td class="ms-formlabel" width="190px" valign="top">
                                    <h3 class="ms-standardheader">Database Status</h3>
                                </td>
                                <td class="ms-formbody" valign="top">
                                    <asp:Label ID="lblDBStatus" runat="server" />
                                    <asp:Label ID="lblDBStatusMsg" runat="server" />
                                </td>
                            </tr>
                            <%--Database Version--%>
                            <tr>
                                <td class="ms-formlabel" width="190px" valign="top">
                                    <h3 class="ms-standardheader">Database Version</h3>
                                </td>
                                <td class="ms-formbody" valign="top">
                                    <asp:Label ID="lblDBVersion" runat="server" />&nbsp;
                                    <asp:Label ID="lblDBVersionMsg" runat="server" Text="Update Available" CssClass="ms-formvalidation"
                                        Visible="false" />
                                </td>
                            </tr>
                            <%--Last Database Update--%>
                            <tr>
                                <td class="ms-formlabel" width="190px" valign="top">
                                    <h3 class="ms-standardheader">Last Database Update</h3>
                                </td>
                                <td class="ms-formbody" valign="top">
                                    <asp:Label ID="lblDBUpdated" runat="server" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblBottomButtons" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 30px;">
                            <tr>
                                <td class="ms-input" style="text-align: right; vertical-align: text-bottom;">
                                    <asp:Button ID="btnRunConfigurationUtility" runat="server" Text="Create/Update" class="ms-ButtonHeightWidth" OnClick="btnRunConfigurationUtility_Click" />
                                    <asp:Button ID="btnFormCancel" runat="server" Text="Cancel" class="ms-ButtonHeightWidth" OnClick="btnCloseBottom_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hfID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfView" runat="server" Value="" />
                        <asp:Label ID="lblErrorMessage" runat="server" Visible="False" CssClass="ms-formvalidation" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <%= PAGE_TITLE %>
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <%= GetHomeURL(true) %>
    &#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <%= GetAdminURL(true) %>
    &#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
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
