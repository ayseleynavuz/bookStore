using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}