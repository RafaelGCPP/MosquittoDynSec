# Dynamic Security Management Web Application

This project is a web application designed to help manage the Dynamic Security plugin in the Eclipse Mosquitto MQTT broker. 
The Dynamic Security plugin uses MQTT messages to manage clients, roles, and groups, which can be cumbersome and not user-friendly for system administrators. 
This application aims to provide an intuitive interface to simplify these management tasks.

## Table of Contents
- [Projects in this Solution](#projects-in-this-solution)
 - [DynSec.API](#dynsecapi)
 - [DynSec.Model](#dynsecmodel)
 - [DynSec.MQTT](#dynsecmqtt)
 - [DynSec.Protocol](#dynsecprotocol)
 - [DynSec.Web](#dynsecweb)
 - [DynSec.Tests](#dynsectests)
 - [DynSec.GraphQL](#dynsecgraphql)

## Projects in this Solution

### DynSec.API

This project is the backend of the application. It is a RESTful API built with ASP.NET 9, and provides controllers to interact with the MQTT broker. 
It is also responsible for configuring and making services like logging and the MQTT client available for dependency injection.

### DynSec.Model

This project contains the data models used by the Dynamic Security plugin. The Dynamic Security plugin uses JSON to represent the data, 
and this project provides classes to serialize and deserialize the JSON messages to be sent to the MQTT broker.

### DynSec.MQTT

This project is a wrapper around the MQTTnet library, to provide a less verbose way to setup an MQTT client.

### DynSec.Protocol

This project contains the main engine of the application. It instantiates the MQTT client and uses the publisher-subscriber pattern to send and receive messages from the MQTT broker.
Once the messages are received, they are deserialized into the appropriate data models and sent to the API for processing.

### DynSec.Web

This project is the frontend of the application. It is built with Angular and provides a user-friendly interface for managing the Dynamic Security plugin.

### DynSec.GraphQL

This project provides a GraphQL API for the application. It allows clients to query and manipulate the data models used by the Dynamic Security plugin in a flexible and efficient manner.


