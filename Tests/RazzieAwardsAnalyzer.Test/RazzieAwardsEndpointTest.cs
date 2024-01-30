using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RazzieAwardsAnalyzer.Application.DTOs.Response;
using System.Net;

namespace RazzieAwardsAnalyzer.Test
{
    public class RazzieAwardsEndpointTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public RazzieAwardsEndpointTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Check_Return_Data_Get_Endpoint()
        {
            //Arrange
            var response = await _client.GetAsync("/api/razzieawards");
            

            //Act
            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<AwardIntervalResponseDTO>(responseString);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseData);
        }
    }
}