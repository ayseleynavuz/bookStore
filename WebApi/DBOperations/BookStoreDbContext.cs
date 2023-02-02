using System;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions <BookStoreDbContext>options) : base(options)
		{ }
		public DbSet<Book> Books { get; set;}
		public DbSet<Genre> Genres { get; set;}
        public DbSet<Author> Authors { get; set; }
        public override int SaveChanges() => base.SaveChanges();
	}
}