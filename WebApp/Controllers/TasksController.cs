using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApp.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApp.Controllers
{
    public class TasksController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select TaskID, Employee, Task, Details, CONVERT(varchar(10), DateOfCreation, 120) as DateOfCreation, CONVERT(varchar(10), Deadline, 120) as Deadline, Status from dbo.Tasks";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Tasks tasks)
        {
            try
            {
                //Console.OpenStandardOutput;
                Console.WriteLine(tasks.Deadline);

                DataTable table = new DataTable();
                string query = @"insert into dbo.Tasks (Employee, Task, Details, DateOfCreation, Deadline, Status) values ('" + tasks.Employee + @"','" + tasks.Task + @"','" + tasks.Details + @"','" + tasks.DateOfCreation + @"','" + tasks.Deadline + @"','" + tasks.Status + @"')";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }
        public string Put(Tasks tasks)
        {
            try
            {
                string dateofcreation = tasks.DateOfCreation.ToString().Split(' ')[0];
                string deadline = tasks.Deadline.ToString().Split(' ')[0];
                string query = @"update dbo.Tasks set Employee = '" + tasks.Employee + @"',Task = '" + tasks.Task + @"',Details = '" + tasks.Details + @"',DateOfCreation = '" + dateofcreation + @"',Deadline = '" + deadline + @"',Status = '" + tasks.Status + @"' where TaskID = '" + tasks.TaskID + @"' ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Update FAILED";
            }
        }
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"delete from dbo.Tasks where TaskID = " + id;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }
    }
}