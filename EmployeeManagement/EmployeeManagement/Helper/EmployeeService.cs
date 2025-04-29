using EmployeeManagement.Models;
using MySql.Data.MySqlClient;
using System.Data;

public class EmployeeService
{
    private readonly string _connectionString;

    public EmployeeService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Save(EmployeeModel emp)
    {
        using (MySqlConnection conn = new MySqlConnection(_connectionString))
        {
            conn.Open();

            string query = emp.Id == 0
                ? @"INSERT INTO Employees (Name, Designation, DateOfJoin, DOB, Salary, Gender, State)
                   VALUES (@Name, @Designation, @DateOfJoin, @DOB, @Salary, @Gender, @State)"
                : @"UPDATE Employees SET
                    Name = @Name,
                    Designation = @Designation,
                    DateOfJoin = @DateOfJoin,
                    DOB = @DOB,
                    Salary = @Salary,
                    Gender = @Gender,
                    State = @State
                   WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@DateOfJoin", emp.DateOfJoin);
                cmd.Parameters.AddWithValue("@DOB", emp.DOB);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@State", emp.State);
                if (emp.Id != 0)
                    cmd.Parameters.AddWithValue("@Id", emp.Id);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<EmployeeModel> GetAll()
    {
        var employees = new List<EmployeeModel>();

        using (MySqlConnection conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Employees";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Designation = reader["Designation"].ToString(),
                            DateOfJoin = Convert.ToDateTime(reader["DateOfJoin"]),
                            DOB = Convert.ToDateTime(reader["DOB"]),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            Gender = reader["Gender"].ToString(),
                            State = reader["State"].ToString()
                        });
                    }
                }
            }
        }

        return employees;
    }

    public void Delete(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = "DELETE FROM Employees WHERE Id = @Id";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public EmployeeModel getemployeedetails(int id)
    {
        EmployeeModel employee = null;

        using (MySqlConnection conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Employees WHERE Id = @Id";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new EmployeeModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Designation = reader["Designation"].ToString(),
                            DateOfJoin = Convert.ToDateTime(reader["DateOfJoin"]),
                            DOB = Convert.ToDateTime(reader["DOB"]),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            Gender = reader["Gender"].ToString(),
                            State = reader["State"].ToString()
                        };
                    }
                }
            }
        }

        return employee;
    }

}