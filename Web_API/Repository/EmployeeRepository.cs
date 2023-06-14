using Web_API.context;
using Web_API.Contract;
using Web_API.Entities;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace Web_API.Repository
{
    public class EmployeeRepository : IEmpRepository
    {

        private readonly EmployeeContext employeeContext;

        public EmployeeRepository(EmployeeContext _employeeContext)
        {
            employeeContext = _employeeContext;
        }

        public void Delete(string email)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@PtEmail", email);
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeletePatient";

                    

                    cmd.ExecuteNonQuery();  // Delete Query Executed

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<NewPatient> GellAllPatient()
        {
            List<NewPatient> employee = new List<NewPatient>();
             
            using (var connection = employeeContext.CreateConnection())
            //using (var connection = employeeContext.SqlConnection())
            {

                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "AllPatient";

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        NewPatient emp = new NewPatient();

                        emp.Pt_Id = dr.GetInt32("Pt_Id");
                        emp.PtName = dr.GetString("PtName");
                        emp.PtEmail = dr.GetString("PtEmail");
                        emp.PtPhone = dr.GetString("PtPhone");
                        emp.PtAge = dr.GetString("PtAge");
                        emp.PtGender = dr.GetString("PtGender");
                        emp.PtAddress = dr.GetString("PtAddress");

                        
                        employee.Add(emp);


                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
                return employee;
            
            }
        }

        public Employee GetSingleEmployee(string email)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();

                    //SqlCommand cmd=conn.CreateCommand();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Users where Email=@Email";
                    cmd.Parameters.AddWithValue("@Email",email);


                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.Name = dr.GetString("Name");
                        emp.Email = dr.GetString("Email");
                        emp.PhoneNo = dr.GetString("PhoneNo");
                        emp.Password1 = dr.GetString("Password1");
                        return emp;
                    }
                    else
                    {
                        return null;
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public NewPatient Get_SinglePatient_Details(string email)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();

                    //SqlCommand cmd=conn.CreateCommand();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.Parameters.AddWithValue("@PtEmail", email);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from NewPatient where PtEmail=@PtEmail";
                   


                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        NewPatient emp = new NewPatient();
                        emp.Pt_Id = dr.GetInt32("Pt_Id");
                        emp.PtName = dr.GetString("PtName");
                        emp.PtEmail = dr.GetString("PtEmail");
                        emp.PtPhone = dr.GetString("PtPhone");
                        emp.PtAge = dr.GetString("PtAge");
                        emp.PtGender = dr.GetString("PtGender");
                        emp.PtAddress = dr.GetString("PtAddress");
                        return emp;
                    }
                    else
                    {
                        return null;
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool checkMail(string Email)
        {

            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.CommandText = "select * from User where Email = @Email";


                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Employee emp = new Employee();

                        emp.Email = dr.GetString("Email");

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    dr.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }


        public bool New_Patient_EMailcheck(string PtEmail)
        {

            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PtEmail", PtEmail);
                    cmd.CommandText = "select * from NewPatient where PtEmail = @PtEmail";


                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        NewPatient emp = new NewPatient();

                        emp.PtEmail = dr.GetString("PtEmail");

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    dr.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }


        public bool Check_Existing_Appointment (DateTime Appointment_Date)
        {

            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Appointment_Date", Appointment_Date);
                    cmd.CommandText = "select * from Appointment where Appointment_Date = @Appointment_Date";


                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Appointment amp = new Appointment();

                         amp.Appointment_Date= dr.GetDateTime("Appointment_Date");

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    dr.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }


        public void Insert(Employee emp)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                  
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertEmployee";

                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@PhoneNo", emp.PhoneNo);
                    cmd.Parameters.AddWithValue("@Password1", emp.Password1);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                        throw; 
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdatePatient(string email,NewPatient emp)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.Parameters.AddWithValue("@PtName", emp.PtName);
                    cmd.Parameters.AddWithValue("@PtEmail", email);
                    cmd.Parameters.AddWithValue("@PtPhone", emp.PtPhone);
                    cmd.Parameters.AddWithValue("@PtAge", emp.PtAge);
                    cmd.Parameters.AddWithValue("@PtGender", emp.PtGender);
                    cmd.Parameters.AddWithValue("@PtAddress", emp.PtAddress);
                    
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "UpdatePatient";

                   
                   
                    cmd.CommandText = "Update NewPatient SET  PtName=@PtName  , PtPhone=@PtPhone , PtAge = @PtAge , PtGender=@PtGender,PtAddress=@PtAddress where PtEmail=@PtEmail;";

                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw;

                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //  SELECT * from UserRegister where LoginName=@LoginName AND Password=@Password;


        //<-------------------Login_Method Start----------------->

        public Employee Login(string Email, string Password1)
        {
            //   SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Mini_Project;Integrated Security=True;");


            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;

                    cmd.CommandType = CommandType.Text;




                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password1", Password1);

                    cmd.CommandText = "SELECT * from AdminData where Email = @Email and Password1 =@Password1";
                    //  cmd.CommandText = "GetLogin";



                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Employee emp = new Employee();

                        emp.Email = dr.GetString("Email");
                        emp.Password1 = dr.GetString("Password1");
                        emp.Name = dr.GetString("Name");

                        return emp;
                    }
                    else
                    {
                        return null;
                    }

                    dr.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        //<-------------------Login_Method End----------------->

        //<---------new patient Method start >


        public void New_Patient_Insert(NewPatient emp)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertNewPatient";

                    cmd.Parameters.AddWithValue("@PtName", emp.PtName);
                    cmd.Parameters.AddWithValue("@PtEmail", emp.PtEmail);
                    cmd.Parameters.AddWithValue("@PtPhone", emp.PtPhone);
                    cmd.Parameters.AddWithValue("@PtAge", emp.PtAge);
                    cmd.Parameters.AddWithValue("@PtGender", emp.PtGender);
                    cmd.Parameters.AddWithValue("@PtAddress", emp.PtAddress);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //<-------- new patient Method End  ------>

        //  <----------New_Appointment_Start -------->

        public void New_Appointment(Appointment value)
        {


            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Insert_Appointment";

                    cmd.Parameters.AddWithValue("@Appointment_Name", value.Appointment_Name);
                    cmd.Parameters.AddWithValue("@Appointment_Email", value.Appointment_Email);
                    cmd.Parameters.AddWithValue("@Appointment_Disease", value.Appointment_Disease);
                    cmd.Parameters.AddWithValue("@Appointment_Description", value.Appointment_Description);
                    cmd.Parameters.AddWithValue("@Appointment_Date", value.Appointment_Date);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }


        }


        //<----------New_Appointment_End -------->

        public List<Appointment> SearchPatient(string Appointment_Email)
        {
            List<Appointment> appointments = new List<Appointment>();

          
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Appointment_Email", Appointment_Email);
                  
                     cmd.CommandText = "SELECT * from Appointment where Appointment_Email = @Appointment_Email";
                    //  cmd.CommandText = "GetLogin"



                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Appointment emp = new Appointment();

                        emp.Appointment_Id = dr.GetInt32("Appointment_Id");
                        emp.Appointment_Name = dr.GetString("Appointment_Name");
                        emp.Appointment_Email = dr.GetString("Appointment_Email");
                        emp.Appointment_Date = dr.GetDateTime("Appointment_Date");
                        emp.Appointment_Disease = dr.GetString("Appointment_Disease");
                        emp.Appointment_Description=dr.GetString("Appointment_Description");

                        appointments.Add(emp);
                    }
                    

                    dr.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    connection.Close();
                }
                return appointments;
            }

        }

        public Appointment Get_SingleAppointment_Details(int id)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();

                    //SqlCommand cmd=conn.CreateCommand();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.Parameters.AddWithValue("@Appointment_Id", id);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Appointment where Appointment_Id=@Appointment_Id";



                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        Appointment emp = new Appointment();
                        emp.Appointment_Id = dr.GetInt32("Appointment_Id");
                        emp.Appointment_Name = dr.GetString("Appointment_Name");
                        emp.Appointment_Email = dr.GetString("Appointment_Email");
                        emp.Appointment_Date = dr.GetDateTime("Appointment_Date");
                        emp.Appointment_Disease = dr.GetString("Appointment_Disease");
                        emp.Appointment_Description = dr.GetString("Appointment_Description");
                       
                        return emp;
                    }
                    else
                    {
                        return null;
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Single_Appointment_Delete(int _id)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Appointment_Id", _id);
                    cmd.Connection = (SqlConnection)connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Delete_Single_Appointment";



                    cmd.ExecuteNonQuery();  // Delete Query Executed

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            };
        }

        public void Update_Appointment(int id, Appointment appointment)
        {
            using (var connection = employeeContext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)connection;
                    cmd.Parameters.AddWithValue("@Appointment_Name", appointment.Appointment_Name);
                    cmd.Parameters.AddWithValue("@Appointment_Id", id);
                    cmd.Parameters.AddWithValue("@Appointment_Email", appointment.Appointment_Email);
                    cmd.Parameters.AddWithValue("@Appointment_Date", appointment.Appointment_Date);
                    cmd.Parameters.AddWithValue("@Appointment_Disease", appointment.Appointment_Disease);
                    cmd.Parameters.AddWithValue("@Appointment_Description", appointment.Appointment_Description);

                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "UpdatePatient";



                    cmd.CommandText = "Update Appointment SET  Appointment_Name=@Appointment_Name  , Appointment_Email=@Appointment_Email , Appointment_Date = @Appointment_Date , Appointment_Disease=@Appointment_Disease, Appointment_Description=@Appointment_Description  where  Appointment_Id=@Appointment_Id;";

                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw;

                }
                finally
                {
                    connection.Close();
                }
            }
        }


        //public NewPatient Get_SinglePatient_Details(string email)
        //{
        //    using (var connection = employeeContext.CreateConnection())
        //    {
        //        try
        //        {
        //            connection.Open();

        //            //SqlCommand cmd=conn.CreateCommand();

        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = (SqlConnection)connection;
        //            cmd.CommandType = CommandType.Text;

        //            cmd.CommandText = "select * from Users where Email=@Email";
        //            cmd.Parameters.AddWithValue("@Email", email);


        //            SqlDataReader dr = cmd.ExecuteReader();


        //            if (dr.Read())
        //            {
        //                Employee emp = new Employee();
        //                emp.Name = dr.GetString("Name");
        //                emp.Email = dr.GetString("Email");
        //                emp.PhoneNo = dr.GetString("PhoneNo");
        //                emp.Password1 = dr.GetString("Password1");
        //                return emp;
        //            }
        //            else
        //            {
        //                return null;
        //            }

        //            dr.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}


        //<---------------------------------------------------------------->


    }
}
