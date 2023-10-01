# Contacts App

## Description

This is a simple contacts app that allows you to add, edit, and delete contacts. It also allows you to search for contacts by name.
It uses Nuxt as a frontend framework and .Net Web API with Microsoft SQL Server as a backend.

## Installation

### Backend

1. Install .NET Core 3.1 SDK
2. Install Entity Framework Core CLI
3. Set connection string in user secrets
4. Run `dotnet ef database update` in `Contacts.Backend` directory or `Update-Database` in VS Package Manager Console
5. Run `dotnet run` in `Contacts.Backend` directory or run in VS Studio

### Frontend

1. Clone the repository
2. Open the `Frontend` folder in a terminal
3. Run `npm install`
4. Run `npm run dev`

## Usage

To use this app, simply add a contact by clicking the "Add Contact" button. You can then edit or delete the contact by clicking the corresponding buttons. You can also search for a contact by name by typing in the search bar.

## License

This project is licensed under the MIT license.
