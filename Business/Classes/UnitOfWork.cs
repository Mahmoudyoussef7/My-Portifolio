using Business.Interfaces;
using Business.Repositories;
using Data.DbContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Classes
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private IGenericRepo<T> entity;
        private readonly DataContainer _data;

        public UnitOfWork(DataContainer data)
        {
            _data = data;
        }

        public IGenericRepo<T> Entity
        { 
            get
            {
                return entity??(entity = new GenricRepo<T>(_data));
            }

        }


        public void Save(T entity)
        {
            _data.SaveChanges();
        }
    }
}
