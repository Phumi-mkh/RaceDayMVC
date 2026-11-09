# 🏁 RaceBay - Full Stack Sports Platform

This repository contains the full-stack RaceBay application, consisting of an ASP.NET Core Web API (Part 2) and an ASP.NET Core MVC Web Application (Part 3).

##  Architecture
- **RaceBay.API**: Handles business logic, database operations, JWT authentication, and Azure Blob Storage integration.
- **RaceBay.MVC**: The front-end web application. It consumes the API via HTTP requests and handles session management using Cookies. It does not connect to a database directly.

##  How to Run the Application (Docker)

The MVC application is fully containerized. You can run it using the following Docker commands.

### Prerequisites
- Ensure the Part 2 API is running and accessible.
- Docker Desktop installed.

### Single Command Run
Navigate to the root of the repository and run:

\`\`\`bash
docker build -t racebay-mvc ./RaceBay.MVC && docker run -d -p 8080:8080 -e ApiSettings__BaseUrl="YOUR_API_URL_HERE/" --name racebay-app racebay-mvc
\`\`\`

*(Replace `YOUR_API_URL_HERE` with the actual URL of your running API, e.g., `http://host.docker.internal:5000` if the API is running locally).*

Once running, access the MVC application at: **http://localhost:8080**

##  Demo Video
[Link to your unlisted YouTube video goes here]
