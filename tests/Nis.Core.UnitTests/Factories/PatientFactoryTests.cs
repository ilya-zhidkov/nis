using Xunit;
using System;
using Nis.Core.Models;
using FluentAssertions;
using Nis.Core.Factories;

namespace Nis.Core.UnitTests.Factories
{
    public class PatientFactoryTests
    {
        private readonly BaseFactory<Patient> _factory;

        public PatientFactoryTests() => _factory = new PatientFactory();

        [Fact]
        public void it_should_be_an_instance_of_base_factory() => _factory.Should().BeAssignableTo<BaseFactory<Patient>>();

        [Fact]
        public void it_should_create_ten_articles()
        {
            var articles = _factory.Create(count: 10);
            articles.Should().HaveCount(10);
        }

        [Fact]
        public void it_should_throw_if_negative_count_is_passed()
        {
            ((Action)(() => _factory.Create(count: -1)))
                .Should()
                .Throw<ArgumentOutOfRangeException>();
        }
    }
}
