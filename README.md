RaceDay MVC Platform
Project Overview
This is the frontend MVC application for the RaceDay platform. It consumes the RESTful API built in Part 2 and provides a role-aware interface for Organisers and Participants.

 Features
Authentication: Session-based login and registration via the API.

Role-Based Access: Distinct dashboards and routing for Organisers and Participants.

Participant Interface: View upcoming events, enrol in categories, and view personal race results.

Organiser Interface: Create and manage events, view enrolments, and capture race results.

Cloud Storage: Event banner images are uploaded to Azure Blob Storage via the API.

 Prerequisites
Before running this application, ensure you have:

.NET 10 SDK installed.

Docker Desktop installed (if running via container).

The RaceDay API running locally (usually on https://localhost:7123).

 Running Locally (Without Docker)
Open the solution in Visual Studio.

Ensure the API project is running simultaneously (Right-click Solution -> Configure Startup Projects -> Multiple startup projects).

Press F5 or click the green Play button to start the MVC application.

The browser will open at https://localhost:[PORT].

 Running with Docker
To run this application inside a Docker container, follow these steps:

Open your terminal/command prompt and navigate to the root folder of this project (where the Dockerfile is located).

Build the Docker image:

bash
docker build -t raceday-mvc .
Run the Docker container:

bash
docker run -d -p 8080:8080 --name raceday-app raceday-mvc
Open your browser and navigate to http://localhost:8080.



youtube video 
https://youtu.be/RZuLyzIA5ys
