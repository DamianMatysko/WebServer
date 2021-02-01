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
            string query = @"select EmployeeID, EmployeeName, Department, MailID, CONVERT(varchar(10), DOJ, 120) as DOJ from dbo.Employee";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Employee employee)
        {

            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Employee (EmployeeName, Department, MailID, DOJ) values ('" + employee.EmployeeName + @"','" + employee.Department + @"','" + employee.MailID + @"','" + employee.DOJ + @"')";
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
        public string Put(Employee employee)
        {
            try
            {
                string doj = employee.DOJ.ToString().Split(' ')[0];
                string query = @"update dbo.Employee set EmployeeName = '" + employee.EmployeeName + @"',Department = '" + employee.Department +@"',MailID = '" + employee.MailID + @"',DOJ = '" + doj + @"' where EmployeeID = '" + employee.EmployeeID+@"' ";
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
                string query = @"delete from dbo.Employee where EmployeeID = " + id;
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
