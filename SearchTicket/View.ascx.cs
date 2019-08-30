/*
' Copyright (c) 2019  Tsystems
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace TsystemsSearchTicket
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from SearchTicketModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : SearchTicketModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        public object Resource { get; private set; }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoggerAdapter _log = new LoggerAdapter();
            SqlConnection cnn = null;
           
            try
            {
                string cad = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
                cnn = new SqlConnection(cad);
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                var query = "SELECT * FROM Tickets WHERE IncidentID= @incident";
                DataTable table = new DataTable();
                SqlParameter param = new SqlParameter("@incident", txtSearch.Text);
                cmd.Parameters.Add(param);
                cnn.Open();
                cmd.CommandText = query;
                SqlDataAdapter da = null;
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                    var status = string.Empty;
                    if (table.Rows.Count > 0)
                    {
                        StatusLabel.Text = "";
                        ShowContent.Visible = true;
                        foreach (DataRow dr in table.Rows)
                        {
                            lblstatus.Text = dr["TicketStatus"].ToString();
                            status = dr["TicketStatus"].ToString();
                            myPanel.GroupingText = dr["IncidentID"].ToString();
                            lblDescription.Text = dr["Description"].ToString();
                            lblCategory.Text = dr["Category"].ToString();
                            lblAssignUserID.Text = dr["AssignUserID"].ToString();
                            lblOpenTime.Text = dr["OpenTime"].ToString();
                            lblResolveTime.Text = dr["ResolveTime"].ToString();
                            lblResolutionDesc.Text = dr["ResolutionDesc"].ToString();
                        }
                       
                        switch (status)
                        {
                            case "Closed":
                                imgStatus.ImageUrl = "~/Images/bullet_red.png";
                              //////  lblstatus.Text = rm.GetString("IdPagina");// "Cerrado";
                                break;
                            case "Open":
                                imgStatus.ImageUrl = "~/Images/bullet_green.png";
                             //   lblstatus.Text = "Abierto";
                                break;
                            case "Resolved":
                                imgStatus.ImageUrl = "~/Images/bullet_green.png";
                             //   lblstatus.Text = "Resuelto";
                                break;
                            case "Pending Customer":
                                imgStatus.ImageUrl = "~/Images/bullet_blue.png";
                              //  lblstatus.Text = "Pendiente";
                                break;
                            case "Accepted":
                                imgStatus.ImageUrl = "~/Images/bullet_blue.png";
                               // lblstatus.Text = "Pendiente";
                                break;
                            default:
                                imgStatus.ImageUrl = "~/Images/bullet_blue.png";
                                break;
                        }
                    }
                    else
                    {
                        LocalizeString("Description.Text");
                        StatusLabel.Text =  "No se encontraron resultados";
                        ShowContent.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, "Error en: <<--consulta el ticket-->>");
                StatusLabel.Text = "Ha ocurrido un error favor de contactar al administrador del sitio";
                ShowContent.Visible = false;
            }
            finally
            {
                if (cnn != null && cnn.State != ConnectionState.Closed)
                {
                    cnn.Close();
                }
            }
        }
    }
}