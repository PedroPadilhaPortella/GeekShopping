# Arquitetura de Microsservicos com Dotnet

## Domine microservices, construa um e-commerce com ASP.NET .NET 'Core' 6 Oauth2 OpenID Identity Server RabbitMQ Ocelot e++

This project is part of a comprehensive microservices architecture course that focuses on building a scalable e-commerce platform using ASP.NET Core 6. The stack includes modern technologies like OAuth2, OpenID, IdentityServer, RabbitMQ, and Ocelot API Gateway.

## Project Architeture

![image](https://github.com/user-attachments/assets/bb99e0e1-076e-41c0-b01a-28fc215cfce4)

## Technologies Used

- **ASP.NET Core 6**: Web framework for building modern applications.
- **C#**: Programming language used throughout the project.
- **OAuth2**: Authorization framework to secure APIs.
- **OpenID Connect**: Identity layer for OAuth2 to handle authentication.
- **JWT (JSON Web Tokens)**: Secure token-based authentication.
- **IdentityServer**: Authentication and authorization server for handling security.
- **RabbitMQ**: Message broker for asynchronous communication between microservices.
- **Ocelot**: API Gateway for routing and securing microservices.
- **Swagger (Swashbuckle)**: Tool for API documentation and testing.
- **Postman**: API testing tool used during development.
- **Docker**: Container platform for managing RabbitMQ and other services.

To run the project, clone it with ´git clone https://github.com/PedroPadilhaPortella/Arquitetura_de_Microsservicos_com_Dotnet.git´. 
Then open the **GeekShopping** solution in Visual Studio and restore it.

Using MySql database, create all the schemas required and then, run all projects migrations for each one of this projects `GeekShopping.IdentityServer`, `GeekShopping.ProductAPI`, `GeekShopping.CartAPI`, `GeekShopping.CouponAPI`, `GeekShopping.Email`, `GeekShopping.OrderAPI`.

**Databases Names:**

- `geek_shopping_identity_server`
- `geek_shopping_product_api`
- `geek_shopping_cart_api`
- `geek_shopping_coupon_api`
- `geek_shopping_email`
- `geek_shopping_order_api`

To start the RabbitMQ Message Broker container with Docker:

> docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
