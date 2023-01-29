using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookByIdValidator : AbstractValidator<GetByIdCommand>
    {
        public GetBookByIdValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
        }
    }
}