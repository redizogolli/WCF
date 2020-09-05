using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Operations
{
    public class Sql
    {
        //private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=TestDb;Encrypt=False;Integrated Security=True;User ID=\"DESKTOP-T1LCAF0\\Redi Zogolli\"";
        private readonly string _connectionString;

        public Sql(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Employee> GetEmployees()
        {
            var employeeList = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT e.EMPNO,e.FIRSTNME,e.MIDINIT,e.LASTNAME,e.WORKDEPT,e.PHONENO,e.HIREDATE,e.JOB,e.EDLEVEL,e.SEX,e.BIRTHDATE,e.SALARY,e.BONUS,e.COMM FROM Employee e", connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                employeeList.Add(new Employee
                                {
                                    EMPNO = reader["EMPNO"].ToString(),
                                    FIRSTNME = reader["FIRSTNME"].ToString(),
                                    MIDINIT = reader["MIDINIT"].ToString(),
                                    LASTNAME = reader["LASTNAME"].ToString(),
                                    WORKDEPT = reader["WORKDEPT"].ToString(),
                                    PHONENO = reader["PHONENO"].ToString(),
                                    HIREDATE = reader["HIREDATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["HIREDATE"]) : null,
                                    JOB = reader["JOB"].ToString(),
                                    EDLEVEL = Convert.ToInt16(reader["EDLEVEL"]),
                                    SEX = reader["SEX"].ToString(),
                                    BIRTHDATE = reader["BIRTHDATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["BIRTHDATE"]) : null,
                                    SALARY = reader["SALARY"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["SALARY"]) : null,
                                    BONUS = reader["BONUS"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["BONUS"]) : null,
                                    COMM = reader["COMM"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["COMM"]) : null,
                                });
                            }

                            reader.NextResult();
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }

            return employeeList;
        }
        public Employee GetEmployeeByNo(string employeeNo)
        {
            Employee employee = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Select * From Employee where EMPNO=@employeeNo", connection))
                {
                    command.Parameters.AddWithValue("@employeeNo", employeeNo);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        employee = new Employee
                        {
                            EMPNO = reader["EMPNO"].ToString(),
                            FIRSTNME = reader["FIRSTNME"].ToString(),
                            MIDINIT = reader["MIDINIT"].ToString(),
                            LASTNAME = reader["LASTNAME"].ToString(),
                            WORKDEPT = reader["WORKDEPT"].ToString(),
                            PHONENO = reader["PHONENO"].ToString(),
                            HIREDATE = reader["HIREDATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["HIREDATE"]) : null,
                            JOB = reader["JOB"].ToString(),
                            EDLEVEL = Convert.ToInt16(reader["EDLEVEL"]),
                            SEX = reader["SEX"].ToString(),
                            BIRTHDATE = reader["BIRTHDATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["BIRTHDATE"]) : null,
                            SALARY = reader["SALARY"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["SALARY"]) : null,
                            BONUS = reader["BONUS"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["BONUS"]) : null,
                            COMM = reader["COMM"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["COMM"]) : null,
                        };
                    }
                    reader.Close();
                    connection.Close();
                }
            }

            return employee;
        }
        public void CreateEmployee(Employee employee)
        {
            var query = @"INSERT INTO Employee (EMPNO, FIRSTNME, MIDINIT, LASTNAME, WORKDEPT, PHONENO, HIREDATE, JOB, EDLEVEL, SEX, BIRTHDATE, SALARY, BONUS, COMM) " +
                " VALUES (@empNo, @firstName, @midInit, @lastName, @workDept, @phoneNo, @hireDate, @job, @edLevel, @sex, @birthday, @salary, @bonus, @comm);";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@empNo",employee.EMPNO);
                    command.Parameters.AddWithValue("@firstName", employee.FIRSTNME);
                    command.Parameters.AddWithValue("@midInit", employee.MIDINIT);
                    command.Parameters.AddWithValue("@lastName", employee.LASTNAME);
                    command.Parameters.AddWithValue("@workDept", employee.WORKDEPT);
                    command.Parameters.AddWithValue("@phoneNo", employee.PHONENO);
                    command.Parameters.AddWithValue("@hireDate", employee.HIREDATE);
                    command.Parameters.AddWithValue("@job", employee.JOB);
                    command.Parameters.AddWithValue("@edLevel", employee.EDLEVEL);
                    command.Parameters.AddWithValue("@sex", employee.SEX);
                    command.Parameters.AddWithValue("@birthday", employee.BIRTHDATE);
                    command.Parameters.AddWithValue("@salary", employee.SALARY);
                    command.Parameters.AddWithValue("@bonus", employee.BONUS);
                    command.Parameters.AddWithValue("@comm", employee.COMM);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void UpdateEmployee(string employeeNo,Employee employee)
        {
            var query = "Update Employee " +
                        @"SET FIRSTNME = @firstName
                        , MIDINIT = @midInit
                        , LASTNAME = @lastName
                        , WORKDEPT = @workDept
                        ,PHONENO = @phoneNo
                        ,HIREDATE = @hireDate
                        ,JOB = @job
                        ,EDLEVEL = @edLevel
                        ,SEX = @sex
                        ,BIRTHDATE = @birthday
                        ,SALARY = @salary
                        ,BONUS = @bonus
                        ,COMM = @comm " +
                        " Where EMPNo = @empNO;";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empNo", employeeNo);
                    command.Parameters.AddWithValue("@firstName", employee.FIRSTNME);
                    command.Parameters.AddWithValue("@midInit", employee.MIDINIT);
                    command.Parameters.AddWithValue("@lastName", employee.LASTNAME);
                    command.Parameters.AddWithValue("@workDept", employee.WORKDEPT);
                    command.Parameters.AddWithValue("@phoneNo", employee.PHONENO);
                    command.Parameters.AddWithValue("@hireDate", employee.HIREDATE);
                    command.Parameters.AddWithValue("@job", employee.JOB);
                    command.Parameters.AddWithValue("@edLevel", employee.EDLEVEL);
                    command.Parameters.AddWithValue("@sex", employee.SEX);
                    command.Parameters.AddWithValue("@birthday", employee.BIRTHDATE);
                    command.Parameters.AddWithValue("@salary", employee.SALARY);
                    command.Parameters.AddWithValue("@bonus", employee.BONUS);
                    command.Parameters.AddWithValue("@comm", employee.COMM);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void DeleteEmployee(string employeeNo)
        {
            var query = "Delete Employee  Where EMPNo = @empNO;";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@empNo", employeeNo);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}