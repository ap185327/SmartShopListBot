# SmartShopListBot

## Overview
SmartShopListBot is a demonstration project showcasing how to integrate a Telegram bot with Large Language Models (LLMs). The primary goal is to illustrate how user input can be normalized into a structured shopping list using advanced AI capabilities. The project is designed to be simple and easy to understand.

## Features
- **Telegram Bot Integration**: Demonstrates how to set up and interact with a Telegram bot.
- **LLM Interaction**: Showcases the use of LLMs to process and normalize user input into structured data.
- **Shopping List Normalization**: Converts free-form user input into organized shopping lists.

## Project Structure
The project is organized into several modules:

- **Smile.Mediator.Contracts**: Defines the mediator pattern contracts for handling commands and queries.
- **Smile.Mediator.Core**: Implements the core mediator logic and extensions.
- **Smile.SmartShopListBot.Application**: Contains application-level logic, including commands, handlers, and strategies.
- **Smile.SmartShopListBot.Domain**: Defines domain models.
- **Smile.SmartShopListBot.Infrastructure**: Provides infrastructure-level services, helpers, and validators.
- **Smile.SmartShopListBot.WebApp**: Hosts the web application, including configuration files and entry points.

## Getting Started

### Prerequisites
- .NET SDK (version 9.0 or higher)
- Docker (optional, for containerized deployment)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/ap185327/SmartShopListBot.git
   ```
2. Navigate to the project directory:
   ```bash
   cd SmartShopListBot
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application
1. Build the solution:
   ```bash
   dotnet build
   ```
2. Run the web application:
   ```bash
   dotnet run --project src/Smile.SmartShopListBot.WebApp
   ```

### Docker Deployment
1. Build the Docker image:
   ```bash
   docker build -t smartshoplistbot .
   ```
2. Run the container:
   ```bash
   docker run -p 5000:5000 smartshoplistbot
   ```

## Usage
Interact with the bot via Telegram by sending commands and queries. The bot will process your input and normalize it into a structured shopping list.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.