using Microsoft.AspNetCore.Mvc;
using Dapper;
using Para.Data.Context;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController : ControllerBase
    {
        private readonly DapperContext _context;

        public CustomerReportController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomerReport")]
        public async Task<IActionResult> GetCustomerReport()
        {
            var query = @"
                SELECT 
                c.Id, c.FirstName, c.LastName, c.Email, c.CustomerNumber,
                cd.FatherName, cd.MotherName, cd.EducationStatus, ca.Country, ca.City,ca.AddressLine,cp.CountyCode ,cp.Phone
                FROM 
                Customer c
                LEFT JOIN 
                CustomerDetail cd ON c.Id = cd.CustomerId
                LEFT JOIN 
                CustomerAddress ca ON c.Id = ca.CustomerId
                LEFT JOIN 
                CustomerPhone cp ON c.Id = cp.CustomerId
                WHERE cd.IsActive = 1";

            using (var connection = _context.CreateConnection())
            {
                var customerReports = await connection.QueryAsync<CustomerReportResponse>(query);
                return Ok(customerReports);
            }
        }
    }
}