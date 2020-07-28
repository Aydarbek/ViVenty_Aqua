using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViVenty.Domain.Entities;

namespace ViVenty.Domain.Concrete
{
    class DbInitializer : DropCreateDatabaseIfModelChanges<ViventyContext>
    {
        protected override void Seed(ViventyContext db)
        {
            Hsuit marine = new Hsuit
            {
                Name = "Марин",
                Description = "Хорошая модель для кайтинга",
                Price = 7000,
                DefaultPhoto = "SpSuit_M1_front.jpg",
                Category = "Водные костюмы"
            };

            Hsuit kasatka = new Hsuit
            {
                Name = "Касатка",
                Description = "Хорошая модель для серфинга",
                Price = 8000,
                DefaultPhoto = "SpSuit_M2_front.jpg",
                Category = "Водные костюмы"
            };

            Hsuit ariel = new Hsuit
            {
                Name = "Ариэль",
                Description = "Идеальная модель для серфинга",
                Price = 7500,
                DefaultPhoto = "SpSuit_M3_front.jpg",
                Category = "Водные костюмы"
            };

            Hsuit izabella = new Hsuit
            {
                Name = "Изабелла",
                Description = "Классный современный купальник",
                Price = 6000,
                DefaultPhoto = "SwSuit_S1_front.jpg",
                Category = "Купальники"
            };

            Hsuit volna = new Hsuit
            {
                Name = "Волна",
                Description = "Изящный и яркий купальный костюм",
                Price = 5000,
                DefaultPhoto = "SwSuit_S2_front.jpg",
                Category = "Купальники"
            };


            db.Hsuits.Add(marine);
            db.Hsuits.Add(kasatka);
            db.Hsuits.Add(ariel);
            db.Hsuits.Add(izabella);
            db.Hsuits.Add(volna);

            db.SaveChanges();

            Photo Sp1_front = new Photo
            {
                Image = "SpSuit_M1_front.jpg",
                hsuit = db.Hsuits.Find(1), 
                Nr = 0
            };

            Photo Sp1_back = new Photo
            {
                Image = "SpSuit_M1_back.jpg",
                hsuit = db.Hsuits.Find(1),
                Nr = 1
            };

            Photo Sp2_front = new Photo
            {
                Image = "SpSuit_M2_front.jpg",
                hsuit = db.Hsuits.Find(2),
                Nr = 0
            };

            Photo Sp2_back = new Photo
            {
                Image = "SpSuit_M2_back.jpg",
                hsuit = db.Hsuits.Find(2),
                Nr = 1
            };

            Photo Sp3_front = new Photo
            {
                Image = "SpSuit_M3_front.jpg",
                hsuit = db.Hsuits.Find(3),
                Nr = 0
            };

            Photo Sp3_back = new Photo
            {
                Image = "SpSuit_M3_back.jpg",
                hsuit = db.Hsuits.Find(3),
                Nr = 1
            };

            Photo Sw1_front = new Photo
            {
                Image = "SwSuit_S1_front.jpg",
                hsuit = db.Hsuits.Find(4),
                Nr = 0
            };

            Photo Sw1_back = new Photo
            {
                Image = "SwSuit_S1_back.jpg",
                hsuit = db.Hsuits.Find(4),
                Nr = 1
            };

            Photo Sw2_front = new Photo
            {
                Image = "SwSuit_S2_front.jpg",
                hsuit = db.Hsuits.Find(5),
                Nr = 0
            };

            Photo Sw2_back = new Photo
            {
                Image = "SwSuit_S2_back.jpg",
                hsuit = db.Hsuits.Find(5),
                Nr = 1
            };

            db.Photos.Add(Sp1_front);
            db.Photos.Add(Sp1_back);
            db.Photos.Add(Sp2_front);
            db.Photos.Add(Sp2_back);
            db.Photos.Add(Sp3_front);
            db.Photos.Add(Sp3_back);
            db.Photos.Add(Sw1_front);
            db.Photos.Add(Sw1_back);
            db.Photos.Add(Sw2_front);
            db.Photos.Add(Sw2_back);

            db.SaveChanges();
        }
    }
}
