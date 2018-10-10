using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace GTICLOUD
{
    public partial class authenticate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string sEmail = email.Text;
            string sAuth_key = authenticate_key.Text;


            string query = DB_Querys.Authentication(sEmail, sAuth_key);

            DB.CloseConn();
            SqlCommand cmd = DB.ExecuteReader(query);
            SqlDataReader dbr = cmd.ExecuteReader();

            if (dbr.HasRows == false)
            {
                Response.Write("<script>alert('You are not registered for this site contact to Admin');</script>");

            }
            else {


                while (dbr.Read()) {


                    if (dbr["email"].ToString() == email.Text && dbr["authentication_key"].ToString() == authenticate_key.Text && dbr["is_authenticate"].Equals(true))
                    {
                        Session[Macros.SESSION_KEY] = dbr["authentication_key"].ToString();
                        Response.Redirect("site.aspx?skey=" + dbr["sitekey"].ToString());
                    }
                    else {

                        Response.Write("<script>alert('You are not authenticate for this site');</script>");
                    }

                        
                }
            
            }
            
        }
    }
}