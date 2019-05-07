namespace Sample.Tests.Integration.Features.Owners
{
    using Sample.Core.Domain;
    using Sample.Web.Features.Owners;
    using Shouldly;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using static SliceFixture;

    public class OwnerIndexTests : IntegrationTestBase
    {

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var owner1 = new Owner("Zowner");
            var owner2 = new Owner("Aowner");
            var owner3 = new Owner("Bowner");
            await InsertAsync(owner1, owner2, owner3).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_return_a_paged_list_of_owners()
        {
            //Arrange
            var query = new Index.Query();

            //Act
            var result = await SendAsync(query);

            //Assert
            result.ShouldNotBeNull();
            result.Results.TotalCount.ShouldBe(3);
            result.Results.Data.Count().ShouldBe(3);
        }

        [Fact]
        public async Task Should_return_a_paged_list_of_owners_sorted_ascending_by_name()
        {
            //Arrange
            var query = new Index.Query { Order = "-name" };

            //Act
            var result = await SendAsync(query).ConfigureAwait(false);

            //Asert
            result.ShouldNotBeNull();
            result.Results.TotalCount.ShouldBe(3);
            result.Results.Data.First().Name.ShouldBe("Zowner");
            result.Results.Data.Last().Name.ShouldBe("Aowner");
        }

        [Fact]
        public async Task Should_return_a_paged_list_of_owners_sorted_descending_by_name()
        {
            //Arrange
            var query = new Index.Query { Order = "name" };

            //Act
            var result = await SendAsync(query).ConfigureAwait(false);

            //Asert
            result.ShouldNotBeNull();
            result.Results.TotalCount.ShouldBe(3);
            result.Results.Data.First().Name.ShouldBe("Aowner");
            result.Results.Data.Last().Name.ShouldBe("Zowner");
        }

        [Fact]
        public async Task Should_return_a_paged_list_of_owners_with_correct_number_of_pages()
        {
            //Arrange
            var query = new Index.Query { Limit = 1 };

            //Act
            var result = await SendAsync(query).ConfigureAwait(false);

            //Assert
            result.ShouldNotBeNull();
            result.Results.TotalCount.ShouldBe(3);
            result.Results.Page.ShouldBe(1);
            result.Results.HasPreviousPage.ShouldBeFalse();
            result.Results.HasNextPage.ShouldBeTrue();
            result.Results.Data.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Should_return_a_paged_list_of_owners_that_contain_Name()
        {
            //Arrange
            var query = new Index.Query { Contains = "z" };

            //Act
            var result = await SendAsync(query).ConfigureAwait(false);

            //Assert
            result.ShouldNotBeNull();
            result.Results.TotalCount.ShouldBe(1);
            result.Results.Page.ShouldBe(1);
            result.Results.HasPreviousPage.ShouldBeFalse();
            result.Results.HasNextPage.ShouldBeFalse();
            result.Results.Data.Count().ShouldBe(1);
            result.Results.Data.First().Name.ShouldBe("Zowner");
        }

        //add contains tests for each property added to "Contains" PageSortExtensions
    }
}

