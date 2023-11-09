using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryIt
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
    public class Util
    {
        public Boolean checkZIP(string zipcode)
        {
            // Checks the validity of the ZIP code
            string usZipValid = @"^\d{5}(?:[-\s]\d{4})?$"; // makes sure that the zipcode is a valid US ZIP

            if (Regex.Match(zipcode, usZipValid).Success)
                return true;
            else
                return false;
        }

        public void clearLabel(Label lbl)
        {
            // Clears the text of the label in the parameter
            lbl.Text = "";
        }
    }
}