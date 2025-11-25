🏦 Real-Time Banking Notifications

🔐 Login Page

<img width="474" height="397" alt="Capture d&#39;écran 2025-11-25 122718" src="https://github.com/user-attachments/assets/2b647d66-921e-4a47-a0a9-3632a7035189" />

Login → Authentication API → Token → WebSocket Connection



🚀 Project Description

This project demonstrates how to create a real-time banking system using:

An ASP.NET Core API for authentication and operation processing

A WebSocket service for sending instant notifications

A modern HTML/JS interface to display alerts

🎯 Goal:
Notify a user instantly when an account becomes negative.

🏗️ System Architecture
Client (HTML/JS)
       │
       ▼
POST /api/Authentication  → Returns token
       │
       ▼
ws://localhost:5298/ws?token=xxxx
       │
       ▼
Receive banking notifications in real time

📂 Project Structure

<img width="461" height="522" alt="image" src="https://github.com/user-attachments/assets/a39df36c-9fb8-407d-a5b6-1a39128aacd6" />
<img width="367" height="172" alt="image" src="https://github.com/user-attachments/assets/19f42d9b-a1d1-428a-9557-e18fd04f11ac" />

⚙️ Technologies Used
Technology	Role
ASP.NET Core 8	REST API + WebSocket
C#	Business logic & services
HTML5 / CSS3	User interface
JavaScript WebSocket API	Real-time communication
JSON	Notification format
🔐 Authentication API
✔️ Request
POST /api/Authentication
Content-Type: application/json

{
  "login": "1",
  "password": "1"
}

✔️ Response
{
  "value": "3e3c9dad-b4b1-418b-8933-7161db3d13ce",
  "fullName": "hamza"
}

🔌 WebSocket Connection
ws://localhost:5298/ws?token=3e3c9dad-b4b1-418b-8933-7161db3d13ce

💳 Operation Example
{
  "NumeroCompte": "C001",
  "Type": 0,
  "Montant": 500,
  "DateHeure": "2025-11-25T12:00:00"
}


Business logic:

compte.Solde += op.Type == TypeOperation.Credit ? op.Montant : -op.Montant;

⚠️ Notification Sent
{
  "type": "NEGATIVE_BALANCE_ALERT",
  "message": "⚠️ Warning! Your account C001 is in negative balance.",
  "timestamp": "2025-11-25T10:00:00Z",
  "userId": "1"
}

💻 Web Interface 

🟢 WebSocket connected
🔔 Notification displayed
👤 User name
📉 Negative balance detected
<img width="466" height="462" alt="Capture d&#39;écran 2025-11-25 143215" src="https://github.com/user-attachments/assets/fa86f10c-6954-4195-8546-1e5246fc0ef7" />
<img width="462" height="413" alt="Capture d&#39;écran 2025-11-25 143235" src="https://github.com/user-attachments/assets/a6494daa-572d-42af-a5bc-6d8f09bbd87a" />
<img width="474" height="397" alt="Capture d&#39;écran 2025-11-25 122718" src="https://github.com/user-attachments/assets/0b885c2e-dc1f-4f82-9eef-a4a6909c71a3" />




📦 Installation & Execution
1️⃣ Backend
dotnet run

2️⃣ Frontend

Open index.html in a browser.

🧪 Testing with Swagger
POST http://localhost:5298/api/Operation

🧑‍💻 Author

Alya Ichaoui
Project carried out as part of the Master IoT program.
WebSocket + ASP.NET Core + HTML/JS

<p align="center"> <img src="https://img.shields.io/badge/Backend-C%23_.NET_8-blue?style=for-the-badge" /> <img src="https://img.shields.io/badge/Frontend-HTML%2FJS-orange?style=for-the-badge" /> <img src="https://img.shields.io/badge/WebSocket-Real_Time-00d26a?style=for-the-badge" /> <img src="https://img.shields.io/badge/Status-Working_Successfully-brightgreen?style=for-the-badge" /> </p> <p align="center"> <img src="https://img.shields.io/github/license/alyaproject/banking-notification?style=flat-square" /> <img src="https://img.shields.io/badge/Made%20with-❤️-red?style=flat-square" /> </p>
