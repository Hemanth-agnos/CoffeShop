using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        bool AddCustomer(Customer customer);

        List<Customer> GetAllCustomers();

        Customer GetCustomer(int cusomerId);

        bool UpdateCustomer(Customer customer);

        bool DeleteCustomer(int cusomerId);
    }
}
