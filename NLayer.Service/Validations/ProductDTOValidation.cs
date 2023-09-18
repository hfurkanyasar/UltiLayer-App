using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class ProductDTOValidation : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");

            //ınclusivebetween arasında demek 0 olmasını engelliyor çünkü
            //value typeların defult değeri 0 girilmez ise 0 olur.
            // referans typelerin null gelebilir ama value typler null kontrolu yapılamaz.

            RuleFor(a => a.Price).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} must be graeter 0");

            RuleFor(a => a.Stock).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} must be greater 0");

            // value typeler için aralık belirtilmesi gerekli.
            
        }
    }
}
