# TaskBoard Application

## Project Overview

TaskBoard is a full-stack web application for managing tasks organized in boards. Users can create personal boards, add tasks with details like title, dates, priority, and status, and perform CRUD operations on tasks and boards.

### Features
- **User Management**: Create and list users
- **Board Management**: Create boards associated with users, delete boards
- **Task Management**: Create, read, update, and delete tasks within boards
- **Data Validation**: Ensures task titles are not empty and start dates don't exceed end dates

### Architecture
- **Backend**: ASP.NET Core Web API with Entity Framework Core and SQLite database
- **Frontend**: Angular application with reactive forms and HTTP client
- **Communication**: RESTful API with JSON payloads

## Technologies Used
- **Backend**: .NET 10, ASP.NET Core, Entity Framework Core, SQLite
- **Frontend**: Angular 21, TypeScript, SCSS
- **Development Tools**: Visual Studio, VS Code, npm, dotnet CLI

## Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js and npm
- Git (optional)

### Backend Setup
1. Navigate to the `TaskBoardApi/TaskBoardApi` folder
2. Run `dotnet restore`
3. Run `dotnet run --urls http://localhost:5000`

### Frontend Setup
1. Navigate to the `taskboard-ui` folder
2. Run `npm install`
3. Run `npm start` (includes proxy to backend)

### Accessing the Application
- **Frontend**: http://localhost:4200
- **Backend API**: http://localhost:5000

## API Testing with Swagger

The backend includes Swagger UI for interactive API documentation and testing.

### Accessing Swagger
1. Ensure the backend is running on http://localhost:5000
2. Open a browser and navigate to: `http://localhost:5000/swagger`
3. The Swagger UI will display all available endpoints

### Testing API Calls

#### Users
- **GET /api/users**: Retrieve all users
- **POST /api/users**: Create a new user
  - Body: `{ "username": "string" }`
- **DELETE /api/users/{id}**: Delete a user by ID

#### Boards
- **GET /api/boards/user/{userId}**: Get boards for a specific user
- **POST /api/boards**: Create a new board
  - Body: `{ "name": "string", "userId": number }`
- **DELETE /api/boards/{id}**: Delete a board by ID

#### Tasks
- **GET /api/tasks/board/{boardId}**: Get tasks for a specific board
- **POST /api/tasks**: Create a new task
  - Body:
    ```json
    {
      "title": "string",
      "startDate": "string (YYYY-MM-DD)",
      "endDate": "string (YYYY-MM-DD)",
      "assignedTo": "string",
      "owner": "string",
      "priority": "string",
      "status": "string",
      "boardId": number
    }
    ```
- **PUT /api/tasks/{id}**: Update an existing task (same body as POST)
- **DELETE /api/tasks/{id}**: Delete a task by ID

### Example API Test Flow
1. Create a user via POST /api/users
2. Create a board for that user via POST /api/boards
3. Add tasks to the board via POST /api/tasks
4. Retrieve tasks via GET /api/tasks/board/{boardId}

## Database
The application uses SQLite with Entity Framework Core. The database file `taskboard.db` is created automatically in the `TaskBoardApi/TaskBoardApi` folder.

## Upcoming Version

### Authentication and Authorization
The next version will include user authentication and role-based permissions:
- Users will need to log in to access their boards and tasks
- Permission-based access control:
  - Users can only view and manage their own boards and tasks
  - Admin roles may have broader access
- Secure API endpoints with JWT tokens
- Enhanced security for data privacy

This will ensure that each user has isolated access to their task management data.