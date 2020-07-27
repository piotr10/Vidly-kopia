using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public IMapper _mapper;
        
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET /api/customers ->aby uzyskać zasób
        public IHttpActionResult GetCustomers()
        {                                      //.Select(Mapper.Map<Customer,CustomerDto>) -> na potrzeby Automapper
            var customerDtos =  _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(_mapper.Map<Customer,CustomerDto>);
            //return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);

            return Ok(customerDtos);
        }
        
        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) // jeżeli dany zasób nie zostanie znaleziony zwrócimy standardową odpowiedź nie znalezniono w innym przypadku 
            {                           // zwrócimy customer
                return NotFound();
            }

            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
            //return Ok(Mapper.Map<Customer, CustomerDto>(customer)); // Mapper.Map<Customer, CustomerDto> (customer) -> na potrzeby AutoMapper
        }
        
        // POST /api/customers ->aby utworzyć zasób
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //wpierw przeprowadzamy validację
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            //var customer = Mapper.Map<CustomerDto, Customer>(customerDto); // cala linijka na potrzeby AutoMapper

            //w przeciwnym razie dodajemy ten obiekt do naszego context i zapisujemy zmiany
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            //zwracamy obiekt 
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        
        // PUT /api/customers/1 ->aby zaaktualizować klineta
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            //wpierw przeprowadzamy validację
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //uzyskujemy klienta z bazy danych
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //sprawdzamy czy istnieje takie id
            if (customerInDb == null)
            {
                return NotFound();
            }

            //jeżeli wszystko jest dobrze aktualizujemy klienta
            _mapper.Map(customerDto, customerInDb);
            //Mapper.Map(customerDto, customerInDb); // -> linijka na potrzeby AutoMapper
           // customerInDb.Name = customerDto.Name;
           // customerInDb.Birthdate = customerDto.Birthdate;
           // customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
           // customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            //zapisujemy zmiany
            _context.SaveChanges();

            return Ok(_mapper.Map<CustomerDto, Customer>(customerDto)); // czy to ejst dobrze :/
        }

        // DELETE /api/customers/1 ->aby usunąć klineta
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            //uzyskujemy klienta z bazy danych
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //sprawdzamy czy istnieje takie id
            if (customerInDb == null)
            {
                NotFound();
            }

            //obiekt zostanie oznaczony w pamięci jako usuniety
            _context.Customers.Remove(customerInDb);

            //zapisujemy zmiany
            _context.SaveChanges();
            return Ok();
        }
    }
}
