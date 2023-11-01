using FinancePlan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Fake> Get()
        {
            return GetEmployees();
        }
        private List<Fake> GetEmployees()
        {
            return new List<Fake>()
            {
                new Fake()
                {
                    Id = 1,
                    FirstName= "John",
                    LastName = "Smith",
                    EmailId ="John.Smith@gmail.com"
                },
                new Fake()
                {
                Id = 2,
                    FirstName= "Jane",
                    LastName = "Doe",
                    EmailId ="Jane.Doe@gmail.com"
                }
            };
        }
    }
}
