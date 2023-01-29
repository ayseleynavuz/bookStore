using System;
using AutoMapper;
using BookStore;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBookDetail
{
	public class GetBookDetailQuery
	{
		private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
           var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
           if (book is null)
                throw new InvalidOperationException("Belirtilen Id'ye sahip kitap mevcut değildir.");

            BookDetailViewModel vm =new BookDetailViewModel();
           return vm;
        }

        public class BookDetailViewModel
		{
			public string? Title { get; set; }
            public int PageCount { get; set; }
            public string? PublishDate { get; set; }
            public string? Genre { get; set; }
        };
	}
}