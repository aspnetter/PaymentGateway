# PaymentsGateway

This is an API based application that allows shoppers to pay for their products and merchants to run reporting on their income from sales. 

It was designed using DDD principles as, simple as it is right now, it is meant to evolve into a bigger and more complex system where it is important that the development team are aligned with the business goals. 

**MediatR** is used for handling asynchronous request / response communication between different components in and outside the Gateway which would definitely be the case in real world scenario.

## The structure

The structure and the architecture of the solution follows the DDD-oriented service design approach, including the following core layers:

 - **PaymentsGateway.Application** - the interaction layer, handling commands, queries and notifications from other application layers and systems (in our case, the API and the Bank Simulator)
 - **PaymentsGateway.Domain** where the business models, rules and the situation are expressed    
 - **PaymentsGateway.Infrastructure** - DB persistence layer based on EF Core
    
The two additional projects in the solution:

- **PaymentsGetway.API** - HTTP network access for the consumers of our Gateway service
- **BankSimulator** - a very simple .NET hosted service which runs a background task on the 20 seconds timer, checking if there are any pending / unprocessed payments in its in-memory storage, and decides on the status / response based on the hardcoded sample card numbers and possible statuses. After that, it notifies the PaymentGateway Application on the fact that the payment has been processed and the result.

## The flows

- **1.x** - The Shopper is making a new payment
- **2.x** - The Bank is processing a pending payment
- **3.x** - The Merchant is getting information about a payment

![Main Payment Gateway flows](https://github.com/aspnetter/PaymentGateway/blob/main/assets/Flow_diagram.png)

## PreRequisites

Since both the API and the bank simulator are containerized, you only need **Docker** to run the system

Since there is no UI, you will also need your favourite REST API client tool, e.g. **Postman** or **Curl** to test it

## Building & Running

Open the terminal, navigate to the project root folder

    cd <your_relative_path>/PaymentsGateway

and run 

    docker-compose up

to launch the Postgres database and run EF db migrations, then the PaymentsGateway API and the bank simulator worker service

    docker-compose down

 
to stop and remove the containers and all their resources

    dotnet test src/PaymentGateway.Domain.UnitTests

to run unit tests

## Sample requests and data

The API service is set up to run on **localhost:3000**.

**Make a Payment**


    POST /api/payments
                      {
                      	"merchantId":"8e3734c8-e9d2-4f96-8a6f-a66cdd1e31a0", // Guid
                      	"cardholderName": "Ozzy Cat", //string
                      	"cardNumber": "5549877477014359", //Valid credit card no
                      	"expiryMonth": 1, // expiry month, 1 to 12
                      	"expiryYear": 38, // expiry year
                      	"cvv": "500", // CVV, 3 or 4 digits
                      	"amount": 20.00, // amount, with cents
                      	"currencyCode": "UAH" // ISO currency code
                      }

  

**Get Payment by ID**
    
    GET /api/payments/{payment_id}

**Test credit card numbers** to simulate different bank responses are in [this JSON file](https://github.com/aspnetter/PaymentGateway/blob/main/assets/testCards.json)

## What’s Next?

-   **Authentication** - MerchantId is currently passed in the JSON body of an unauthenticated request, which means, knowing the right merchant ID, one can send a lot of money their way

-   **Security** - encryption of sensitive customer data in transit and at rest

-   **Observability** - monitoring and logging, no need for extra justification in a highly sensitive and performance critical system like this, especially with microservices architecture
   
-   **Idempotency** - now every payment is seen as a new payment, even if it’s just the same request with the same merchant Id, customer, amount and parameters. While there should be a retry mechanism for one payment, which means there should be some way of identifying the same payment between the client and the API (e.g. passing the payment id to the POST request for retries)
    
-   **Message Queueing** - a more reliable message queueing mechanism that allows message persistence, acknowledgements and retries
   

-   **More tests** - Unit and BDD Feature tests describing and testing API behaviours which can serve as higher level integration tests in this case
   
-   **Configuration management** - secure parameters - DB connection strings, passwords and keys should be coming from a secure vault and never get to source control / repo
