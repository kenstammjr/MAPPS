<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServerFilters.ascx.cs" Inherits="MAPPS.CONTROLTEMPLATES.ServerFilters" %>
<table>
    <tr>
        <td>
            <table class="mapps-usercontrol-title-table" style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="mapps-usercontrol-title-table-cell-both" style="text-align: left; background-color: rgb(136, 117, 52); padding-top: 2px; padding-bottom: 4px; padding-left: 15px; padding-right: 15px; border-top-right-radius: 4px; border-top-left-radius: 4px;">
                        <asp:HyperLink ID="lnkTitle" runat="server" Text="Filters" ForeColor="white" Font-Size="13px"></asp:HyperLink></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div class="ms-quicklaunchouter">
                <div class="ms-quickLaunch">
                    <div style="padding-left: 5px; padding-right: 2px;">
                        <div style="padding-top: 5px; padding-bottom: 5px;">
                            Type<br />
                            <asp:DropDownList ID="ddlServerType" runat="server" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 5px;">
                            Function<br />
                            <asp:DropDownList ID="ddlServerFunction" runat="server" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 5px;">
                            Version<br />
                            <asp:DropDownList ID="ddlServerVersion" runat="server" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 5px;">
                            Status<br />
                            <asp:DropDownList ID="ddlServerStatus" runat="server" />
                        </div>
                        <hr />
                        <table>
                            <tr>
                                <td width="99%" class="ms-toolbar">
                                    <img width="1" height="18" alt=""
                                        src="/_layouts/images/blank.gif" />
                                </td>
                                <td style="padding-left: 5px;" class="ms-toolbar">
                                    <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click"
                                        CssClass="ms-ButtonHeightWidth" Text="Apply" />
                                </td>
                                <td style="padding-left: 5px;" class="ms-toolbar">
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click"
                                        CssClass="ms-ButtonHeightWidth" Text="Reset" />
                                </td>
                            </tr>
</table>
</div>
                </div>
            </div>
        </td>
    </tr>
</table>
