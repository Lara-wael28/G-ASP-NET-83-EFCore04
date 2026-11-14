using EFcore04.Data;
using EFcore04.Models;
using Microsoft.EntityFrameworkCore;

namespace EFcore04
{
    internal class Program
    {
        static void Main(string[] args)
        {
                using BankContext context = new BankContext();

            while (true)
                {
                    Console.Clear();

                    Console.WriteLine("======================================");
                    Console.WriteLine("     NATIONAL BANK MANAGEMENT SYSTEM");
                    Console.WriteLine("======================================");
                    Console.WriteLine("1) Add a new Customer");
                    Console.WriteLine("2) Open a new Account for a Customer");
                    Console.WriteLine("3) Update Account Status");
                    Console.WriteLine("4) Remove an Account from a Customer");
                    Console.WriteLine("5) List all Customers with accounts");
                    Console.WriteLine("0) Exit");
                    Console.WriteLine("======================================");

                    Console.Write("Choose an option: ");

                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int choice))
                    {
                        Console.WriteLine(
                            "Invalid input. Please enter a number.");

                        Pause();

                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            AddCustomer();
                            Pause();
                            break;

                        case 2:
                            OpenAccount();
                            Pause();
                            break;

                        case 3:
                            UpdateAccountStatus();
                            Pause();
                            break;

                        case 4:
                            RemoveAccountFromCustomer();
                            Pause();
                            break;

                        case 5:
                            ListCustomersWithAccounts();
                            Pause();
                            break;

                        case 0:
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            Console.WriteLine(
                                "Invalid option. Please choose from 0 to 5.");

                            Pause();
                            break;
                    }
                }
            }

            static void Pause()
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Press any key to return to the menu...");

                Console.ReadKey();
            }

            static void AddCustomer()
            {
                Console.Clear();

                Console.WriteLine("========== ADD CUSTOMER ==========");

                string fullName = ReadRequiredString(
                    "Full Name: ");

                string nationalId = ReadRequiredString(
                    "National ID: ");

                DateTime dateOfBirth = ReadDate(
                    "Date of Birth (yyyy-MM-dd): ");

                string email = ReadRequiredString(
                    "Email: ");

                string phone = ReadRequiredString(
                    "Phone Number: ");

                string address = ReadRequiredString(
                    "Address: ");

                string customerType;

                while (true)
                {
                    Console.Write("Customer Type (Individual/Business): ");

                    customerType = Console.ReadLine();

                    if (customerType == "Individual" ||
                        customerType == "Business")
                    {
                        break;
                    }

                    Console.WriteLine(
                        "Please enter Individual or Business.");
                }

                using var context = new BankContext();

                bool nationalIdExists = context.Customers
                    .Any(c => c.NationalId == nationalId);

                if (nationalIdExists)
                {
                    Console.WriteLine(
                        "A customer with this National ID already exists.");

                    return;
                }

                var customer = new Customer
                {
                    FullName = fullName,
                    NationalId = nationalId,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    CustomerType = customerType
                };

                context.Customers.Add(customer);

                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine(
                    $"Customer added successfully. Id = {customer.Id}");
            }

            static void OpenAccount()
            {
                Console.Clear();

                Console.WriteLine("========== OPEN ACCOUNT ==========");

                string accountNumber = ReadRequiredString(
                    "Account Number: ");

                string accountType = ReadRequiredString(
                    "Account Type: ");

                string branchCode = ReadRequiredString(
                    "Branch Code: ");

                int customerId = ReadInt(
                    "Customer Id: ");

                string ownershipType;

                while (true)
                {
                    Console.Write(
                        "Ownership Role (Primary/CoHolder): ");

                    ownershipType = Console.ReadLine();

                    if (ownershipType == "Primary" ||
                        ownershipType == "CoHolder")
                    {
                        break;
                    }

                    Console.WriteLine(
                        "Please enter Primary or CoHolder.");
                }

                using var context = new BankContext();

                var branch = context.Branches.FirstOrDefault(b => b.code == branchCode);

                if (branch == null)
                {
                    Console.WriteLine(
                        "Branch not found.");

                    return;
                }
                var customer = context.Customers
                    .Find(customerId);

                if (customer == null)
                {
                    Console.WriteLine(
                        "Customer not found.");

                    return;
                }

                bool accountExists = context.Accounts
                    .Any(a => a.AccountNumber == accountNumber);

                if (accountExists)
                {
                    Console.WriteLine(
                        "An account with this number already exists.");

                    return;
                }

                var account = new Account
                {
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    CurrentBalance = 0,
                    OpeningDate = DateTime.Now,
                    BranchCode = branchCode
                };

                context.Accounts.Add(account);

                var customerAccount = new CustomerAccount
                {
                    CustomerId = customerId,
                    AccountNumber = accountNumber,
                    OwnershipType = ownershipType,
                    OwnershipStartDate = DateTime.Now,
                    AccountStatus = "Active"
                };

                context.CustomerAccounts.Add(customerAccount);

                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine(
                    "Account opened successfully.");
            }
            static void UpdateAccountStatus()
            {
                Console.Clear();

                Console.WriteLine(
                    "========== UPDATE ACCOUNT STATUS ==========");

                string accountNumber = ReadRequiredString(
                    "Account Number: ");

                int customerId = ReadInt(
                    "Customer Id: ");

                using var context = new BankContext();

                var customerAccount = context.CustomerAccounts
                    .FirstOrDefault(ca =>
                        ca.AccountNumber == accountNumber &&
                        ca.CustomerId == customerId);

                if (customerAccount == null)
                {
                    Console.WriteLine(
                        "Customer account relationship not found.");

                    return;
                }

                if (customerAccount.AccountStatus == "Active")
                {
                    customerAccount.AccountStatus = "Inactive";
                }
                else
                {
                    customerAccount.AccountStatus = "Active";
                }

                context.SaveChanges();

                Console.WriteLine(
                    $"Account status changed to: " +
                    $"{customerAccount.AccountStatus}");
            }
            static void RemoveAccountFromCustomer()
            {
                Console.Clear();

                Console.WriteLine(
                    "========== REMOVE ACCOUNT FROM CUSTOMER ==========");

                string accountNumber = ReadRequiredString(
                    "Account Number: ");

                int customerId = ReadInt(
                    "Customer Id: ");

                using var context = new BankContext();

                var customerAccount = context.CustomerAccounts
                    .FirstOrDefault(ca =>
                        ca.AccountNumber == accountNumber &&
                        ca.CustomerId == customerId);

                if (customerAccount == null)
                {
                    Console.WriteLine(
                        "Customer account relationship not found.");

                    return;
                }

                context.CustomerAccounts.Remove(customerAccount);

                context.SaveChanges();

                Console.WriteLine(
                    "Account removed from customer successfully.");
            }

            static void ListCustomersWithAccounts()
            {
                Console.Clear();

                Console.WriteLine(
                    "========== CUSTOMERS WITH ACCOUNTS ==========");

                using var context = new BankContext();

                var customers = context.Customers
                    .Include(c => c.CustomerAccounts)
                    .ThenInclude(ca => ca.Account)
                    .ToList();

                if (customers.Count == 0)
                {
                    Console.WriteLine(
                        "No customers found.");

                    return;
                }

                foreach (var customer in customers)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------");

                    Console.WriteLine(
                        $"Customer Id   : {customer.Id}");

                    Console.WriteLine(
                        $"Full Name     : {customer.FullName}");

                    Console.WriteLine(
                        $"National ID   : {customer.NationalId}");

                    Console.WriteLine(
                        $"Email         : {customer.Email}");

                    Console.WriteLine(
                        $"Phone         : {customer.PhoneNumber}");

                    Console.WriteLine(
                        $"Address       : {customer.Address}");

                    Console.WriteLine(
                        $"Customer Type : {customer.CustomerType}");

                    Console.WriteLine("Accounts:");

                    if (customer.CustomerAccounts.Count == 0)
                    {
                        Console.WriteLine(
                            "  No accounts.");
                    }
                    else
                    {
                        foreach (
                            var customerAccount
                            in customer.CustomerAccounts)
                        {
                            var account =
                                customerAccount.Account;

                            Console.WriteLine(
                                $"  Account Number : " +
                                $"{account.AccountNumber}");

                            Console.WriteLine(
                                $"  Account Type   : " +
                                $"{account.AccountType}");

                            Console.WriteLine(
                                $"  Balance        : " +
                                $"{account.CurrentBalance}");

                            Console.WriteLine(
                                $"  Ownership      : " +
                                $"{customerAccount.OwnershipType}");

                            Console.WriteLine(
                                $"  Status         : " +
                                $"{customerAccount.AccountStatus}");

                            Console.WriteLine();
                        }
                    }
                }
            }

            static string ReadRequiredString(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string value = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return value;
                    }

                    Console.WriteLine(
                        "This field cannot be empty.");
                }
            }
            static int ReadInt(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int value))
                    {
                        return value;
                    }

                    Console.WriteLine(
                        "Invalid number. Please try again.");
                }
            }
            static DateTime ReadDate(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (DateTime.TryParse(input, out DateTime date))
                    {
                        return date;
                    }

                    Console.WriteLine(
                        "Invalid date. Please try again.");
                }
            }
        }
    }

