
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Car;
using CAR_RENTAL_MS_III.Repositories;
using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using CarRental_Max.Models.Car;

namespace CAR_RENTAL_MS_III.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IFeatureRepository _featureRepository;


        public CarService(ICarRepository carRepository, IFeatureRepository featureRepository)
        {
            _carRepository = carRepository;
            _featureRepository = featureRepository;

        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            return cars.Select(c => new CarDto
            {
                Id = c.Id,
               
                ModelId = c.ModelId,
                Year = c.Year,
                RegistrationNumber = c.RegistrationNumber,
                CategoryId = c.CategoryId,
                IsAvailable = c.IsAvailable,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null) return null;

            return new CarDto
            {
                Id = car.Id,
               
                ModelId = car.ModelId,
                Year = car.Year,
                RegistrationNumber = car.RegistrationNumber,
                CategoryId = car.CategoryId,
                IsAvailable = car.IsAvailable,
                ImageUrl = car.ImageUrl
            };
        }

        //public async Task AddCarAsync(CarDto carDto)
        //{
        //    var car = new Car
        //    {

        //        ModelId = carDto.ModelId,
        //        Year = carDto.Year,
        //        RegistrationNumber = carDto.RegistrationNumber,
        //        CategoryId = carDto.CategoryId,
        //        IsAvailable = carDto.IsAvailable,
        //        PricePerDay = carDto.PricePerDay,
        //        ImageUrl = carDto.ImageUrl
        //    };
        //    await _carRepository.AddCarAsync(car);
        //}


        //public async Task AddCarAsync(CarDto carDto)
        //{
        //    var car = new Car
        //    {
        //        ModelId = carDto.ModelId,
        //        Year = carDto.Year,
        //        RegistrationNumber = carDto.RegistrationNumber,
        //        CategoryId = carDto.CategoryId,
        //        IsAvailable = carDto.IsAvailable,
        //        PricePerDay = carDto.PricePerDay,
        //        ImageUrl = carDto.ImageUrl,
        //        Seat = carDto.NumberOfSeats,
        //        TransmissionId = carDto.TransmissionId,
        //        FuelTypeId = carDto.FuelTypeId,
        //        Features = new List<Feature>() // Initialize the features list
        //    };

        //    // Add features based on FeatureIds
        //    if (carDto.FeatureIds != null)
        //    {
        //        foreach (var featureId in carDto.FeatureIds)
        //        {
        //            var feature = await _featureRepository.GetFeatureByIdAsync(featureId);
        //            if (feature != null)
        //            {
        //                car.Features.Add(feature);
        //            }
        //        }
        //    }

        //    await _carRepository.AddCarAsync(car);
        //}


        public async Task AddCarAsync(CarDto carDto)
        {
            var car = new Car
            {
                ModelId = carDto.ModelId,
                Year = carDto.Year,
                RegistrationNumber = carDto.RegistrationNumber,
                CategoryId = carDto.CategoryId,
                IsAvailable = carDto.IsAvailable,
                PricePerDay = carDto.PricePerDay,
                ImageUrl = carDto.ImageUrl,
                TransmissionId = carDto.TransmissionId,
                FuelTypeId = carDto.FuelTypeId,
                Features = new List<Feature>(), // Initialize the features list
                Seats = new List<Seat>() // Initialize the seats list
            };

            // Add the number of seats
            for (int i = 0; i < carDto.NumberOfSeats; i++)
            {
                car.Seats.Add(new Seat { Id = i + 1, Type = $"Seat {i + 1}" }); // Create seat instances
            }

            // Add features based on FeatureIds
            if (carDto.FeatureIds != null)
            {
                foreach (var featureId in carDto.FeatureIds)
                {
                    var feature = await _featureRepository.GetFeatureByIdAsync(featureId);
                    if (feature != null)
                    {
                        car.Features.Add(feature);
                    }
                }
            }

            await _carRepository.AddCarAsync(car);
        }




        public async Task UpdateCarAsync(CarDto carDto)
        {
            var car = new Car
            {
                Id = carDto.Id,
                
                ModelId = carDto.ModelId,
                Year = carDto.Year,
                RegistrationNumber = carDto.RegistrationNumber,
                CategoryId = carDto.CategoryId,
                IsAvailable = carDto.IsAvailable,
                PricePerDay = carDto.PricePerDay,
                ImageUrl = carDto.ImageUrl
            };
            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteCarAsync(id);
        }


        // Category 

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _carRepository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _carRepository.GetCategoryByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new CarCategory
            {
                Name = categoryDto.Name
            };
            await _carRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new CarCategory
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name
            };
            await _carRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _carRepository.DeleteCategoryAsync(id);
        }




        // Model 

        public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
        {
            var models = await _carRepository.GetAllModelsAsync();
            return models.Select(m => new ModelDto
            {
                Id = m.Id,
                Name = m.Name,
                Make= m.Make,
                CategoryId = m.CategoryId
            }).ToList();
        }

        public async Task<ModelDto> GetModelByIdAsync(int id)
        {
            var model = await _carRepository.GetModelByIdAsync(id);
            if (model == null) return null;

            return new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                Make = model.Make,
                CategoryId = model.CategoryId
            };
        }

        public async Task AddModelAsync(ModelDto modelDto)
        {
            var model = new Model
            {
                Name = modelDto.Name,
                Make = modelDto.Make,
                CategoryId = modelDto.CategoryId
            };
            await _carRepository.AddModelAsync(model);
        }

        public async Task UpdateModelAsync(ModelDto modelDto)
        {
            var model = new Model
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Make = modelDto.Make,
                CategoryId = modelDto.CategoryId
            };
            await _carRepository.UpdateModelAsync(model);
        }

        public async Task DeleteModelAsync(int id)
        {
            await _carRepository.DeleteModelAsync(id);
        }

    }

}





        




