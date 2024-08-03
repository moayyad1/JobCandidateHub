# Job Candidate Hub API

This project is a web application with an API for storing information about job candidates. It allows adding or updating candidate contact information in a SQL database using RESTful API endpoints. The application is designed with the potential for future extension to handle a large volume of candidate data.

## Table of Contents

- [Description](#description)
- [Functional Requirements](#functional-requirements)
- [Technical Requirements](#technical-requirements)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
  - [Running Tests](#running-tests)
- [API Documentation](#api-documentation)
  - [Add or Update Candidate](#add-or-update-candidate)
- [Improvements](#improvements)
- [Assumptions](#assumptions)
- [Time Spent](#time-spent)

## Description

The Job Candidate Hub API is a web application that provides a single API endpoint for adding or updating candidate information in a SQL database. The endpoint uses the candidate's email as a unique identifier to either create a new record or update an existing one.

## Functional Requirements

- The application provides a REST API endpoint for adding or updating candidate information.
- The following candidate information is handled:
  - First name (required)
  - Last name (required)
  - Phone number
  - Email (required, unique identifier)
  - Time interval for preferred call
  - LinkedIn profile URL
  - GitHub profile URL
  - Free text comment (required)

## Technical Requirements

- The application is built using the .NET 6 stack.
- It should be self-deploying and easily run out of the box in Visual Studio.
- Unit tests should be provided, ensuring reasonable coverage.
- Consideration for caching where applicable.
- Use Git for source control with logical commit structures.

## Getting Started

### Prerequisites

- .NET 6 SDK
- SQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/candidate-hub-api.git
   cd candidate-hub-api
