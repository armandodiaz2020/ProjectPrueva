using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica_API.Data;
using Practica_API.Models;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;


        public BeersController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;

        }
        [HttpGet(" GetBeers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBeers()
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Beers
                           .Where(tag => tag.DeletedAt == null)
                           .Select(tag => new { tag.Id, tag.Name, tag.Description, tag.Amount, tag.Price })
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
        [HttpGet(" GetBeers/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBeers(int Id)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Beers
                           .Where(tag => tag.DeletedAt == null && tag.Id == Id)
                           .Select(tag => new { tag.Name, tag.Description, tag.Amount, tag.Price })
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

        [HttpPost("CreateBeers")]
        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateBeers(Beers beer)
        {
            Respuesta<object> respuesta = new();
            try
            {
                
                if (beer != null)
                {
                    beer.UpdatedAt = DateTime.UtcNow;
                    beer.CreatedAt = DateTime.UtcNow;
                    await _context.Beers.AddAsync(beer);
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

        [HttpPut("UpdateBeers/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateMechanic(int Id, Beers bee)
        {
            Respuesta<object> respuesta = new();
            try
            {

                var c = await _context.Beers.Where(q => q.Id == Id).FirstOrDefaultAsync();

                if (c != null && c.DeletedAt == null)
                {
                    if (c.Name != bee.Name)
                    {
                        c.Name = bee.Name;
                    }
                    if (c.Description != bee.Description)
                    {
                        c.Description = bee.Description;
                    }
                    if (c.Amount != bee.Amount)
                    {
                        c.Amount = bee.Amount;
                    }
                    if (c.Price != bee.Price)
                    {
                        c.Price = bee.Price;
                    }
                    c.UpdatedAt = DateTime.UtcNow;

                    await _repository.UpdateAsync(c);
                    respuesta.Ok = 1;
                    respuesta.Message = "Success";

                }
                else
                {
                    respuesta.Ok = 0;
                    respuesta.Message = "Beer not found";
                }

            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }


        [HttpDelete("Beers/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteBeers(int Id)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var beer = await _repository.SelectById<Beers>(Id);
                if (beer != null)
                {
                    beer.DeletedAt = DateTime.UtcNow;
                    await _repository.UpdateAsync(beer);
                    respuesta.Ok = 1;
                    respuesta.Message = "Success";
                }
                else
                {
                    respuesta.Ok = 0;
                    respuesta.Message = "Beer not found";
                }
            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }
        [HttpGet("AllBeers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllBeers(int pagNumber, int pagSize)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var Beers = await GetTagsList(pagNumber, pagSize);
                var items = await _context.Beers.CountAsync();

                respuesta.Data.Add(new
                {
                    totalItems = items,
                    totalPages = (int)Math.Ceiling((double)items / pagSize),
                    actualPage = pagNumber,
                    tags = Beers
                });
                respuesta.Ok = 1;
                respuesta.Message = "Success";
            }
            catch (Exception e)
            {
                respuesta.Ok = 0;
                respuesta.Message = e.Message + " " + e.InnerException;
            }
            return Ok(respuesta);
        }
        private async Task<List<Beers>> GetTagsList(int pagNumber, int pagSize)
        {
            try
            {
                var query = _context.Beers
                                .OrderByDescending(tag => tag.Id)
                                .Skip((pagNumber - 1) * pagNumber)
                                .Take(pagSize);

                var tags = await query.Select(tag => new Beers
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    Amount = tag.Amount,
                    Price = tag.Price
                }).ToListAsync();

                return tags;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
