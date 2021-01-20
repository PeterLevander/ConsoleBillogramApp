using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAPITest.Module
{
    public class CustomerResponse
    {
        //public string Id { get; set; }
        public string status { get; set; }
        public Customer data { get; set; }
    }

    public class Customer
    {
        public int? customer_no { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string org_no { get; set; }
        public Delivery_address delivery_address { get; set; }
        public Contact contact { get; set; }
        public Address address { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
        //public Company_type company_type { get; set; }
    }

    public enum Company_type
    {
        business,
        individual,
        foreignbusiness,
        foreignindividual
    }
    public class Delivery_address
    {
        public string city { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string careof { get; set; }
        public string street_address { get; set; }
    }
    public class Contact
    {
        public string phone { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public Contact(string Phone, string Email, string Name)
        {
            phone = Phone;
            email = Email;
            name = Name;
        }
    }
    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string careof { get; set; }
        public bool use_careof_as_attention { get; set; }
        public string street_address { get; set; }

        public Address(string City, string Country, string Zipcode, string Careof, string Street_address, bool Ucaa)
        {
            city = City;
            country = Country;
            zipcode = Zipcode;
            careof = Careof;
            use_careof_as_attention = Ucaa;
            street_address = Street_address;
        }
    }


}
