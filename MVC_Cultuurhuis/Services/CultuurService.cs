using MVC_Cultuurhuis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Services
{
    public class CultuurService
    {
        public List<Genre> GetAllGenres()
        {
            using(var db = new CultuurHuisMVCEntities())
            {
                return (db.Genres.OrderBy(m => m.Naam)).ToList();
            }
        }
        public Genre GetGenre(int? id)
        {
            using(var db = new CultuurHuisMVCEntities())
            {
                return db.Genres.Find(id);
            }
        }

        public List<Voorstelling> GetVoorstellingenPerGenre(int? id)
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                var query = from voorstelling in db.Voorstellingen
                            where voorstelling.Datum >= new DateTime(2012, 9, 3) && voorstelling.Genre.GenreNr == id
                            orderby voorstelling.Datum
                            select voorstelling;
                return query.ToList();
            }
        }   

        public Voorstelling GetVoorstellingById(int id)
        {
            using (var db = new CultuurHuisMVCEntities())
            {
                return (db.Voorstellingen.Find(id));
            }
        }

    }
}