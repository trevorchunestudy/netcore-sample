namespace Sample.Tests.Integration.Features.Owners
{
    using Sample.Core.Domain;
    using Sample.Data.Extensions;
    using Sample.Web.Features.Owners;
    using Shouldly;
    using System.Threading.Tasks;
    using Xunit;
    using static SliceFixture;

    public class OwnerDeleteTest : IntegrationTestBase
    {
        [Fact]
        public async Task Should_soft_delete_owner()
        {
            //Arrange
            var createdId = await EntitySetupHelper.CreateOwner().ConfigureAwait(false);
            var command = new Delete.Command { Id = createdId };

            //Act
            await SendAsync(command).ConfigureAwait(false);
            var deleted = await ExecuteDbContextAsync<Owner>(x => x.Owners.FirstActiveAsync(y => y.Id == createdId)).ConfigureAwait(false);

            //Assert
            deleted.ShouldBeNull();
        }
    }
}
