using System.Threading.Tasks;

namespace Sample.Tests.Integration.Features.Owners
{
    using Sample.Web.Features.Owners;
    using Shouldly;
    using Xunit;
    using static SliceFixture;

    public class OwnerReadTest : IntegrationTestBase
    {
        [Fact]
        public async Task Should_read_block()
        {
            //Arrange
            var createdId = await EntitySetupHelper.CreateOwner().ConfigureAwait(false);
            var query = new Details.Query { Id = createdId };

            //Act
            var entity = await SendAsync(query).ConfigureAwait(false);

            //Assert
            entity.ShouldNotBeNull();
            entity.Id.ShouldBe(createdId);
        }
    }
}
