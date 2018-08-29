using FluentValidation.TestHelper;
using Sample.Web.Features.Owners;
using System;
using System.Linq;
using Xunit;

namespace Sample.Tests.Unit.Validation
{
    public class OwnerValidatorTests
    {
        private readonly OwnerValidator _validator;
        private readonly Random _random;
        private readonly OwnerViewModel _model;

        public OwnerValidatorTests()
        {
            _validator = new OwnerValidator();
            _random = new Random();
            _model = new OwnerViewModel();
        }

        [Fact]
        public void Should_throw_error_when_Name_is_null()
        {
            _model.Name = null;
            _validator.ShouldHaveValidationErrorFor(m => m.Name, _model);

        }

        [Fact]
        public void Should_throw_error_when_Name_is_empty()
        {
            _model.Name = "";
            _validator.ShouldHaveValidationErrorFor(m => m.Name, _model);
        }

        [Fact]
        public void Should_throw_error_when_Name_is_too_long()
        {
            _model.Name = TestHelpers.RandomStrings(101, 101, 1, _random).First();
            _validator.ShouldHaveValidationErrorFor(m => m.Name, _model);
        }
    }
}
