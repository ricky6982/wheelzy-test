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
   - 1-b ) The issue was resolved within a solution consisting of multiple projects utilizing .NET 7.
     
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


