<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FichaPersonal.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Matriz.FichaPersonal" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ficha Personal</title>
    <link href="../Styles/Matriz.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ColorBoxAlt.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/MATRIZ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.window.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        $(document).ready(function () {

        });
        function CargarResumen(pathUrl) {

            $.fn.colorbox({ href: pathUrl, width: "800px", height: "600px", iframe: true, opacity: "0.8", open: true, close: "" });
        }
        function ficha(codigoUsuario, pais) {

            var parametros = { accion: 'fichaPersonal', codigoUsuario: codigoUsuario, pais: pais };

            var fichaPersonal = MATRIZ.Ajax(url, parametros, false);
            $("#lblNombre").append(fichaPersonal.NombresApellidos);
            $("#lblRol").append(fichaPersonal.Rol);
            $("#lblCorreo").append(fichaPersonal.CorreoElectronico);
            $("#lblCub").append(fichaPersonal.Cub);
            $("#lblCodigo").append(fichaPersonal.CodigoGerenteRegionoZona);
            $("#lblFechaNacimiento").append(fichaPersonal.FechaNacimiento);
            $("#lblEstadoCivil").append(fichaPersonal.EstadoCivil);
            $("#lblNumHijo").append(fichaPersonal.CantidadHijos);
            $("#lblDomicilio").append(fichaPersonal.Domicilio);
            $("#lblTelefono").append(fichaPersonal.TelefonoFijo);
            $("#lblCelular").append(fichaPersonal.CorreoElectronico);
            $("#lblFormacion").append(fichaPersonal.Formacion);

            if (fichaPersonal.CentroEstudios != null)
                $("#lblCentroEstudios").append(fichaPersonal.CentroEstudios.CentroDeEstudios);

            $("#lblExperiencia").append(fichaPersonal.ExperienciaProfesional);
            $("#lblFechaIngresoCIA").append(fichaPersonal.FechaIngreso);
            $("#lblRegion").append(fichaPersonal.DescripcionRegionoZona);
            $("#lblFechaAsigcnacion").append(fichaPersonal.FechaAsignacionActual);
            $("#lblJefe").append(fichaPersonal.NombreJefe);

            if (fichaPersonal.PersonaCargo != null)
                $("#lblPersonaCargo").append(fichaPersonal.PersonaCargo.PersonasACargo);

            if (fichaPersonal.PuestosOcupados != null)
                $("#lblPosiciones").append(fichaPersonal.PuestosOcupados.PosicionAnterior);

            $("#lblJefesAnteriores").append(""); //No encontrado
            $("#lblReconocimientos").append(""); //No encontrado
            $("#lblPruebas").append(""); //No encontrado
            $("#lblEvaluaciones").append(""); //No encontrado
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <p class="labelTituloPrincipal" style="margin: 0px">
                    <span id="lblNombre"></span><span class="labelTituloPlomo" id="lblRol"><span></span>
                    </span>
                </p>
                <div>
                    <label class="labelInfo">
                        <span id="lblCorreo"></span>
                    </label>
                </div>
                <div>
                    <label class="labelInfo">
                        <span id="lblCub"></span>
                    </label>
                    <label class="labelTitulo">
                        <span id="lblCodigo"></span>
                    </label>
                </div>
            </div>
            <div style="margin-top: 10px">
                <ul class="tabnav" style="clear: both">
                    <li>
                        <asp:LinkButton runat="server" ID="btnDatosPersonales" Text="DATOS PERSONALES" CssClass="current"
                            OnClick="btnDatosPersonales_Click" /></li>
                    <li>
                        <asp:LinkButton runat="server" ID="btnDatosBelcorp" Text="DATOS BELCORP" OnClick="btnDatosBelcorp_Click" /></li>
                    <li>
                        <asp:LinkButton runat="server" ID="btnOtrosDatos" Text="OTROS DATOS" OnClick="btnOtrosDatos_Click" /></li>
                    <li class="derecha">
                        <asp:HyperLink ID="hlResumen" runat="server">  <img src="<%=Utils.RelativeWebRoot%>Styles/Matriz/btnverultimodialogo.png"
                        class="lanzar" style="border-width: 0px; cursor: pointer" /></asp:HyperLink>
                    </li>
                </ul>
            </div>
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <table class="tablaFichaPersonal">
                        <tr>
                            <td class="labelDescripcion" style="width: 180px">FECHA DE NACIMIENTO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblFechaNacimiento"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">ESTADO CIVIL:
                            </td>
                            <td class="labelInfo">
                                <span id="lblEstadoCivil"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">HIJOS:
                            </td>
                            <td class="labelInfo">
                                <span id="lblNumHijo"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">DOMICILIO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblDomicilio"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">TELÉFONO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblTelefono"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">CELULAR:
                            </td>
                            <td class="labelInfo">
                                <span id="lblCelular"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">FORMACIÓN:
                            </td>
                            <td class="labelInfo">
                                <span id="lblFormacion"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">CENTROS DE ESTUDIOS:
                            </td>
                            <td class="labelInfo">
                                <span id="lblCentroEstudios"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">EXPERIENCIA PROFESIONAL:
                            </td>
                            <td class="labelInfo">
                                <span id="lblExperiencia"></span>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table class="tablaFichaPersonal">
                        <tr>
                            <td class="labelDescripcion" style="width: 180px">FECHA DE INGRESO A CIA:
                            </td>
                            <td class="labelInfo">
                                <span id="lblFechaIngresoCIA"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">REGIÓN A CARGO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblRegion"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">FECHA DE ASIGNACIÓN CARGO ACTUAL:
                            </td>
                            <td class="labelInfo">
                                <span id="lblFechaAsigcnacion"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">JEFE DIRECTO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblJefe"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">PERSONAS A CARGO:
                            </td>
                            <td class="labelInfo">
                                <span id="lblPersonaCargo"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">POSICIONES ANTERIORES EN BELCORP:
                            </td>
                            <td class="labelInfo">
                                <span id="lblPosiciones"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">JEFES DIRECTOS ANTERIORES:
                            </td>
                            <td class="labelInfo">
                                <span id="lblJefesAnteriores"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">RECONOCIMIENTOS:
                            </td>
                            <td class="labelInfo">
                                <span id="lblReconocimientos"></span>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <table class="tablaFichaPersonal">
                        <tr>
                            <td class="labelDescripcion" style="width: 180px">PRUEBAS DE INGRESO
                            </td>
                            <td class="labelInfo">
                                <span id="lblPruebas"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelDescripcion">EVALUACIONES
                            </td>
                            <td class="labelInfo">
                                <span id="lblEvaluaciones"></span>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
