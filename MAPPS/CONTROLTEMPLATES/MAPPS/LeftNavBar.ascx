<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNavBar.ascx.cs" Inherits="MAPPS.LeftNavBar" %>

<asp:Panel ID="pnlNav" runat="server" CssClass="MAPPSNav ms-quickLaunch">
    <asp:ListView ID="lvNav" runat="server">
        <layouttemplate>
            <ul class="s4-specialNavLinkList">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </ul>
        </layouttemplate>
        <itemtemplate>
            <li class="<%# DataBinder.Eval(Container,"DataItem.CssClass") %>">
                <a href="<%# DataBinder.Eval(Container,"DataItem.URL") %>" style="color: #262626;">
                    <span style="position: relative; width: 16px; display: inline-block; height: 16px; overflow: hidden" class="s4-clust s4-specialNavIcon">
                        <img style="position: absolute; border: 0;" src="<%# DataBinder.Eval(Container,"DataItem.Image") %>" />
                    </span>
                    <span class="ms-splinkbutton-text">
                        <%# DataBinder.Eval(Container,"DataItem.Name") %>
                    </span>
                </a>
            </li>
        </itemtemplate>
    </asp:ListView>
</asp:Panel>
<asp:Panel ID="pnlNavUser" runat="server" CssClass="MAPPSNavUser ms-quickLaunch">
    <asp:ListView ID="lvNavUser" runat="server">
        <layouttemplate>
            <ul class="s4-specialNavLinkList">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </ul>
        </layouttemplate>
        <itemtemplate>
            <li class="<%# DataBinder.Eval(Container,"DataItem.CssClass") %>">
                <a href="<%# DataBinder.Eval(Container,"DataItem.URL") %>" style="color: #262626;">
                    <span style="position: relative; width: 16px; display: inline-block; height: 16px; overflow: hidden" class="s4-clust s4-specialNavIcon">
                        <img style="position: absolute; border: 0;" src="<%# DataBinder.Eval(Container,"DataItem.Image") %>" />
                    </span>
                    <span class="ms-splinkbutton-text">
                        <%# DataBinder.Eval(Container,"DataItem.Name") %>
                    </span>
                </a>
            </li>
        </itemtemplate>
    </asp:ListView>
</asp:Panel>
<asp:Panel ID="pnlNavLinks" runat="server" CssClass="MAPPSNavUser ms-quickLaunch">
    <asp:ListView ID="lvNavLinks" runat="server">
        <LayoutTemplate>
            <ul class="s4-specialNavLinkList">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="<%# DataBinder.Eval(Container,"DataItem.CssClass") %>">

                <a href="<%# DataBinder.Eval(Container,"DataItem.URL") %>" title="<%# DataBinder.Eval(Container,"DataItem.Description") %>">
                    <span style="position: relative; width: 16px; display: inline-block; height: 16px; overflow: hidden" class="s4-clust s4-specialNavIcon">
                        <img style="position: absolute; border: 0;" src="/_layouts/images/LINKTOPAGE.gif" />
                    </span>
                    <span class="ms-splinkbutton-text">
                        <%# DataBinder.Eval(Container,"DataItem.Name") %>
                    </span>
                </a>
            </li>
        </ItemTemplate>
    </asp:ListView>
    <table cellspacing="0" class="ms-splitbutton" style="width: 100%; margin-top: 5px; visibility: hidden;" >
        <tr>
            <td valign="middle" class="ms-splitbuttontext" style="padding-left: 10px;">
                <asp:ImageButton CssClass="ms-splinkbutton-text" ImageUrl="/_layouts/Images/MAPPS/AddIcon_10x10.png"
                    runat="server" ID="ibtnNewItem" Height="10" Width="10" Visible="False" />
                <asp:LinkButton CssClass="ms-splinkbutton-text" ID="lbtnNewItem" runat="server" Text="Add new link" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Panel>