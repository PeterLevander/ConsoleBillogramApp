using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using ConsoleAPITest.Module;
using System.Linq;

namespace ConsoleAPITest
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }



    class Program
    {
        static HttpClient client = new HttpClient();

        #region Hämtat exempel
        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tCategory: {product.Category}");
        }

        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }
        #endregion

        #region Mina tester

        #region Customer
        static void ShowCustomer(CustomerResponse customer)
        {
            Console.WriteLine($"Status: {customer.status} \tName: {customer.data.name} \tEmail: {customer.data.contact.email}" +
                $"\tAdress: {customer.data.address.street_address} \tNotes: {customer.data.notes} "
                );
        }

        static async Task<CustomerResponse> GetCustomerAsync(string path)
        {
            CustomerResponse customerResp = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                customerResp = await response.Content.ReadAsAsync<CustomerResponse>();
                var cont = await response.Content.ReadAsStringAsync();
                Console.WriteLine(cont);
            }
            return customerResp;
        }
        static async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            CustomerResponse customerResponse = null;
            try
            {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/v2/customer", customer);
            //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var cont = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("RESPONS av CustomerCreate");
                    customerResponse = await response.Content.ReadAsAsync<CustomerResponse>();
                    Console.WriteLine(JsonSerializer.Serialize(customerResponse));

                }

            // return URI of the created resource.
            return response.Headers.Location;
            }
            catch (Exception)
            {

                throw;
            }
            return customerResponse;
        }
        static async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            string xxx = "stannahär";
            var jsonCustomer = JsonSerializer.Serialize(customer);
            //string special = '{"address": {"street_address":"Testadress 12 - Special"}}';

            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/v2/customer/{customer.customer_no}", customer);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            customer = await response.Content.ReadAsAsync<Customer>();
            return customer;
        }
        #endregion

        #region Billogram

        static void ShowBillogram(BillogramResponse billogram)
        {
            Console.WriteLine($"BILLOGRAMResponse \nStatus: {billogram.status} \tID: {billogram.data.Id} \tName: {billogram.data.customer.name} \tCustomerNo: {billogram.data.customer.customer_no}" +
                $"\tTotalSum: {billogram.data.Total_sum} \tInvoiceNo: {billogram.data.Invoice_no}" +
                $"\tRecipient_Url: {billogram.data.Recipient_url} " +
                $"\tAdress: {billogram.data.customer.address.street_address}  "
                );
        }

        static async Task<BillogramResponse> GetBillogramAsync(string path)
        {
            BillogramResponse billogramResp = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {

                var cont = await response.Content.ReadAsStringAsync();
                Console.WriteLine(cont);
                billogramResp = await response.Content.ReadAsAsync<BillogramResponse>();
            }
            return billogramResp;
        }
        /// <summary>
        /// Skapa ett billogram 
        /// </summary>
        /// <param name="billogram"></param>
        /// <returns>Uri</returns>
        static async Task<BillogramResponse> CreateBillogramAsync(BillogramCreate billogram)
        {
            BillogramResponse billogramResp = null;
            var jsonStr = JsonSerializer.Serialize(billogram);
            Console.WriteLine(jsonStr);

            Uri respUri = null;
            try
            {
                // skicka på ett Billogram
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "api/v2/billogram", billogram);
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    respUri = response.Headers.Location;
                    var cont = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(cont);
                     billogramResp = await response.Content.ReadAsAsync<BillogramResponse>();
                }
                else
                {
                    Console.WriteLine(jsonStr);
                    Console.WriteLine($"ERROR: StatusCode: {response.StatusCode} ResonPhrese: {response.ReasonPhrase}");
                    Console.WriteLine(JsonSerializer.Serialize(response));
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                Console.WriteLine($"ERROR: Msg: {msg}");
                throw;
            }

            // return URI of the created resource.
            return billogramResp;
        }

        #endregion

        #endregion

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            string username = "8317-98DpNRv3";
            string password = "4a59915ada274f92ac6e128491855841";
            string authKey = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            // Update port # in the following line.
            //client.BaseAddress = new Uri("http://localhost:64195/");
            client.BaseAddress = new Uri("https://sandbox.billogram.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + authKey);

            try
            {
                //Mitt försök att hämta Customer
                CustomerResponse customerResp = new CustomerResponse();// {data.customer_no = 12345 };
                //customer.data.company_type = Customer.Company_type.foreignindividual;
                var myurl = "api/v2/customer/";
                customerResp = await GetCustomerAsync(myurl + "12345");
                ShowCustomer(customerResp);
                Console.WriteLine($"GetCustomer is done!!");
                // Sparaundan adress
                string sparAdress = customerResp.data.delivery_address.street_address;

                //Uppdatera Customer
                Console.WriteLine("Updating streetadress");
                Customer customer = new Customer();
                customer = customerResp.data;
                //customer.customer_no = customerResp.data.customer_no;
                //customer.address = new Address();
                customer.delivery_address = null;
                customer.address.street_address = "Ny test adress 777 nr 7";
                //customer = await UpdateCustomerAsync(customer);

                //Create Customer
                Customer nyCustomer = new Customer();
                nyCustomer.customer_no = 12370;
                nyCustomer.name = "Levander Company";
                //nyCustomer.org_no = "554466-1234";
                nyCustomer.address = new Address("Täby", "SE", "18773", "", "Nathoertsvagen 56", false);
                nyCustomer.contact = new Contact("08-887766", "goran.test@test.com", "Goran Engstrom");
                //var respCustomer = await CreateCustomerAsync(nyCustomer);
                //Console.WriteLine(respCustomer);

                //Get Billogram 
                BillogramResponse billogramResp = new BillogramResponse();
                myurl = "api/v2/billogram/";
                billogramResp = await GetBillogramAsync(myurl + "wdu4gsU");
                ShowBillogram(billogramResp);
                Console.WriteLine($"GetBillogram is done!!");

                //POST Billogram
                BillogramCreate billogramCreate = new BillogramCreate();
                //billogram.Invoice_date = DateTime.Now;
                //billogram.Due_date = DateTime.Now.AddDays(30);
                CustomerMini smallCustomer = new CustomerMini();
                smallCustomer.customer_no = 12370;
                billogramCreate.customer = smallCustomer;
                // Skapa ett item
                ItemMini item = new ItemMini();
                item.item_no = "4"; // Deltagaravg Rätt focus SME
                item.title = "Produkt försäljning";
                item.count = 1;
                item.vat = 20;
                item.price = 125;
                ItemMini item2 = new ItemMini();
                item2.title = "Detta kan vara en textrad";
                ItemMini item3 = new ItemMini();
                item3.title = "Administattionskostnad";
                item3.count = 1;
                item3.price = 45;
                item3.vat = 12;
                ItemMini[] items = new ItemMini[3] { item, item2, item3 };
                billogramCreate.items = items;
                //Skapa ett Info
                Info info = new Info();
                info.message = "Skapat via ConsoleAPI_Test";
                billogramCreate.info = info;
                On_success on_Success = new On_success();
                on_Success.command = "send";
                on_Success.method = "Email";
                billogramCreate.on_success = on_Success;
                // Create Billogram
                billogramResp = await CreateBillogramAsync(billogramCreate);
                if(billogramResp != null)
                {
                    ShowBillogram(billogramResp);
                }

                //-----------------------------------------------

                //// Create a new product
                //Product product = new Product
                //{
                //    Name = "Gizmo",
                //    Price = 100,
                //    Category = "Widgets"
                //};

                //var url = await CreateProductAsync(product);
                //Console.WriteLine($"Created at {url}");

                //// Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
