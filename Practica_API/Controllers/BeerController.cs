using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica_API.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Practica_API.Controllers;
using Practica_API.Data;
using System.Data;
namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;


        public BeerController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;

        }
        [HttpGet("Beer")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTagsCorrect()
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Beers
                           //.Where(tag => tag.DeletedAt == null)
                           .Select(tag => new { tag.Name, tag.Description })
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
        [HttpGet("TagsCorrect/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTagsCorrect(int Id)
        {
            Respuesta<object> respuesta = new();
            try
            {
                var tags = await _context.Beers
                           .Where(tag => /*tag.DeletedAt == null &&*/ tag.Id == Id)
                           .Select(tag => new { tag.Name, tag.Description })
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

        //[HttpPost("CreateTagsCorrect")]
        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> CreateTagsCorrect(TagsCorrect correct)
        //{
        //    Respuesta<object> respuesta = new();
        //    try
        //    {
        //        if (!string.Equals(correct.Type, "TagsDataMapping", StringComparison.OrdinalIgnoreCase) && !string.Equals(correct.Type, "TagsMapping", StringComparison.OrdinalIgnoreCase))
        //            throw new Exception("The Value for Type is Incorrect");
        //        if (correct != null)
        //        {
        //            correct.UpdatedAt = DateTime.UtcNow;
        //            correct.CreatedAt = DateTime.UtcNow;

        //            await _context.TagsCorrect.AddAsync(correct);
        //            await _context.SaveChangesAsync();
        //            respuesta.Ok = 1;
        //            respuesta.Message = "Success";
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        respuesta.Ok = 0;
        //        respuesta.Message = e.Message + " " + e.InnerException;
        //    }
        //    return Ok(respuesta);
        //}

        //[HttpPut("TagsCorrect/{Id}")]
        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> UpdateMechanic(int Id, TagsCorrect correct)
        //{
        //    Respuesta<object> respuesta = new();
        //    try
        //    {

        //        var c = await _context.TagsCorrect.Where(q => q.LocalId == Id).FirstOrDefaultAsync();

        //        if (c != null && c.DeletedAt == null)
        //        {
        //            if (c.Name != correct.Name)
        //            {
        //                c.Name = correct.Name;
        //            }
        //            if (c.Type != correct.Type)
        //            {
        //                c.Type = correct.Type;
        //            }

        //            c.UpdatedAt = DateTime.UtcNow;
        //            if (!string.Equals(c.Type, "TagsDataMapping", StringComparison.OrdinalIgnoreCase) && !string.Equals(c.Type, "TagsMapping", StringComparison.OrdinalIgnoreCase))
        //                throw new Exception("The Value for Type is Incorrect");
        //            await _repository.UpdateAsync(c);
        //            respuesta.Ok = 1;
        //            respuesta.Message = "Success";

        //        }
        //        else
        //        {
        //            respuesta.Ok = 0;
        //            respuesta.Message = "TagsCorrect not found";
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        respuesta.Ok = 0;
        //        respuesta.Message = e.Message + " " + e.InnerException;
        //    }
        //    return Ok(respuesta);
        //}


        //[HttpDelete("TagsCorrect/{Id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> DeleteTagsCorrect(long Id)
        //{
        //    Respuesta<object> respuesta = new();
        //    try
        //    {
        //        var correct = await _repository.SelectById<TagsCorrect>(Id);
        //        if (correct != null)
        //        {
        //            correct.DeletedAt = DateTime.UtcNow;
        //            await _repository.UpdateAsync(correct);
        //            respuesta.Ok = 1;
        //            respuesta.Message = "Success";
        //        }
        //        else
        //        {
        //            respuesta.Ok = 0;
        //            respuesta.Message = "TagsCorrect not found";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        respuesta.Ok = 0;
        //        respuesta.Message = e.Message + " " + e.InnerException;
        //    }
        //    return Ok(respuesta);
        //}


    }

}
            

