using Business.Interfaces;
using Data.DbContainer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class GenricRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DataContainer data;
        private readonly DbSet<T> table;


        public GenricRepo(DataContainer data)
        {
            this.data = data;
            table=data.Set<T>();
        }
        public void Delete(object id)
        {
            T Existed =  table.Find(id);
            table.Remove(Existed);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            data.Entry(entity).State= EntityState.Modified;
        }
    }
}
