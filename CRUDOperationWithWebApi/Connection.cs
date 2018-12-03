using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CRUDOperationWithWebApi.Models;

namespace CRUDOperationWithWebApi
{
    public class Connection
    {
        SqlConnection con = new SqlConnection();
        public Connection()
        {
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\db\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            con.Open();

            //using (SqlConnection con = new SqlConnection())
            //{
            //    con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\db\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            //    con.Open();
        }

        public bool DeleteEmployee(int empCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spDeleteEmployee", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpCode", empCode);

                    int output = cmd.ExecuteNonQuery();
                    return output > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateEmployee", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpCode", emp.EmpCode);
                    cmd.Parameters.AddWithValue("@FName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LName", emp.LastName);
                    cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                    int output = cmd.ExecuteNonQuery();
                    return output > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddEmployee(Employee emp)
        {            
            try
            {
                using (SqlCommand cmd = new SqlCommand("spAddEmployee", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpCode", emp.EmpCode);
                    cmd.Parameters.AddWithValue("@FName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LName", emp.LastName);
                    cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                    int output = cmd.ExecuteNonQuery();
                    return output > 0 ? true : false;                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee GetEmployeeByCode(int empCode)
        {
            Employee emp = new Employee();
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetEmployeeByCode", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpCode", empCode);
                    var reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            emp.EmpCode = reader["EmpCode"] != null ? Convert.ToInt32(reader["EmpCode"]) : 0;
                            emp.FirstName = reader["FirstName"] != null ? Convert.ToString(reader["FirstName"]) : null;
                            emp.LastName = reader["LastName"] != null ? Convert.ToString(reader["LastName"]) : null;
                            emp.Designation = reader["Designation"] != null ? Convert.ToString(reader["Designation"]) : null;
                            emp.Salary = reader["Salary"] != null ? Convert.ToInt32(reader["Salary"]) : 0;
                        }
                    }
                    else
                    {
                        return emp;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return emp;

        }
        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlCommand cmd = new SqlCommand("spGetEmployees", con))
            {
                try
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmpCode = reader["EmpCode"] != null ? Convert.ToInt32(reader["EmpCode"]) : 0,
                                FirstName = reader["FirstName"] != null ? Convert.ToString(reader["FirstName"]) : null,
                                LastName = reader["LastName"] != null ? Convert.ToString(reader["LastName"]) : null,
                                Designation = reader["Designation"] != null ? Convert.ToString(reader["Designation"]) : null,
                                Salary = reader["Salary"] != null ? Convert.ToInt32(reader["Salary"]) : 0,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    //return employees;
                    throw ex;
                }
                return employees;
            }
        }

    }
}