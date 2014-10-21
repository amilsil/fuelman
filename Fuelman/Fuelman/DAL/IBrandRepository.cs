using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuelman.DAL
{
    public interface IBrandRepository : IDisposable
    {
        IEnumerable<Brand> GetBrands();
        IEnumerable<Model> GetModelsOfBrand(Brand brand);
    }
}
