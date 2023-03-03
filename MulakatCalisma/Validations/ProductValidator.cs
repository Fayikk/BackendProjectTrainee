using FluentValidation;
using MulakatCalisma.DTO;

namespace MulakatCalisma.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Not empty");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Not empty");
        }
    }
}
