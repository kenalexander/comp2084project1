using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP2084Project1.Models
{
    public class EFartTable : IArtMock
    {
        private a1dbEntities4 db = new a1dbEntities4();

        public IQueryable<artTable> artTables { get { return db.artTables; } }

        public IQueryable<museumTable> museumTables { get { return db.museumTables; } }

        public void Delete(artTable artTable)
        {
            db.artTables.Remove(artTable);
            db.SaveChanges();
        }

        public artTable Save(artTable artTable)
        {
            if (artTable.TitleID == 0)
            {
                db.artTables.Add(artTable);
            }
            else
            {
                db.Entry(artTable).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return artTable;
        }
    }
}