using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using S6_JQUERYMVCEF.DatabaseFirst.Models;

namespace S6_JQUERYMVCEF.DatabaseFirst.Repository
{
    public class CustomerRepo
    {
        //Listar todos
        public static IEnumerable<Customer> GetCustomers()
        {
            using var data = new SalesContext();
            var customers = data.Customer.ToList();
            return customers;
        }

        //USAR UN STORM PROCEDIRE 
        public static IEnumerable<Customer> GetCustomersSP()
        {
            using var data = new SalesContext();
            var customers = data.Customer.FromSqlRaw("INSERTAR EL NOMBRE STORM PROCIDURE QUE SE CREA EN EL SQL");
            return customers;
        }


        //Lista de todos **Asincrono**
        public static async Task< IEnumerable<Customer>> GetCustomersAsync()
        {
            using var data = new SalesContext();
            var customers = await data.Customer.ToListAsync();
            return customers;
        }

        //RETORNAR UN CLIENTE DE MODO 
        public static Customer GetCustomer(int id)
        {
            using var data = new SalesContext();
            var customer = data.Customer.Where(x => x.Id == id).FirstOrDefault();
            return customer;
        }

        //RETORNAR UN CLIENTE DE MODO ASINTONA
        public static async Task<Customer> GetCustomer2(int id)
        {
            using var data = new SalesContext();
            var customer = await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            return customer;
        }

        //Insertar a un cliente sin asintona
        public static bool Insert(Customer customer)
        {
            bool exito = true;
            try
            {
                //Insertar
                using var data = new SalesContext();
                data.Customer.Add(customer);
                data.SaveChanges();
            }
            catch (Exception)
            {
                exito = false;
            };
            return exito;
        }

        //Insertar a un cliente Asintona
        //public static async Task<bool> Insert2(Customer customer)
        //{
        //    bool exito = true;
        //    try
        //    {
        //        //Insertar
        //        using var data = new SalesContext();
        //        await data.Customer.AddAsync(customer);
        //        await data.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        exito = false;
        //    };
        //    return exito;
        //}

        //**************UPDATE
        //Sin asintona
        public static bool Update(Customer customer)
        {
            bool exito = true;

            try
            {
                using var date = new SalesContext();
                var customerNow = date.Customer.Where(x => x.Id == customer.Id).FirstOrDefault();

                customerNow.FirstName = customer.FirstName;
                customerNow.LastName = customer.LastName;
                customerNow.Country= customer.Country;
                customerNow.City = customer.City;
                customerNow.Phone = customer.Phone;

                date.SaveChanges();
            }
            catch (Exception)
            {
                exito = false;
            };
            return exito;
        }
        //Asintona
        public static async Task<bool> Update2(Customer customer)
        {
            bool exito = true;

            try
            {
                using var date = new SalesContext();
                var customerNow =await date.Customer.Where(x => x.Id == customer.Id).FirstOrDefaultAsync();

                customerNow.FirstName = customer.FirstName;
                customerNow.LastName = customer.LastName;
                customerNow.Country = customer.Country;
                customerNow.City = customer.City;
                customerNow.Phone = customer.Phone;

                await date.SaveChangesAsync();
            }
            catch (Exception)
            {
                exito = false;
            };
            return exito;
        }

        //*****DELETE
        //SIN asintona
        public static bool Delete2(int id)
        {
            bool exito = true;
            try
            {
                using var data = new SalesContext();
                var customerNow = data.Customer.Where(x => x.Id == id).FirstOrDefault();
                data.Customer.Remove(customerNow);
                data.SaveChanges();

            }
            catch (Exception)
            {
                exito = false;

            };

            return exito;
        }
        //Con asintona
        public static async Task<bool> Delete2(int id)
        {
            bool exito = true;
            try
            {
                using var data = new SalesContext();
                var customerNow = await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
                data.Customer.Remove(customerNow);
                await data.SaveChangesAsync();

            }
            catch (Exception)
            {
                exito = false;
             
            };

            return exito;
        }
    }
}
