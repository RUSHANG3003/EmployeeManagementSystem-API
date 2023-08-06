using EmployeeManagementSystem.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagementSystem.DAL
{
    public class EmployeeRepository
    {
        string connectionString;

        public EmployeeRepository()
        {
            connectionString = "Server=RUSHANG; Database=EmployeeManagement; Trusted_Connection = true; TrustServerCertificate=true";
        }

        //GetAllEmployee
        public List<Employee>? GetAllEmployee()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<Employee> employeelist = new List<Employee>();
                string query = "Select * From Employee";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Employee employee = new Employee();

                    employee.Id = Guid.Parse(dataReader["Id"].ToString());

                    employee.Name = dataReader["Name"].ToString();
                    employee.Designation = dataReader["Designation"].ToString();
                    employee.Address = dataReader["Address"].ToString();
                    employee.Age = dataReader["Age"].ToString();
                    employee.PhoneNo = dataReader["PhoneNo"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.PinCode = dataReader["PinCode"].ToString();
                    employee.Email = dataReader["Email"].ToString();

                    employeelist.Add(employee);
                }

                cmd.Connection.Close();

                return employeelist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddEmployee
        public Guid? AddEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [Employee](Id, Name, Designation, Address, Age, Gender, Email, PinCode, PhoneNo) Values(@Id, @Name, @Designation, @Address, @Age, @Gender, @Email, @PinCode, @PhoneNo)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("Designation", SqlDbType.VarChar).Value = employee.Designation;
                cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = employee.Name;
                cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = employee.Address;
                cmd.Parameters.Add("Gender", SqlDbType.VarChar).Value = employee.Gender;
                cmd.Parameters.Add("PinCode", SqlDbType.VarChar).Value = employee.PinCode;
                cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = employee.Email;
                cmd.Parameters.Add("PhoneNo", SqlDbType.VarChar).Value = employee.PhoneNo;
                cmd.Parameters.Add("Age", SqlDbType.VarChar).Value = employee.Age;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                if (Result > 0)
                    return id;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //GetEmployee
        public Employee? GetEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Employee? returnvalue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select * From Employee";

                //Validation Logic
                if (employee != null)
                {
                    if (employee.Id != null)
                    {
                        query += " Where id= @id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "id",
                            Value = employee.Id
                        });
                    }

                    if (!string.IsNullOrEmpty(employee.Email))
                    {
                        query += " Where Email= @Email";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Email",
                            Value = employee.Email
                        });
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    returnvalue = new Employee();
                    returnvalue.Id = Guid.Parse(dataReader["Id"].ToString());

                    returnvalue.Designation = dataReader["Designation"].ToString();
                    returnvalue.Name = dataReader["Name"].ToString();
                    returnvalue.Email = dataReader["Email"].ToString();
                    returnvalue.PinCode = dataReader["PinCode"].ToString();
                    returnvalue.Address = dataReader["Address"].ToString();
                    returnvalue.Age = dataReader["Age"].ToString();
                    returnvalue.Gender = dataReader["Gender"].ToString();
                    returnvalue.PhoneNo = dataReader["PhoneNo"].ToString();

                    break;

                }

                cmd.Connection.Close();

                return returnvalue;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Update Employee
        public Employee? UpdateEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Employee SET [Name]=@Name,Designation= @Designation,Address= @Address,Age= @Age,Gender= @Gender,Email= @Email,PinCode= @PinCode,PhoneNo= @PhoneNo Where [Id] = @id";

                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = employee.Id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = employee.Name;
                cmd.Parameters.Add("@designation", SqlDbType.VarChar).Value = employee.Designation;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = employee.Gender;
                cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = employee.Address;
                cmd.Parameters.Add("@pincode", SqlDbType.VarChar).Value = employee.PinCode;
                cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = employee.Age;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = employee.Email;
                cmd.Parameters.Add("@phoneNo", SqlDbType.VarChar).Value = employee.PhoneNo;
                cmd.Connection.Open();
                var dataRaeder = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return employee;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }


        }


        //Delete Employee
        public bool Delete(Guid id)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection=conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Delete From Employee Where id=@id";

                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            if(result>0)
                return true;
            else
                return false;

        }
        

    }
}