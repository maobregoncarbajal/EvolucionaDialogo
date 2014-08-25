<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="Filer.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Ftp.Filer" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Admin/Ftp/CSS/Default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="smPrimary" runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mvFiler" runat="server">
        <%-- PRIMARY VIEW --%>
        <asp:View ID="vView" runat="server">
            <asp:UpdatePanel ID="upPrimary" runat="server">
                <ContentTemplate>
                    <div id="buttons" style="text-align: left; padding-left: 100px">
                        <br />
                        <asp:Label ID="lblRoot" runat="server" CssClass="title" />
                        <br />
                        <asp:Button ID="btnReturn" runat="server" CausesValidation="false" CssClass="buttonSml" Text="Volver" OnClick="btnReturn_Click" Visible="false" />
                        <asp:Button ID="btnHome" runat="server" CausesValidation="false" CssClass="buttonSml" Text="Inicio" OnClick="btnHome_Click" Visible="false" />
                        <asp:Button ID="btnNewFile" runat="server" CausesValidation="false" CssClass="buttonSml" Text="Nuevo archivo" OnClick="btnNewFile_Click" />
                        <asp:Button ID="btnNewFolder" runat="server" CausesValidation="false" CssClass="buttonSml" Text="Nueva carpeta" OnClick="btnNewFolder_Click" />
                        <asp:Button ID="btnUpload" runat="server" CausesValidation="false" CssClass="buttonSml" Text="Subir archivo" OnClick="btnUpload_Click" />
                    </div>
                    <div style="padding-left: 100px">
                        <asp:PlaceHolder ID="phDisplay" runat="server" />
                    </div>
                    <br />
                    <div style="text-align: left; padding-left: 100px">
                        <br />
                        <asp:Button ID="btnProcesar" runat="server" CausesValidation="false"
                            CssClass="buttonSml" Text="Pre Cargar la información de los archivos al Sistema"
                            OnClick="btnProcesar_Click" />
                        <br />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>

        <%-- RENAME --%>
        <asp:View ID="vRename" runat="server">
            <asp:Panel ID="pnlRename" runat="server" DefaultButton="btnRenameSave">
                <table style="border: 1px solid #909070;" cellpadding="5" cellspacing="0">
                    <thead>
                        <tr>
                            <td colspan="2" class="header">
                                <asp:Label ID="lblRename" runat="server" /></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvRename" runat="server" ErrorMessage="El nuevo nombre no debe estar vacío" ControlToValidate="txtRename" SetFocusOnError="True">*&nbsp;</asp:RequiredFieldValidator>Introduzca el nuevo nombre:</td>
                            <td>
                                <asp:TextBox ID="txtRename" runat="server" Columns="35" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnRenameCancel" runat="server" CausesValidation="false" CssClass="button" Text="Cancelar" OnClick="Cancel" UseSubmitBehavior="false" />
                                <asp:Button ID="btnRenameSave" runat="server" CssClass="button" Text="Guardar" OnClick="btnRenameSave_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:Panel>
        </asp:View>
        <%-- NEW FILE --%>
        <asp:View ID="vNewFile" runat="server">
            <asp:Panel ID="pnlNewFile" runat="server" DefaultButton="btnNewFileSave">
                <table style="border: 1px solid #909070;" cellpadding="5" cellspacing="0">
                    <thead>
                        <tr>
                            <td colspan="2" class="header">Nuevo archivo</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNewFile" runat="server" ErrorMessage="El nuevo nombre del archivo no debe estar vacío" ControlToValidate="txtNewFile" SetFocusOnError="True">*&nbsp;</asp:RequiredFieldValidator>Introduzca el nuevo nombre de archivo:</td>
                            <td>
                                <asp:TextBox ID="txtNewFile" runat="server" Columns="35" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnNewFileCancel" runat="server" CausesValidation="false" CssClass="button" Text="Cancelar" OnClick="Cancel" UseSubmitBehavior="false" />
                                <asp:Button ID="btnNewFileSave" runat="server" CssClass="button" Text="Guardar" OnClick="btnNewFileSave_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:Panel>
        </asp:View>
        <%-- NEW FOLDER --%>
        <asp:View ID="vFolder" runat="server">
            <asp:Panel ID="pnlNewFolder" runat="server" DefaultButton="btnNewFolderSave">
                <table style="border: 1px solid #909070;" cellpadding="5" cellspacing="0">
                    <thead>
                        <tr>
                            <td colspan="2" class="header">Nueva carpeta</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNewFolder" runat="server" ErrorMessage="El nuevo nombre de la carpeta no debe estar vacío" ControlToValidate="txtNewFolder" SetFocusOnError="True">*&nbsp;</asp:RequiredFieldValidator>Introduzca el nuevo nombre de la carpeta:</td>
                            <td>
                                <asp:TextBox ID="txtNewFolder" runat="server" Columns="35" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnNewFolderCancel" runat="server" CausesValidation="false" CssClass="button" Text="Cancelar" OnClick="Cancel" UseSubmitBehavior="false" />
                                <asp:Button ID="btnNewFolderSave" runat="server" CssClass="button" Text="Guardar" OnClick="btnNewFolderSave_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:Panel>
        </asp:View>
        <%-- UPLOAD A FILE --%>
        <asp:View ID="vUpload" runat="server">
            <asp:Panel ID="pnlUpload" runat="server" DefaultButton="btnUploadSave">
                <table style="border: 1px solid #909070;" cellpadding="5" cellspacing="0">
                    <thead>
                        <tr>
                            <td class="header">Cargar un archivo</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ErrorMessage="Se requiere un nombre de archivo" ControlToValidate="fuUpload" SetFocusOnError="true">*&nbsp;</asp:RequiredFieldValidator>
                                <asp:FileUpload ID="fuUpload" runat="server" CssClass="button" Width="400px" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnUploadCancel" runat="server" CausesValidation="false" CssClass="button" Text="Cancelar" UseSubmitBehavior="false" OnClick="Cancel" />
                                <asp:Button ID="btnUploadSave" runat="server" CssClass="button" Text="Subir" OnClick="btnUploadSave_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:ValidationSummary ID="valSummary" runat="server" />
</asp:Content>
