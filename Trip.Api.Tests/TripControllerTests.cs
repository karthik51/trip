using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Trip.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Trip.Api.Tests
{
    public class TripControllerTests : IClassFixture<TestFixture<Startup>>
    {       
        private readonly HttpClient _httpClient;

        public TripControllerTests(TestFixture<Startup> fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task CreateNewTrip_PassValidCustomer_ReturnsTrip()
        {
            // Arrange
            Ride rideInfo = null;
            string json = @"{    
                'rideDate': '2019-12-16T19:27:30.125Z',
                'fromLocation': 'Perumbakkam',
                'toLocation': 'Perungudi',
                'rideByUser': 'karthik',
                'vehicleCategoryType': 'Mini',
                'rideAcceptedByUser': 'ram',
                'isRideCancelled': false,
                'isRideCompleted': false,
                'vehicleImage': 'image',
                'vehicleNumber': 'Tn14 AD 4444'
              }";
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var request = "/api/v1/trips/CreateRide";

            // Act
            var response = await _httpClient.PostAsync(request, httpContent);

            // Assert
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                rideInfo = JsonConvert.DeserializeObject<Ride>(responseContent);
            }

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && rideInfo.Id != null);
        }

        [Fact]
        public async Task GetTrip_All_Admin_ReturnsAllTrip()
        {
            // Arrange
            List<Ride> rides = null;

            string token = GetJWTToken("admin", "Admin");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _httpClient.DefaultRequestHeaders.Add("accept","application/json");
            var request = "/api/v1/trips/GetAllTrips";

            // Act
            var response = await _httpClient.GetAsync(request);

            // Assert
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                rides = JsonConvert.DeserializeObject<List<Ride>>(responseContent);
            }

            Assert.True(rides != null && rides.Count > 0);
        }

        [Fact]
        public async Task GetTrip_All_Customer_ReturnsAllTrip()
        {
            // Arrange
            List<Ride> rides = null;
           
            string token = GetJWTToken("customer", "Customer");

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            var request = "/api/v1/trips/GetTripsByCustomer?name=karthik";

            // Act
            var response = await _httpClient.GetAsync(request);

            // Assert
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                rides = JsonConvert.DeserializeObject<List<Ride>>(responseContent);
            }

            Assert.True(rides != null && rides.Count > 0);
        }

        [Fact]
        public async Task GetTrip_All_Driver_ReturnsAllTrip()
        {
            // Arrange
            List<Ride> rides = null;

            string token = GetJWTToken("driver", "Driver");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            var request = "/api/v1/trips/GetTripsForEmployee?username=ram";

            // Act
            var response = await _httpClient.GetAsync(request);

            // Assert
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                rides = JsonConvert.DeserializeObject<List<Ride>>(responseContent);
            }

            Assert.True(rides != null && rides.Count > 0);
        }        

        private string GetJWTToken(string username, string Role)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenInfo = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenInfo);            
            return token.ToString();
        }
    }
}
