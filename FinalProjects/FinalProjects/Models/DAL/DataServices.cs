using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using Final_project.Models;
using System.Data;

namespace Final_project.Models.DAL
{
    public class DataServices
    {
        SqlConnection Connect(string connectionStringName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = CommandSTR;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 5;
            return cmd;
        }

        public void deleteproj(string proName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = deleteprojCommand(proName, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand deleteprojCommand(string proName, SqlConnection con)
        {
            string commandStr = "UPDATE Project_2022 SET StatusId=6 WHERE ProjectName = @proName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@proName", SqlDbType.NVarChar);
            cmd.Parameters["@proName"].Value = proName;
            return cmd;
        }

        public void DeletePresentations()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = DeletePresentationsCommand(con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand DeletePresentationsCommand(SqlConnection con)
        {
            string commandStr = "Delete from Presentation_2022 delete from Presentations_Details_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public void DeletePreference(ProjectPreference PP)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = DeletePreferenceCommand(PP, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand DeletePreferenceCommand(ProjectPreference PP, SqlConnection con)
        {
            string commandStr = "DELETE FROM ProjectPreference_2022 WHERE ProjectName=@proName AND TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@proName", SqlDbType.NVarChar);
            cmd.Parameters["@proName"].Value = PP.ProjectName;
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = PP.TeamId;
            return cmd;
        }
        public void deleteOrg(string orgName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = DeleteOrgCommand(orgName, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand DeleteOrgCommand(string orgName, SqlConnection con)
        {
            string commandStr = "Update Organization_2022 set StatusId=2 WHERE organizationName=@orgName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgName", SqlDbType.NVarChar);
            cmd.Parameters["@orgName"].Value = orgName;
            return cmd;
        }

      
        public List<OrgContact> getContactsOrg(string orgName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandcontacts(con, orgName);
                List<OrgContact> ContactsList = new List<OrgContact>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    OrgContact OC = new OrgContact();
                    OC.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OC.FirstName = (string)dataReader["FirstName"];
                    OC.LastName = (string)dataReader["LastName"];
                    OC.Role = (string)dataReader["Role"];
                    OC.Email = (string)dataReader["Email"];
                    OC.IsMajor = (Boolean)dataReader["IsMajor"];
                    OC.OrganizationName = (string)dataReader["OrganizationName"];
                    ContactsList.Add(OC);
                }
                return ContactsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading contact", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandcontacts(SqlConnection con, string orgName)
        {
            string commandStr = "select * from OrgContact_2022 where organizationName=@orgName and ContactStatus=1";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgName", SqlDbType.NVarChar);
            cmd.Parameters["@orgName"].Value = orgName;
            return cmd;
        }

        public OrgContact getContactbyPhone(string phone)
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandcontactPho(con, phone);

                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows == false)
                {
                    return null;
                }
                else
                {
                    dataReader.Read();
                    OrgContact OC = new OrgContact();
                    OC.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OC.FirstName = (string)dataReader["FirstName"];
                    OC.LastName = (string)dataReader["LastName"];
                    OC.Role = (string)dataReader["Role"];
                    OC.Email = (string)dataReader["Email"];
                    OC.IsMajor = (Boolean)dataReader["IsMajor"];
                    OC.OrganizationName = (string)dataReader["OrganizationName"];
                    return OC;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandcontactPho(SqlConnection con, string phone)
        {
            string commandStr = "select * from OrgContact_2022 where PhoneNumber=@phone";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar);
            cmd.Parameters["@phone"].Value = phone;
            return cmd;
        }


        public List<Mentor> GetMentorsList()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectGetMentorsList(con);
                List<Mentor> MentorsList = new List<Mentor>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Mentor m = new Mentor();
                    m.PhoneNumber = (string)dataReader["PhoneNumber"];
                    m.FirstName = (string)dataReader["FirstName"];
                    m.LastName = (string)dataReader["LastName"];
                    m.Mail = (string)dataReader["Mail"];
                    m.Notes = (String)dataReader["Notes"];
                    m.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    m.IsJudge = (Boolean)dataReader["IsJudge"];
                    if (dataReader["TeamId"] != System.DBNull.Value)
                        m.TeamId = (String)dataReader["TeamId"];
                    else
                        m.TeamId = " ";
                    MentorsList.Add(m);
                }
                return MentorsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading contact", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectGetMentorsList(SqlConnection con )
        {
            string commandStr = "select Distinct * from Mentors_2022 m left join Team_2022 t on t.Mentor=m.PhoneNumber where m.MentorStatus=1";
            SqlCommand command = new SqlCommand(commandStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int InsertOrg(Organization organization)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsert(organization, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsert(Organization organization, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Organization_2022] (OrganizationName,Description,Logo,Notes,StatusId,SpecializationId) " +
                "VALUES('" + organization.OrganizationName + "','" + organization.Description + "','" + organization.Logo + "','" + organization.Notes + "','" + organization.StatusId + "','" + organization.SpecializationId + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int InsertPro(Project project)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertPro(project, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertPro(Project project, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Project_2022] (ProjectName,ProjectDescription,Url,Notes,StatusId,SpecializationId) " +
                "VALUES('" + project.ProjectName + "','" + project.ProjectDescription + "','" + project.Url + "','" + project.Notes + "','" + project.StatusId + "','" + project.SpecializationId + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int InsertProjectPreference(ProjectPreference projectPreference)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertProjectPreference(projectPreference, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                //throw new Exception("Failed in Insert", ex);
                Console.WriteLine("preference not saved");
                return 0;
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertProjectPreference(ProjectPreference projectPreference, SqlConnection con)
        {
            string insertStr = "INSERT INTO [ProjectPreference_2022] (TeamId,ProjectName,Preference) " +
                "VALUES('" + projectPreference.TeamId + "','" + projectPreference.ProjectName + "','" + projectPreference.Preference +"')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        

        public int InsertConP(ContactInProject contactInProject)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertConPro(contactInProject, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertConPro(ContactInProject contactInProject, SqlConnection con)
        {
            string insertStr = "INSERT INTO [contactInProject_2022] (ContactPhoneNumber,ProjectName,OrganizationName,Contact1) " +
                "VALUES('" + contactInProject.ContactPhoneNumber + "','" + contactInProject.ProjectName + "','" + contactInProject.OrganizationName + "','" + contactInProject.Contact1 + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int Insertcon(OrgContact orgContact)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertcon(orgContact, con);
                int affected = command.ExecuteNonQuery();
                //if (ReviewsList == null)
                //    ReviewsList = new List<Review>();
                //ReviewsList.Add(review);
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertcon(OrgContact orgContact, SqlConnection con)
        {
            string insertStr = "INSERT INTO [OrgContact_2022] (PhoneNumber,FirstName,LastName,Email,Role,IsMajor,OrganizationName,ContactStatus) " +
                "VALUES('" + orgContact.PhoneNumber + "','" + orgContact.FirstName + "','" + orgContact.LastName + "','" + orgContact.Email + "','" + orgContact.Role + "','" + orgContact.IsMajor + "','" + orgContact.OrganizationName + "','" + 1 + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int ReadIdfromDB(string orgname)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandorg(con, orgname);

                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows == false)
                {
                    return 0;
                }
                else
                {
                    dataReader.Read();
                    //int OrganizationId = (int)dataReader["organizationId"];
                    int OrganizationId = Convert.ToInt32(dataReader["organizationId"]);
                    dataReader.Close();
                    return OrganizationId;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand createSelectCommandorg(SqlConnection con, string OrganizationName)
        {
            string commandStr = "SELECT * FROM Organization_2022 WHERE OrganizationName =@OrganizationName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@OrganizationName", SqlDbType.NVarChar);
            cmd.Parameters["@OrganizationName"].Value = OrganizationName;
            return cmd;
        }

        public List<Status> ReadStatus()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStatus(con);
                List<Status> statusList = new List<Status>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Status s = new Status();
                    s.StatusText = (string)dataReader["StatusText"];
                    statusList.Add(s);
                }
                return statusList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandStatus(SqlConnection con)
        {
            string commandStr = "select * from Status_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public bool ReadPreferenceByTeam(string teamId,string finalProj)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createReadPreferenceByTeam(con, teamId, finalProj);
                List<ProjectPreference> PPList = new List<ProjectPreference>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows == false)

                    return false;


                else return true;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createReadPreferenceByTeam(SqlConnection con ,string teamId,string finalProj)
        {
            string commandStr = "select * from ProjectPreference_2022 where TeamId=@teamId and ProjectName=@finalProj";
            SqlCommand command = createCommand(con, commandStr);
            command.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            command.Parameters["@TeamId"].Value = teamId;
            command.Parameters.Add("@finalProj", SqlDbType.NVarChar);
            command.Parameters["@finalProj"].Value = finalProj;
            return command;
        }

        public List<ProjectPreference> ReadPreferenceByTeamId(string teamId)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createReadPreferenceByTeamId(con, teamId);
                List<ProjectPreference> PPList = new List<ProjectPreference>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    ProjectPreference PP = new ProjectPreference();
                    PP.Preference = Convert.ToInt32(dataReader["Preference"]);
                    PP.ProjectName = (string)dataReader["ProjectName"];
                    PP.TeamId = (string)dataReader["TeamId"];
                    PPList.Add(PP);
                }
                return PPList;

            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createReadPreferenceByTeamId(SqlConnection con, string teamId)
        {
            string commandStr = "select * from ProjectPreference_2022 where TeamId=@teamId";
            SqlCommand command = createCommand(con, commandStr);
            command.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            command.Parameters["@TeamId"].Value = teamId;
    
            return command;
        }
        public List<ProjectPreference> ReadAllPreference()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createReadAllPreference(con);
                List<ProjectPreference> PPList = new List<ProjectPreference>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectPreference PP = new ProjectPreference();
                    PP.Preference = Convert.ToInt32(dataReader["Preference"]);
                    PP.ProjectName = (string)dataReader["ProjectName"];
                    PP.TeamId = (string)dataReader["TeamId"];
                    try
                    {
                        PP.SuggestedSolution = (Boolean)(dataReader["SuggestedSolution"]);
                    }
                    catch
                    {
                        PP.SuggestedSolution = false;
                    }
                    try
                    {
                        PP.FinalSolution = (Boolean)dataReader["FinalSolution"];

                    }
                    catch
                    {
                        PP.FinalSolution = false;

                    }
              
           
                    PPList.Add(PP);
                }
                return PPList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
  
        private SqlCommand createReadAllPreference(SqlConnection con)
        {
            string commandStr = "select * from ProjectPreference_2022 ";
            SqlCommand command = createCommand(con, commandStr);
            return command;
        }

        public List<Team> ReadAllTeams()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createReadAllTeams(con);
                List<Team> TList = new List<Team>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Team t = new Team();
                    t.TeamId = (string)dataReader["TeamId"];
                    t.SpecializationName = (string)dataReader["SpecializationName"];
                    try
                    {
                        t.MentorPhoneNumber = (string)dataReader["Mentor"];
                    }
                    catch
                    {
                        t.MentorPhoneNumber = "000";
                    }
                    TList.Add(t);
                }
                return TList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createReadAllTeams(SqlConnection con)
        {
            string commandStr = "select * from Team_2022 t join Specialization_2022 sp on t.SpecializationId=sp.SpecializationId where t.StatusId<>0";
            SqlCommand command = createCommand(con, commandStr);
            return command;
        }
        
        public List<Specialization> ReadSpecial()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandSpecial(con);
                List<Specialization> SpecializationList = new List<Specialization>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Specialization sp = new Specialization();
                    sp.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    sp.SpecializationType = (string)dataReader["SpecializationType"];
                    sp.SpecializationName = (string)dataReader["SpecializationName"];
                    SpecializationList.Add(sp);
                }
                return SpecializationList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandSpecial(SqlConnection con)
        {
            string commandStr = "select * from Specialization_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }
        public List<ProjectStatus> ReadStatusPro()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStatusPro(con);
                List<ProjectStatus> statusProList = new List<ProjectStatus>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectStatus sp = new ProjectStatus();
                    sp.StatusText = (string)dataReader["StatusText"];
                    statusProList.Add(sp);
                }
                return statusProList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandStatusPro(SqlConnection con)
        {
            string commandStr = "select * from ProjectStatus_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }
        public OrganizationTable ReadOrgByName(string orgName)
        {

            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandOrgByName(con, orgName);

                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows == false)
                {
                    return null;
                }
                else
                {
                    OrganizationTable OT = new OrganizationTable();
                    dataReader.Read();
                    OT.OrganizationName = (string)dataReader["OrganizationName"];
                    OT.SpecializationName = (string)dataReader["SpecializationName"];
                    OT.Description = (string)dataReader["Description"];
                    OT.FirstName = (string)dataReader["FirstName"];
                    OT.LastName = (string)dataReader["LastName"];
                    OT.Email = (string)dataReader["Email"];
                    OT.Role = (string)dataReader["Role"];
                    OT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OT.Logo = (string)dataReader["Logo"];
                    OT.Notes = (string)dataReader["Notes"];
                    OT.StatusText = (string)dataReader["StatusText"];
                    return OT;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand createSelectCommandOrgByName(SqlConnection con, string orgname)
        {
            string commandStr = "select * from Organization_2022 o join OrgContact_2022 c on o.organizationName = c.organizationName join Status_2022 s on o.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=o.SpecializationId where o.OrganizationName = @orgname";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgname", SqlDbType.NVarChar);
            cmd.Parameters["@orgname"].Value = orgname;
            return cmd;
        }

        public List<OrganizationTable> Readorg()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandorg(con);
                List<OrganizationTable> organizationsTableList = new List<OrganizationTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    OrganizationTable OT = new OrganizationTable();
                    OT.OrganizationName = (string)dataReader["OrganizationName"];
                    OT.Description = (string)dataReader["Description"];
                    OT.FirstName = (string)dataReader["FirstName"];
                    OT.LastName = (string)dataReader["LastName"];
                    OT.Email = (string)dataReader["Email"];
                    OT.Role = (string)dataReader["Role"];
                    OT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OT.Notes = (string)dataReader["Notes"];
                    OT.Logo = (string)dataReader["Logo"];
                    OT.StatusText = (string)dataReader["StatusText"];
                    OT.SpecializationName = (string)dataReader["SpecializationName"];
                    organizationsTableList.Add(OT);
                }
                return organizationsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandorg(SqlConnection con)
        {
            string commandStr = "select * from Organization_2022 o join OrgContact_2022 c on o.organizationName = c.organizationName join Status_2022 s on o.StatusId = s.StatusId join Specialization_2022 sp on o.SpecializationId=sp.SpecializationId where c.IsMajor = 1";
            SqlCommand cmd = createCommand(con, commandStr);
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }

        public List<ProjectTable> ReadProjByOrg(string orgName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = ReadProjByOrgCommand(con, orgName);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand ReadProjByOrgCommand(SqlConnection con, string orgName)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.PhoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId where oc.IsMajor = 1 and o.OrganizationName=@orgName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgName", SqlDbType.NVarChar);
            cmd.Parameters["@orgName"].Value = orgName;
            return cmd;
        }
        public List<ProjectTable> ReadProjByOrgandStatus(int selectedsta, string orgName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = ReadProjByOrgandStatusCommand(con, selectedsta, orgName);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand ReadProjByOrgandStatusCommand(SqlConnection con,int selectedsta, string orgName)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.PhoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId where oc.IsMajor = 1 and o.OrganizationName=@orgName and s.StatusId=@selectedsta";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgName", SqlDbType.NVarChar);
            cmd.Parameters["@orgName"].Value = orgName;
            cmd.Parameters.Add("@selectedsta", SqlDbType.Int);
            cmd.Parameters["@selectedsta"].Value = selectedsta;
            return cmd;
        }


        public List<OrganizationTable> ReadOrgs(int selectedsta)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandorgS(con, selectedsta);
                List<OrganizationTable> organizationsTableList = new List<OrganizationTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    OrganizationTable OT = new OrganizationTable();
                    OT.OrganizationName = (string)dataReader["OrganizationName"];
                    OT.Description = (string)dataReader["Description"];
                    OT.FirstName = (string)dataReader["FirstName"];
                    OT.LastName = (string)dataReader["LastName"];
                    OT.Email = (string)dataReader["Email"];
                    OT.Role = (string)dataReader["Role"];
                    OT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OT.Notes = (string)dataReader["Notes"];
                    OT.Logo = (string)dataReader["Logo"];
                    OT.StatusText = (string)dataReader["StatusText"];
                    OT.SpecializationName = (string)dataReader["SpecializationName"];
                    organizationsTableList.Add(OT);
                }
                return organizationsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandorgS(SqlConnection con, int selectedsta)
        {
            string commandStr = "select * from Organization_2022 o join OrgContact_2022 c on o.organizationName = c.organizationName join Status_2022 s on o.StatusId = s.StatusId join Specialization_2022 sp on o.SpecializationId=sp.SpecializationId where c.IsMajor = 1 and o.StatusId=@selectedsta";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@selectedsta", SqlDbType.NVarChar);
            cmd.Parameters["@selectedsta"].Value = selectedsta;
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }
        public void UpdateContact(OrgContact contact)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateContactCommand(con, contact);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateContactCommand(SqlConnection con, OrgContact contact)
        {

            string commandStr = "UPDATE OrgContact_2022 SET phoneNumber=@phone, firstName=@firstName , lastName=@lastName, email=@email,IsMajor=@IsMajor,[role]=@role WHERE phoneNumber=@phone";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar);
            cmd.Parameters["@phone"].Value = contact.PhoneNumber;
            cmd.Parameters.Add("@firstName", SqlDbType.NVarChar);
            cmd.Parameters["@firstName"].Value = contact.FirstName;
            cmd.Parameters.Add("@lastName", SqlDbType.NVarChar);
            cmd.Parameters["@lastName"].Value = contact.LastName;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar);
            cmd.Parameters["@email"].Value = contact.Email;
            cmd.Parameters.Add("@role", SqlDbType.NVarChar);
            cmd.Parameters["@role"].Value = contact.Role;
            cmd.Parameters.Add("@OrganizationName", SqlDbType.NVarChar);
            cmd.Parameters["@OrganizationName"].Value = contact.OrganizationName;
            cmd.Parameters.Add("@IsMajor", SqlDbType.NVarChar);
            cmd.Parameters["@IsMajor"].Value = contact.IsMajor;

            return cmd;
        }

        public void UpdateOrg(Organization org)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateOrgCommand(con, org);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand UpdateOrgCommand(SqlConnection con, Organization org)
        {

            string commandStr = "UPDATE Organization_2022 SET [Description] = @Description, Logo = @Logo ,Notes=@Notes, StatusId = @StatusId , SpecializationId=@specId WHERE organizationName = @OrganizationName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters["@Description"].Value = org.Description;
            cmd.Parameters.Add("@Logo", SqlDbType.NVarChar);
            cmd.Parameters["@Logo"].Value = org.Logo;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar);
            cmd.Parameters["@Notes"].Value = org.Notes;
            cmd.Parameters.Add("@OrganizationName", SqlDbType.NVarChar);
            cmd.Parameters["@OrganizationName"].Value = org.OrganizationName;
            cmd.Parameters.Add("@StatusId", SqlDbType.Int);
            cmd.Parameters["@StatusId"].Value = org.StatusId;
            cmd.Parameters.Add("@specId", SqlDbType.Int);
            cmd.Parameters["@specId"].Value = org.SpecializationId;
            return cmd;
        }

