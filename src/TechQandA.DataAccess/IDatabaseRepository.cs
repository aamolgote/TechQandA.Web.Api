using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechQandA.DataAccess
{
    /// <summary>
    /// Database Repository Interface
    /// </summary>
    public interface IDatabaseRepository
    {
        string EndPoint { get; }
        string AuthKey { get; }
        string DatabaseId { get; }
    }
}
