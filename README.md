# CommentCategorization

Welcome to **CommentCategorization** – an intelligent comment categorization system built on a microservice architecture, designed to filter and analyze user comments efficiently.

---

## 🚀 Overview

CommentCategorization processes and analyzes large volumes of feedback based on various criteria such as content, city, location, date, and interests using structured tags. It helps businesses extract actionable insights from raw comment data.

---

## ✨ Key Features

- **Advanced Categorization**: Filters comments using structured tags for better organization and analysis.
- **Scalability & Performance**: Handles large datasets with high efficiency.
- **Pagination Support**: Enables structured and seamless navigation of comment data.
- **Bulk Upload**: `PostCommentDetailsFromCSV` allows mass upload of comment data through CSV files.

---

## ⚙️ Other Features

### 🧱 Microservice Architecture
- Built using modular services for easy scalability.
- Powered by **Azure Function Apps** for efficient backend processing.

### 📈 Real-Time Monitoring
- Integrated with **Azure Application Insights** for live performance tracking.

### 💾 Efficient Data Storage
- Uses **Azure SQL Server** to manage and query persistent data.

### 🔐 API Gateway
- Secured and managed via **Azure API Management (APIM)** for streamlined API calls.

---

## 🧰 Technology Stack

| Component          | Description                                      |
|-------------------|--------------------------------------------------|
| **Framework**      | .NET Core                                        |
| **Language**       | C#                                               |
| **Microservices**  | Azure Function Apps                              |
| **Database**       | Azure SQL Server                                 |
| **Monitoring**     | Azure Application Insights                       |
| **API Gateway**    | Azure API Management (APIM)                      |

---

## 🛠️ Getting Started

To run CommentSense locally:

### 🔁 Clone the Repository

```bash
git clone https://github.com/yourusername/commentcategorization.git
cd commentsense