        public int ReadStatusId(string sta)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStatusId(con, sta);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dataReader.Read();
                int StatusId = Convert.ToInt32(dataReader["StatusId"]);
                dataReader.Close();
                return StatusId;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandStatusId(SqlConnection con, string StatusText)
        {
            string commandStr = "SELECT * FROM Status_2022 WHERE StatusText =@StatusText";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@StatusText", SqlDbType.NVarChar);
            cmd.Parameters["@StatusText"].Value = StatusText;
            return cmd;
        }

        public int ReadStatusProId(string sta)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStatuProsId(con, sta);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dataReader.Read();
                int StatusId = Convert.ToInt32(dataReader["StatusId"]);
                dataReader.Close();
                return StatusId;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandStatuProsId(SqlConnection con, string StatusText)
        {
            string commandStr = "SELECT * FROM ProjectStatus_2022 WHERE StatusText =@StatusText";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@StatusText", SqlDbType.NVarChar);
            cmd.Parameters["@StatusText"].Value = StatusText;
            return cmd;
        }

        public int ReadSpeId(string Spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStatusSpeId(con, Spe);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dataReader.Read();
                int SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                dataReader.Close();
                return SpecializationId;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading SpecializationId", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandStatusSpeId(SqlConnection con, string SpecializationName)
        {
            string commandStr = "SELECT * FROM Specialization_2022 WHERE SpecializationName=@SpecializationName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@SpecializationName", SqlDbType.NVarChar);
            cmd.Parameters["@SpecializationName"].Value = SpecializationName;
            return cmd;
        }


