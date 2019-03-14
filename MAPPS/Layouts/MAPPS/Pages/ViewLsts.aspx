<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewLsts.aspx.cs" Inherits="MAPPS.Pages.ViewLsts" MasterPageFile="/_layouts/15/mapps/masterpages/app.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr class="ms-vl-sectionHeaderRow">
            <td style="border-bottom: solid; border-bottom-color: gray; border-bottom-width: thin;">
                <span class="ms-vl-sectionHeader">
                    <h2 class="ms-webpart-titleText">
                        <span>Lists, Libraries, and other Apps
			</span><span class="ms-textXLarge ms-vl-appnewsubsitelink" id="newapp" runat="server">
                            <a class="ms-heroCommandLink" id="addapp" href="/_layouts/15/addanapp.aspx">
                                <span class="ms-list-addnew-imgSpan20">
                                    <img class="ms-list-addnew-img20" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,allapps_clickToCreateSite%>' EncodeMethod='HtmlEncode'/>" src="/_layouts/15/images/spcommon.png?rev=23">
                                </span></a>
                        </span>
                    </h2>
                </span>

            </td>
            <td class="ms-alignRight" style="text-wrap: none; border-bottom: solid; border-bottom-color: gray; border-bottom-width: thin;"><span class="ms-splinkbutton-text">Type</span>&nbsp;<asp:DropDownList ID="ddlBaseType" runat="server" CssClass="ms-vb" AutoPostBack="true" OnSelectedIndexChanged="ddlBaseType_SelectedIndexChanged">
                <asp:ListItem Text="Any" Value="Any" />
                <asp:ListItem Text="Discussion Board" Value="DiscussionBoard" />
                <asp:ListItem Text="Document Library" Value="DocumentLibrary" />
                <asp:ListItem Text="Generic List" Value="GenericList" />
                <asp:ListItem Text="Issue" Value="Issue" />
                <asp:ListItem Text="Survey" Value="Survey" />
                <asp:ListItem Text="UnSpecified BaseType" Value="UnSpecifiedBaseType" />
                <asp:ListItem Text="Unused" Value="Unused" />
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                <a class="ms-calloutLink ms-vl-alignactionsmiddle" id="ctl00_PlaceHolderMain_diidIOSiteWorkflows" accesskey="w" href="/_layouts/workflow.aspx" showimageandtext="true"><span style="width: 16px; height: 16px; overflow: hidden; display: inline-block; position: relative;">
                    <img style="border-width: 0px; left: 0px !important; top: -629px !important; position: absolute;" src="/_layouts/15/images/fgimg.png?rev=23">
                </span>&nbsp;<span class="ms-splinkbutton-text">Site Workflows</span></a>
                <a class="ms-calloutLink ms-vl-settingsmarginleft ms-vl-alignactionsmiddle" id="ctl00_PlaceHolderMain_diidSiteSettings" accesskey="s" href="/_layouts/settings.aspx" showimageandtext="true"><span style="width: 15px; height: 14px; overflow: hidden; display: inline-block; position: relative;">
                    <img style="border-width: 0px; left: -179px !important; top: -114px !important; position: absolute;" src="/_layouts/15/images/spcommon.png?rev=23">
                </span>&nbsp;<span class="ms-splinkbutton-text">Settings</span></a>
                <a class="ms-calloutLink ms-vl-settingsmarginleft ms-vl-alignactionsmiddle" id="ctl00_PlaceHolderMain_diidRecycleBin" href="/_layouts/RecycleBin.aspx" showimageandtext="true"><span style="width: 16px; height: 16px; overflow: hidden; display: inline-block; position: relative;">
                    <img style="border-width: 0px; left: -196px !important; top: -155px !important; position: absolute;" src="/_layouts/15/images/spcommon.png?rev=23">
                </span>&nbsp;<span class="ms-splinkbutton-text">Recycle Bin
                    <asp:Label ID="lblRecycleBinCount" runat="server" Text="0"></asp:Label></span></a>

            </td>
        </tr>

        <tr>
            <td colspan="3">
                <SharePoint:SPGridView ID="gvData" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
                    Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                    AllowPaging="false" EmptyDataText="There are no items of the selected type.">
                    <EmptyDataRowStyle CssClass="ms-vb" />
                    <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                    <AlternatingRowStyle CssClass="ms-alternating" />
                    <Columns>
                        <%-- Image --%>
                        <asp:TemplateField HeaderText="" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnImage" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DefaultViewURL") %>'
                                    CommandName="Redirect" ToolTip="Item" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.ImageURL") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                        </asp:TemplateField>
                        <%-- Title --%>
                        <asp:TemplateField HeaderText="Title" SortExpression="Title">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnTitle" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DefaultViewURL") %>'
                                    CommandName="Redirect" Text='<%# DataBinder.Eval(Container,"DataItem.Title") %>' />
                                <asp:Label ID="lblNew" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CreatedOn") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Wrap="false" />
                        </asp:TemplateField>
                        <%-- Description --%>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- ItemCount --%>
                        <asp:TemplateField HeaderText="Item Count">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCount" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ItemCount") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- BaseType --%>
                        <asp:TemplateField HeaderText="BaseType">
                            <ItemTemplate>
                                <asp:Label ID="lblBaseType" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.BaseType") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- EntityTypeName --%>
                        <asp:TemplateField HeaderText="EntityTypeName">
                            <ItemTemplate>
                                <asp:Label ID="lblEntityTypeName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EntityTypeName") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- Temp --%>
                        <asp:TemplateField HeaderText="Temp">
                            <ItemTemplate>
                                <asp:Label ID="lblTemp" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Temp") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <%-- LastModified --%>
                        <asp:TemplateField HeaderText="Last Modified (local)">
                            <ItemTemplate>
                                <asp:Label ID="lblLastModified" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LastModified") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Wrap="false" />
                        </asp:TemplateField>
                        <%-- ID - Hidden --%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Visible="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </SharePoint:SPGridView>
            </td>
        </tr>
        <tr class="ms-vl-sectionHeaderRow">
            <td colspan="2" style="border-bottom: solid; border-bottom-color: gray; border-bottom-width: thin;">
                <span class="ms-vl-sectionHeader" style="margin-top: 42px;">
                    <h2 class="ms-webpart-titleText">
                        <span>Subsites
			</span><span class="ms-textXLarge ms-vl-appnewsubsitelink" id="newSubsite" runat="server">
                            <a class="ms-heroCommandLink" id="createnewsite" href="/_layouts/15/newsbweb.aspx">
                                <span class="ms-list-addnew-imgSpan20">
                                    <img class="ms-list-addnew-img20" alt="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,allapps_clickToCreateSite%>' EncodeMethod='HtmlEncode'/>" src="/_layouts/15/images/spcommon.png?rev=23">
                                </span></a>
                        </span>
                    </h2>
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <SharePoint:SPGridView ID="gvSubsites" runat="server" AutoGenerateColumns="False"
                    OnRowCommand="gvSubsites_RowCommand"
                    Width="100%" BorderStyle="None" BorderWidth="0px" GridLines="None"
                    AllowPaging="false" EmptyDataText="This site does not have any subsites.">
                    <EmptyDataRowStyle CssClass="ms-vb" />
                    <HeaderStyle CssClass="ms-viewheadertr" Height="22px" />
                    <AlternatingRowStyle CssClass="ms-alternating" />
                    <Columns>
                        <%-- Image --%>
                        <asp:TemplateField HeaderText="" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnImage" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.URL") %>'
                                    CommandName="Redirect" ToolTip="Site" ImageUrl="/_layouts/15/images/sharepointfoundation16.png" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Width="5px" />
                        </asp:TemplateField>
                        <%-- Title --%>
                        <asp:TemplateField HeaderText="Title" SortExpression="Title">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.URL") %>'
                                    CommandName="Redirect" Text='<%# DataBinder.Eval(Container,"DataItem.Title") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" Wrap="false" Width="100px" />
                        </asp:TemplateField>
                        <%-- Description --%>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- ID - Hidden --%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Visible="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="ms-vh2" />
                            <ItemStyle CssClass="ms-vb2" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </SharePoint:SPGridView>
            </td>
        </tr>
        <tr class="ms-vl-sectionHeaderRow">
            <td colspan="2" style="border-bottom: solid; border-bottom-color: gray; border-bottom-width: thin;">
                <span class="ms-vl-sectionHeader" style="margin-top: 42px;">
                    <h2 class="ms-webpart-titleText">
                        <span>User Permissions
			</span>
                    </h2>
                </span>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Site Contents
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <asp:Label ID="lblPageTitleInTitle" runat="server" Text="Site Contents"></asp:Label>
</asp:Content>

