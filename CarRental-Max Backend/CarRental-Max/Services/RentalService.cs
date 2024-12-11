using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;

using CarRental_Max.Models.Car;
using CarRental_Max.Models.Rentals;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Services
{
    public class RentalService: IRentalService
    {

        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalService(IRentalRepository rentalRepository, ICarRepository carRepository,ICustomerRepository customerRepository)
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
                    Year = car.Year
                },
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                RentalDate = rental.RentalDate,
                Status = rental.Status,
                ReturnDate = rental.ReturnDate
            };
        }

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
                        Year = car.Year
                    },
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
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
            if (customer == null) return null;

            var rentals = await _rentalRepository.GetRentalsByCustomerIdAsync(customer.Id);

            // Assuming we need only the latest rental for details
            var rental = rentals.OrderByDescending(r => r.RentalDate).FirstOrDefault();
            if (rental == null) return null;

            var car = await _carRepository.GetCarByIdAsync(rental.CarId);

            return new RentalDetailsDto
            {
                Id = rental.Id,
                Car = new CarDto
                {
                    Id = car.Id,
                    ModelId = car.ModelId,
                    Year = car.Year
                },
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CustomerNIC = customer.Nic,
                RentalDate = rental.RentalDate,
                Status = rental.Status,
                ReturnDate = rental.ReturnDate
            };
        }

        public async Task RentCarAsync(int customerId, int carId)
        {
            var rental = new Rental
            {
                CustomerId = customerId,
                CarId = carId,
                RentalDate = DateTime.UtcNow,
                Status = RentalStatus.Pending,
                
            };

            await _rentalRepository.AddRentalAsync(rental);
        }

        public async Task<string> ReturnCarByNicAndRegistrationAsync(string nic, string carRegistrationNumber)
        {
            // Step 1: Retrieve rental details using NIC and registration number
            var rental = await _rentalRepository.GetRentalByNicAndCarRegistrationAsync(nic, carRegistrationNumber);
            if (rental == null)
            {
                return "No active rental found for this customer and specified car.";
            }

            // Proceed to return the car
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
        public async Task<string> AcceptRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return "Rental not found.";
            rental.Status = RentalStatus.Accepted;
            await _rentalRepository.UpdateRentalAsync(rental);
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
                        Year = car.Year
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



















    }



}