        public List<ProjectBook> ReadprojectBook()
        {
            SqlConnection con = null;
            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProjectBook(con);
                List<ProjectBook> projectList = new List<ProjectBook>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectBook PB = new ProjectBook();
                    PB.OrganizationName = (string)dataReader["OrganizationName"];
                    PB.OrgDescription = (string)dataReader["OrgDescription"];
                    PB.Logo = (string)dataReader["Logo"];
                    PB.ProjectName = (string)dataReader["ProjectName"];
                    PB.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PB.StatusText = (string)dataReader["StatusText"];
                    PB.SpecializationName = (string)dataReader["SpecializationName"];
                    PB.OrgSpecializationName = (string)dataReader["OrgSpecializationName"];
                    projectList.Add(PB);
                }
                return projectList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProjectBook(SqlConnection con)
        {
            string commandStr = "select Distinct o.OrganizationName,o.[Description] as OrgDescription,o.Logo,p.ProjectName,p.ProjectDescription,s.StatusText,spe.SpecializationName,specOrg.SpecializationName as OrgSpecializationName from Project_2022 p join ContactInProject_2022 C on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 spe on p.SpecializationId = spe.SpecializationId join Specialization_2022 specOrg on o.SpecializationId=specOrg.SpecializationId";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }


        public List<ProjectTable> Readpro()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandpro(con);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.SpecializationName= (string)dataReader["SpecializationName"];
                    PT.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.StatusId = Convert.ToInt32(dataReader["StatusId"]);
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandpro(SqlConnection con)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.phoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=p.SpecializationId where c.Contact1=1";
            SqlCommand cmd = createCommand(con, commandStr);
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }

       
        
