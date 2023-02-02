using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBookDetail;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;
using FluentValidation;
using WebApi.BookOperations.CrateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.Contollers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.Id = id;
                result= query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


         [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel  newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model=newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);   
                command.Handle();
                
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook )
        {
           UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model=updatedBook;
                UpdateBookCommandValidator validator =new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command =new DeleteBookCommand(_context); 
          try
            {
                command.BookId=id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            
        }

    }
}