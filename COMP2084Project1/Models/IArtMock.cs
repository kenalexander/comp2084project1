using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2084Project1.Models
{
    public interface IArtMock
    {
        IQueryable<artTable> artTables { get; }
        IQueryable<museumTable> museumTables { get; }

        artTable Save(artTable artTable);
        void Delete(artTable artTable);
    }
}
