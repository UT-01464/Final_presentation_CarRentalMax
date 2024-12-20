


select * from Transmissions

INSERT INTO Transmissions (Type)
VALUES 
    ('Manual'),
    ('Automatic'),
    ('Semi-Automatic'),
    ('CVT (Continuously Variable Transmission)'),
    ('Dual-Clutch Transmission (DCT)'),
    ('Tiptronic'),
    ('AMT (Automated Manual Transmission)'),
    ('Torque Converter Automatic'),
    ('Direct Shift Gearbox (DSG)'),
    ('Sequential Manual Transmission (SMT)');


SELECT * FROM FuelTypes;

INSERT INTO FuelTypes (Type)
VALUES 
    ('Petrol'),
    ('Diesel'),
    ('Electric'),
    ('Hybrid'),
    ('CNG'),
    ('LPG'),
    ('Ethanol'),
    ('Biodiesel'),
    ('Hydrogen'),
    ('Methanol');

select * from Features

INSERT INTO Features (Name)
VALUES 
    ('Air Conditioning'),
    ('Bluetooth'),
    ('Cruise Control'),
    ('Sunroof'),
    ('Leather Seats'),
    ('Backup Camera'),
    ('GPS Navigation'),
    ('Heated Seats'),
    ('Alloy Wheels'),
    ('Power Windows');


select * from CarCategories
INSERT INTO CarCategories (Name)
VALUES 
    ('Sedan'),
    ('SUV'),
    ('Hatchback'),
    ('Convertible'),
    ('Coupe'),
    ('Wagon'),
    ('Minivan'),
    ('Truck'),
    ('Sports Car'),
    ('Luxury');

select * from Models

INSERT INTO Models (Name, CategoryId, Make)
VALUES 
    ('Model S', 10, 'Tesla'),
    ('Civic', 1, 'Honda'),
    ('Camry', 1, 'Toyota'),
    ('Mustang', 9, 'Ford'),
    ('X5', 2, 'BMW'),
    ('A3', 5, 'Audi'),
    ('RAV4', 2, 'Toyota'),
    ('F-150', 8, 'Ford'),
    ('Charger', 9, 'Dodge'),
    ('Accord', 1, 'Honda');

select *  from Cars
ALTER TABLE cars ADD NumberOfSeats INT;


INSERT INTO Cars (ModelId, Year, RegistrationNumber, CategoryId, PricePerDay, IsAvailable, ImageUrl, NumberOfSeats, TransmissionId, FuelTypeId)
VALUES 
    (1, 2023, 'ABC1234', 1, 70.00, 1, 'https://cdn.pixabay.com/photo/2017/12/28/12/02/chevrolet-3040155_960_720.jpg', 5, 1, 2),
    (2, 2022, 'XYZ5678', 2, 90.00, 1, 'https://cdn.pixabay.com/photo/2020/01/21/13/58/audi-4785244_960_720.jpg', 5, 2, 1),
    (3, 2021, 'LMN9101', 3, 50.00, 1, 'https://cdn.pixabay.com/photo/2020/02/21/14/02/mercedes-4862621_960_720.jpg', 4, 1, 3),
    (4, 2023, 'DEF2345', 1, 100.00, 1, 'https://cdn.pixabay.com/photo/2019/01/17/20/18/luxury-car-3934485_960_720.jpg', 5, 2, 1),
    (5, 2020, 'GHI3456', 4, 60.00, 1, 'https://cdn.pixabay.com/photo/2016/11/18/13/47/car-1835505_960_720.jpg', 7, 1, 2),
    (6, 2019, 'JKL4567', 2, 120.00, 1, 'https://cdn.pixabay.com/photo/2015/01/19/13/51/car-604019_960_720.jpg', 5, 2, 1),
    (7, 2023, 'MNO5678', 3, 80.00, 1, 'https://cdn.pixabay.com/photo/2020/04/29/21/51/sports-car-5100221_960_720.jpg', 2, 1, 3),
    (8, 2022, 'PQR6789', 1, 75.00, 1, 'https://cdn.pixabay.com/photo/2015/09/02/12/45/suv-918408_960_720.jpg', 5, 1, 2),
    (9, 2021, 'STU7890', 4, 65.00, 1, 'https://cdn.pixabay.com/photo/2020/02/21/14/02/mercedes-4862621_960_720.jpg', 6, 2, 3),
    (10, 2023, 'VWX8901', 3, 150.00, 1, 'https://cdn.pixabay.com/photo/2018/01/08/12/35/bmw-3065407_960_720.jpg', 4, 1, 1);


select * from Customers
select * from Rentals