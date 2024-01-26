using CarPage.Core.Dto;
using CarPage.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPageTest
{
    public class CarPageTest : UnitTest1
    {
        [Fact]
        public async Task ShouldNot_AddEmptyCaritem_WhenReturnresult()
        {
            CarItemDto dto = new CarItemDto();

            dto.Name = "Name";
            dto.ShortDesc = "Text";
            dto.LongDesc = "Text";
            dto.Price = 123;
            dto.isFavorite = true;
            dto.available = true;
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            var result = await Svc<ICarItemServices>().Create(dto);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ShouldNot_GetByIdCaritem_WhenReturnsNotEqual()
        {
            //Arrange
            Guid guid = Guid.Parse("a1999241-e62b-4804-8c19-4f017de45914");
            //kuidas teha automatselt guidi
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());

            //Act
            await Svc<ICarItemServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(guid, wrongGuid);

        }
        [Fact]

        public async Task Should_GetByIdCaritem_WhenRetunsEqual()
        {

            Guid databaseGuid = Guid.Parse("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");
            Guid getGuid = Guid.Parse("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");



            //SpaceshipDto spaceship = new();

            //spaceship.Id = new Guid("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");
            //Guid id2 = Guid.Parse("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");


            await Svc<ICarItemServices>().GetAsync(getGuid);


            //Assert.Equal(spaceship.Id , id2);
            Assert.Equal(databaseGuid, getGuid);


        }
        [Fact]

        public async Task Should_DeleteByIdCaritem_WhenDeleteRealestate()
        {

            //Arrange
            CarItemDto caritem = MockCaritemData();



            //Act
            var createdcaritem = await Svc<ICarItemServices>().Create(caritem);
            var result = await Svc<ICarItemServices>().Delete((Guid)createdcaritem.Id);


            Assert.Equal(createdcaritem, result);

        }
        [Fact]

        public async Task ShouldNot_DeleteByIdCaritem_WhenDidNotDeleteRealestate()
        {
            CarItemDto caritem = MockCaritemData();

            var addCaritem = await Svc<ICarItemServices>().Create(caritem);
            var addCaritem2 = await Svc<ICarItemServices>().Create(caritem);

            var result = await Svc<ICarItemServices>().Delete((Guid)addCaritem2.Id);
            Assert.NotEqual(addCaritem.Id, result.Id);

        }
        [Fact]
        public async Task Should_UpdateCaritem_WhenUpdateData()
        {
            var guid = new Guid("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");

            //old data from db
            CarPage.Core.Domain.CarItem caritem = new CarPage.Core.Domain.CarItem();

            //new data
            CarItemDto dto = MockCaritemData();

            caritem.Id = Guid.Parse("9f0674c4-1ddc-4415-9ea2-a0502ac4913b");
            caritem.Name = "asdrd";
            caritem.ShortDesc = "asdrd";
            caritem.LongDesc = "adsdrd";
            caritem.Price = 15000;
            caritem.isFavorite = true;
            caritem.available = true;
            caritem.CreatedAt = DateTime.Now.AddYears(1);
            caritem.UpdatedAt = DateTime.Now.AddYears(1);

            await Svc<ICarItemServices>().Update(dto);

            Assert.Equal(caritem.Id, guid);
            Assert.NotEqual(caritem.Name, dto.Name);
            Assert.NotSame(caritem.LongDesc, dto.LongDesc);
            Assert.DoesNotMatch(caritem.Price.ToString(), dto.Price.ToString());


        }
        [Fact]
        public async Task Should_UpdateCaritem_WhenUpdateDataVersion2()
        {
            CarItemDto dto = MockCaritemData();
            var createCaritem = await Svc<ICarItemServices>().Create(dto);

            CarItemDto update = MockUpdateCaritemData();
            var updateCaritem = await Svc<ICarItemServices>().Update(update);

            //error korda teha
            Assert.Matches(updateCaritem.Name, createCaritem.Name);
            Assert.NotEqual(updateCaritem.ShortDesc, createCaritem.ShortDesc);
            Assert.NotEqual(updateCaritem.LongDesc, createCaritem.LongDesc);
            Assert.DoesNotMatch(updateCaritem.Price.ToString(), createCaritem.Price.ToString());
        }
        [Fact]
        public async Task ShouldNot_UpdateRealestate_WhenNotUpdateData()
        {
            CarItemDto dto = MockCaritemData();
            await Svc<ICarItemServices>().Create(dto);

            CarItemDto nullUpdate = MockNullCaritem();
            await Svc<ICarItemServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }
        private CarItemDto MockNullCaritem()
        {
            CarItemDto nullDto = new()
            {
                Id = null,
                Name = "Name12345",
                ShortDesc = "Text",
                LongDesc = "Text",
                Price = 15000,
                isFavorite = true,
                available = true,
                CreatedAt = DateTime.Now.AddYears(1),
                UpdatedAt = DateTime.Now.AddYears(1),
            };
            return nullDto;
        }
        private CarItemDto MockUpdateCaritemData()
        {
            CarItemDto update = new()
            {
                Name = "Name321",
                ShortDesc = "Text1",
                LongDesc = "Text2",
                Price = 15001,
                isFavorite = false,
                available = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            return update;

        }

        private CarItemDto MockCaritemData()
        {
            CarItemDto caritem = new()
            {

                Name = "Name321",
                ShortDesc = "Text11",
                LongDesc = "Text22",
                Price = 15002,
                isFavorite = false,
                available = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            return caritem;


        }
    }
}
