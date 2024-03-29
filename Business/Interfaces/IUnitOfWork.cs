﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepo<T> Entity { get; }
        void Save();
    }
}
