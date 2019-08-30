<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="TsystemsSearchTicket.View" %>

<div style="margin: 10px; padding: 5px;">
        <label><%=LocalizeString("NoTicket.Text")%></label>
        <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
        <asp:Button runat="server" ID="btnSearch" ValidationGroup="frmSearchTciket" Text="<%=LocalizeString("Search.Text")%>" OnClick="btnSearch_Click" />
        <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" ValidationGroup="frmSearchTciket"  runat="server" ControlToValidate="txtSearch" ErrorMessage="El número de ticket es requerido"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label runat="server" ID="StatusLabel" Text="" ForeColor="Blue" />
        <div runat="server" id="ShowContent" visible="false">
            <table style="margin-left: 20px; padding: 5px;">
                <tr>
                    <td>
                        <asp:Image ID="imgStatus" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                    </td>
                </tr>

            </table>
            <hr>
            <asp:Panel ID="myPanel" runat="server" GroupingText="ticket">
            <hr>
                <table style="margin-left: 20px; padding: 5px;">
                    <tr>
                        <td><strong><label><%=LocalizeString("Description.Text")%></label>:</strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><label><%=LocalizeString("Category.Text")%></label>:</strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><label><%=LocalizeString("AssignUserID.Text")%></label></strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblAssignUserID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><label><%=LocalizeString("OpenTime.Text")%></label>:</strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblOpenTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><label><%=LocalizeString("ResolveTime.Text")%></label>:</strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblResolveTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><label><%=LocalizeString("ResolutionDesc.Text")%></label>:</strong> 
                        </td>
                        <td>
                            <asp:Label ID="lblResolutionDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </div>
    </div>