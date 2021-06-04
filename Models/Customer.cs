using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class Address 
    {
        public string AddressLine1;
        public string AddressLine2;
        public string City;
        public string State;
        public string Country;
        public string PostalCode;
    }
}
