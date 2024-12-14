using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CarRental_Max.Models.Car;
using CarRental_Max.Models.Rentals;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalService(IRentalRepository rentalRepository, ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        public async Task<RentalDto> GetRentalByIdAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return null;

            var car = await _carRepository.GetCarByIdAsync(rental.CarId);
            var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

            return new RentalDto
            {
                Id = rental.Id,
                Car = new CarDto
                {
                    Id = car.Id,
                    ModelId = car.ModelId,
                    Year = car.Year,
                    RegistrationNumber= car.RegistrationNumber,
                },
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                RentalDate = rental.RentalDate,
                Status = rental.Status,
                ReturnDate = rental.ReturnDate
            };
        }

        //public async Task<IEnumerable<RentalDto>> GetAllRentalsAsync()
        //{
        //    var rentals = await _rentalRepository.GetAllRentalsAsync();
        //    var rentalDtos = new List<RentalDto>();

        //    foreach (var rental in rentals)
        //    {
        //        var car = await _carRepository.GetCarByIdAsync(rental.CarId);
        //        var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

        //        rentalDtos.Add(new RentalDto
        //        {
        //            Id = rental.Id,
        //            Car = new CarDto
        //            {
        //                Id = car.Id,
        //                ModelId = car.ModelId,
        //                Year = car.Year
        //            },
        //            CustomerName = $"{customer.FirstName} {customer.LastName}",
        //            RentalDate = rental.RentalDate,
        //            Status = rental.Status,
        //            ReturnDate = rental.ReturnDate
        //        });
        //    }

        //    return rentalDtos;
        //}



        public async Task<IEnumerable<RentalDto>> GetAllRentalsAsync()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var rentalDtos = new List<RentalDto>();

            foreach (var rental in rentals)
            {
                var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

                rentalDtos.Add(new RentalDto
                {
                    Id = rental.Id,
                    Car = new CarDto
                    {
                        Id = car.Id,
                        ModelId = car.ModelId,
                        Year = car.Year,
                        RegistrationNumber = car.RegistrationNumber,
                        CategoryId = car.CategoryId,
                        IsAvailable = car.IsAvailable,
                        ImageUrl = car.ImageUrl,
                        PricePerDay = car.PricePerDay // Include PricePerDay
                    },
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    CustomerNIC = customer.Nic, // Include NIC
                    RentalDate = rental.RentalDate,
                    Status = rental.Status,
                    ReturnDate = rental.ReturnDate
                });
            }

            return rentalDtos;
        }




        public async Task<RentalDetailsDto> GetRentalDetailsByNicAsync(string nic)
        {
            var customer = await _customerRepository.GetCustomerByNicAsync(nic);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found for the provided NIC.");
            }

            var rentals = await _rentalRepository.GetRentalsByCustomerIdAsync(customer.Id);
            var rental = rentals.OrderByDescending(r => r.RentalDate).FirstOrDefault();
            if (rental == null)
            {
                throw new InvalidOperationException("No rentals found for this customer.");
            }

            var car = await _carRepository.GetCarByIdAsync(rental.CarId);
            if (car == null)
            {
                throw new InvalidOperationException("Car not found for the rental.");
            }

            return new RentalDetailsDto
            {
                Id = rental.Id,
                Car = new CarDto
                {
                    Id = car.Id,
                    ModelId = car.ModelId,
                    Year = car.Year,
                    RegistrationNumber=car.RegistrationNumber
                },
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CustomerNIC = customer.Nic,
                RentalDate = rental.RentalDate,
                Status = rental.Status,
                ReturnDate = rental.ReturnDate
            };
        }

        //public async Task RentCarAsync(int customerId, int carId)
        //{
        //    // Check if the customer exists
        //    var customerExists = await _customerRepository.CustomerExistsAsync(customerId);
        //    if (!customerExists)
        //    {
        //        throw new ArgumentException("Customer does not exist.");
        //    }

        //    // Check if the car exists
        //    var carExists = await _carRepository.CarExistsAsync(carId);
        //    if (!carExists)
        //    {
        //        throw new ArgumentException("Car does not exist.");
        //    }

        //    // Create the rental object
        //    var rental = new Rental
        //    {
        //        CustomerId = customerId,
        //        CarId = carId,
        //        RentalDate = DateTime.UtcNow,
        //        Status = RentalStatus.Pending,
        //    };

        //    // Add the rental to the repository
        //    await _rentalRepository.AddRentalAsync(rental);
        //}


        public async Task<RentalDetailsDto> RentCarAsync(int customerId, int carId, int rentalDays)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            if (car == null || !car.IsAvailable)
            {
                throw new InvalidOperationException("Car is not available for rent.");
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            var rental = new Rental
            {
                CustomerId = customerId,
                CarId = carId,
               
                RentalDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(rentalDays),
                Status = RentalStatus.Pending // Set status to Pending
            };

            await _rentalRepository.AddRentalAsync(rental);

            return new RentalDetailsDto
            {
                Id = rental.Id,
                Car = new CarDto
                {
                    Id = car.Id,
                    ModelId = car.ModelId,
                    Year = car.Year,
                    RegistrationNumber = car.RegistrationNumber,
                    CategoryId = car.CategoryId,
                    IsAvailable = car.IsAvailable,
                    ImageUrl = car.ImageUrl
                },
                CustomerName = customer.FirstName,
                CustomerNIC = customer.Nic,
                RentalDate = rental.RentalDate,
                Status = rental.Status,
                ReturnDate = rental.ReturnDate
            };
        }









        public async Task<string> ReturnCarByNicAndRegistrationAsync(int id)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(id);
            if (rental == null)
            {
                return "No active rental found for this customer and specified car.";
            }

            var currentDate = DateTime.UtcNow;
            var dueDate = rental.ReturnDate ?? currentDate;
            bool isOverdue = currentDate > dueDate;
            decimal overdueFees = 0;

            if (isOverdue)
            {
                var overdueDays = (currentDate - dueDate).Days;
                overdueFees = overdueDays * 10m; // Example fee per day
            }

            rental.Status = RentalStatus.Returned;
            rental.ReturnDate = currentDate;

            await _rentalRepository.UpdateRentalAsync(rental);

            var totalRentalDays = (currentDate - rental.RentalDate).Days;
            var totalCost = (totalRentalDays * rental.Car.PricePerDay) + overdueFees;

            return isOverdue
                ? $"Car returned successfully. Total cost: {totalCost:C}. Overdue fees: {overdueFees:C}."
                : $"Car returned successfully. Total cost: {totalCost:C}.";
        }

        //public async Task<string> AcceptRentalAsync(int rentalId)
        //{
        //    var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
        //    if (rental == null) return "Rental not found.";
        //    rental.Status = RentalStatus.Accepted;
        //    await _rentalRepository.UpdateRentalAsync(rental);
        //    return "Rental accepted.";
        //}

        public async Task<string> AcceptRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return "Rental not found.";

            rental.Status = RentalStatus.Accepted; // Set status to Accepted
            await _rentalRepository.UpdateRentalAsync(rental);

            // Update car availability only after acceptance
            var car = await _carRepository.GetCarByIdAsync(rental.CarId);
            if (car != null)
            {
                car.IsAvailable = false; // Mark car as not available
                await _carRepository.UpdateCarAsync(car);
            }

            return "Rental accepted.";
        }

        public async Task<string> RejectRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return "Rental not found.";
            rental.Status = RentalStatus.Rejected;
            await _rentalRepository.UpdateRentalAsync(rental);
            return "Rental rejected.";
        }

        public async Task<IEnumerable<RentalDto>> GetRentalHistoryByCustomerIdAsync(int customerId)
        {
            var rentals = await _rentalRepository.GetRentalsByCustomerIdAsync(customerId);
            var rentalDtos = new List<RentalDto>();

            foreach (var rental in rentals)
            {
                var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

                rentalDtos.Add(new RentalDto
                {
                    Id = rental.Id,
                    Car = new CarDto
                    {
                        Id = car.Id,
                        ModelId = car.ModelId,
                        Year = car.Year,
                        RegistrationNumber= car.RegistrationNumber
                    },
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    RentalDate = rental.RentalDate,
                    Status = rental.Status,
                    ReturnDate = rental.ReturnDate
                });
            }

            return rentalDtos;
        }

        public async Task<string> ExtendRentalPeriodAsync(ExtendRentalDto extendRentalDto)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(extendRentalDto.RentalId);
            if (rental == null) return "Rental not found.";

            if (extendRentalDto.NewReturnDate <= rental.RentalDate)
                return "New return date must be after the current rental date.";

            rental.ReturnDate = extendRentalDto.NewReturnDate;
            await _rentalRepository.UpdateRentalAsync(rental);

            return "Rental period extended successfully.";
        }

        public async Task<IEnumerable<OverdueCarDto>> GetOverdueCarsAsync(decimal overdueFeePerDay)
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var overdueCars = new List<OverdueCarDto>();

            foreach (var rental in rentals)
            {
                if (rental.Status != RentalStatus.Returned && rental.ReturnDate < DateTime.UtcNow)
                {
                    var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);
                    var car = await _carRepository.GetCarByIdAsync(rental.CarId);

                    var overdueDays = (DateTime.UtcNow - rental.ReturnDate.Value).Days;
                    var overdueFees = overdueDays * overdueFeePerDay;

                    overdueCars.Add(new OverdueCarDto
                    {
                        RentalId = rental.Id,
                        CustomerName = $"{customer.FirstName} {customer.LastName}",
                        ModelId = car.ModelId,
                        DueDate = rental.ReturnDate.Value,
                        OverdueFees = overdueFees
                    });
                }
            }

            return overdueCars;
        }

        public async Task<string> UpdateOverdueFeeAsync(UpdateOverdueFeeDto updateOverdueFeeDto)
        {
            return $"Overdue fee updated to {updateOverdueFeeDto.NewOverdueFee}";
        }




        //public async Task<IEnumerable<RentalDto>> GetPendingRentalsAsync()
        //{
        //    var rentals = await _rentalRepository.GetAllRentalsAsync();
        //    var pendingRentals = rentals.Where(r => r.Status == RentalStatus.Pending);
        //    var rentalDtos = new List<RentalDto>();

        //    foreach (var rental in pendingRentals)
        //    {
        //        var car = await _carRepository.GetCarByIdAsync(rental.CarId);
        //        var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

        //        rentalDtos.Add(new RentalDto
        //        {
        //            Id = rental.Id,
        //            Car = new CarDto
        //            {
        //                Id = car.Id,
        //                ModelId = car.ModelId,
        //                Year = car.Year
        //            },
        //            CustomerName = $"{customer.FirstName} {customer.LastName}",
        //            RentalDate = rental.RentalDate,
        //            Status = rental.Status,
        //            ReturnDate = rental.ReturnDate
        //        });
        //    }

        //    return rentalDtos;
        //}


        public async Task<IEnumerable<RentalDto>> GetPendingRentalsAsync()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var pendingRentals = rentals.Where(r => r.Status == RentalStatus.Pending);
            var rentalDtos = new List<RentalDto>();

            foreach (var rental in pendingRentals)
            {
                var car = await _carRepository.GetCarByIdAsync(rental.CarId);
                var customer = await _customerRepository.GetCustomerByIdAsync(rental.CustomerId);

                rentalDtos.Add(new RentalDto
                {
                    Id = rental.Id,
                    Car = new CarDto
                    {
                        Id = car.Id,
                        ModelId = car.ModelId,
                        Year = car.Year,
                        RegistrationNumber = car.RegistrationNumber,
                        CategoryId = car.CategoryId,
                        IsAvailable = car.IsAvailable,
                        ImageUrl = car.ImageUrl,
                        PricePerDay = car.PricePerDay // Include PricePerDay
                    },
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    CustomerNIC = customer.Nic, // Include NIC
                    RentalDate = rental.RentalDate,
                    Status = rental.Status,
                    ReturnDate = rental.ReturnDate
                });
            }

            return rentalDtos;
        }




        public async Task<int> GetPendingCountAsync()
        {
            return await _rentalRepository.GetCountByStatusAsync(RentalStatus.Pending);
        }

        public async Task<int> GetAcceptedCountAsync()
        {
            return await _rentalRepository.GetCountByStatusAsync(RentalStatus.Accepted);
        }

        public async Task<int> GetRejectedCountAsync()
        {
            return await _rentalRepository.GetCountByStatusAsync(RentalStatus.Rejected);
        }

        public async Task<string> ConfirmRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return "Rental not found.";

            rental.Status = RentalStatus.Rented; // Change status to Rented
            await _rentalRepository.UpdateRentalAsync(rental);

            return "Rental confirmed and is now active.";
        }










    }
}