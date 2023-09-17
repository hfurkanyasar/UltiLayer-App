using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDTOValidation:AbstractValidator<ProductDTO>
    {
        public ProductDTOValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertName} is required")
                .NotEmpty().WithMessage("{PropertName} is required");

            //ınclusivebetween arasında demek 0 olmasını engelliyor çünkü
            //value typeların defult değeri 0 girilmez ise 0 olur.
            // referans typelerin null gelebilir ama value typler null kontrolu yapılamaz.

            RuleFor(a => a.Price).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertName} must be graeter 0");

            RuleFor(a => a.Stock).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertName} must be graeter 0");

            // value typeler için aralık belirtilmesi gerekli.
            RuleFor(a => a.CategoryID).InclusiveBetween(1, int.MaxValue)
               .WithMessage("{PropertName} must be graeter 0");
        }
    }
}
