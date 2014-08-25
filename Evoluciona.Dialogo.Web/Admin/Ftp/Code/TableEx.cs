
namespace Evoluciona.Dialogo.Web.Admin.Ftp.Code
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Dialogo.Helpers;

    #region Comments
    /// <summary>
    /// Create and return a table of files and folders.
    /// </summary>
    /// <remarks>
    /// <h3>Changes</h3>
    /// <list type="table">
    /// 	<listheader>
    /// 		<th>Author</th>
    /// 		<th>Date</th>
    /// 		<th>Details</th>
    /// 	</listheader>
    /// 	<item>
    /// 		<term>Mark Merrens</term>
    /// 		<description>17/03/2010</description>
    /// 		<description>Created.</description>
    /// 	</item>
    /// </list>
    /// </remarks>
    #endregion

    public class TableEx
    {
        #region Properties
        /// <summary>
        /// Gets or sets the _data.
        /// </summary>
        /// <value>The _data.</value>
        string[] _data { get; set; }

        /// <summary>
        /// Gets or sets the _root path.
        /// </summary>
        /// <value>The _root.</value>
        string _root { get; set; }

        /// <summary>
        /// Gets or sets the _page.
        /// </summary>
        /// <value>The _page.</value>
        Page _page { get; set; }

        /// <summary>
        /// Gets or sets the name of the display page.
        /// </summary>
        /// <value>The _display page.</value>
        string _displayPage { get; set; }
        #endregion

        #region Private Members
        /// <summary>
        /// The table object.
        /// </summary>
        Table _table;
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="TableEx"/> class.
        /// </summary>
        /// <param name="Page">The page.</param>
        /// <param name="Root">The root path.</param>
        public TableEx(Page Page, string Root)
        {
            // Populate the local members.
            _page = Page;
            _displayPage = _page.Request.FilePath;
            _root = Root;

            // Get a list of files and folders from the root.
            string[] files = Directory.GetFiles(_root);
            string[] folders = Directory.GetDirectories(_root);
            _data = new string[files.Length + folders.Length];
            folders.CopyTo(_data, 0);
            files.CopyTo(_data, folders.Length);
        }
        #endregion

        #region Table Methods
        /// <summary>
        /// Create and return the table.
        /// </summary>
        /// <returns>Table</returns>
        public Table Create(string TipoAdmin)
        {
            // Create the table.
            _table = new Table();

            // Create the header row(s).
            Header();
            RootRow();

            // Create the data rows.
            DataRows(TipoAdmin);

            // Decorate the table.
            _table.BorderColor = Color.DimGray;
            _table.BorderStyle = BorderStyle.Solid;
            _table.BorderWidth = Unit.Pixel(1);
            _table.CellPadding = 3;
            _table.GridLines = GridLines.Both;
            _table.Style.Add("border-collapse", "collapse");
            _table.Style.Add("width", "900px");

            // Table complete: return.
            return _table;
        }

        /// <summary>
        /// Create the header row.
        /// </summary>
        void Header()
        {
            TableRow row = new TableRow();
            TableCell cell;

            cell = new TableCell();
            cell.Text = "Nombre";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.HorizontalAlign = HorizontalAlign.Right;
            cell.Text = "Tamaño";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Tipo";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.Text = "RO";
            cell.ToolTip = "El archivo es de sólo lectura";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Último acceso";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Última modificación";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Acciones";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            row.BackColor = Color.LightGray;
            _table.Rows.AddAt(0, row);
        }

        /// <summary>
        /// If below the root give a way to return.
        /// </summary>
        void RootRow()
        {
            // The root from the config.
            string primary = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings.Get("WFRoot"));

            // If it doesn't match where we are now then create a new row.
            if (primary != _root)
            {
                // Create the row.
                TableRow row = new TableRow();
                TableCell cell;

                // Cell to hold the image and hyperlink.
                cell = new TableCell();

                // Get the folder image.
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = _page.ClientScript.GetWebResourceUrl(typeof(Filer), "Evoluciona.Dialogo.Web.Admin.Ftp.Images.folder_up.png");
                img.ImageAlign = ImageAlign.AbsBottom;
                img.Style.Add("padding-right", "5px");
                cell.Controls.AddAt(0, img);

                // Get the parent directory.
                DirectoryInfo di = new DirectoryInfo(_root);
                string parent = di.Parent.FullName;

                // Add the url.
                // Create the hyperlink.
                HyperLink hl = new HyperLink();
                hl.Text = "..";
                hl.NavigateUrl =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0}?r={1}",
                        _displayPage,
                        UrlEncoding.Encode(parent));

                cell.Controls.AddAt(1, hl);

                // Add the composite cell to the row.
                row.Cells.Add(cell);

                // Add dummy cells.
                cell = new TableCell();
                row.Cells.Add(cell);
                cell = new TableCell();
                row.Cells.Add(cell);
                cell = new TableCell();
                row.Cells.Add(cell);
                cell = new TableCell();
                row.Cells.Add(cell);
                cell = new TableCell();
                row.Cells.Add(cell);
                cell = new TableCell();
                row.Cells.Add(cell);

                // And add the row to the table.
                _table.Rows.AddAt(1, row);

                // Show the home button.
                //((Button)_page.FindControl("btnHome")).Visible = true;
                ContentPlaceHolder cph = (ContentPlaceHolder)_page.Master.FindControl("ContentPlaceHolder1");
                ((Button)cph.FindControl("btnHome")).Visible = true;

            }
        }

        /// <summary>
        /// Creates data rows from each file and folder item.
        /// </summary>
        void DataRows(string TipoAdmin)
        {
            TableRow row;
            TableCell cell;
            FileInfo fi;
            HyperLink hl;

            foreach (string item in _data)
            {
                // Will need some file info.
                fi = new FileInfo(item);

                // If the item attributes are Hidden or System, ignore.
                if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || (fi.Attributes & FileAttributes.System) == FileAttributes.System)
                {
                    continue;
                }

                // New row for each row found.
                row = new TableRow();

                if (fi.Attributes == FileAttributes.Directory)
                {
                    // New cells for each item found.
                    cell = new TableCell();

                    // Get the folder image.
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    img.ImageUrl = _page.ClientScript.GetWebResourceUrl(typeof(Filer), "Evoluciona.Dialogo.Web.Admin.Ftp.Images.folder.png");
                    img.ImageAlign = ImageAlign.AbsBottom;
                    img.Style.Add("padding-right", "5px");
                    cell.Controls.AddAt(0, img);

                    // Create the hyperlink.
                    hl = new HyperLink();
                    hl.Text = fi.Name;
                    hl.NavigateUrl =
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "{0}?r={1}",
                            _displayPage,
                            UrlEncoding.Encode(fi.FullName));

                    // Add the url.
                    cell.Controls.AddAt(1, hl);

                    // Add the composite cell to the row.
                    row.Cells.Add(cell);
                }
                else
                {
                    // Open the file to view as appropriate.
                    cell = new TableCell();

                    // Get the file type image.
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    img.ImageUrl = _page.ClientScript.GetWebResourceUrl(typeof(Filer), "Evoluciona.Dialogo.Web.Admin.Ftp.Images.file.png");
                    img.ImageAlign = ImageAlign.AbsBottom;
                    img.Style.Add("padding-right", "5px");
                    cell.Controls.AddAt(0, img);

                    // Create the hyperlink.
                    hl = new HyperLink();
                    hl.Text = fi.Name;
                    hl.NavigateUrl =
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Filer.aspx?x=x&o={0}",
                            UrlEncoding.Encode(fi.FullName));

                    cell.Controls.AddAt(1, hl);

                    // Add the composite cell to the row.
                    row.Cells.Add(cell);
                }

                // The size of the file.
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Right;
                // 20100601: Fix from Tony Hecht via CodeProject: original code failed when looking at compressed folders.
                cell.Text = ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory) ? string.Empty : FormatFileSize(fi.Length);
                row.Cells.Add(cell);

                // The type of file: if a file has no extension display as 'unknown'.
                cell = new TableCell();
                cell.Text =
                    (string.IsNullOrEmpty(fi.Extension))
                        ? ((fi.Attributes == FileAttributes.Directory) ? "folder" : "unknown")
                        : fi.Extension.Replace(".", string.Empty).ToLowerInvariant();
                row.Cells.Add(cell);

                // Is the file readonly?
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = (fi.IsReadOnly) ? "x" : string.Empty;
                cell.ForeColor = Color.MidnightBlue;
                cell.Font.Bold = true;
                row.Cells.Add(cell);

                // Last access time.
                cell = new TableCell();
                cell.Text = fi.LastAccessTime.ToString();
                row.Cells.Add(cell);

                // Last modified time.
                cell = new TableCell();
                cell.Text = fi.LastWriteTime.ToString();
                row.Cells.Add(cell);

                // Action buttons.
                ImageButton btn;
                cell = new TableCell();

                // Rename.
                btn = new ImageButton();
                btn.ImageUrl = _page.ClientScript.GetWebResourceUrl(typeof(Filer), "Evoluciona.Dialogo.Web.Admin.Ftp.Images.rename.png");
                btn.ToolTip = "Renombrar";
                btn.Command += new CommandEventHandler(Rename);
                btn.CommandArgument = fi.FullName;
                btn.Style.Add("padding-right", "3px");
                if (String.Equals(TipoAdmin, Constantes.RolAdminEvaluciona))
                {
                    btn.Enabled = false;
                }
                cell.Controls.AddAt(0, btn);

                // Delete.
                btn = new ImageButton();
                btn.ImageUrl = _page.ClientScript.GetWebResourceUrl(typeof(Filer), "Evoluciona.Dialogo.Web.Admin.Ftp.Images.delete.png");
                btn.ToolTip = "Borrar";
                btn.Command += new CommandEventHandler(Delete);
                btn.CommandArgument = fi.FullName;
                string deleteMsg =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "¿Estás seguro de que quieres eliminar {0}?",
                        fi.Name);
                ConfirmButton(btn, deleteMsg);
                if (String.Equals(TipoAdmin, Constantes.RolAdminEvaluciona))
                {
                    btn.Enabled = false;
                }
                cell.Controls.AddAt(1, btn);
                row.Cells.Add(cell);

                // Add the row to the table.
                _table.Rows.Add(row);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Renames the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void Rename(object sender, CommandEventArgs e)
        {
            string file = e.CommandArgument.ToString();
            string renameUrl =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}&f={1}&m=1",
                    UrlEncoding.Encode(_root),
                    UrlEncoding.Encode(file));
            HttpContext.Current.Response.Redirect(renameUrl, true);
        }

        /// <summary>
        /// Deletes the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void Delete(object sender, CommandEventArgs e)
        {
            string file = e.CommandArgument.ToString();

            FileInfo fi = new FileInfo(file);

            // Determine type: folder or file.
            if (fi.Attributes == FileAttributes.Directory)
            {
                ReadOnlyFolderDelete(file);
            }
            else
            {
                // Remove all attributes first otherwise
                // this will fail against readonly files.
                fi.Attributes = FileAttributes.Normal;
                File.Delete(file);
            }

            // Send back to self to refresh.
            string deleteUrl =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}",
                    UrlEncoding.Encode(_root));
            HttpContext.Current.Response.Redirect(deleteUrl, true);
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Formats the size of the file.
        /// </summary>
        /// <param name="Bytes">The bytes.</param>
        /// <returns>string</returns>
        static string FormatFileSize(long Bytes)
        {
            Decimal size = 0;
            string result;

            if (Bytes >= 1073741824)
            {
                size = Decimal.Divide(Bytes, 1073741824);
                result =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "{0:##.##} gb",
                        size);
            }
            else if (Bytes >= 1048576)
            {
                size = Decimal.Divide(Bytes, 1048576);
                result =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "{0:##.##} mb",
                        size);
            }
            else if (Bytes >= 1024)
            {
                size = Decimal.Divide(Bytes, 1024);
                result =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "{0:##.##} kb",
                        size);
            }
            else if (Bytes > 0 & Bytes < 1024)
            {
                size = Bytes;
                result =
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "{0:##.##} bytes",
                        size);
            }
            else
            {
                result = "0 bytes";
            }

            return result;
        }

        /// <summary>
        /// Add a javascript confirm to a <see cref="WebControl"/>.
        /// </summary>
        /// <param name="control">Would normally expect this to be a <see cref="Button"/>.</param>
        /// <param name="Message">The message to display.</param>
        static void ConfirmButton(WebControl Control, string Message)
        {
            if (Control != null)
            {
                string confirm =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "javascript:if(confirm('{0}') == false) return false;",
                        Message);
                Control.Attributes.Add("onclick", confirm);
            }
        }

        /// <summary>
        /// Will delete a readonly folder and all sub-folders and files contained therein.
        /// </summary>
        /// <param name="Path">The path.</param>
        static void ReadOnlyFolderDelete(string Path)
        {
            DirectoryInfo di = new DirectoryInfo(Path);
            Stack<DirectoryInfo> folders = new Stack<DirectoryInfo>();
            DirectoryInfo folder;

            // Add to the stack.
            folders.Push(di);

            while (folders.Count > 0)
            {
                // Get the folder and set all attributes to normal.
                folder = folders.Pop();
                folder.Attributes = FileAttributes.Normal;

                // Add to the stack.
                foreach (DirectoryInfo dir in folder.GetDirectories())
                {
                    folders.Push(dir);
                }

                // Set and delete all of the files.
                foreach (FileInfo fi in folder.GetFiles())
                {
                    fi.Attributes = FileAttributes.Normal;
                    fi.Delete();
                }
            }

            // Delete the folder and all sub-folders.
            di.Delete(true);
        }
        #endregion
    }
}