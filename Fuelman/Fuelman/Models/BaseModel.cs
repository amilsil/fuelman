using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
    }
}