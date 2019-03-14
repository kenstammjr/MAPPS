<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Modules.ascx.cs" Inherits="MAPPS.CONTROLTEMPLATES.Modules" %>
<table class="mapps-usercontrol-title-table" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td colspan="2">
            <SharePoint:SPGridView ID="gvData" runat="server" AutoGenerateColumns="False"
                AllowSorting="False"
                Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                AllowPaging="False" BackColor="White" OnRowCommand="gvData_RowCommand">
                <EmptyDataRowStyle CssClass="ms-vb" />
                <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                <AlternatingRowStyle CssClass="ms-alternating" />
                <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Position="Bottom" />
                <Columns>
                    <%-- Image --%>
                    <asp:TemplateField HeaderText="" Visible="True">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnSelect" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.URL") %>'
                                CommandName="Select" ToolTip="Select option" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.ImageURL") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left" />
                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                    </asp:TemplateField>
                    <%-- Name/Description --%>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlName" runat="server" NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.URL") %>'
                                Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>' /><br />
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left-none" Wrap="false" />
                        <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Wrap="false" />
                    </asp:TemplateField>
                 </Columns>
            </SharePoint:SPGridView>
        </td>
    </tr>
</table>
<asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>
