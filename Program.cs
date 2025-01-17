using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class ParkingLot
    {
        private readonly Dictionary<int, Vehicle> _slots;
        private readonly int _capacity;

        public ParkingLot(int capacity)
        {
            _capacity = capacity;
            _slots = new Dictionary<int, Vehicle>();
        }

        public string Park(Vehicle vehicle)
        {
            if (_slots.Values.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            {
                return $"Vehicle with registration number {vehicle.RegistrationNumber} is already parked.";
            }

            if (vehicle.Type.ToLower() != "mobil" && vehicle.Type.ToLower() != "motor")
            {
                return "Only 'Mobil' or 'Motor' types are allowed.";
            }

            if (_slots.Count >= _capacity)
            {
                return "Sorry, parking lot is full";
            }

            int slot = Enumerable.Range(1, _capacity).FirstOrDefault(s => !_slots.ContainsKey(s));
            _slots[slot] = vehicle;
            return $"Allocated slot number: {slot}";
        }

        public string Leave(int slot)
        {
            if (!_slots.ContainsKey(slot))
            {
                return $"Slot number {slot} is already empty.";
            }

            _slots.Remove(slot);
            return $"Slot number {slot} is free.";
        }

        public string Status()
        {
            if (_slots.Count == 0)
            {
                return "Parking lot is empty";
            }

            var rows = new List<string>
            {
                string.Format("{0,-6} {1,-16} {2,-10} {3,-10}", "Slot", "No.Registration", "Type", "Colour")
            };

            rows.AddRange(_slots.Select(s =>
                string.Format("{0,-6} {1,-16} {2,-10} {3,-10}",
                              s.Key, s.Value.RegistrationNumber, s.Value.Type, s.Value.Colour)));

            return string.Join(Environment.NewLine, rows);
        }

        public int GetAvailableSlots() => _capacity - _slots.Count;

        public int GetOccupiedSlots() => _slots.Count;

        public IEnumerable<Vehicle> GetVehiclesByColour(string colour)
            => _slots.Values.Where(v => v.Colour.Equals(colour, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Vehicle> GetVehiclesByType(string type)
            => _slots.Values.Where(v => v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Vehicle> GetVehiclesByPlate(bool isOdd)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"\d+");
            return _slots.Values.Where(v =>
            {
                var match = regex.Match(v.RegistrationNumber);
                if (match.Success && int.TryParse(match.Value, out int numericPart))
                {
                    return isOdd ? numericPart % 2 != 0 : numericPart % 2 == 0;
                }
                return false;
            });
        }

        public int GetSlotByRegistration(string registration)
            => _slots.FirstOrDefault(kvp => kvp.Value.RegistrationNumber == registration).Key;
    }

    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parkingLot = null;
            Console.WriteLine("Welcome to the Parking System");

            while (true)
            {
                try
                {
                    Console.Write("Enter command: ");
                    var input = Console.ReadLine()?.Trim().Split(' ');
                    if (input == null || input.Length == 0) continue;

                    var command = input[0].ToLower();
                    switch (command)
                    {
                        case "create_parking_lot":
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: create_parking_lot <capacity>");
                                continue;
                            }
                            int capacity = int.Parse(input[1]);
                            parkingLot = new ParkingLot(capacity);
                            Console.WriteLine($"Created a parking lot with {capacity} slots");
                            break;

                        case "park":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 4)
                            {
                                Console.WriteLine("Usage: park <registration_number> <colour> <type>");
                                continue;
                            }
                            var vehicle = new Vehicle
                            {
                                RegistrationNumber = input[1],
                                Colour = input[2],
                                Type = input[3]
                            };
                            Console.WriteLine(parkingLot.Park(vehicle));
                            break;

                        case "leave":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: leave <slot_number>");
                                continue;
                            }
                            int slot = int.Parse(input[1]);
                            Console.WriteLine(parkingLot.Leave(slot));
                            break;

                        case "status":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            Console.WriteLine(parkingLot.Status());
                            break;

                        case "type_of_vehicles":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: type_of_vehicles <type>");
                                continue;
                            }
                            string type = input[1];
                            Console.WriteLine(parkingLot.GetVehiclesByType(type).Count());
                            break;

                        case "registration_numbers_for_vehicles_with_colour":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: registration_numbers_for_vehicles_with_colour <colour>");
                                continue;
                            }
                            string colour = input[1];
                            var vehiclesWithColour = parkingLot.GetVehiclesByColour(colour)
                                                               .Select(v => v.RegistrationNumber);
                            Console.WriteLine(vehiclesWithColour.Any()
                                ? string.Join(", ", vehiclesWithColour)
                                : $"No vehicles with colour {colour}.");
                            break;

                        case "registration_numbers_for_vehicles_with_ood_plate":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            var oddPlates = parkingLot.GetVehiclesByPlate(true)
                                                      .Select(v => v.RegistrationNumber);
                            Console.WriteLine(oddPlates.Any()
                                ? string.Join(", ", oddPlates)
                                : "No vehicles with odd-numbered plates.");
                            break;

                        case "registration_numbers_for_vehicles_with_event_plate":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            var evenPlates = parkingLot.GetVehiclesByPlate(false)
                                                       .Select(v => v.RegistrationNumber);
                            Console.WriteLine(evenPlates.Any()
                                ? string.Join(", ", evenPlates)
                                : "No vehicles with even-numbered plates.");
                            break;

                        case "slot_numbers_for_vehicles_with_colour":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: slot_numbers_for_vehicles_with_colour <colour>");
                                continue;
                            }
                            string color = input[1];
                            var slotsWithColour = parkingLot.GetVehiclesByColour(color)
                                                            .Select(v => parkingLot.GetSlotByRegistration(v.RegistrationNumber));
                            Console.WriteLine(slotsWithColour.Any()
                                ? string.Join(", ", slotsWithColour)
                                : $"No vehicles with colour {color}.");
                            break;

                        case "slot_number_for_registration_number":
                            if (parkingLot == null)
                            {
                                Console.WriteLine("Parking lot not created yet.");
                                continue;
                            }
                            if (input.Length < 2)
                            {
                                Console.WriteLine("Usage: slot_number_for_registration_number <registration_number>");
                                continue;
                            }
                            string registration = input[1];
                            int slotNumber = parkingLot.GetSlotByRegistration(registration);
                            Console.WriteLine(slotNumber > 0
                                ? slotNumber.ToString()
                                : "Not found");
                            break;

                        case "exit":
                            return;

                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
