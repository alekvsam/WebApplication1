using AngleSharp;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace WebApplication1Test
{
    public class RentURLFilterModelUnitTest
    {
        [Fact]
        public void ToURLString_RoomsMoreThanRangeReturnOutOfRangeException()
        {
            WebApplication1.Models.RentURLFilterModel model = new WebApplication1.Models.RentURLFilterModel
            {
                Rooms = 15
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => model.ToURLString());
        }
    }
}
