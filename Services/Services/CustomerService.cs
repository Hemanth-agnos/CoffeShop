using Models;
using PetaPoco;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        public Database DB { get; set; }

        private readonly AutoMapper.IMapper Mapper;

        public CustomerService(Database db, AutoMapper.IMapper mapper)
        {
            this.DB = db;
            this.Mapper = mapper;
        }

        public bool AddCustomer(Customer customer)
        {
            var customerModel = this.Mapper.Map<Customer, DataModel.Customer>(customer);
            return Convert.ToInt32(this.DB.Insert(customerModel)) > 0;
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = this.DB.Fetch<DataModel.Customer>(string.Empty);
            return this.Mapper.Map<List<DataModel.Customer>, List<Customer>>(customers);
        }

        public Customer GetCustomer(int customerId)
        {
            var customer = this.DB.SingleOrDefault<DataModel.Customer>("Where Id = @0", customerId);
            return this.Mapper.Map<DataModel.Customer, Customer>(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            var customerModel = this.Mapper.Map<Customer, DataModel.Customer>(customer);
            return this.DB.Update(customerModel) > 0;
        }

        public bool DeleteCustomer(int cusomerId)
        {
            return this.DB.Delete<DataModel.Customer>(cusomerId) > 0;
        }
    }
}
