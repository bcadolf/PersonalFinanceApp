

using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data.Models;
using PersonalFinanceApp.Data.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace PersonalFinanceApp.Data.Data;


public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.Migrate();

        // Seed UserProfile
        if (!context.Users.Any())
        {
            var userProfile = new UserProfile
            {
                Id = Guid.Parse("d1c9b8e5-9c3a-4f0b-8c2e-1a2b3c4d5e6f"),
                UserName = "testuser@example.com",
                Email = "testuser@example.com",
                FirstName = "Test",
                LastName = "User"
            };

            // Set password hash for the user
            var passwordHasher = new PasswordHasher<UserProfile>();
            userProfile.PasswordHash = passwordHasher.HashPassword(userProfile, "Password123!");

            context.Users.Add(userProfile);
            context.SaveChanges();
        }

        var userProfileId = Guid.Parse("d1c9b8e5-9c3a-4f0b-8c2e-1a2b3c4d5e6f");

        // Seed FinanceCategories
        if (!context.FinanceCategories.Any())
        {
            var categories = new List<FinanceCategory>
            {
                new FinanceCategory
                {
                    Id = Guid.Parse("50e14f43-dd4e-412f-864d-78943ea28d91"),
                    CategoryName = "Groceries",
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new FinanceCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Transportation",
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new FinanceCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Entertainment",
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new FinanceCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Utilities",
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                }
            };

            context.FinanceCategories.AddRange(categories);
            context.SaveChanges();
        }

        // Seed Budgets
        if (!context.Set<Budget>().Any())
        {
            var budget = new Budget
            {
                Id = Guid.NewGuid(),
                BudgetName = "Monthly Budget",
                UserProfileId = userProfileId,
                IsCurrent = true,
                lastUpdated = DateTime.UtcNow,
                isDeleted = false
            };

            context.Set<Budget>().Add(budget);
            context.SaveChanges();
        }

        var budgetId = context.Set<Budget>().First(b => b.UserProfileId == userProfileId).Id;

        // Seed BudgetItems
        if (!context.BudgetItems.Any())
        {
            var financeCategoryIds = context.FinanceCategories.Where(fc => fc.UserProfileId == userProfileId).ToList();

            var budgetItems = new List<BudgetItem>
            {
                new BudgetItem
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Groceries",
                    Amount = 200.00m,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Groceries").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new BudgetItem
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Gas",
                    Amount = 150.00m,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Transportation").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new BudgetItem
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Movies",
                    Amount = 50.00m,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Entertainment").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                }
            };

            context.BudgetItems.AddRange(budgetItems);
            context.SaveChanges();
        }

        // Seed FinanceAccounts
        if (!context.Set<FinanceAccount>().Any())
        {
            var accounts = new List<FinanceAccount>
            {
                new FinanceAccount
                {
                    Id = Guid.NewGuid(),
                    AccountName = "Checking Account",
                    Balance = 1500.00m,
                    UserProfileId = userProfileId,
                    FinanceAccountBank = "Bank of America",
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new FinanceAccount
                {
                    Id = Guid.NewGuid(),
                    AccountName = "Savings Account",
                    Balance = 5000.00m,
                    UserProfileId = userProfileId,
                    FinanceAccountBank = "Chase",
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                }
            };

            context.Set<FinanceAccount>().AddRange(accounts);
            context.SaveChanges();
        }

        var accountIds = context.Set<FinanceAccount>().Where(fa => fa.UserProfileId == userProfileId).ToList();

        // Seed Transactions
        if (!context.Set<Transaction>().Any())
        {
            var financeCategoryIds = context.FinanceCategories.Where(fc => fc.UserProfileId == userProfileId).ToList();

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    Description = "Grocery Shopping",
                    Amount = -75.50m,
                    Date = DateTime.UtcNow.AddDays(-5),
                    IsPending = false,
                    FinanceAccountId = accountIds.First().Id,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Groceries").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    Description = "Gas Station",
                    Amount = -45.00m,
                    Date = DateTime.UtcNow.AddDays(-3),
                    IsPending = false,
                    FinanceAccountId = accountIds.First().Id,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Transportation").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    Description = "Movie Tickets",
                    Amount = -25.00m,
                    Date = DateTime.UtcNow.AddDays(-1),
                    IsPending = false,
                    FinanceAccountId = accountIds.First().Id,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Entertainment").Id,
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    Description = "Salary Deposit",
                    Amount = 2000.00m,
                    Date = DateTime.UtcNow.AddDays(-10),
                    IsPending = false,
                    FinanceAccountId = accountIds.First().Id,
                    FinanceCategoryId = financeCategoryIds.First(fc => fc.CategoryName == "Utilities").Id, // Using utilities as income category for now
                    UserProfileId = userProfileId,
                    lastUpdated = DateTime.UtcNow,
                    isDeleted = false
                }
            };

            context.Set<Transaction>().AddRange(transactions);
            context.SaveChanges();
        }
    }
}