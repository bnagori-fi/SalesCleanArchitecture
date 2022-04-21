using AutoMapper;
using Sales.Application.Mappings;
using System;
using System.Linq;
using Xunit;

namespace Sales.Application.Test
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => {

                var profiles = typeof(MappingProfile)
                    .Assembly
                    .GetTypes()
                    .Where(p => !p.IsAbstract && typeof(Profile).IsAssignableFrom(p));
                
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}