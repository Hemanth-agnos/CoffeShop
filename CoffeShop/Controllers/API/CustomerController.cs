using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeShop.Controllers.API
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        public ICustomerService CustomerService;
        public CustomerController(ICustomerService customerService)
        {
            this.CustomerService = customerService;
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return this.CustomerService.GetAllCustomers();
        }

        [HttpGet]
        [Route("{id}")]
        public Customer GetCustomerById(int id)
        {
            return this.CustomerService.GetCustomer(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("add")]
        public bool AddCustomer([FromBody] Customer customer)
        {
            return this.CustomerService.AddCustomer(customer);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("update/{id}")]
        public bool UpdateCustomer(int id, [FromBody] Customer customer)
        {
            return this.CustomerService.UpdateCustomer(customer);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteCustomer(int id)
        {
            this.DeleteCustomer(id);
        }
    }
}