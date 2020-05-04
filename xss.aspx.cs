using System;
using System.Data;
using System.Data.SqlClient;

namespace CxAudit_Demo
{
    public partial class xss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
				string qs = Request.QueryString[0]; 
				string sanitized = myCustomSanitizer(qs);
				message.Text = sanitized;
			}
            catch { }
        }//end Page_Load
		
		protected string myCustomSanitizer(string sVar) {

			// ------------------------------------------------------------
			// Some custom logic that sanitizes for Reflected XSS
			// Not a real implementation. For demonstration purposes only.
			// ------------------------------------------------------------

			return Regex.Replace(sVar, @"(<|>)", "");
		}

        private void getName()
        {
            SqlConnection conn = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=" + Constants.DB_PASSWORD + ";");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT NAME FROM Users;";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
                message.Text = reader["NAME"].ToString();

            conn.Close();
        }
    }
}
