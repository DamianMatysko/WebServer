using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = "SELECT EmployeeID, EmployeeName, Department, MailID, Password, convert(varchar(10), DOJ, 120) as DOJ  FROM dbo.Employees";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var data = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                data.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee employee)
        {
            try
            {
                DateTime now = DateTime.Now;
                DataTable table = new DataTable();
                string query = @"insert into dbo.Employees (EmployeeName, Department, MailID, DOJ, Password) values ('" + employee.EmployeeName + "', '" + employee.Department + "','" + employee.MailID + "','" + now + "', '" + employee.Password + "')";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var data = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    data.Fill(table);
                }

                return "Added successfully";
            }
            catch (Exception)
            {
                return "Failed to add";
            }
        }
        public string Put(Employee employee)
        {
            try
            {
                DataTable table = new DataTable();
                string query = "update dbo.Employees set EmployeeName = '" + employee.EmployeeName + "', Department = '" + employee.Department + "', MailID = '" + employee.MailID + "', DOJ = '" + employee.DOJ + "' where EmployeeID = " + employee.EmployeeID + "";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var data = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    data.Fill(table);
                }

                return "Updated successfully!";
            }
            catch (Exception)
            {
                return "Failed to update!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = "delete from dbo.Employees where EmployeeID = " + id + "";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var data = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    data.Fill(table);
                }

                return "Deleted successfully!";
            }
            catch (Exception)
            {
                return "Failed to delete!";
            }
        }
    }
}
