using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica_API.Data;
using Practica_API.Models;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;


        public CustomerController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;

        }
        [HttpGet(" GetCustomers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers()
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Customers
                           .Where(tag => tag.DeletedAt == null)
                           .Select(tag => new { tag.Name, tag.LastName, tag.Address,tag.Phone })
                           .Distinct()
                           .ToListAsync();

                respuesta.Ok = 1;
                respuesta.Data.Add(tags);
            }
            catch (Exception ex)
            {
                respuesta.Ok = 0;
                respuesta.Message = ex.Message + " " + ex.InnerException;
            }

            return Ok(respuesta);
        }
        [HttpGet(" GetCustomers/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers(int Id)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Customers
                           .Where(tag => tag.DeletedAt == null && tag.Id == Id)
                           .Select(tag => new { tag.Name, tag.LastName, tag.Address, tag.Phone })
                           .Distinct()
                           .ToListAsync();

                respuesta.Ok = 1;
                respuesta.Data.Add(tags);
            }
            catch (Exception ex)
            {
                respuesta.Ok = 0;
                respuesta.Message = ex.Message + " " + ex.InnerException;
            }

            return Ok(respuesta);
        }

        [HttpPost("CreateCustomer")]
        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateCustomer(Customer cust)
        {
            Respuesta<object> respuesta = new();
            try
            {
                //if (!string.Equals(correct.Type, "TagsDataMapping", StringComparison.OrdinalIgnoreCase) && !string.Equals(correct.Type, "TagsMapping", StringComparison.OrdinalIgnoreCase))
                //    throw new Exception("The Value for Type is Incorrect");
                if (cust != null)
                {
                    cust.UpdatedAt = DateTime.UtcNow;
                    cust.CreatedAt = DateTime.UtcNow;

                    await _context.Customers.AddAsync(cust);
                    await _context.SaveChangesAsync();
                    respuesta.Ok = 1;
                    respuesta.Message = "Success";
                }

            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }

        [HttpPut("UpdateCustomers/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateMechanic(int Id, Customer cust)
        {
            Respuesta<object> respuesta = new();
            try
            {

                var c = await _context.Customers.Where(q => q.Id == Id).FirstOrDefaultAsync();

                if (c != null && c.DeletedAt == null)
                {
                    if (c.Name != cust.Name)
                    {
                        c.Name = cust.Name;
                    }
                    if (c.LastName != cust.LastName)
                    {
                        c.LastName = cust.LastName;
                    }
                    if (c.Address != cust.Address)
                    {
                        c.Address = cust.Address;
                    }
                    if (c.Phone != cust.Phone)
                    {
                        c.Phone = cust.Phone;
                    }
                    c.UpdatedAt = DateTime.UtcNow;
                   
                    await _repository.UpdateAsync(c);
                    respuesta.Ok = 1;
                    respuesta.Message = "Success";

                }
                else
                {
                    respuesta.Ok = 0;
                    respuesta.Message = "Customer not found";
                }

            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }


        [HttpDelete("Customer/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var cust = await _repository.SelectById<Customer>(Id);
                if (cust != null)
                {
                    cust.DeletedAt = DateTime.UtcNow;
                    await _repository.UpdateAsync(cust);
                    respuesta.Ok = 1;
                    respuesta.Message = "Success";
                }
                else
                {
                    respuesta.Ok = 0;
                    respuesta.Message = "Customer not found";
                }
            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }


    }
}
