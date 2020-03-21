namespace PocketArtCollection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using PocketArtCollection;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<PocketArtCollectionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PocketArtCollectionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.PieceOfArts.AddOrUpdate(x => x.Title,
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Title = "Syrenka warszawska",
                    Techinque = "Fresk",
                    Artist = "Pablo Picasso",
                    Description = "123",
                    Period = "Pierwsza awangarda",
                    Picture = PictureTools.ImageToBase64(Properties.Resources._12_WarszawskasyrenaPablaPicassaz1948r_,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1946,
                    UsersEmail = "author@thesis.sggw"
                },
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(), 
                    Title = "Fontanna",
                    Techinque = "Rzezba",
                    Artist = "Marcel Duchamp",
                    Description = "123",
                    Period = "Pierwsza awangarda",
                    Picture = PictureTools.ImageToBase64(Properties.Resources.fontannaDuchamp,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1932,
                    UsersEmail = "author@thesis.sggw"
                },
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Title = "Mona Lisa",
                    Techinque = "Obraz olejny",
                    Artist = "Leonardo da Vinci",
                    Description = "123",
                    Period = "Renesans",
                    Picture = PictureTools.ImageToBase64(Properties.Resources.monLisaDaVinci,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1499,
                    UsersEmail = "author@thesis.sggw"
                },
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Title = "Dama z lasiczka",
                    Techinque = "Obraz olejny",
                    Artist = "Leonardo da Vinci",
                    Description = "123",
                    Period = "Renesans",
                    Picture = PictureTools.ImageToBase64(Properties.Resources.damaZLasiczkaDaVinci,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1506,
                    UsersEmail = "user@thesis.sggw"
                },
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Title = "L.H.O.O.Q.",
                    Techinque = "Obraz",
                    Artist = "Marcel Duchamp",
                    Description = "123",
                    Period = "Pierwsza awangarda",
                    Picture = PictureTools.ImageToBase64(Properties.Resources.lhooqDuchamp,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1940,
                    UsersEmail = "user@thesis.sggw"
                },
                new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Title = "Guernica",
                    Techinque = "Obraz",
                    Artist = "Pablo Picasso",
                    Description = "123",
                    Period = "Pierwsza awangarda",
                    Picture = PictureTools.ImageToBase64(Properties.Resources.guernicaPicasso,
                    System.Drawing.Imaging.ImageFormat.Jpeg),
                    DateOfCreation = 1950,
                    UsersEmail = "user@thesis.sggw"
                });
        }
    }
}