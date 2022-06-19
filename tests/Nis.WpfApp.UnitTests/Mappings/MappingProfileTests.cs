using Xunit;
using AutoMapper;
using Nis.WpfApp.Mappings;

namespace Nis.WpfApp.UnitTests.Mappings;

public class MappingProfileTests : BaseUnitTest
{
    private readonly MapperConfiguration _configuration;

    public MappingProfileTests() => _configuration = new MapperConfiguration(options => options.AddProfile<MappingProfile>());

    [Fact]
    public void it_should_be_valid_if_mapping_exists() => _configuration.AssertConfigurationIsValid();
}
