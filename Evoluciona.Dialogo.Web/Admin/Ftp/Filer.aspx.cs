﻿
namespace Evoluciona.Dialogo.Web.Admin.Ftp
{
    using BusinessEntity;
    using BusinessLogic;
    using Code;
    using Dialogo.Helpers;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web.Configuration;
    using System.Web.UI.WebControls;

    #region Comments
    /// <summary>
    /// Obtain and display a list of files and folders derived from 'Root'.
    /// </summary>
    /// <remarks>
    /// 	<h3>Changes</h3>
    /// 	<list type="table">
    /// 		<listheader>
    /// 			<th>Author</th>
    /// 			<th>Date</th>
    /// 			<th>Details</th>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>Mark Merrens</term>
    /// 			<description>17/03/2010</description>
    /// 			<description>Created.</description>
    /// 		</item>
    /// 		<item>
    /// 			<term>Mark Merrens</term>
    /// 			<description>14/06/2010</description>
    /// 			<description>Eat own words and put everything into a single page!</description>
    /// 		</item>
    /// 	</list>
    /// </remarks>
    #endregion

    public partial class Filer : BasePage
    {
        #region Private Members
        /// <summary>
        /// The FileInfo object.
        /// </summary>
        FileInfo fi;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <value>The _root.</value>
        string _root
        {
            get
            {
                // Is the root in the session?
                string root = UrlEncoding.Decode(Request.QueryString["r"]);

                // If it's not there use the default.
                if (string.IsNullOrEmpty(root))
                {
                    root = Server.MapPath(WebConfigurationManager.AppSettings.Get("WFRoot"));
                }

                return root;
            }
        }

        /// <summary>
        /// Gets the source or return path.<br/>
        /// Note: it is up to the user to ensure that the config value contains a valid url.
        /// </summary>
        /// <value>The _return.</value>
        string _return
        {
            get { return WebConfigurationManager.AppSettings.Get("WFReturn"); }
        }

        /// <summary>
        /// Get the mode.<br/>
        /// 0: View.<br/>
        /// 1: Rename.<br/>
        /// 2: New File.<br/>
        /// 3: New Folder.<br/>
        /// 4: Upload File.
        /// </summary>
        /// <value>The mode.</value>
        int _mode
        {
            get { return Convert.ToInt32(Request.QueryString["m"]); }
        }

        /// <summary>
        /// Gets the full path to the file or folder.
        /// </summary>
        /// <value>The _file.</value>
        string _file
        {
            get { return UrlEncoding.Decode(Request.QueryString["f"]); }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            BeAdmin objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
            if (objAdmin == null)
            {
                Response.Redirect("~/error.aspx?mensaje=sesion");
                return;
            }

            string TipoAdmin = objAdmin.TipoAdmin;

            base.OnLoad(e);
            Setup(TipoAdmin);

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    break;
                case Constantes.RolAdminPais:
                    break;
                case Constantes.RolAdminEvaluciona:
                    btnNewFile.Visible = false;
                    btnNewFolder.Visible = false;
                    btnUpload.Visible = false;
                    btnProcesar.Visible = false;
                    break;
            }

        }
        #endregion

        #region Setup
        /// <summary>
        /// Setups this instance.
        /// </summary>
        void Setup(string TipoAdmin)
        {
            // Should the Return button be available?
            // If there is a config entry for WFReturn and it has a value then display the Return button.
            if (!string.IsNullOrEmpty(_return))
            {
                btnReturn.Visible = true;
            }

            // Display the correct view.
            mvFiler.ActiveViewIndex = _mode;

            // Instantiate according to need.
            switch (_mode)
            {
                case 0:	// View
                    SetupView(TipoAdmin);
                    break;
                case 1:	// Rename
                    // Get the FileInfo object.
                    fi = new FileInfo(_file);

                    if (!IsPostBack)
                    {
                        lblRename.Text =
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "Rename {0}",
                                fi.Name);

                        txtRename.Text = fi.Name;
                    }

                    txtRename.Focus();
                    break;
                case 2:	//New File.
                    txtNewFile.Focus();
                    break;
                case 3:	// New Folder.
                    txtNewFolder.Focus();
                    break;
                case 4:	// Upload.
                    fuUpload.Focus();
                    break;
            }

