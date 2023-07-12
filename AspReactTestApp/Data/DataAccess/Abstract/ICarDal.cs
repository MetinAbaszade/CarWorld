﻿using AspReactTestApp.Data.Core.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Data.DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
    }
}
