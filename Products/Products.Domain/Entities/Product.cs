using Products.Domain.Common;
using Products.Domain.Extensions;

namespace Products.Domain.Entities
{
    public class Product
    {
        private Product() { }
        private Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public static Result<Product> Create(string name, string code, decimal price)
        {
            var product = new Product(code, name, price);

            var validateResult = product.Validate();

            if (validateResult.IsFailure)
            {
                return Result<Product>.Failure(validateResult.ErrorCode);
            }

            return Result<Product>.Success(product);
        }

        private Result Validate()
        {
            var validateNameResult = ValidateName();

            if (validateNameResult.IsFailure)
                return validateNameResult;

            var validateCodeResult = ValidateCode();

            if (validateCodeResult.IsFailure)
                return validateCodeResult;

            var validatePriceResult = ValidatePrice();

            if(validatePriceResult.IsFailure)
                return validatePriceResult;

            return Result.Success();
        }

        private Result ValidatePrice()
        {
            if (Price <= 0)
            {
                return Result.Failure("PRICE_ERROR");
            }

            if (!Price.HasMaximumTwoDecimalPlaces())
            {
                return Result.Failure("PRICE_COMMA_ERROR");
            }

            return Result.Success();
        }

        private Result ValidateCode()
        {
            if (string.IsNullOrEmpty(Code))
            {
                return Result.Failure("CODE_EMPTY");
            }

            if (Code.Length < 3)
            {
                return Result.Failure("CODE_ERROR");
            }

            if (Code.Length > 200)
            {
                return Result.Failure("CODE_ERROR");
            }
            return Result.Success();
        }

        private Result ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Result.Failure("NAME_EMPTY");
            }

            if (Name.Length < 3)
            {
                return Result.Failure("NAME_ERROR");
            }

            if (Name.Length > 200)
            {
                return Result.Failure("NAME_ERROR");
            }

            return Result.Success();
        }
    }
}
