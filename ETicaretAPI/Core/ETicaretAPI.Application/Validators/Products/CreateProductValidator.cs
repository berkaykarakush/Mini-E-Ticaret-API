using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VMCreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Lutfen urun adinizi giriniz")
                .NotNull().WithMessage("Lutfen urun adinizi giriniz")
                .MaximumLength(150).WithMessage("Lutfen urun adini 5 ile 150 karakter arasinda giriniz")
                .MinimumLength(5).WithMessage("Lutfen urun adini 5 ile 150 karakter arasinda giriniz");

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("Lutfen stock bilgisini bos gecmeyiniz")
                .NotNull().WithMessage("Lutfen stock bilgisini bos gecmeyiniz")
                .Must(s => s >= 0).WithMessage("Stock bilgisi negatif olamaz");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Lutfen price bilgisini bos gecmeyiniz")
                .NotNull().WithMessage("Lutfen price bilgisini bos gecmeyiniz")
                .Must(s => s >= 0).WithMessage("Price bilgisi negatif olamaz");
        }
    }
}
