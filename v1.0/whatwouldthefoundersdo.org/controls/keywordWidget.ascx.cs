using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wwfd.Data.CoreDataObjectsTableAdapters;
using Wwfd.Data;

namespace Wwfd.Web.controls
{
	public partial class WebUserControl1 : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			KeywordPhrasesTableAdapter phrases = new KeywordPhrasesTableAdapter();
			phrases.GetTopKeywordPhrases();


		}
	}
}