
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

        //public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        //{
        //    var cars = await _carRepository.GetAllCarsAsync();
        //    return cars.Where(c => c.IsAvailable) // Exclude rented cars
        //               .Select(c => new CarDto
        //               {
        //                   Id = c.Id,
        //                   ModelId = c.ModelId,
        //                   Year = c.Year,
        //                   RegistrationNumber = c.RegistrationNumber,
        //                   CategoryId = c.CategoryId,
        //                   IsAvailable = c.IsAvailable,
        //                   PricePerDay= c.PricePerDay,
        //                   ImageUrl = c.ImageUrl
        //               }).ToList();
        //}



        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            return cars.Where(c => c.IsAvailable) // Exclude rented cars
                       .Select(c => new CarDto
                       {
                           Id = c.Id,
                           ModelId = c.ModelId,
                           Year = c.Year,
                           RegistrationNumber = c.RegistrationNumber,
                           CategoryId = c.CategoryId,
                           IsAvailable = c.IsAvailable,
                           PricePerDay = c.PricePerDay,
                           ImageUrl = c.ImageUrl,
                           TransmissionId = c.TransmissionId,
                           FuelTypeId = c.FuelTypeId
                       }).ToList();
        }






        //public async Task<CarDto> GetCarByIdAsync(int id)
        //{
        //    var car = await _carRepository.GetCarByIdAsync(id);
        //    if (car == null) return null;

        //    return new CarDto
        //    {
        //        Id = car.Id,
        //        ModelId = car.ModelId,
        //        Year = car.Year,
        //        RegistrationNumber = car.RegistrationNumber,
        //        CategoryId = car.CategoryId,
        //        IsAvailable = car.IsAvailable,
        //        PricePerDay= car.PricePerDay,
        //        ImageUrl = car.ImageUrl
        //    };
        //}


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
                PricePerDay = car.PricePerDay,
                ImageUrl = car.ImageUrl,
                TransmissionId = car.TransmissionId,
                FuelTypeId = car.FuelTypeId
            };
        }







        //public async Task AddCarAsync(CarDto carDto)
        //{
        //    // Validate for duplicate registration number
        //    var existingCar = await _carRepository.GetCarByRegistrationNumberAsync(carDto.RegistrationNumber);
        //    if (existingCar != null)
        //    {
        //        throw new ArgumentException("A car with the same registration number already exists.");
        //    }

        //    var car = new Car
        //    {
        //        ModelId = carDto.ModelId,
        //        Year = carDto.Year,
        //        RegistrationNumber = carDto.RegistrationNumber,
        //        CategoryId = carDto.CategoryId,
        //        IsAvailable = carDto.IsAvailable,
        //        PricePerDay = carDto.PricePerDay,
        //        ImageUrl = carDto.ImageUrl,
        //        TransmissionId = carDto.TransmissionId,
        //        FuelTypeId = carDto.FuelTypeId,
        //        Features = new List<Feature>(),

        //    };


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
            // Validate for duplicate registration number
            var existingCar = await _carRepository.GetCarByRegistrationNumberAsync(carDto.RegistrationNumber);
            if (existingCar != null)
            {
                throw new ArgumentException("A car with the same registration number already exists.");
            }

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
                FuelTypeId = carDto.FuelTypeId
            };

            await _carRepository.AddCarAsync(car);
        }




        public async Task UpdateCarAsync(CarDto carDto)
        {
            var existingCar = await _carRepository.GetCarByIdAsync(carDto.Id);
            if (existingCar == null)
            {
                throw new ArgumentException("Car not found.");
            }

            // Validate for duplicate registration number
            var carWithSameRegNum = await _carRepository.GetCarByRegistrationNumberAsync(carDto.RegistrationNumber);
            if (carWithSameRegNum != null && carWithSameRegNum.Id != carDto.Id)
            {
                throw new ArgumentException("A car with the same registration number already exists.");
            }

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
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                throw new ArgumentException("Car not found.");
            }

            // Check if the car has any active rentals
            var hasActiveRentals = await _carRepository.HasActiveRentalsAsync(id);
            if (hasActiveRentals)
            {
                throw new InvalidOperationException("Cannot delete a car that has active rentals.");
            }

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


        public async Task<CarDto> GetCarByRegistrationNumberAsync(string registrationNumber)
        {
            var car = await _carRepository.GetCarByRegistrationNumberAsync(registrationNumber);
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
    }

}





        




