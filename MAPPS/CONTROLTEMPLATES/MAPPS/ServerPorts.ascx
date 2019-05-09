<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServerPorts.ascx.cs" Inherits="MAPPS.CONTROLTEMPLATES.ServerPorts" %>
<table id="tblList" runat="server" class="mapps-usercontrol-title-table" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td colspan="2">
            <SharePoint:SPGridView ID="gvData" runat="server" AutoGenerateColumns="False"
                AllowSorting="False"
                Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                AllowPaging="False" BackColor="White" OnRowCommand="gvData_RowCommand">
                <EmptyDataRowStyle CssClass="ms-vb" Wrap="false" />
                <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                <AlternatingRowStyle CssClass="ms-alternating" />
                <PagerStyle CssClass="ms-pagebreadcrumb" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Position="Bottom" />
                <Columns>
                    <%-- Edit --%>
                    <asp:TemplateField HeaderText="" Visible="True">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                CommandName="EditItem" ToolTip="Edit" ImageUrl="~/_layouts/Images/EditItem.gif" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left" />
                        <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Width="5px" />
                    </asp:TemplateField>
                    <%-- Port --%>
                    <asp:TemplateField HeaderText="Port">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnName" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                CommandName="ViewItem" Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left-none" Wrap="false" />
                        <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" />
                    </asp:TemplateField>
                    <%-- Protocol --%>
                    <asp:TemplateField HeaderText="Protocol">
                        <ItemTemplate>
                            <asp:Label ID="lblSize" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Protocol") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left" Wrap="false" />
                        <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" />
                    </asp:TemplateField>
                    <%-- Description --%>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="mapps-grid-header-left" Wrap="false" />
                        <ItemStyle CssClass="mapps-grid-text" HorizontalAlign="Left" Wrap="false" />
                    </asp:TemplateField>

                </Columns>
            </SharePoint:SPGridView>
        </td>
    </tr>
</table>
<table id="tblItem" runat="server" visible="false" class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0" width="50%">
    <%--Port--%>
    <tr>
        <td nowrap="true" valign="top" width="10px" class="ms-formlabel">
            <h3 class="ms-standardheader">Port
                        <asp:Label ID="lblPortRequired" runat="server" CssClass="ms-formvalidation" Text="*" ToolTip="This is a required field." />
            </h3>
        </td>
        <td valign="middle" width="350px" class="snapps-formbody">
            <asp:DropDownList ID="ddlPort" runat="server" CssClass="ms-long" />
            <asp:Label ID="lblPortView" runat="server" CssClass="mapps-item" />
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
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                            CssClass="ms-ButtonHeightWidth" Text="Delete" />
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
<input id="hfItemID" type="hidden" runat="server" />
<asp:Label ID="lblErrorMessage" runat="server" CssClass="ms-error"></asp:Label>