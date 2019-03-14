<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationTree.ascx.cs" Inherits="MAPPS.CONTROLTEMPLATES.NavigationTree" %>
<table>
    <tr>
        <td>
            <table class="mapps-usercontrol-title-table" style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="mapps-usercontrol-title-table-cell-both" style="text-align: left; background-color: rgb(136, 117, 52); padding-top: 2px; padding-bottom: 4px; padding-left: 15px; padding-right: 15px; border-top-right-radius: 4px; border-top-left-radius: 4px;">
                        <asp:HyperLink ID="lnkTitle" runat="server" Text="Navigation" ForeColor="white" Font-Size="13px"></asp:HyperLink></td>
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
                            <ul id="mappsmenu" class="navtree"></ul>
                        </div>
                    </div>
                </div>
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/jquery.json-2.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/json2.js"></script>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/jquery-cookie.js"></script>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/jquery-treeview-1.4.0.min.js"></script>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/jquery-treeview-async-0.1.0.js"></script>
<script type="text/javascript" src="/_layouts/15/mapps/scripts/mappsmenu.js"></script>
<link href="/_layouts/15/mapps/styles/mappsmenu.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#mappsmenu').treeview({
            url: "/_layouts/15/mapps/webservices/data.asmx/Nodes?menuid=1",
            expandMode: "simple",
            unique: true,
            animated: "fast"
        })
    });
</script>
<asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>

