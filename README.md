# BoxCommerceTest

This is the project for the BoxCommerceTest.

## Database

On the folder DB backup you will find a .bak or a .sql script of the database used for this test to be restore or executed.

## Appsettings.json Modification

Each Service (Except the Orchestration) and also the BoxSendNotification have their own appsettings.json ,please make sure to change the configuration where the database (restored/executed on the step above) is running including credentials. 

## Optional IIS Configuration (Only Services)

To have all the Services running without run VS for each project can recommend to configure IIS that could look something like this

![image](https://github.com/kevinsz8/BoxCommerceTest/assets/122753396/a4598c8b-a49e-4391-9c22-b8a3398ff7c0)

Also each Service should have configuration bindings like this (you can check Appsettings.json on orchestration to check what ports you need to configure on each service)

![image](https://github.com/kevinsz8/BoxCommerceTest/assets/122753396/47e5ceab-1d9a-43a0-b64f-e7a600317206)


Taking into consideration that each service should have his own ApplicationPool with the following configuration

![image](https://github.com/kevinsz8/BoxCommerceTest/assets/122753396/d954e011-aa33-486b-913c-b130844ab3b4)

## The only ones we need to run from VS is the Orchestration project and BoxSendNotification app console.

## Installation RabbitMQ

Install rabbitmq docker file using the following command
```c
docker pull rabbitmq:3-management
```

Then run an docker container for the RabbitMQ

```c
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
```

## BoxSendNotification

This App Console is our listener on the RabbitMQ queue. We need to configure the ApplicationDbContext.cs with the correct configuration where the database is running including credentials, this is using a valid smtp server to send notifications to customers (free version of 300 emails/daily), so if you add valid emails to the table Customers then you will be able to receive email notifications. 

## To test the application with the UI

Once everything is running you can open CreateOrders.html on a browser with the following structure "CreateOrders.html?CustomerId=850BAA2B-D485-4A01-81C7-BE71909B03A3" you can change the CustomerId with the ones on the table Customers to start the testing. (You should be able to see an screen like this)

![image](https://github.com/kevinsz8/BoxCommerceTest/assets/122753396/5253c7d8-ed53-43b7-a9f6-45baa34559d7)





