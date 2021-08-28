using System.Collections.Generic;
using FluentAssertions;
using GreenFlux.SmartCharging.Application.Services;
using Xunit;

namespace GreenFlux.SmartCharging.Application.Tests
{
    public class ValidationServiceTest
    {
        [Fact]
        public void ValidationService_ValidCapacity()
        {
            double groupMaxAmps = 1000;
            List<double> connectorsMaxCurrentAmps = new()
            {
                100,
                200,
                300,
                300
            };
            bool result = ValidationService.IsInGroupCapacity(groupMaxAmps, connectorsMaxCurrentAmps);
            result.Should().Be(true);
        }

        [Fact]
        public void ValidationService_NotEnoughCapacity()
        {
            double groupMaxAmps = 1000;
            List<double> connectorsMaxCurrentAmps = new()
            {
                100,
                200,
                300,
                300,
                300
            };
            bool result = ValidationService.IsInGroupCapacity(groupMaxAmps, connectorsMaxCurrentAmps);
            result.Should().Be(false);
        }
    }
}
