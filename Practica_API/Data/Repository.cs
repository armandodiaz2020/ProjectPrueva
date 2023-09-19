using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Practica_API.Data
{
    public class Repository<TDbContext> : IRepository where TDbContext : DbContext
    {
        private TDbContext _dbContext;

        public Repository(TDbContext context)
        {
            this._dbContext = context;
        }
        public async Task CreateAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Add(entity);
            _ = await this._dbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Remove(entity);
            _ = await this._dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> SelectAll<T>() where T : class
        {
            return await this._dbContext
                    .Set<T>()
                    .ToListAsync();
        }

        public async Task<T> SelectById<T>(int Id) where T : class
        {
            return await this._dbContext.Set<T>().FindAsync(Id);
        }
        public async Task<T> SelectById<T>(long Id) where T : class
        {
            return await this._dbContext.Set<T>().FindAsync(Id);
        }



        public async Task UpdateAsync<T>(T entity) where T : class
        {
            this._dbContext.Set<T>().Update(entity);
            _ = await this._dbContext.SaveChangesAsync();
        }

    }
}
