# Summary

## Technical Assessment with solutions
1) Define the database tables with its corresponding data types and relationships for the
   following case:
   A customer wants to sell his car so we need the car information (car year, make, model,
   and submodel), the location of the car (zip code) and the list of buyers that are willing to
   buy the car in that zip code. Each buyer will have a quote (amount) and only one buyer
   will be marked as the current one (not necessarily the one with the highest quote). We
   also want to track the progress of the case using different statuses (Pending
   Acceptance, Accepted, Picked Up, etc.) and considering that we care about the current
   status and the status history (previous statuses indicate when it happened, who changed
   it, etc.). The status “Picked Up” has a mandatory status date but the rest of the statuses
   don’t.
   Write a SQL query to show the car information, current buyer name with its quote and
   current status name with its status date. Do the same thing using Entity Framework.
   Make sure your queries don’t have any unnecessary data.

   - 1-a ) The issue was resolved in the next file: [SqlFile](WheelzyTest/Persistence/ScriptDatabase.sql)
   - 1-b ) The issue was resolved within a solution consisting of multiple projects utilizing .NET 7. You can find de Solution in the folder named WheelzyTest. 
     
     - The entity framework query was implemented in the following file: [CarService](WheelzyTest/Service/CarService.cs) 

2) What would you do if you had data that doesn’t change often but it’s used pretty much all
   the time?

   - I would use a caching mechanism to store the data in memory. This would allow for faster access to the data and reduce the load on the database. For instance, I would add CacheManager in each endpoint of the API.
   

3) Analyze the following method and make changes to make it better. Explain your
   changes.
 
```csharp
public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices)
{
    foreach (var invoice in invoices)
    {
        var customer =
            dbContext.Customers.SingleOrDefault(invoice.CustomerId.Value);
        customer.Balance -= invoice.Total;
        dbContext.SaveChanges();
    }
}
```
Solution:

```csharp

public class InvoiceService : IInvoiceService // I guess this is the name of the service
{
   public ICustomerService CustomerService { get; set; } // Injected by DI
   
   public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices)
   {
       var countSuccess = 0;
       var countError = 0;
       foreach (var invoice in invoices)
       {
           try 
           {
              CustomerService.DecreaseBalance(customer.Id, invoice.Total); // Delegate the responsibility to the CustomerService
                countSuccess++;
           }
           catch (Exception ex)
           {
               // Log the exception but don't stop the process
                countError++;
           }
       }
         // Log the countSuccess and countError
   }
}

public class CustomerService : ICustomerService
{
    public Customer DecreaseBalance(int customerId, decimal amount)
    {
        var customer = dbContext.Customers.SingleOrDefault(customerId);
        if (customer == null) // Add validation
            throw new Exception("Customer not found");
        
        customer.Balance -= amount;
        dbContext.SaveChanges();

        return customer;
    }
}

```


4) Implement the following method using Entity Framework, making sure your query is
   efficient in all the cases (when all the parameters are set, when some of them are or
   when none of them are). If a “filter” is not set it means that it will not apply any filtering
   over that field (no ids provided for customer ids it means we don’t want to filter by
   customer).

```csharp
public async Task<List<OrderDTO>> GetOrders(DateTime dateFrom, DateTime dateTo, // I changed here the return type to List<OrderDTO> instead of a single OrderDTO because the method name is GetOrders.
    List<int> customerIds, List<int> statusIds, bool? isActive)
{
    var query = dbContext.Orders
        .Include(x => x.Customer)
        .Where(x => x.Date >= dateFrom && x.Date <= dateTo);

    if (customerIds != null && customerIds.Any())
        query = query.Where(x => customerIds.Contains(x.Customer.Id));

    if (statusIds != null && statusIds.Any())
        query = query.Where(x => statusIds.Contains(x.StatusId));

    if (isActive.HasValue)
        query = query.Where(x => x.IsActive == isActive.Value);

    var orders = await query.ToListAsync();

    return orders.Select(mapper.Map<OrderDTO>).ToList(); // I consider that the project is using AutoMapper
}
```

5) Bill, from the QA Department, assigned you a high priority task indicating there’s a bug when someone changes the status from “Accepted” to “Picked Up”.

   Define how you would proceed, step by step, until you create the Pull Request.

    - I would start by reproducing the bug in my local environment, with the help of the QA Department. Obviously, setting all the necessary data equal to the production environment. 
    - Then I would create a new branch from the main branch, its name would be something like "fix/bug-accepted-to-picked-up".
    - If there are tests, that none of them are failing, I would write a new test to reproduce the bug. 
    - I would fix the bug and run the tests again.
    - If all the tests are passing, I would push the branch to the remote repository and create a Pull Request, first to the test branch and then to the main branch.
    
