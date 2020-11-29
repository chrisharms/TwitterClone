using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class TagControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Text
        {
            get { return Tag.Text; }
            set { Tag.Text = $"{value} "; }
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler ButtonClick;

        protected void Tag_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }
    }
}