            lblRoot.Text = _root;
        }
        #endregion

        #region General Events
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Cancel(object sender, EventArgs e)
        {
            Refresh();
        }
        #endregion

        #region View Events
        /// <summary>
        /// Handles the Click event of the btnHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnHome_Click(object sender, EventArgs e)
        {
            // Return here with no query string items.
            Response.Redirect("Filer.aspx", true);
        }

        /// <summary>
        /// Handles the Click event of the btnNewFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNewFile_Click(object sender, EventArgs e)
        {
            string newFile =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}&f=&m=2",
                    UrlEncoding.Encode(_root));
            Response.Redirect(newFile, true);
        }

        /// <summary>
        /// Handles the Click event of the btnNewFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNewFolder_Click(object sender, EventArgs e)
        {
            string newFolder =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}&f=&m=3",
                    UrlEncoding.Encode(_root));
            Response.Redirect(newFolder, true);
        }

        /// <summary>
        /// Handles the Click event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string upload =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}&f=&m=4",
                    UrlEncoding.Encode(_root));
            Response.Redirect(upload, true);
        }

        /// <summary>
        /// Handles the Click event of the btnReturn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            // If we reach here then we've already tested that we can do this in Setup.
            Response.Redirect(_return, true);
        }
        #endregion

        #region Rename Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnRenameSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Change the name to the new one.
                string newName = txtRename.Text;
                string newPath = fi.FullName.Replace(fi.Name, newName);

                // Is this a file or a folder?
                if (fi.Attributes == FileAttributes.Directory)
                {
                    Directory.Move(_file, newPath);
                }
                else
                {
                    File.Move(_file, newPath);
                }

                Refresh();
            }
        }
        #endregion

        #region New File Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNewFileSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string file = txtNewFile.Text;
                string path =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"{0}\{1}",
                        _root,
                        file);

                StreamWriter writer = File.CreateText(path);
                writer.Close();

                Refresh();
            }
        }
        #endregion

        #region New Folder Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNewFolderSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string folder = txtNewFolder.Text;
                string path =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"{0}\{1}",
                        _root,
                        folder);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Refresh();
            }
        }
        #endregion

        #region Upload Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUploadSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Leave if no file selected.
                if (!fuUpload.HasFile)
                {
                    fuUpload.Focus();
                    return;
                }

                // Create the path for the new file.
                string path =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"{0}\{1}",
                        _root,
                        fuUpload.FileName);

                // Save to the current folder.
                fuUpload.SaveAs(path);

                Refresh();
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Set up the view.
        /// </summary>
        void SetupView(string TipoAdmin)
        {
            // If the folder is bogus...
            if (!Directory.Exists(_root))
            {
                Label label = new Label();
                label.Text =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "No se puede encontrar '{0}'<br/>Por favor, actualice o modifique el valor 'Root' en 'Web.config'.",
                        _root);
                phDisplay.Controls.Add(label);
                return;
            }

            // Get a new table of files and folders.
            Table table = new TableEx(this, _root).Create(TipoAdmin);

            // Display the table.
            if (table != null)
            {
                phDisplay.Controls.Add(table);
            }
            else
            {
                // Table wasn't created.
                Label label = new Label();
                label.Text = "No se puede crear la tabla.";
                phDisplay.Controls.Add(label);
            }

            // Finally, if a file link was clicked, open it.
            OpenFile();
        }

        /// <summary>
        /// Refreshes the page.
        /// </summary>
        void Refresh()
        {
            string refresh =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Filer.aspx?r={0}",
                    UrlEncoding.Encode(_root));
            Response.Redirect(refresh, true);
        }

        /// <summary>
        /// Display the given file.
        /// </summary>
        void OpenFile()
        {
            // Someone clicked on a file or folder...
            // Doesn't matter what's in x: if it has anything then open the file.
            if (!string.IsNullOrEmpty(Request.QueryString["x"]))
            {
                // Get the object.
                string file = Request.QueryString["o"];
                FileInfo fi = new FileInfo(file);

                // Display the file.
                // 'application/octet-stream' should be okay for most cases.
                string attachment =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "attachment; filename={0}",
                        fi.Name);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", attachment);
                Response.TransmitFile(fi.FullName);
                Response.End();

                // Clear the querystring.
                Request.QueryString.Remove("x");
            }
        }
        #endregion

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            BlCargaAdam cargaAdam = new BlCargaAdam();

            string dtsName = "ESE_Evoluciona_Pre_Carga_Adam";

            if (cargaAdam.CargarArchivoAdam(dtsName))
            {
                Response.Redirect("PreCarga.aspx");
            }

        }
    }
}