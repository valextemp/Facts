using Calabonga.Facts.Web.Infrastruture.Mappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.Tests
{
    public class AutomapperTests
    {
        [Fact]
        [Trait("Automapper","Mapper Configuration")]
        // Тест на создание корректной конфигурации Automapper
        public void ItShouldCorrectConfigured()
        {
            //arrange
            var config = MapperRegistration.GetMapperConfiguration();

            //act

            //assert
            config.AssertConfigurationIsValid();
        }
    }
}
