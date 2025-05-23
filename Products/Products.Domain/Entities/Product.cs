using Products.Domain.Extensions;
using System;

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

        public static Product Create(string name, string code, decimal price)
        {
            var product = new Product(code, name, price);

            product.Validate();

            return product;
        }

        private void Validate()
        {
            ValidateName();
            ValidateCode();
            ValidatePrice();
        }

        private void ValidatePrice()
        {
            if (Price <= 0)
            {
                throw new ArgumentException("PRICE_ERROR");
            }

            if (!Price.HasMaximumTwoDecimalPlaces())
            {
                throw new ArgumentException("PRICE_COMMA_ERROR");
            }
        }

        private void ValidateCode()
        {
            if (string.IsNullOrEmpty(Code))
            {
                throw new ArgumentException("CODE_EMPTY");
            }

            if (Code.Length < 3 || Code.Length > 200)
            {
                throw new ArgumentException("CODE_ERROR");
            }
        }

        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("NAME_EMPTY");
            }

            if (Name.Length < 3 || Name.Length > 200)
            {
                throw new ArgumentException("NAME_ERROR");
            }
        }
    }
}
