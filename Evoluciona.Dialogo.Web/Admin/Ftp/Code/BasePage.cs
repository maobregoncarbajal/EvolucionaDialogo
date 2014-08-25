
namespace Evoluciona.Dialogo.Web.Admin.Ftp.Code
{
    using System;
    using System.Web.UI;

    public abstract class BasePage : Page
    {
        #region Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.InitComplete"/> event after page initialization.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion
    }
}