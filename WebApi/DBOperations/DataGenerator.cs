using System;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
	public class DataGenerator
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			{
				if (context.Books.Any())
				{
					return;
				}

				context.Authors.AddRange(
                    new Author{
                        FirstName = "Eric",
                        LastName = "Ries",
                        DateOfBirth = new DateTime(1978, 09, 22)
                    },
                    new Author{
                        FirstName = "Charlotte",
                        MiddleName = "Perkins",
                        LastName = "Gilman",
                        DateOfBirth = new DateTime(1860, 07, 03)
                    },
                    new Author{
                        FirstName = "Frank",
                        LastName = "Herbert",
                        DateOfBirth = new DateTime(1920, 10, 08)
                    }
                );

				context.Genres.AddRange(
					new Genre
					{
						Name="Personal Growth"
					},
					new Genre
					{
						Name="Science Fiction"
					},
					new Genre
					{
						Name="Roman"
					}
				);

				context.Books.AddRange(
				 new Book { Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
				 new Book { Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
				 new Book { Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2008, 12, 21) });
				context.SaveChanges();
			}
		}
	}
}