TODO:
POST /baskets
{ customer: "Andrei", paysVAT:true }
PUT /baskets/{id}/article-line
{ item: "tomato", price: 20 }
GET /baskets/{id}
will receive a json object with all the items added in that specific basket
will have a field with totalNet where you just need to sum all the prices
VAT = 10%
{
id: 1,
items: [
{
item: "tomato", price: 20
},
{
item: "juice", price: 10
},
],
totalNet: 30,
totalGross: 33,
customer: "Andrei",
paysVAT: true
}
PATCH /baskets/{id}
{ close: true, payed: true }
 
ARCHITECTURE:
You can structure the application however you want.
a) DDD-Event Sourcing
OR
b) 3rd Layer (Business , Data Access , Controllers)
You need to create a swagger to use the app. (use swashbuckle for dotnetcore)
 
MUST:
Technologies:
server: .netcore 3.1 or higher
database: postgreSQL or mssql (ORM: efcore or dapper)
git: push the code into your repository using git
unit testing: write some happy flow tests ( 20 % coverage + )
