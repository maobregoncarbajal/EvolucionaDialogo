namespace Evoluciona.Dialogo.Web.Helpers
{
    using BusinessLogic;
    using WsDirectorio;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Utilidades para el sitio.
    /// </summary>
    public static class Utils
    {
        #region URL handling

        private static string _relativeWebRoot;

        /// <summary>
        /// Retorna la ruta relativa al sitio
        /// </summary>
        public static string RelativeWebRoot
        {
            get { return _relativeWebRoot ?? (_relativeWebRoot = VirtualPathUtility.ToAbsolute("~/")); }
        }

        /// <summary>
        /// Retorna la ruta absoluta al sitio
        /// </summary>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    throw new WebException("El actual HttpContext es nulo");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }

        /// <summary>
        /// Convierte una url relativa a una absoluta.
        /// </summary>
        public static Uri ConvertToAbsolute(string relativeUri)
        {
            if (String.IsNullOrEmpty(relativeUri))
                throw new ArgumentNullException("relativeUri");

            var absolute = AbsoluteWebRoot.ToString();
            var index = absolute.LastIndexOf(RelativeWebRoot, StringComparison.Ordinal);

            return new Uri(absolute.Substring(0, index) + relativeUri);
        }

        #endregion URL handling

        #region Mensajes

        /// <summary>
        /// Muestra un mensaje alert
        /// </summary>
        /// <param name="clienteScript">ClientScript de la pagina</param>
        /// <param name="mensaje">Contenido del mensaje</param>
        public static void MensajeClientScriptAlert(ClientScriptManager clienteScript, string mensaje)
        {
            const string script = "mensaje";
            if (clienteScript.IsStartupScriptRegistered(script)) return;
            var msg = string.Format("alert('{0}');", mensaje);
            clienteScript.RegisterStartupScript(typeof(Page), script, msg, true);
        }

        #endregion Mensajes

        #region Query String

        /// <summary>
        /// Ontiene el valor de una query string  por su nombre.
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <returns>Valor de la Query string</returns>
        public static string QueryString(string name)
        {
            var result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[name] != null)
                result = HttpContext.Current.Request.QueryString[name];
            return result;
        }

        /// <summary>
        /// Ontiene el valor entero de una query string.
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <returns>valor de la query string</returns>
        public static int QueryStringInt(string name)
        {
            var resultStr = QueryString(name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Ontiene el valor entero de una query string.
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>valor de la query string</returns>
        public static int QueryStringInt(string name, int defaultValue)
        {
            var resultStr = QueryString(name).ToUpperInvariant();
            return resultStr.Length > 0 ? Int32.Parse(resultStr) : defaultValue;
        }

        /// <summary>
        /// Retorna la url de la pagina actual.
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrl(bool includeQueryString)
        {
            var url = string.Empty;
            if (HttpContext.Current == null)
                return url;

            if (includeQueryString)
            {
                var storeHost = AbsoluteWebRoot.ToString();
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                url = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            return url;
        }

        /// <summary>
        /// Modifica la query string
        /// </summary>
        /// <param name="url">Url a modificar</param>
        /// <param name="queryStringModification">Query string modification</param>
        /// <param name="targetLocationModification">Target location modification</param>
        /// <returns>Nueva url</returns>
        public static string ModifyQueryString(string url, string queryStringModification, string targetLocationModification)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (targetLocationModification == null)
                targetLocationModification = string.Empty;
            targetLocationModification = targetLocationModification.ToLowerInvariant();

            var str = string.Empty;
            var str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("#", StringComparison.Ordinal));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (var str3 in str.Split(new[] { '&' }))
                    {
                        if (string.IsNullOrEmpty(str3)) continue;
                        var strArray = str3.Split(new[] { '=' });
                        if (strArray.Length == 2)
                        {
                            dictionary[strArray[0]] = strArray[1];
                        }
                        else
                        {
                            dictionary[str3] = null;
                        }
                    }
                    foreach (var str4 in queryStringModification.Split(new[] { '&' }))
                    {
                        if (string.IsNullOrEmpty(str4)) continue;
                        var strArray2 = str4.Split(new[] { '=' });
                        if (strArray2.Length == 2)
                        {
                            dictionary[strArray2[0]] = strArray2[1];
                        }
                        else
                        {
                            dictionary[str4] = null;
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (var str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] == null) continue;
                        builder.Append("=");
                        builder.Append(dictionary[str5]);
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(targetLocationModification))
            {
                str2 = targetLocationModification;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        /// <summary>
        /// Remueve la query string de la url.
        /// </summary>
        /// <param name="url">Url a modificar</param>
        /// <param name="queryString">query string a remover</param>
        /// <returns>New url</returns>
        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();

            var str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            }
            if (string.IsNullOrEmpty(queryString)) return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
            if (string.IsNullOrEmpty(str)) return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
            var dictionary = new Dictionary<string, string>();
            foreach (var str3 in str.Split(new[] { '&' }))
            {
                if (string.IsNullOrEmpty(str3)) continue;
                var strArray = str3.Split(new[] { '=' });
                if (strArray.Length == 2)
                {
                    dictionary[strArray[0]] = strArray[1];
                }
                else
                {
                    dictionary[str3] = null;
                }
            }
            dictionary.Remove(queryString);

            var builder = new StringBuilder();
            foreach (var str5 in dictionary.Keys)
            {
                if (builder.Length > 0)
                {
                    builder.Append("&");
                }
                builder.Append(str5);
                if (dictionary[str5] == null) continue;
                builder.Append("=");
                builder.Append(dictionary[str5]);
            }
            str = builder.ToString();
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        #endregion Query String

        #region Métodos Render

        /// <summary>
        /// Renderiza page meta tags.
        /// </summary>
        /// <param name="page">Instancia de la pagina</param>
        /// <param name="name">Nombre de la meta.</param>
        /// <param name="content">Contenido</param>
        /// <param name="overwriteExisting">Sobreescribir, si existe.</param>
        public static void RenderMetaTag(Page page, string name, string content, bool overwriteExisting)
        {
            if (page == null || page.Header == null)
                return;

            foreach (Control control in page.Header.Controls)
            {
                if (!(control is HtmlMeta)) continue;
                var meta = control as HtmlMeta;
                if (!meta.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(content))
                    continue;
                if (overwriteExisting)
                    meta.Content = content;
                else
                {
                    if (string.IsNullOrEmpty(meta.Content))
                        meta.Content = content;
                }
            }
        }

        /// <summary>
        /// Extrae el titulo de la actual petición.
        /// </summary>
        /// <returns></returns>
        public static string ExtractTitle()
        {
            var root = GetThisPageUrl(false).Split('/');
            var name = (string.IsNullOrEmpty(root[root.GetUpperBound(0)])) ? root[root.GetUpperBound(0) - 1] : root[root.GetUpperBound(0)];
            root = name.Split('.');
            name = root[0];

            var title = (name.Substring(0, 1).ToUpperInvariant()) + name.Substring(1, name.Length - 1);
            return HttpContext.Current.Server.UrlEncode(title);
        }

        /// <summary>
        /// Agregamos una referencia del Stylesheet al head de la pagina
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url">url relativa</param>
        public static void RenderStylesheet(Page page, string url)
        {
            if (page == null || page.Header == null)
                return;

            foreach (Control item in page.Controls)
            {
                if (!(item is HtmlLink)) continue;
                var rssLink = item as HtmlLink;
                if (rssLink.Href.Equals(url, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            var link = new HtmlLink();
            link.Attributes["type"] = "text/css";
            link.Attributes["href"] = url;
            link.Attributes["rel"] = "stylesheet";
            page.Controls.Add(link);
        }

        /// <summary>
        /// Agrega una referencia del script a la pagina
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url">url relativa</param>
        /// <param name="placeInBottom">insertar el archivo al final</param>
        /// <param name="addDeferAttribute">atributo defer</param>
        public static void RenderJavaScript(Page page, string url, bool placeInBottom, bool addDeferAttribute)
        {
            if (page == null || page.Header == null)
                return;

            if (placeInBottom)
            {
                var script = "<script type=\"text/javascript\"" + (addDeferAttribute ? " defer=\"defer\"" : string.Empty) + " src=\"" + url + "\"></script>";
                page.ClientScript.RegisterStartupScript(page.GetType(), url.GetHashCode().ToString(CultureInfo.InvariantCulture), script);
            }
            else
            {
                foreach (Control item in page.Controls)
                {
                    if (!(item is HtmlGenericControl)) continue;
                    var rssLink = item as HtmlGenericControl;
                    if (rssLink.Attributes["src"].Equals(url, StringComparison.OrdinalIgnoreCase))
                        return;
                }

                var script = new HtmlGenericControl("script");
                script.Attributes["type"] = "text/javascript";
                script.Attributes["src"] = url;
                if (addDeferAttribute)
                {
                    script.Attributes["defer"] = "defer";
                }

                page.Controls.Add(script);
            }
        }

        /// <summary>
        /// Agrega la meta content-type a la cabecera.
        /// </summary>
        public static void RenderMetaContentType(Page page)
        {
            if (page == null || page.Header == null)
                return;

            var meta = new HtmlMeta
            {
                HttpEquiv = "content-type",
                Content = page.Response.ContentType + "; charset=" + page.Response.ContentEncoding.HeaderName
            };
            page.Header.Controls.Add(meta);
        }

        /// <summary>
        /// Establece el idioma predeterminado de las hojas de estilo
        /// y javascripts para el documento html.
        /// </summary>
        public static void RenderDefaultLanguages(Page page)
        {
            if (page == null || page.Header == null)
                return;

            page.Response.AppendHeader("Content-Style-Type", "text/css");
            page.Response.AppendHeader("Content-Script-Type", "text/javascript");
        }

        /// <summary>
        /// Agrega links genericos a la cabecera.
        /// </summary>
        public static void RenderGenericLink(Page page, string relation, string title, string href)
        {
            if (page == null || page.Header == null)
                return;

            var link = new HtmlLink();
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = href;
            page.Header.Controls.Add(link);
        }

        /// <summary>
        /// Agrega links genericos a la cabecera.
        /// </summary>
        public static void RenderGenericLink(Page page, string type, string relation, string title, string href)
        {
            if (page == null || page.Header == null)
                return;

            var link = new HtmlLink();
            link.Attributes["type"] = type;
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = href;
            page.Header.Controls.Add(link);
        }

        /// <summary>
        /// Agrega la funcionalidad de actualizar una pagina desde otra
        /// </summary>
        /// <param name="page">Pagina actual</param>
        /// <param name="tipo">Clase que lo descencadenara</param>
        /// <param name="controlId">Nombre del control</param>
        /// <param name="closePage">True cierra la pagina, en caso contrario false</param>
        public static void RenderActionRefresh(Page page, Type tipo, string controlId, bool closePage)
        {
            page.ClientScript.RegisterStartupScript(tipo, "closerefresh", "<script language=javascript>try {window.opener.document.forms[0]." + controlId + ".click();}catch (e){}" + ((closePage) ? "window.close()" : "") + "</script>");
        }

        #endregion Métodos Render

        #region Cache

        /// <summary>
        /// Establecer cache
        /// </summary>
        /// <param name="cacheName">nombre de la cache</param>
        /// <param name="cacheValue">valor de la cache</param>
        public static void SetCache(string cacheName, object cacheValue)
        {
            if (HttpRuntime.Cache[cacheName] == null)
            {
                HttpRuntime.Cache.Insert(cacheName, cacheValue);
            }
        }

        /// <summary>
        /// Remueve un bjeto de la cache
        /// </summary>
        /// <param name="cacheName"></param>
        public static void RemoveCache(string cacheName)
        {
            HttpRuntime.Cache.Remove(cacheName);
        }

        /// <summary>
        /// Retorna el valo rde la cache
        /// </summary>
        /// <param name="cacheName">Nombre de la cache</param>
        /// <returns></returns>
        public static object GetCacheValue(string cacheName)
        {
            return HttpRuntime.Cache[cacheName];
        }

        #endregion Cache

        #region Imagenes

        /// <summary>
        /// Obtiene el tipo de imagen segun la extension
        /// </summary>
        /// <param name="extension">Extension a evaluar</param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string extension)
        {
            extension = extension.ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".ico":
                    return ImageFormat.Icon;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Guarda una imagen en disco.
        /// </summary>
        /// <param name="caratula">array de bits a guardar</param>
        /// <param name="pathDirectorio">Directorio en donde se guardara el archivo.</param>
        /// <param name="nombreArchivo">nombre del archivo</param>
        public static void GuardarImagen(byte[] caratula, string pathDirectorio, string nombreArchivo)
        {
            try
            {
                if (!Directory.Exists(pathDirectorio))
                    Directory.CreateDirectory(pathDirectorio);

                var pathImagenActual = Path.Combine(pathDirectorio, nombreArchivo);
                var extension = Path.GetExtension(nombreArchivo).Substring(1);

                var ms = new MemoryStream(caratula);

                var imagensave = new Bitmap(ms);
                imagensave.Save(pathImagenActual, GetImageFormat(extension));
                imagensave.Dispose();
                ms.Close();
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message);
            }
        }

        #endregion Imagenes

        #region formatear la entrada y salida de datos

        /// <summary>
        /// Formato de fehca: ddd, dd MMM yyyy HH:mm:ss (\G\M\T)
        /// </summary>
        /// <param name="time">Date a formatear</param>
        /// <returns></returns>
        public static string FormatearFecha(DateTime time)
        {
            var rfc822Dt = time.ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T",
                DateTimeFormatInfo.InvariantInfo);
            return rfc822Dt;
        }

        /// <summary>
        /// Retira todos los caracteres especiales de un texto especificado
        /// </summary>
        /// <param name="text">texto a formatear</param>
        /// <returns>retorna un texto sin caracteres especiales</returns>
        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var regex = new Regex(@"(\s|-)+", RegexOptions.Compiled);

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(";", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = regex.Replace(text, "-");
            text = RemoveCharacters(text);

            return text;
        }

        /// <summary>
        /// Remover caracteres especiales
        /// </summary>
        /// <param name="text">texto a analizar</param>
        /// <returns></returns>
        private static string RemoveCharacters(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return HttpUtility.UrlEncode(sb.ToString()).Replace("%", string.Empty);
        }

        /// <summary>
        /// Retira todas las etiquetas html del string especificado
        /// </summary>
        /// <param name="html">el estring que cntiene el html</param>
        /// <returns>un string sin etiquetas html</returns>
        public static string StripHtml(string html)
        {
            var stripHtml = new Regex(@"<(.|\n)*?>", RegexOptions.Compiled);
            return string.IsNullOrEmpty(html) ? string.Empty : stripHtml.Replace(html, string.Empty);
        }

        #endregion formatear la entrada y salida de datos

        #region Busqueda de Controles

        public static Control BuscarControl(Page page, string nombreControl)
        {
            var ctrl = page.FindControl(nombreControl);
            return ctrl;
        }

        #endregion Busqueda de Controles

        #region "Links de Administracion por nodo"

        /// <summary>
        /// Retorna una lista con enlaces de navegacion
        /// </summary>
        /// <param name="nodo">nodo de donde se estraeran los enlaces</param>
        /// <returns></returns>
        public static List<HtmlAnchor> LinksMenu(string nodo)
        {
            var lstLinks = new List<HtmlAnchor>();
            var root = SiteMap.Providers["SecuritySiteMap"].RootNode;
            if (root == null) return lstLinks;
            foreach (SiteMapNode adminNode in root.ChildNodes)
            {
                if (!adminNode.Title.Equals(nodo)) continue;
                if (!adminNode.IsAccessibleToUser(HttpContext.Current)) continue;
                foreach (SiteMapNode item in adminNode.ChildNodes)
                {
                    var itemNav = new HtmlAnchor {HRef = item.Url, InnerHtml = item.Title};
                    lstLinks.Add(itemNav);
                }
                break;
            }
            return lstLinks;
        }

        #endregion "Links de Administracion por nodo"

        #region "Xml"
        /// <summary> 
        /// Converts a single XML tree to the type of T 
        /// </summary> 
        /// <typeparam name="T">Type to return</typeparam> 
        /// <param name="xml">XML string to convert</param> 
        /// <returns></returns> 
        public static T XmlToObject<T>(string xml)
        {
            using (var xmlStream = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(XmlReader.Create(xmlStream));
            }
        }

        /// <summary> 
        /// Converts the XML to a list of T
        /// </summary> 
        /// <typeparam name="T">Type to return</typeparam> 
        /// <param name="xml">XML string to convert</param> 
        /// <param name="nodePath">XML Node path to select </param> 
        /// <returns></returns> 
        public static List<T> XmlToObjectList<T>(string xml, string nodePath)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xml);

            var returnItemsList = new List<T>();

            foreach (XmlNode xmlNode in xmlDocument.SelectNodes(nodePath))
            {
                returnItemsList.Add(XmlToObject<T>(xmlNode.OuterXml));
            }

            return returnItemsList;
        }
        #endregion

        #region "List to DataTable"
        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        //public static DataTable ToDataTable<T>(List<T> items)
        //{
        //    var tb = new DataTable(typeof(T).Name);

        //    var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var prop in props)
        //    {
        //        var t = GetCoreType(prop.PropertyType);
        //        tb.Columns.Add(prop.Name, t);
        //    }

        //    foreach (var item in items)
        //    {
        //        var values = new object[props.Length];

        //        for (var i = 0; i < props.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item, null);
        //        }

        //        tb.Rows.Add(values);
        //    }

        //    return tb;
        //}

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            var properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }



        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            }
            return t;
        }



        public static DataTable GenericListToDataTable(object list)
        {
            var listType = list.GetType();
            if (!listType.IsGenericType) return null;
            //determine the underlying type the List<> contains
            var elementType = listType.GetGenericArguments()[0];

            //create empty table -- give it a name in case
            //it needs to be serialized
            var dt = new DataTable(elementType.Name + "List");

            //define the table -- add a column for each public
            //property or field
            var miArray = elementType.GetMembers(
                BindingFlags.Public | BindingFlags.Instance);
            foreach (var mi in miArray)
            {
                switch (mi.MemberType)
                {
                    case MemberTypes.Property:
                    {
                        var pi = mi as PropertyInfo;
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                        break;
                    case MemberTypes.Field:
                    {
                        var fi = mi as FieldInfo;
                        dt.Columns.Add(fi.Name, fi.FieldType);
                    }
                        break;
                }
            }

            //populate the table
            var il = list as IList;
            foreach (var record in il)
            {
                var i = 0;
                var fieldValues = new object[dt.Columns.Count];
                foreach (DataColumn c in dt.Columns)
                {
                    var mi = elementType.GetMember(c.ColumnName)[0];
                    switch (mi.MemberType)
                    {
                        case MemberTypes.Property:
                        {
                            var pi = mi as PropertyInfo;
                            fieldValues[i] = pi.GetValue(record, null);
                        }
                            break;
                        case MemberTypes.Field:
                        {
                            var fi = mi as FieldInfo;
                            fieldValues[i] = fi.GetValue(record);
                        }
                            break;
                    }
                    i++;
                }
                dt.Rows.Add(fieldValues);
            }
            return dt;
        }


        public static void ConsumirWsObtenerClientesDirectorio(string pais, string region, string zona, string cargo, string periodo, string estadoCargo)
        {

            var blAlbama = new BlAlbama();
            var blWsDirectorio = new BlWsDirectorio();

            var oListPaises = blAlbama.ListarPaises(pais);
            var tablaLimpiada = blWsDirectorio.DeleteClientesDirectorio(pais);

            if (!tablaLimpiada) return;
            foreach (var oPais in oListPaises)
            {
                var wsDirectorio = new ProcesoDIRWebServiceImplService { Url = WebConfigurationManager.AppSettings.Get("WsDirectorio" + oPais.Codigo), Timeout = 180000 };
                try
                {
                    var resultado = wsDirectorio.obtenerClientesDirectorio(oPais.Codigo, region, zona, cargo, periodo, estadoCargo);
                    switch (resultado.codigo)
                    {
                        case "0":
                            {
                                var dtClienteDirWebService = ConvertToDataTable(resultado.clienteDIRWebService);
                                var strInsert = blWsDirectorio.InsertClientesDirectorio(dtClienteDirWebService);

                                if (strInsert == "1")
                                {
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, "Correcto " + resultado.mensaje);
                                }
                                else
                                {
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, strInsert);
                                }
                            }
                            break;
                        default:
                            blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, resultado.mensaje);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, exception.Message);
                }
            }

            var varCarga = blWsDirectorio.DirectorioCargaInClientes();
            blWsDirectorio.InsertarLogCargaDirectorio("DIC",
                varCarga == "1" ? "Correcto: Carga ESE_DIRECTORIO_IN_CLIENTES" : varCarga);

        }

        #endregion
    }
}