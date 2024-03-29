# Project Name
Developing Middleware Components

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Docker](#docker)
- [Building the Docker Image](#building-the-docker-image)
- [Running the Docker Container](#running-the-docker-container)


## Introduction

The project is about implementations of middleware in a .net core application. The feature make use of request middleware to   
log incoming request. Also Exception middleware which handle all exceptions which occurs in the application and return  user-friendly error message. Rate limit   middleware to protect the endpoint to  
from unnecessary calls. Finally  response compression middleware which is  use to  change response based on use request.

## Features

- Request Logging Middleware
-  Response Compression Middleware
- Exception Handling Middleware
- RateLimit



## Prerequisites


- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)

## Getting Started

### Installation

Instructions on how to install and configure the project.

1. Clone the repository.
2. Navigate to the project directory.

### Running the Application

Instructions on how to run the API locally.

1. Restore dependencies: `dotnet restore`.
2. Build the project: `dotnet build`.
3. Run the project: `dotnet run`.



## Docker

### Building the Docker Image

Instructions on how to build the Docker image for the application.

1. Build the Docker image: `docker build -t <image-name> .`.

### Running the Docker Container

Instructions on how to run the Docker container.

1. Run the Docker container: `docker run -d -p <host-port>:<container-port> <image-name>`.