        private SqlCommand createSelectCommandForFilters(SqlConnection con, string filterName)
        {
            string tblName = "";
            if (filterName == "specialization")
            {
                tblName = "Specialization_2022";
            }
            if (filterName == "status")
            {
                tblName = "Status_2022";
            }
            if (filterName == "organization")
            {
                tblName = "Organization_2022";
            }
            string commandStr = "select * from " + tblName;
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public void UpdatePro(Project pro)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateProCommand(con, pro);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand UpdateProCommand(SqlConnection con, Project pro)
        {

            string commandStr = "UPDATE Project_2022 SET [ProjectDescription] = @ProjectDescription ,Notes=@Notes, StatusId = @StatusId ,Url=@url WHERE ProjectName = @ProjectName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@ProjectDescription", SqlDbType.NVarChar);
            cmd.Parameters["@ProjectDescription"].Value = pro.ProjectDescription;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar);
            cmd.Parameters["@Notes"].Value = pro.Notes;
            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar);
            cmd.Parameters["@ProjectName"].Value = pro.ProjectName;
            cmd.Parameters.Add("@StatusId", SqlDbType.Int);
            cmd.Parameters["@StatusId"].Value = pro.StatusId;
            cmd.Parameters.Add("@url", SqlDbType.NVarChar);
            cmd.Parameters["@url"].Value = pro.Url;
            return cmd;
        }

        public int InsetTeam(Team team)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertTeam(team, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }

        SqlCommand CreateInsertTeam(Team team, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Team_2022] (TeamId,[Password],IsAccepted,StatusId) " +
                "VALUES('" + team.TeamId + "','" + team.Password + "','" + false + "','" + 0 + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public string Login(Team team)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = CreateLogin(team, con);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                        return (string)dataReader[0];
                }
                return "-1";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Login", ex);
            }

            finally
            {
                con.Close();
            }
        }

        SqlCommand CreateLogin(Team team, SqlConnection con)
        {
            string commandStr = "SELECT * from [Team_2022] WHERE TeamId=@TeamId and Password=@Password";
            SqlCommand command = createCommand(con, commandStr);
            command.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            command.Parameters["@TeamId"].Value = team.TeamId;
            command.Parameters.Add("@Password", SqlDbType.NVarChar);
            command.Parameters["@Password"].Value = team.Password;

            return command;
        }

        public string LoginManager(Manager M)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = CreateLoginManager(M, con);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                        return (string)dataReader[0];
                }
                return "-1";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Login", ex);
            }

            finally
            {
                con.Close();
            }
        }

        SqlCommand CreateLoginManager(Manager M, SqlConnection con)
        {
            string commandStr = "SELECT * from [Manager_2022] WHERE Mail=@Mail and Password=@Password";
            SqlCommand command = createCommand(con, commandStr);
            command.Parameters.Add("@Mail", SqlDbType.NVarChar);
            command.Parameters["@Mail"].Value = M.Mail;
            command.Parameters.Add("@Password", SqlDbType.NVarChar);
            command.Parameters["@Password"].Value = M.Password;

            return command;
        }

        public List<OrgContact> getContacts()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = SelectCommandcontacts(con);
                List<OrgContact> ContactsList = new List<OrgContact>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    OrgContact OC = new OrgContact();
                    OC.PhoneNumber = (string)dataReader["PhoneNumber"];
                    OC.FirstName = (string)dataReader["FirstName"];
                    OC.LastName = (string)dataReader["LastName"];
                    OC.Role = (string)dataReader["Role"];
                    OC.Email = (string)dataReader["Email"];
                    OC.IsMajor = (Boolean)dataReader["IsMajor"];
                    OC.OrganizationName = (string)dataReader["OrganizationName"];
                    ContactsList.Add(OC);
                }
                return ContactsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading contact", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand SelectCommandcontacts(SqlConnection con)
        {
            string commandStr = "select * from OrgContact_2022 where ContactStatus=1";
            SqlCommand command = new SqlCommand(commandStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        
                    public List<Student> GetStudentsList()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = SelectCommandGetStudentsList(con);
                List<Student> StudentsList = new List<Student>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Student s = new Student();
                    s.PhoneNumber = (string)dataReader["PhoneNumber"];
                    s.FirstName = (string)dataReader["FirstName"];
                    s.LastName = (string)dataReader["LastName"];
                    s.StudentId = (string)dataReader["studentId"];
                    s.SpecializationId = Convert.ToInt32(dataReader["specializationId"]);
                    //s.TeamId = (string)dataReader["teamId"];
                    StudentsList.Add(s);
                }
                return StudentsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading students", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand SelectCommandGetStudentsList(SqlConnection con)
        {
            string commandStr = "select * from Student_2022 s where s.TeamId is null and s.StudentStatus=1";
            SqlCommand command = new SqlCommand(commandStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public List<Student> GetStudentsListTeams()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = SelectCommandGetStudentsTeamsList(con);
                List<Student> StudentsList = new List<Student>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Student s = new Student();
                    s.PhoneNumber = (string)dataReader["PhoneNumber"];
                    s.FirstName = (string)dataReader["FirstName"];
                    s.LastName = (string)dataReader["LastName"];
                    s.StudentId = (string)dataReader["studentId"];
                    s.SpecializationId = Convert.ToInt32(dataReader["specializationId"]);
                    s.TeamId = (string)dataReader["teamId"];
                    StudentsList.Add(s);
                }
                return StudentsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading students", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand SelectCommandGetStudentsTeamsList(SqlConnection con)
        {
            string commandStr = "select * from Student_2022 s where s.TeamId is not null and s.StudentStatus=1";
            SqlCommand command = new SqlCommand(commandStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public List<ProjectTable> ReadProByStatus(int selectedsta)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProS(con, selectedsta);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.SpecializationName = (string)dataReader["SpecializationName"];
                    PT.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    PT.StatusId = Convert.ToInt32(dataReader["StatusId"]);
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProS(SqlConnection con, int selectedsta)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.phoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=p.SpecializationId  where c.Contact1=1 and p.StatusId=@selectedsta";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@selectedsta", SqlDbType.NVarChar);
            cmd.Parameters["@selectedsta"].Value = selectedsta;
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }

        public List<ProjectTable> ReadProByStatusSpe(int selectedsta, string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProStSp(con, selectedsta,spe);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.SpecializationName = (string)dataReader["SpecializationName"];
                    PT.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    PT.StatusId = Convert.ToInt32(dataReader["StatusId"]);
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProStSp(SqlConnection con, int selectedsta ,string spe)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.phoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=p.SpecializationId  where c.Contact1=1 and p.StatusId=@selectedsta and sp.SpecializationName=@spe";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@selectedsta", SqlDbType.NVarChar);
            cmd.Parameters["@selectedsta"].Value = selectedsta;
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }

        public List<ProjectTable> ReadProBySpe(string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProSp(con, spe);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.SpecializationName = (string)dataReader["SpecializationName"];
                    PT.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    PT.StatusId = Convert.ToInt32(dataReader["StatusId"]);
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1 = (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProSp(SqlConnection con, string spe)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.phoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=p.SpecializationId  where c.Contact1=1 and sp.SpecializationName=@spe";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            //cmd.Parameters.Add("@userId", SqlDbType.NVarChar);
            return cmd;
        }
        public List<ProjectTable> ReadProByName(string ProName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProByName(con, ProName);
                List<ProjectTable> ProjectsTableList = new List<ProjectTable>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectTable PT = new ProjectTable();
                    PT.ProjectName = (string)dataReader["ProjectName"];
                    PT.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PT.Url = (string)dataReader["Url"];
                    PT.Notes = (string)dataReader["Notes"];
                    PT.StatusText = (string)dataReader["StatusText"];
                    PT.SpecializationName = (string)dataReader["SpecializationName"];
                    PT.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    PT.StatusId = Convert.ToInt32(dataReader["StatusId"]);
                    PT.OrganizationName = (string)dataReader["OrganizationName"];
                    PT.FirstName = (string)dataReader["FirstName"];
                    PT.LastName = (string)dataReader["LastName"];
                    PT.Email = (string)dataReader["Email"];
                    PT.Role = (string)dataReader["Role"];
                    PT.PhoneNumber = (string)dataReader["PhoneNumber"];
                    PT.Contact1= (Boolean)dataReader["Contact1"];
                    ProjectsTableList.Add(PT);
                }
                return ProjectsTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


       

        private SqlCommand createSelectCommandProByName(SqlConnection con, string proName)
        {
            string commandStr = "select * from Project_2022 p join ContactInProject_2022 c on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join OrgContact_2022 oc on c.ContactPhoneNumber=oc.phoneNumber join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 sp on sp.SpecializationId=p.SpecializationId where p.ProjectName = @proName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@proName", SqlDbType.NVarChar);
            cmd.Parameters["@proName"].Value = proName;
            return cmd;
        }

        public void UpdateProContact(ContactInProject contact)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateProContactCommand(con, contact);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateProContactCommand(SqlConnection con, ContactInProject contact)
        {

            string commandStr = "UPDATE ContactInProject_2022 SET ContactPhoneNumber=@ContactPhoneNumber, ProjectName=@ProjectName , OrganizationName=@OrganizationName WHERE ProjectName=@ProjectName and Contact1=@Contact1";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@ContactPhoneNumber", SqlDbType.NVarChar);
            cmd.Parameters["@ContactPhoneNumber"].Value = contact.ContactPhoneNumber;
            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar);
            cmd.Parameters["@ProjectName"].Value = contact.ProjectName;
            cmd.Parameters.Add("@OrganizationName", SqlDbType.NVarChar);
            cmd.Parameters["@OrganizationName"].Value = contact.OrganizationName;
            cmd.Parameters.Add("@Contact1", SqlDbType.NVarChar);
            cmd.Parameters["@Contact1"].Value = contact.Contact1;

            return cmd;
        }

        public List<ProjectBook> ReadBProBySpe(string spe)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProjectBookSpe(con, spe);
                List<ProjectBook> projectList = new List<ProjectBook>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectBook PB = new ProjectBook();
                    PB.OrganizationName = (string)dataReader["OrganizationName"];
                    PB.OrgDescription = (string)dataReader["OrgDescription"];
                    PB.Logo = (string)dataReader["Logo"];
                    PB.ProjectName = (string)dataReader["ProjectName"];
                    PB.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PB.StatusText = (string)dataReader["StatusText"];
                    PB.SpecializationName = (string)dataReader["SpecializationName"];
                    PB.OrgSpecializationName = (string)dataReader["OrgSpecializationName"];
                    projectList.Add(PB);
                }
                return projectList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProjectBookSpe(SqlConnection con, string spe)
        {
            string commandStr = "select Distinct o.OrganizationName,o.[Description] as OrgDescription,o.Logo,p.ProjectName,p.ProjectDescription,s.StatusText,spe.SpecializationName,specOrg.SpecializationName as OrgSpecializationName from Project_2022 p join ContactInProject_2022 C on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 spe on p.SpecializationId = spe.SpecializationId join Specialization_2022 specOrg on o.SpecializationId=specOrg.SpecializationId where spe.SpecializationName=@spe";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            return cmd;
        }

        public List<ProjectBook> ReadProByOrgsp(string org)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProjectBookOrgsp(con, org);
                List<ProjectBook> projectList = new List<ProjectBook>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectBook PB = new ProjectBook();
                    PB.OrganizationName = (string)dataReader["OrganizationName"];
                    PB.OrgDescription = (string)dataReader["OrgDescription"];
                    PB.Logo = (string)dataReader["Logo"];
                    PB.ProjectName = (string)dataReader["ProjectName"];
                    PB.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PB.StatusText = (string)dataReader["StatusText"];
                    PB.SpecializationName = (string)dataReader["SpecializationName"];
                    PB.OrgSpecializationName = (string)dataReader["OrgSpecializationName"];
                    projectList.Add(PB);
                }
                return projectList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProjectBookOrgsp(SqlConnection con, string org)
        {
            string commandStr = "select Distinct o.OrganizationName,o.[Description] as OrgDescription,o.Logo,p.ProjectName,p.ProjectDescription,s.StatusText,spe.SpecializationName,specOrg.SpecializationName as OrgSpecializationName from Project_2022 p join ContactInProject_2022 C on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 spe on p.SpecializationId = spe.SpecializationId join Specialization_2022 specOrg on o.SpecializationId=specOrg.SpecializationId where specOrg.SpecializationName=@org";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@org", SqlDbType.NVarChar);
            cmd.Parameters["@org"].Value = org;
            return cmd;
        }

        public List<ProjectBook> ReadProByOrgSpe(string org, string spe)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandProjectBookSpeOrg(con, org, spe);
                List<ProjectBook> projectList = new List<ProjectBook>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectBook PB = new ProjectBook();
                    PB.OrganizationName = (string)dataReader["OrganizationName"];
                    PB.OrgDescription = (string)dataReader["OrgDescription"];
                    PB.Logo = (string)dataReader["Logo"];
                    PB.ProjectName = (string)dataReader["ProjectName"];
                    PB.ProjectDescription = (string)dataReader["ProjectDescription"];
                    PB.StatusText = (string)dataReader["StatusText"];
                    PB.SpecializationName = (string)dataReader["SpecializationName"];
                    PB.OrgSpecializationName = (string)dataReader["OrgSpecializationName"];
                    projectList.Add(PB);
                }
                return projectList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand createSelectCommandProjectBookSpeOrg(SqlConnection con, string org, string spe)
        {
            string commandStr = "select Distinct o.OrganizationName,o.[Description] as OrgDescription,o.Logo,p.ProjectName,p.ProjectDescription,s.StatusText,spe.SpecializationName,specOrg.SpecializationName as OrgSpecializationName from Project_2022 p join ContactInProject_2022 C on p.ProjectName = c.ProjectName join Organization_2022 o on c.OrganizationName = o.OrganizationName join ProjectStatus_2022 s on p.StatusId = s.StatusId join Specialization_2022 spe on p.SpecializationId = spe.SpecializationId join Specialization_2022 specOrg on o.SpecializationId=specOrg.SpecializationId where spe.SpecializationName=@spe and specOrg.SpecializationName=@org";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            cmd.Parameters.Add("@org", SqlDbType.NVarChar);
            cmd.Parameters["@org"].Value = org;
            return cmd;
        }

        public void UpdateTeam2(string student1, string student2, int teamid,string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateStuTeam2(con, student1, student2, teamid,spe);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateStuTeam2(SqlConnection con, string student1, string student2, int teamid,string spe)
        {

            string commandStr = "UPDATE Student_2022 SET TeamId=@teamid WHERE StudentId=@student1 or StudentId=@student2  UPDATE Team_2022 SET StatusId=2,SpecializationId=@spe WHERE TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@student1", SqlDbType.NVarChar);
            cmd.Parameters["@student1"].Value = student1;
            cmd.Parameters.Add("@student2", SqlDbType.NVarChar);
            cmd.Parameters["@student2"].Value = student2;
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = teamid;
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;


            return cmd;
        }

        public void UpdateTeam3(string student1, string student2, string student3, int teamid,string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateStuTeam3(con, student1, student2, student3, teamid,spe);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateStuTeam3(SqlConnection con, string student1, string student2, string student3, int teamid,string spe)
        {

            string commandStr = "UPDATE Student_2022 SET TeamId=@teamid WHERE (StudentId=@student1 or StudentId=@student2 or StudentId=@student3) UPDATE Team_2022 SET StatusId=2,SpecializationId=@spe WHERE TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@student1", SqlDbType.NVarChar);
            cmd.Parameters["@student1"].Value = student1;
            cmd.Parameters.Add("@student2", SqlDbType.NVarChar);
            cmd.Parameters["@student2"].Value = student2;
            cmd.Parameters.Add("@student3", SqlDbType.NVarChar);
            cmd.Parameters["@student3"].Value = student3;
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = teamid;
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            return cmd;
        }

        public void UpdateTeam4(string student1, string student2, string student3, string student4, int teamid,string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateStuTeam4(con, student1, student2, student3, student4, teamid,spe);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateStuTeam4(SqlConnection con, string student1, string student2, string student3, string student4, int teamid,string spe)
        {

            string commandStr = "UPDATE Student_2022 SET TeamId=@teamid WHERE StudentId=@student1 or StudentId=@student2 or StudentId=@student3 or StudentId=@student4  UPDATE Team_2022 SET StatusId=2,SpecializationId=@spe WHERE TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@student1", SqlDbType.NVarChar);
            cmd.Parameters["@student1"].Value = student1;
            cmd.Parameters.Add("@student2", SqlDbType.NVarChar);
            cmd.Parameters["@student2"].Value = student2;
            cmd.Parameters.Add("@student3", SqlDbType.NVarChar);
            cmd.Parameters["@student3"].Value = student3;
            cmd.Parameters.Add("@student4", SqlDbType.NVarChar);
            cmd.Parameters["@student4"].Value = student4;
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = teamid;
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            return cmd;
        }

        public void UpdateTeamreason(string reason, int teamId)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateTeamreasonW(con, reason, teamId);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed update team", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateTeamreasonW(SqlConnection con, string reason, int teamId)
        {

            string commandStr = "UPDATE Team_2022 SET IsAccepted=0, AdditionalInfo=@reason WHERE TeamId=@teamId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@reason", SqlDbType.NVarChar);
            cmd.Parameters["@reason"].Value = reason;
            cmd.Parameters.Add("@teamId", SqlDbType.NVarChar);
            cmd.Parameters["@teamId"].Value = teamId;
            return cmd;
        }

        public void UpdateTeamStatus(int status,int teamId)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateTeamStatusD(con, status, teamId);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed update team", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateTeamStatusD(SqlConnection con, int status,int teamId)
        {

            string commandStr = "UPDATE Team_2022 SET StatusId=@status WHERE TeamId=@teamId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@status", SqlDbType.NVarChar);
            cmd.Parameters["@status"].Value = status;
            cmd.Parameters.Add("@teamId", SqlDbType.NVarChar);
            cmd.Parameters["@teamId"].Value = teamId;
            return cmd;
        }

        public void UpdateTeam(int teamid)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateStuTeam(con,teamid);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateStuTeam(SqlConnection con,int teamid)
        {

            string commandStr = "UPDATE Student_2022 SET TeamId=null WHERE TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = teamid;


            return cmd;
        }
        public List<StudentsTeam> ReadStuByTeam()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStuByTeam(con);
                List<StudentsTeam> StudentTableList = new List<StudentsTeam>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    StudentsTeam ST = new StudentsTeam();
                    ST.TeamId = (string)dataReader["TeamId"];
                    ST.FirstName = (string)dataReader["FirstName"];
                    ST.PhoneNumber = (string)dataReader["PhoneNumber"];
                    ST.LastName = (string)dataReader["LastName"];
                    ST.AdditionalInfo = (string)dataReader["AdditionalInfo"];
                    ST.IsAccepted = (Boolean)dataReader["IsAccepted"];
                    ST.SpecializationName = (string)dataReader["SpecializationName"];
                    StudentTableList.Add(ST);
                }
                return StudentTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand createSelectCommandStuByTeam(SqlConnection con)
        {
            string commandStr = "select * from Team_2022 t join Student_2022 s on t.TeamId = s.TeamId join Specialization_2022 sp on s.SpecializationId=sp.SpecializationId";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public void UpdateTeamStatus(Team team)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateTeamStaCommand(con, team);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateTeamStaCommand(SqlConnection con, Team team)
        {
            string commandStr = "UPDATE Team_2022 SET IsAccepted=@IsAccepted WHERE TeamId=@TeamId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@IsAccepted", SqlDbType.NVarChar);
            cmd.Parameters["@IsAccepted"].Value = team.IsAccepted;
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            cmd.Parameters["@TeamId"].Value = team.TeamId;
            return cmd;
        }
        public void UpdatePreference(string TeamId, string Project)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdatePreferenceCommand(con, TeamId, Project);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in update of preference", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdatePreferenceCommand(SqlConnection con, string TeamId, string Project)
        {

            string commandStr = "UPDATE ProjectPreference_2022 SET SuggestedSolution=1 WHERE TeamId=@TeamId and ProjectName=@Project UPDATE ProjectPreference_2022 SET SuggestedSolution=0 WHERE TeamId=@TeamId and ProjectName!=@Project";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            cmd.Parameters["@TeamId"].Value = TeamId;
            cmd.Parameters.Add("@Project", SqlDbType.NVarChar);
            cmd.Parameters["@Project"].Value = Project;
            return cmd;
        }
        public void UpdatePreferenceFinal(string TeamId, string Project,string mentor)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdatePreferenceFinalCommand(con, TeamId, Project,mentor);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in update of preference", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdatePreferenceFinalCommand(SqlConnection con, string TeamId, string Project,string mentor)
        {

            string commandStr = "UPDATE ProjectPreference_2022 SET FinalSolution=1 WHERE TeamId=@TeamId and ProjectName=@Project UPDATE ProjectPreference_2022 SET FinalSolution=0 WHERE TeamId=@TeamId and ProjectName!=@Project UPDATE Team_2022 SET Mentor=@mentor WHERE TeamId = @TeamId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar);
            cmd.Parameters["@TeamId"].Value = TeamId;
            cmd.Parameters.Add("@Project", SqlDbType.NVarChar);
            cmd.Parameters["@Project"].Value = Project;
            cmd.Parameters.Add("@mentor", SqlDbType.NVarChar);
            cmd.Parameters["@mentor"].Value = mentor;
            return cmd;
        }
        

        public void UpdateMajor(string organizationName,string majorPhone)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateMajorCon(con, organizationName, majorPhone);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateMajorCon(SqlConnection con, string OrganizationName,string majorPhone)
        {

            string commandStr = "UPDATE OrgContact_2022 SET isMajor=0 WHERE OrganizationName=@OrganizationName and PhoneNumber!=@majorPhone";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@OrganizationName", SqlDbType.NVarChar);
            cmd.Parameters["@OrganizationName"].Value = OrganizationName;
            cmd.Parameters.Add("@majorPhone", SqlDbType.NVarChar);
            cmd.Parameters["@majorPhone"].Value = majorPhone;
            return cmd;
        }

        public void UpdateConStatus(string phone,int StatusId)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateConStat(con, phone, StatusId);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateConStat(SqlConnection con, string phoneNumber,int StatusId)
        {

            string commandStr = "UPDATE OrgContact_2022 SET ContactStatus=@StatusId WHERE PhoneNumber=@phoneNumber";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar);
            cmd.Parameters["@phoneNumber"].Value = phoneNumber;
            cmd.Parameters.Add("@StatusId", SqlDbType.Int);
            cmd.Parameters["@StatusId"].Value = StatusId;
            return cmd;
        }
        public void UpdateConStatusByOrgName(string orgName)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateConStatusByOrgNameCommand(con, orgName);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateConStatusByOrgNameCommand(SqlConnection con, string orgName)
        {

            string commandStr = "UPDATE OrgContact_2022 SET ContactStatus=0 WHERE OrganizationName=@orgName";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@orgName", SqlDbType.NVarChar);
            cmd.Parameters["@orgName"].Value = orgName;
            return cmd;
        }

        public List<StudentsTeam> ReadTeamSt(string teamid)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStuTeam(con, teamid);
                List<StudentsTeam> StudentTableList = new List<StudentsTeam>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    StudentsTeam ST = new StudentsTeam();
                    ST.TeamId = (string)dataReader["TeamId"];
                    ST.FirstName = (string)dataReader["FirstName"];
                    ST.PhoneNumber = (string)dataReader["PhoneNumber"];
                    ST.LastName = (string)dataReader["LastName"];
                    StudentTableList.Add(ST);
                }
                return StudentTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandStuTeam(SqlConnection con, string teamid)
        {
            string commandStr = "select * from Team_2022 t join Student_2022 s on t.TeamId = s.TeamId where t.TeamId=@teamid";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@teamid", SqlDbType.NVarChar);
            cmd.Parameters["@teamid"].Value = teamid;
            return cmd;
        }
        public List<StudentsTeam> ReadSpeTeam(string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandStuBySpe(con,spe);
                List<StudentsTeam> StudentTableList = new List<StudentsTeam>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    StudentsTeam ST = new StudentsTeam();
                    ST.TeamId = (string)dataReader["TeamId"];
                    ST.FirstName = (string)dataReader["FirstName"];
                    ST.PhoneNumber = (string)dataReader["PhoneNumber"];
                    ST.LastName = (string)dataReader["LastName"];
                    ST.AdditionalInfo = (string)dataReader["AdditionalInfo"];
                    ST.IsAccepted = (Boolean)dataReader["IsAccepted"];
                    ST.SpecializationName = (string)dataReader["SpecializationName"];
                    StudentTableList.Add(ST);
                }
                return StudentTableList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of review", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand createSelectCommandStuBySpe(SqlConnection con,string spe)
        {
            string commandStr = "select * from Team_2022 t join Student_2022 s on t.TeamId = s.TeamId join Specialization_2022 sp on s.SpecializationId=sp.SpecializationId where sp.SpecializationName=@spe";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            return cmd;
        }

        public int InsertMen(Mentor mentor)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertMen(mentor, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (SqlException exception)
            {
                if (exception.Number == 2627) // Cannot insert duplicate key row in object error
                {
                    UpdateMentorS(mentor);
                    return (1);
                }
                else
                    throw new Exception("Failed in Insert", exception);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertMen(Mentor mentor, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Mentors_2022] (PhoneNumber,FirstName,LastName,Notes,Mail,SpecializationId,MentorStatus,IsJudge) " +
                "VALUES('" + mentor.PhoneNumber + "','" + mentor.FirstName + "','" + mentor.LastName + "','" + mentor.Notes + "','" + mentor.Mail + "','" + mentor.SpecializationId+ "','" + mentor.MentorStatus + "','" + mentor.IsJudge + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public void UpdateMentorS(Mentor mentor)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = UpdateMentorSCommand(mentor, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand UpdateMentorSCommand(Mentor mentor, SqlConnection con)
        {
            string commandStr = "UPDATE Mentors_2022 SET MentorStatus=1,FirstName=@FirstName,LastName=@LastName,Notes=@Notes,Mail=@Mail,SpecializationId=@SpecializationId  WHERE PhoneNumber = @phone";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar);
            cmd.Parameters["@phone"].Value = mentor.PhoneNumber;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters["@FirstName"].Value = mentor.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters["@LastName"].Value = mentor.LastName;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar);
            cmd.Parameters["@Notes"].Value = mentor.Notes;
            cmd.Parameters.Add("@Mail", SqlDbType.NVarChar);
            cmd.Parameters["@Mail"].Value = mentor.Mail;
            cmd.Parameters.Add("@SpecializationId", SqlDbType.NVarChar);
            cmd.Parameters["@SpecializationId"].Value = mentor.SpecializationId;
            return cmd;
        }

        public void UpdateMentor(string phone)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = UpdateMentorSTaCommand(phone, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand UpdateMentorSTaCommand(string phone, SqlConnection con)
        {
            string commandStr = "UPDATE Mentors_2022 SET MentorStatus=0 WHERE PhoneNumber = @phone";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar);
            cmd.Parameters["@phone"].Value = phone;
            return cmd;
        }

        public List<Mentor> ReadSpeMen(string spe)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectGetMentorsListSp(con,spe);
                List<Mentor> MentorsList = new List<Mentor>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Mentor m = new Mentor();
                    m.PhoneNumber = (string)dataReader["PhoneNumber"];
                    m.FirstName = (string)dataReader["FirstName"];
                    m.LastName = (string)dataReader["LastName"];
                    m.Mail = (string)dataReader["Mail"];
                    m.Notes = (String)dataReader["Notes"];
                    m.SpecializationId = Convert.ToInt32(dataReader["SpecializationId"]);
                    if (dataReader["TeamId"] != System.DBNull.Value)
                        m.TeamId = (String)dataReader["TeamId"];
                    else
                        m.TeamId = " ";
                    MentorsList.Add(m);
                }
                return MentorsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading contact", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectGetMentorsListSp(SqlConnection con,string spe)
        {
            string commandStr = "select * from Mentors_2022 m left join Team_2022 t on t.Mentor=m.PhoneNumber where m.MentorStatus=1 and m.SpecializationId=@spe"; SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@spe", SqlDbType.NVarChar);
            cmd.Parameters["@spe"].Value = spe;
            return cmd;
        }

        public void UpdateAll()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = UpdateAllMentorSTaCommand(con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand UpdateAllMentorSTaCommand(SqlConnection con)
        {
            string commandStr = "UPDATE Mentors_2022 SET MentorStatus=0 WHERE MentorStatus = 1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public int InsertStu(Student student)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertStu(student, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (SqlException exception)
            {
                if (exception.Number == 2627) // Cannot insert duplicate key row in object error
                {
                    UpdateStudentS(student);
                    return (1);
                }
                else
                    throw new Exception("Failed in Insert", exception);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertStu(Student student, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Student_2022] (StudentId,PhoneNumber,FirstName,LastName,SpecializationId,StudentStatus) " +
                "VALUES('" + student.StudentId+"','"+ student.PhoneNumber + "','" + student.FirstName + "','" + student.LastName  + "','" + student.SpecializationId + "','" + student.StudentStatus + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public void UpdateStudentS(Student student)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = UpdateStuCommand(student, con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand UpdateStuCommand(Student student, SqlConnection con)
        {
            string commandStr = "UPDATE Student_2022 SET StudentStatus=1,FirstName=@FirstName,LastName=@LastName,SpecializationId=@SpecializationId  WHERE StudentId = @StudentId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar);
            cmd.Parameters["@phone"].Value = student.PhoneNumber;
            cmd.Parameters.Add("@StudentId", SqlDbType.NVarChar);
            cmd.Parameters["@StudentId"].Value = student.StudentId;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters["@FirstName"].Value = student.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters["@LastName"].Value = student.LastName;
            cmd.Parameters.Add("@SpecializationId", SqlDbType.NVarChar);
            cmd.Parameters["@SpecializationId"].Value = student.SpecializationId;
            return cmd;
        }

        public void UpdateStuStatus(string StuId)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = UpdateStuSta(con, StuId);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading of artical", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand UpdateStuSta(SqlConnection con, string StuId)
        {

            string commandStr = "UPDATE Student_2022 SET StudentStatus=0 WHERE StudentId= @StudentId";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@StudentId", SqlDbType.NVarChar);
            cmd.Parameters["@StudentId"].Value = StuId;


            return cmd;
        }

        public void UpdateAllStu()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = UpdateAllStuSTaCommand(con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in delete", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand UpdateAllStuSTaCommand(SqlConnection con)
        {
            string commandStr = "UPDATE Student_2022 SET StudentStatus=0 WHERE StudentStatus = 1";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        public List<ProjectPreference> ReadProByMen(string MenPHone)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createReadProByMen(con, MenPHone);
                List<ProjectPreference> PPList = new List<ProjectPreference>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ProjectPreference PP = new ProjectPreference();
                    PP.ProjectName = (string)dataReader["ProjectName"];
                        PP.TeamId = (string)dataReader["TeamId"];
                    PPList.Add(PP);
                }
                return PPList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading status", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createReadProByMen(SqlConnection con, string MenPHone)
        {
            string commandStr = "select * from ProjectPreference_2022  p join Team_2022 t on t.TeamId=p.TeamId and p.FinalSolution=1 join Mentors_2022 m on m.PhoneNumber=t.Mentor where m.PhoneNumber= @MenPHone";
            SqlCommand command = createCommand(con, commandStr);
            command.Parameters.Add("@MenPHone", SqlDbType.NVarChar);
            command.Parameters["@MenPHone"].Value = MenPHone;
            return command;
        }

        public int InsertPre(Presentation presentation)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertPre(presentation, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertPre(Presentation presentation, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Presentation_2022] (Judge1PhoneNumber,Judge2PhoneNumber,PresentationId,TeamId,StartDateAndTime) " +
                "VALUES('" + presentation.Judge1PhoneNumber + "','" + presentation.Judge2PhoneNumber + "','" + presentation.PresentationId + "','" + presentation.TeamId + "','" + presentation.StartDateAndTime + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public int InsertPresentationsDetails(PresentationsDetails presentationsDetails)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand command = CreateInsertPresentationsDetails(presentationsDetails, con);
                int affected = command.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed in Insert", ex);
            }

            finally
            {
                con.Close();
            }
        }
        SqlCommand CreateInsertPresentationsDetails(PresentationsDetails PresentationsDetails, SqlConnection con)
        {
            string insertStr = "INSERT INTO [Presentations_Details_2022] (pres1) " +
                "VALUES('" + PresentationsDetails.Pres1 + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }


        public List<Presentation> ReadPresentation()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandPresentations(con);
                List<Presentation> PresentationsList = new List<Presentation>();
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Presentation p = new Presentation();
                    p.Judge1PhoneNumber= (string)dataReader["Judge1PhoneNumber"];
                    p.Judge2PhoneNumber = (string)dataReader["Judge2PhoneNumber"];
                    p.PresentationId= Convert.ToInt32(dataReader["PresentationId"]);
                    p.TeamId = (string)dataReader["TeamId"];
                    p.StartDateAndTime = (DateTime)dataReader["StartDateAndTime"];
                    PresentationsList.Add(p);
                }
                return PresentationsList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed in reading presentions", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandPresentations(SqlConnection con)
        {
            string commandStr = "select * from Presentation_2022";
            SqlCommand cmd = createCommand(con, commandStr);
   
            return cmd;
        }

        public PresentationsDetails ReadPresentationsDetails()
        {
            SqlConnection con = null;

            try
            {
                con = Connect("webOsDB");
                SqlCommand selectCommand = createSelectCommandPresentationsDetails(con);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                PresentationsDetails pd = new PresentationsDetails();
                while (dataReader.Read())
                {
                    pd.Pres1 = (string)dataReader["Pres1"];
                }
                return pd;

            }
            catch (Exception ex)
            {
                throw new Exception("failed in reading presentions", ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand createSelectCommandPresentationsDetails(SqlConnection con)
        {
            string commandStr = "select * from Presentations_Details_2022";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }


    }


}