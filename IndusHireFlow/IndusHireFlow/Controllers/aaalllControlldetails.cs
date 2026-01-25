# Complete API Endpoints Documentation

## Table of Contents
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

1. [Authentication API](#authentication-api)
2. [Candidates API](#candidates-api)
3. [Jobs API](#jobs-api)
4. [Applications API](#applications-api)
5. [Interviews API](#interviews-api)
6. [Interview Feedback API](#interview-feedback-api)
7. [Offers API](#offers-api)
8. [Messages API](#messages-api)
9. [Users API](#users-api)
10. [Skills API](#skills-api)
11. [Departments API](#departments-api)
12. [Dashboard API](#dashboard-api)

---

## Authentication API

### 1. User Login
**Endpoint:** `POST / api / auth / login`  
**HTTP Method: **POST
* *Authentication:**None(Public)
* *Description:**Authenticate user with email and password

* *Request Body: **
```json
{
    "email": "user@example.com",
  "password": "SecurePass@123"
}
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "userId": "550e8400-e29b-41d4-a716-446655440001",
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roles": ["Admin", "HR Manager"],
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiresIn": "2026-01-26T12:00:00Z"
  }
}
```

**Error Response(400 Bad Request):**
```json
{
  "success": false,
  "message": "Invalid email or password",
  "errors": {
    "email": "Email is required",
    "password": "Password is required"
  }
}
```

---

### 2. User Registration
**Endpoint:** `POST / api / auth / register`  
**HTTP Method: **POST
* *Authentication:**None(Public)
* *Description:**Register new user account

**Request Body: **
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "SecurePass@123",
  "confirmPassword": "SecurePass@123",
  "phoneNumber": "+91 9876543210"
}
```

**Response(201 Created):**
```json
{
  "success": true,
  "message": "Registration successful",
  "data": {
    "userId": "550e8400-e29b-41d4-a716-446655440001",
    "email": "john@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roles": ["Candidate"],
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiresIn": "2026-01-26T12:00:00Z"
  }
}
```

---

### 3. Refresh Token
**Endpoint:** `POST / api / auth / refresh - token`  
**HTTP Method: **POST
* *Authentication:**None(Public)
* *Description:**Refresh expired access token

**Request Body:**
```json
{
  "refreshToken": "eyJhbGciOiJIUzI1NiIs..."
}
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Token refreshed successfully",
  "data": {
    "userId": "550e8400-e29b-41d4-a716-446655440001",
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roles": ["Admin"],
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiresIn": "2026-01-26T12:00:00Z"
  }
}
```

---

### 4. Logout
**Endpoint:** `POST / api / auth / logout`  
**HTTP Method: **POST
* *Authentication:**Required(Bearer Token)
* *Description:**Logout user

** Request Body:**Empty

* *Response(200 OK):**
```json
{
  "success": true,
  "message": "Logout successful",
  "data": null
}
```

---

## Candidates API

### 1. Get All Candidates (Paginated & Filtered)
**Endpoint:** `GET / api / candidates`  
**HTTP Method: **GET
* *Authentication:**Required
* *Description:**Get paginated list of candidates with optional filtering

**Query Parameters:**
- `pageNumber` (int, default: 1) -Page number
- `pageSize` (int, default: 10, max: 100) -Items per page
- `search` (string, optional) - Search by name or email
- `skill` (string, optional) - Filter by skill
- `location` (string, optional) - Filter by location

**Example Request:**
```
GET / api / candidates ? pageNumber = 1 & pageSize = 10 & search = priya & skill = Machine + Operation & location = Mumbai
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Candidates retrieved successfully",
  "data": {
    "items": [
      {
        "id": "550e8400-e29b-41d4-a716-446655440001",
        "firstName": "Priya",
        "lastName": "Sharma",
        "email": "priya@email.com",
        "phoneNumber": "+91 98765 43210",
        "location": "Mumbai",
        "currentRole": "Machine Operator",
        "experience": 3,
        "resumeUrl": "https://example.com/resumes/priya.pdf",
        "profileSummary": "Experienced machine operator...",
        "linkedInProfile": "https://linkedin.com/in/priya-sharma",
        "skills": ["Machine Operation", "Quality Control"],
        "rating": 4.5,
        "notes": "Excellent communication skills",
        "source": "Direct Apply",
        "createdAt": "2024-01-15T08:00:00Z",
        "updatedAt": "2024-01-24T10:30:00Z"
      }
    ],
    "totalCount": 150,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 15
  }
}
```

---

### 2. Get Candidate by ID
**Endpoint:** `GET / api / candidates /{ id}`  
**HTTP Method: **GET
* *Authentication:**Required
* *Description:**Get single candidate details

**URL Parameters:**
- `id` (Guid, required) - Candidate ID

**Example Request:**
```
GET / api / candidates / 550e8400 - e29b - 41d4 - a716 - 446655440001
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Candidate retrieved successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "firstName": "Priya",
    "lastName": "Sharma",
    "email": "priya@email.com",
    "phoneNumber": "+91 98765 43210",
    "location": "Mumbai",
    "currentRole": "Machine Operator",
    "experience": 3,
    "resumeUrl": "https://example.com/resumes/priya.pdf",
    "profileSummary": "Experienced machine operator...",
    "linkedInProfile": "https://linkedin.com/in/priya-sharma",
    "skills": ["Machine Operation", "Quality Control"],
    "rating": 4.5,
    "notes": "Excellent communication skills",
    "source": "Direct Apply",
    "createdAt": "2024-01-15T08:00:00Z",
    "updatedAt": "2024-01-24T10:30:00Z"
  }
}
```

**Error Response(404 Not Found):**
```json
{
  "success": false,
  "message": "Candidate with ID 550e8400-e29b-41d4-a716-446655440001 not found",
  "errors": {
    "id": "Candidate not found"
  }
}
```

---

### 3. Create Candidate
**Endpoint:** `POST / api / candidates`  
**HTTP Method: **POST
* *Authentication:**Required
* *Description:**Create new candidate

** Request Body: **
```json
{
  "firstName": "Rajesh",
  "lastName": "Kumar",
  "email": "rajesh@example.com",
  "phoneNumber": "+91 98765 43218",
  "location": "Bangalore",
  "currentRole": "Senior Developer",
  "experience": 5,
  "resumeUrl": "https://example.com/resumes/rajesh.pdf",
  "profileSummary": "Experienced software developer with 5 years...",
  "linkedInProfile": "https://linkedin.com/in/rajesh-kumar",
  "skills": ["Java", "Spring Boot", "Microservices"],
  "source": "LinkedIn"
}
```

**Response(201 Created):**
```json
{
  "success": true,
  "message": "Candidate created successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440009",
    "firstName": "Rajesh",
    "lastName": "Kumar",
    "email": "rajesh@example.com",
    "phoneNumber": "+91 98765 43218",
    "location": "Bangalore",
    "currentRole": "Senior Developer",
    "experience": 5,
    "resumeUrl": "https://example.com/resumes/rajesh.pdf",
    "profileSummary": "Experienced software developer with 5 years...",
    "linkedInProfile": "https://linkedin.com/in/rajesh-kumar",
    "skills": ["Java", "Spring Boot", "Microservices"],
    "rating": 0,
    "notes": "",
    "source": "LinkedIn",
    "createdAt": "2024-01-26T10:00:00Z",
    "updatedAt": "2024-01-26T10:00:00Z"
  }
}
```

---

### 4. Update Candidate
**Endpoint:** `PUT / api / candidates /{ id}`  
**HTTP Method: **PUT
* *Authentication:**Required
* *Description:**Update existing candidate

**URL Parameters:**
- `id` (Guid, required) - Candidate ID

**Request Body (All fields optional):**
```json
{
  "firstName": "Priya",
  "lastName": "Sharma",
  "email": "priya.sharma@email.com",
  "phoneNumber": "+91 98765 43210",
  "location": "Pune",
  "currentRole": "Senior Machine Operator",
  "experience": 4,
  "skills": ["Machine Operation", "Quality Control", "Supervision"],
  "rating": 4.7,
  "notes": "Promoted to senior role"
}
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Candidate updated successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "firstName": "Priya",
    "lastName": "Sharma",
    "email": "priya.sharma@email.com",
    "phoneNumber": "+91 98765 43210",
    "location": "Pune",
    "currentRole": "Senior Machine Operator",
    "experience": 4,
    "resumeUrl": "https://example.com/resumes/priya.pdf",
    "profileSummary": "Experienced machine operator...",
    "linkedInProfile": "https://linkedin.com/in/priya-sharma",
    "skills": ["Machine Operation", "Quality Control", "Supervision"],
    "rating": 4.7,
    "notes": "Promoted to senior role",
    "source": "Direct Apply",
    "createdAt": "2024-01-15T08:00:00Z",
    "updatedAt": "2024-01-26T11:00:00Z"
  }
}
```

---

### 5. Delete Candidate
**Endpoint:** `DELETE / api / candidates /{ id}`  
**HTTP Method: **DELETE
* *Authentication:**Required
* *Description:**Delete candidate

** URL Parameters:**
- `id` (Guid, required) - Candidate ID

**Example Request:**
```
DELETE / api / candidates / 550e8400 - e29b - 41d4 - a716 - 446655440001
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Candidate deleted successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440001"
  }
}
```

---

### 6. Bulk Actions on Candidates
**Endpoint:** `POST / api / candidates / bulk - action`  
**HTTP Method: **POST
* *Authentication:**Required
* *Description:**Perform bulk operations(delete, update rating, export)

**Request Body: **
```json
{
  "action": "delete",
  "data": {
    "id1": "550e8400-e29b-41d4-a716-446655440001",
    "id2": "550e8400-e29b-41d4-a716-446655440002",
    "id3": "550e8400-e29b-41d4-a716-446655440003"
  }
}
```

**Alternative - Update Rating: **
```json
{
  "action": "update-rating",
  "data": {
    "item1": {
        "id": "550e8400-e29b-41d4-a716-446655440001",
      "rating": 4.8
    },
    "item2": {
        "id": "550e8400-e29b-41d4-a716-446655440002",
      "rating": 4.5
    }
}
}
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Bulk action completed. 3 successful, 0 failed",
  "data": {
    "action": "delete",
    "totalProcessed": 3,
    "successful": 3,
    "failed": 0,
    "failedItems": []
  }
}
```

---

### 7. Get Candidate Statistics
**Endpoint:** `GET / api / candidates / statistics / summary`  
**HTTP Method: **GET
* *Authentication:**Required
* *Description:**Get candidate statistics

**Example Request:**
```
GET / api / candidates / statistics / summary
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Statistics retrieved successfully",
  "data": {
    "totalCandidates": 150,
    "averageExperience": 4.5,
    "averageRating": 4.6,
    "locationCount": 12,
    "uniqueSkillsCount": 35,
    "locations": ["Mumbai", "Bangalore", "Pune", "Delhi", "Hyderabad"],
    "skills": ["Machine Operation", "Quality Control", "Supervision", "Electrical"]
  }
}
```

---

## Jobs API

### 1. Get All Jobs (Paginated & Filtered)
**Endpoint:** `GET / api / jobs`  
**HTTP Method: **GET
* *Authentication:**Required
* *Description:**Get paginated list of jobs with optional filtering

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)
- `search` (string, optional) - Search by title
- `department` (string, optional) - Filter by department
- `location` (string, optional) - Filter by location

**Example Request:**
```
GET / api / jobs ? pageNumber = 1 & pageSize = 10 & search = developer & department = Engineering & location = Bangalore
```

**Response(200 OK):**
```json
{
  "success": true,
  "message": "Jobs retrieved successfully",
  "data": {
    "items": [
      {
        "id": "660e8400-e29b-41d4-a716-446655440001",
        "title": "Senior Software Engineer",
        "description": "We are looking for...",
        "department": "Engineering",
        "location": "Bangalore",
        "employmentType": "Full-time",
        "salaryMin": 100000,
        "salaryMax": 150000,
        "salaryCurrency": "INR",
        "requiredSkills": ["Java", "Spring Boot", "Microservices", "Docker"],
        "experienceYearsRequired": 5,
        "status": "Active",
        "postedDate": "2024-01-15T08:00:00Z",
        "closingDate": "2024-02-15T23:59:59Z",
        "totalPositions": 3,
        "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
        "createdByUserName": "HR Manager",
        "createdAt": "2024-01-15T08:00:00Z",
        "updatedAt": "2024-01-26T10:00:00Z"
      }
    ],
    "totalCount": 25,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 3
  }
}
```

---

### 2. Get Job by ID
**Endpoint:** `GET /api/jobs/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single job details

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job retrieved successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "title": "Senior Software Engineer",
    "description": "We are looking for...",
    "department": "Engineering",
    "location": "Bangalore",
    "employmentType": "Full-time",
    "salaryMin": 100000,
    "salaryMax": 150000,
    "salaryCurrency": "INR",
    "requiredSkills": ["Java", "Spring Boot", "Microservices"],
    "experienceYearsRequired": 5,
    "status": "Active",
    "postedDate": "2024-01-15T08:00:00Z",
    "closingDate": "2024-02-15T23:59:59Z",
    "totalPositions": 3,
    "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
    "createdByUserName": "HR Manager",
    "createdAt": "2024-01-15T08:00:00Z",
    "updatedAt": "2024-01-26T10:00:00Z"
  }
}
```

---

### 3. Create Job
**Endpoint:** `POST /api/jobs`  
**HTTP Method:** POST  
**Authentication:** Required (HR Manager, Admin)  
**Description:** Create new job posting

**Request Body:**
```json
{
  "title": "Full Stack Developer",
  "description": "Looking for experienced full stack developer...",
  "department": "Engineering",
  "location": "Mumbai",
  "employmentType": "Full-time",
  "salaryMin": 80000,
  "salaryMax": 120000,
  "salaryCurrency": "INR",
  "requiredSkills": ["JavaScript", "React", "Node.js", "MongoDB"],
  "experienceYearsRequired": 3,
  "closingDate": "2024-02-20T23:59:59Z",
  "totalPositions": 2
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Job created successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440010",
    "title": "Full Stack Developer",
    "description": "Looking for experienced full stack developer...",
    "department": "Engineering",
    "location": "Mumbai",
    "employmentType": "Full-time",
    "salaryMin": 80000,
    "salaryMax": 120000,
    "salaryCurrency": "INR",
    "requiredSkills": ["JavaScript", "React", "Node.js", "MongoDB"],
    "experienceYearsRequired": 3,
    "status": "Active",
    "postedDate": "2024-01-26T10:00:00Z",
    "closingDate": "2024-02-20T23:59:59Z",
    "totalPositions": 2,
    "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
    "createdByUserName": "HR Manager",
    "createdAt": "2024-01-26T10:00:00Z",
    "updatedAt": "2024-01-26T10:00:00Z"
  }
}
```

---

### 4. Update Job
**Endpoint:** `PUT /api/jobs/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required (Job Creator, HR Manager, Admin)  
**Description:** Update job posting

**Request Body (All fields optional):**
```json
{
  "title": "Senior Full Stack Developer",
  "description": "Updated description...",
  "salaryMin": 90000,
  "salaryMax": 130000,
  "status": "Active",
  "totalPositions": 3
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job updated successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440010",
    "title": "Senior Full Stack Developer",
    "description": "Updated description...",
    "department": "Engineering",
    "location": "Mumbai",
    "employmentType": "Full-time",
    "salaryMin": 90000,
    "salaryMax": 130000,
    "salaryCurrency": "INR",
    "requiredSkills": ["JavaScript", "React", "Node.js", "MongoDB"],
    "experienceYearsRequired": 3,
    "status": "Active",
    "postedDate": "2024-01-26T10:00:00Z",
    "closingDate": "2024-02-20T23:59:59Z",
    "totalPositions": 3,
    "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
    "createdByUserName": "HR Manager",
    "createdAt": "2024-01-26T10:00:00Z",
    "updatedAt": "2024-01-26T11:00:00Z"
  }
}
```

---

### 5. Delete Job
**Endpoint:** `DELETE /api/jobs/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required (Job Creator, Admin)  
**Description:** Delete job posting

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job deleted successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

### 6. Get Job Details (with Applications)
**Endpoint:** `GET /api/jobs/{id}/details`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get job with application summary

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job details retrieved successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "title": "Senior Software Engineer",
    "description": "We are looking for...",
    "department": "Engineering",
    "location": "Bangalore",
    "employmentType": "Full-time",
    "salaryMin": 100000,
    "salaryMax": 150000,
    "salaryCurrency": "INR",
    "requiredSkills": ["Java", "Spring Boot", "Microservices"],
    "experienceYearsRequired": 5,
    "status": "Active",
    "postedDate": "2024-01-15T08:00:00Z",
    "closingDate": "2024-02-15T23:59:59Z",
    "totalPositions": 3,
    "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
    "createdByUserName": "HR Manager",
    "createdAt": "2024-01-15T08:00:00Z",
    "updatedAt": "2024-01-26T10:00:00Z",
    "applicationCount": 15,
    "recentApplications": [
      {
        "id": "770e8400-e29b-41d4-a716-446655440001",
        "jobId": "660e8400-e29b-41d4-a716-446655440001",
        "jobTitle": "Senior Software Engineer",
        "status": "Shortlisted",
        "appliedDate": "2024-01-24T10:00:00Z"
      }
    ]
  }
}
```

---

### 7. Close Job
**Endpoint:** `PUT /api/jobs/{id}/close`  
**HTTP Method:** PUT  
**Authentication:** Required (HR Manager, Admin)  
**Description:** Close job posting

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job closed successfully",
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "status": "Closed"
  }
}
```

---

### 8. Get Active Jobs
**Endpoint:** `GET /api/jobs/active`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get all active job postings

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Active jobs retrieved successfully",
  "data": [
    {
      "id": "660e8400-e29b-41d4-a716-446655440001",
      "title": "Senior Software Engineer",
      "department": "Engineering",
      "location": "Bangalore",
      "status": "Active",
      "postedDate": "2024-01-15T08:00:00Z",
      "closingDate": "2024-02-15T23:59:59Z"
    }
  ]
}
```

---

## Applications API

### 1. Get All Applications (Paginated)
**Endpoint:** `GET /api/applications`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get paginated list of applications

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)
- `status` (string, optional) - Filter by status
- `jobId` (string, optional) - Filter by job

**Example Request:**
```
GET /api/applications?pageNumber=1&pageSize=10&status=Shortlisted
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Applications retrieved successfully",
  "data": {
    "items": [
      {
        "id": "770e8400-e29b-41d4-a716-446655440001",
        "candidateId": "550e8400-e29b-41d4-a716-446655440001",
        "candidateName": "Priya Sharma",
        "candidateEmail": "priya@email.com",
        "jobId": "660e8400-e29b-41d4-a716-446655440001",
        "jobTitle": "Senior Software Engineer",
        "status": "Shortlisted",
        "source": "Direct Apply",
        "appliedDate": "2024-01-24T10:00:00Z",
        "matchScore": 85.5,
        "notes": "Strong technical skills",
        "createdAt": "2024-01-24T10:00:00Z",
        "updatedAt": "2024-01-26T10:00:00Z"
      }
    ],
    "totalCount": 50,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 5
  }
}
```

---

### 2. Get Application by ID
**Endpoint:** `GET /api/applications/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single application details

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Application retrieved successfully",
  "data": {
    "id": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "candidateEmail": "priya@email.com",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "status": "Shortlisted",
    "source": "Direct Apply",
    "appliedDate": "2024-01-24T10:00:00Z",
    "matchScore": 85.5,
    "notes": "Strong technical skills",
    "createdAt": "2024-01-24T10:00:00Z",
    "updatedAt": "2024-01-26T10:00:00Z"
  }
}
```

---

### 3. Create Application
**Endpoint:** `POST /api/applications`  
**HTTP Method:** POST  
**Authentication:** Required  
**Description:** Create new application

**Request Body:**
```json
{
  "candidateId": "550e8400-e29b-41d4-a716-446655440001",
  "jobId": "660e8400-e29b-41d4-a716-446655440001",
  "source": "Direct Apply",
  "notes": "Interested in this position"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Application created successfully",
  "data": {
    "id": "770e8400-e29b-41d4-a716-446655440015",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "candidateEmail": "priya@email.com",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "status": "Applied",
    "source": "Direct Apply",
    "appliedDate": "2024-01-26T12:00:00Z",
    "matchScore": 0,
    "notes": "Interested in this position",
    "createdAt": "2024-01-26T12:00:00Z",
    "updatedAt": "2024-01-26T12:00:00Z"
  }
}
```

---

### 4. Update Application
**Endpoint:** `PUT /api/applications/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Update application status or score

**Request Body (All fields optional):**
```json
{
  "status": "Shortlisted",
  "matchScore": 85.5,
  "notes": "Moved to shortlist"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Application updated successfully",
  "data": {
    "id": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "candidateEmail": "priya@email.com",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "status": "Shortlisted",
    "source": "Direct Apply",
    "appliedDate": "2024-01-24T10:00:00Z",
    "matchScore": 85.5,
    "notes": "Moved to shortlist",
    "createdAt": "2024-01-24T10:00:00Z",
    "updatedAt": "2024-01-26T13:00:00Z"
  }
}
```

---

### 5. Delete Application
**Endpoint:** `DELETE /api/applications/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required  
**Description:** Delete application

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Application deleted successfully",
  "data": {
    "id": "770e8400-e29b-41d4-a716-446655440001"
  }
}
```

---

### 6. Get Applications by Job
**Endpoint:** `GET /api/applications/job/{jobId}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get all applications for a specific job

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Job applications retrieved successfully",
  "data": [
    {
      "id": "770e8400-e29b-41d4-a716-446655440001",
      "candidateId": "550e8400-e29b-41d4-a716-446655440001",
      "candidateName": "Priya Sharma",
      "candidateEmail": "priya@email.com",
      "jobId": "660e8400-e29b-41d4-a716-446655440001",
      "jobTitle": "Senior Software Engineer",
      "status": "Shortlisted",
      "source": "Direct Apply",
      "appliedDate": "2024-01-24T10:00:00Z",
      "matchScore": 85.5,
      "notes": "Strong technical skills",
      "createdAt": "2024-01-24T10:00:00Z",
      "updatedAt": "2024-01-26T10:00:00Z"
    }
  ]
}
```

---

### 7. Get Applications by Candidate
**Endpoint:** `GET /api/applications/candidate/{candidateId}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get all applications for a specific candidate

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Candidate applications retrieved successfully",
  "data": [
    {
      "id": "770e8400-e29b-41d4-a716-446655440001",
      "candidateId": "550e8400-e29b-41d4-a716-446655440001",
      "candidateName": "Priya Sharma",
      "candidateEmail": "priya@email.com",
      "jobId": "660e8400-e29b-41d4-a716-446655440001",
      "jobTitle": "Senior Software Engineer",
      "status": "Shortlisted",
      "source": "Direct Apply",
      "appliedDate": "2024-01-24T10:00:00Z",
      "matchScore": 85.5,
      "notes": "Strong technical skills",
      "createdAt": "2024-01-24T10:00:00Z",
      "updatedAt": "2024-01-26T10:00:00Z"
    }
  ]
}
```

---

### 8. Get Applications by Status (Pipeline)
**Endpoint:** `GET /api/applications/pipeline/{status}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get applications in specific pipeline stage

**URL Parameters:**
- `status` (string) - Applied, Shortlisted, Interview, Selected, Rejected, Offered, Accepted, Joined

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Pipeline applications retrieved successfully",
  "data": [
    {
      "id": "770e8400-e29b-41d4-a716-446655440001",
      "candidateName": "Priya Sharma",
      "status": "Shortlisted",
      "matchScore": 85.5,
      "appliedDate": "2024-01-24T10:00:00Z",
      "currentStage": "Shortlisted"
    }
  ]
}
```

---

### 9. Calculate Match Score
**Endpoint:** `POST /api/applications/{id}/calculate-match`  
**HTTP Method:** POST  
**Authentication:** Required  
**Description:** Calculate AI-powered match score

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Match score calculated successfully",
  "data": {
    "id": "770e8400-e29b-41d4-a716-446655440001",
    "matchScore": 85.5,
    "message": "Strong match based on skills and experience"
  }
}
```

---

## Interviews API

### 1. Get All Interviews (Paginated)
**Endpoint:** `GET /api/interviews`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get paginated list of interviews

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)
- `status` (string, optional) - Filter by status

**Example Request:**
```
GET /api/interviews?pageNumber=1&pageSize=10&status=Scheduled
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interviews retrieved successfully",
  "data": {
    "items": [
      {
        "id": "880e8400-e29b-41d4-a716-446655440001",
        "applicationId": "770e8400-e29b-41d4-a716-446655440001",
        "candidateId": "550e8400-e29b-41d4-a716-446655440001",
        "candidateName": "Priya Sharma",
        "jobId": "660e8400-e29b-41d4-a716-446655440001",
        "jobTitle": "Senior Software Engineer",
        "scheduledDate": "2024-02-01T10:00:00Z",
        "interviewType": "Video",
        "status": "Scheduled",
        "interviewerName": "John Manager",
        "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
        "location": "Google Meet",
        "meetingLink": "https://meet.google.com/abc-defg-hij",
        "notes": "Technical interview round 1",
        "createdAt": "2024-01-24T10:00:00Z",
        "updatedAt": "2024-01-24T10:00:00Z"
      }
    ],
    "totalCount": 20,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 2
  }
}
```

---

### 2. Get Interview by ID
**Endpoint:** `GET /api/interviews/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single interview details

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview retrieved successfully",
  "data": {
    "id": "880e8400-e29b-41d4-a716-446655440001",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "scheduledDate": "2024-02-01T10:00:00Z",
    "interviewType": "Video",
    "status": "Scheduled",
    "interviewerName": "John Manager",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "location": "Google Meet",
    "meetingLink": "https://meet.google.com/abc-defg-hij",
    "notes": "Technical interview round 1",
    "createdAt": "2024-01-24T10:00:00Z",
    "updatedAt": "2024-01-24T10:00:00Z"
  }
}
```

---

### 3. Schedule Interview
**Endpoint:** `POST /api/interviews`  
**HTTP Method:** POST  
**Authentication:** Required  
**Description:** Schedule new interview

**Request Body:**
```json
{
  "applicationId": "770e8400-e29b-41d4-a716-446655440001",
  "scheduledDate": "2024-02-01T10:00:00Z",
  "interviewType": "Video",
  "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
  "location": "Google Meet",
  "meetingLink": "https://meet.google.com/abc-defg-hij",
  "notes": "Technical interview round 1"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Interview scheduled successfully",
  "data": {
    "id": "880e8400-e29b-41d4-a716-446655440010",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "scheduledDate": "2024-02-01T10:00:00Z",
    "interviewType": "Video",
    "status": "Scheduled",
    "interviewerName": "John Manager",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "location": "Google Meet",
    "meetingLink": "https://meet.google.com/abc-defg-hij",
    "notes": "Technical interview round 1",
    "createdAt": "2024-01-26T14:00:00Z",
    "updatedAt": "2024-01-26T14:00:00Z"
  }
}
```

---

### 4. Update Interview
**Endpoint:** `PUT /api/interviews/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Update interview details

**Request Body (All fields optional):**
```json
{
  "scheduledDate": "2024-02-02T10:00:00Z",
  "status": "Completed",
  "location": "Conference Room A",
  "meetingLink": "https://meet.google.com/xyz-abcd-efg",
  "notes": "Rescheduled to Room A"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview updated successfully",
  "data": {
    "id": "880e8400-e29b-41d4-a716-446655440010",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "scheduledDate": "2024-02-02T10:00:00Z",
    "interviewType": "Video",
    "status": "Completed",
    "interviewerName": "John Manager",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "location": "Conference Room A",
    "meetingLink": "https://meet.google.com/xyz-abcd-efg",
    "notes": "Rescheduled to Room A",
    "createdAt": "2024-01-26T14:00:00Z",
    "updatedAt": "2024-01-26T15:00:00Z"
  }
}
```

---

### 5. Delete Interview
**Endpoint:** `DELETE /api/interviews/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required  
**Description:** Delete interview

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview deleted successfully",
  "data": {
    "id": "880e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

### 6. Cancel Interview
**Endpoint:** `PUT /api/interviews/{id}/cancel`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Cancel interview

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview cancelled successfully",
  "data": {
    "id": "880e8400-e29b-41d4-a716-446655440010",
    "status": "Cancelled"
  }
}
```

---

### 7. Get Upcoming Interviews
**Endpoint:** `GET /api/interviews/upcoming/{days}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get upcoming interviews for next N days

**URL Parameters:**
- `days` (int) - Number of days to look ahead

**Example Request:**
```
GET /api/interviews/upcoming/7
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Upcoming interviews retrieved successfully",
  "data": [
    {
      "id": "880e8400-e29b-41d4-a716-446655440001",
      "applicationId": "770e8400-e29b-41d4-a716-446655440001",
      "candidateName": "Priya Sharma",
      "jobTitle": "Senior Software Engineer",
      "scheduledDate": "2024-01-30T10:00:00Z",
      "interviewType": "Video",
      "status": "Scheduled",
      "meetingLink": "https://meet.google.com/abc-defg-hij"
    }
  ]
}
```

---

### 8. Get Interviews by Interviewer
**Endpoint:** `GET /api/interviews/interviewer/{interviewerId}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get interviews conducted by specific interviewer

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interviewer interviews retrieved successfully",
  "data": [
    {
      "id": "880e8400-e29b-41d4-a716-446655440001",
      "candidateName": "Priya Sharma",
      "jobTitle": "Senior Software Engineer",
      "scheduledDate": "2024-02-01T10:00:00Z",
      "interviewType": "Video",
      "status": "Scheduled"
    }
  ]
}
```

---

## Interview Feedback API

### 1. Get Feedback by ID
**Endpoint:** `GET /api/interviews/{interviewId}/feedback`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get feedback for specific interview

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview feedback retrieved successfully",
  "data": {
    "id": "990e8400-e29b-41d4-a716-446655440001",
    "interviewId": "880e8400-e29b-41d4-a716-446655440001",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "interviewerName": "John Manager",
    "overallRating": "Excellent",
    "technicalScore": 9,
    "communicationScore": 8,
    "culturalFitScore": 9,
    "comments": "Strong candidate with excellent technical skills and good communication",
    "recommendation": "Hire",
    "createdAt": "2024-02-01T11:00:00Z",
    "updatedAt": "2024-02-01T11:00:00Z"
  }
}
```

---

### 2. Create Interview Feedback
**Endpoint:** `POST /api/interviews/{interviewId}/feedback`  
**HTTP Method:** POST  
**Authentication:** Required (Interviewer)  
**Description:** Add feedback for completed interview

**Request Body:**
```json
{
  "interviewId": "880e8400-e29b-41d4-a716-446655440001",
  "overallRating": "Excellent",
  "technicalScore": 9,
  "communicationScore": 8,
  "culturalFitScore": 9,
  "comments": "Strong candidate with excellent technical skills",
  "recommendation": "Hire"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Interview feedback created successfully",
  "data": {
    "id": "990e8400-e29b-41d4-a716-446655440010",
    "interviewId": "880e8400-e29b-41d4-a716-446655440001",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "interviewerName": "John Manager",
    "overallRating": "Excellent",
    "technicalScore": 9,
    "communicationScore": 8,
    "culturalFitScore": 9,
    "comments": "Strong candidate with excellent technical skills",
    "recommendation": "Hire",
    "createdAt": "2024-02-01T11:00:00Z",
    "updatedAt": "2024-02-01T11:00:00Z"
  }
}
```

---

### 3. Update Interview Feedback
**Endpoint:** `PUT /api/interviews/{interviewId}/feedback`  
**HTTP Method:** PUT  
**Authentication:** Required (Feedback Creator)  
**Description:** Update interview feedback

**Request Body (All fields optional):**
```json
{
  "overallRating": "Good",
  "technicalScore": 8,
  "communicationScore": 8,
  "culturalFitScore": 8,
  "comments": "Good candidate, minor improvements needed",
  "recommendation": "Keep in Queue"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview feedback updated successfully",
  "data": {
    "id": "990e8400-e29b-41d4-a716-446655440010",
    "interviewId": "880e8400-e29b-41d4-a716-446655440001",
    "interviewerId": "550e8400-e29b-41d4-a716-446655440050",
    "interviewerName": "John Manager",
    "overallRating": "Good",
    "technicalScore": 8,
    "communicationScore": 8,
    "culturalFitScore": 8,
    "comments": "Good candidate, minor improvements needed",
    "recommendation": "Keep in Queue",
    "createdAt": "2024-02-01T11:00:00Z",
    "updatedAt": "2024-02-01T12:00:00Z"
  }
}
```

---

### 4. Delete Interview Feedback
**Endpoint:** `DELETE /api/interviews/{interviewId}/feedback`  
**HTTP Method:** DELETE  
**Authentication:** Required  
**Description:** Delete interview feedback

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Interview feedback deleted successfully",
  "data": {
    "id": "990e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

## Offers API

### 1. Get All Offers (Paginated)
**Endpoint:** `GET /api/offers`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get paginated list of offers

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)
- `status` (string, optional) - Filter by status

**Example Request:**
```
GET /api/offers?pageNumber=1&pageSize=10&status=Extended
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offers retrieved successfully",
  "data": {
    "items": [
      {
        "id": "aa0e8400-e29b-41d4-a716-446655440001",
        "applicationId": "770e8400-e29b-41d4-a716-446655440001",
        "candidateId": "550e8400-e29b-41d4-a716-446655440001",
        "candidateName": "Priya Sharma",
        "jobId": "660e8400-e29b-41d4-a716-446655440001",
        "jobTitle": "Senior Software Engineer",
        "salaryOffered": 120000,
        "salaryCurrency": "INR",
        "offerDate": "2024-02-05T10:00:00Z",
        "expiryDate": "2024-02-20T23:59:59Z",
        "status": "Extended",
        "offerLetter": "https://example.com/offers/offer-001.pdf",
        "notes": "Standard offer",
        "createdAt": "2024-02-05T10:00:00Z",
        "updatedAt": "2024-02-05T10:00:00Z"
      }
    ],
    "totalCount": 10,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

---

### 2. Get Offer by ID
**Endpoint:** `GET /api/offers/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single offer details

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offer retrieved successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440001",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "salaryOffered": 120000,
    "salaryCurrency": "INR",
    "offerDate": "2024-02-05T10:00:00Z",
    "expiryDate": "2024-02-20T23:59:59Z",
    "status": "Extended",
    "offerLetter": "https://example.com/offers/offer-001.pdf",
    "notes": "Standard offer",
    "createdAt": "2024-02-05T10:00:00Z",
    "updatedAt": "2024-02-05T10:00:00Z"
  }
}
```

---

### 3. Create Offer
**Endpoint:** `POST /api/offers`  
**HTTP Method:** POST  
**Authentication:** Required (HR Manager, Admin)  
**Description:** Create new job offer

**Request Body:**
```json
{
  "applicationId": "770e8400-e29b-41d4-a716-446655440001",
  "salaryOffered": 120000,
  "salaryCurrency": "INR",
  "expiryDate": "2024-02-20T23:59:59Z",
  "offerLetter": "https://example.com/offers/offer-001.pdf",
  "notes": "Standard offer with health insurance"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Offer created successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440010",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "salaryOffered": 120000,
    "salaryCurrency": "INR",
    "offerDate": "2024-02-05T10:00:00Z",
    "expiryDate": "2024-02-20T23:59:59Z",
    "status": "Extended",
    "offerLetter": "https://example.com/offers/offer-001.pdf",
    "notes": "Standard offer with health insurance",
    "createdAt": "2024-02-05T10:00:00Z",
    "updatedAt": "2024-02-05T10:00:00Z"
  }
}
```

---

### 4. Update Offer
**Endpoint:** `PUT /api/offers/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Update offer details

**Request Body (All fields optional):**
```json
{
  "salaryOffered": 130000,
  "status": "Extended",
  "expiryDate": "2024-02-25T23:59:59Z",
  "notes": "Increased salary offer"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offer updated successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440010",
    "applicationId": "770e8400-e29b-41d4-a716-446655440001",
    "candidateId": "550e8400-e29b-41d4-a716-446655440001",
    "candidateName": "Priya Sharma",
    "jobId": "660e8400-e29b-41d4-a716-446655440001",
    "jobTitle": "Senior Software Engineer",
    "salaryOffered": 130000,
    "salaryCurrency": "INR",
    "offerDate": "2024-02-05T10:00:00Z",
    "expiryDate": "2024-02-25T23:59:59Z",
    "status": "Extended",
    "offerLetter": "https://example.com/offers/offer-001.pdf",
    "notes": "Increased salary offer",
    "createdAt": "2024-02-05T10:00:00Z",
    "updatedAt": "2024-02-06T10:00:00Z"
  }
}
```

---

### 5. Delete Offer
**Endpoint:** `DELETE /api/offers/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required  
**Description:** Delete offer

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offer deleted successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

### 6. Accept Offer
**Endpoint:** `PUT /api/offers/{id}/accept`  
**HTTP Method:** PUT  
**Authentication:** Required (Candidate)  
**Description:** Candidate accepts offer

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offer accepted successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440010",
    "status": "Accepted",
    "candidateName": "Priya Sharma"
  }
}
```

---

### 7. Reject Offer
**Endpoint:** `PUT /api/offers/{id}/reject`  
**HTTP Method:** PUT  
**Authentication:** Required (Candidate)  
**Description:** Candidate rejects offer

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Offer rejected successfully",
  "data": {
    "id": "aa0e8400-e29b-41d4-a716-446655440010",
    "status": "Rejected",
    "candidateName": "Priya Sharma"
  }
}
```

---

## Messages API

### 1. Send Message
**Endpoint:** `POST /api/messages`  
**HTTP Method:** POST  
**Authentication:** Required  
**Description:** Send message in conversation

**Request Body:**
```json
{
  "conversationId": "bb0e8400-e29b-41d4-a716-446655440001",
  "receiverId": "550e8400-e29b-41d4-a716-446655440051",
  "content": "Hi Priya, how are you doing with the onboarding?",
  "attachments": []
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Message sent successfully",
  "data": {
    "id": "cc0e8400-e29b-41d4-a716-446655440001",
    "conversationId": "bb0e8400-e29b-41d4-a716-446655440001",
    "senderId": "550e8400-e29b-41d4-a716-446655440050",
    "senderName": "John Manager",
    "receiverId": "550e8400-e29b-41d4-a716-446655440051",
    "receiverName": "Priya Sharma",
    "content": "Hi Priya, how are you doing with the onboarding?",
    "attachments": [],
    "isRead": false,
    "createdAt": "2024-02-06T10:00:00Z"
  }
}
```

---

### 2. Get Conversation Messages
**Endpoint:** `GET /api/messages/conversation/{conversationId}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get messages in a conversation

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 20)

**Example Request:**
```
GET /api/messages/conversation/bb0e8400-e29b-41d4-a716-446655440001?pageNumber=1&pageSize=20
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Conversation messages retrieved successfully",
  "data": {
    "items": [
      {
        "id": "cc0e8400-e29b-41d4-a716-446655440001",
        "conversationId": "bb0e8400-e29b-41d4-a716-446655440001",
        "senderId": "550e8400-e29b-41d4-a716-446655440050",
        "senderName": "John Manager",
        "receiverId": "550e8400-e29b-41d4-a716-446655440051",
        "receiverName": "Priya Sharma",
        "content": "Hi Priya, how are you doing with the onboarding?",
        "attachments": [],
        "isRead": true,
        "createdAt": "2024-02-06T10:00:00Z"
      }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 20,
    "totalPages": 1
  }
}
```

---

### 3. Mark Message as Read
**Endpoint:** `PUT /api/messages/{id}/read`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Mark message as read

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Message marked as read successfully",
  "data": {
    "id": "cc0e8400-e29b-41d4-a716-446655440001",
    "isRead": true
  }
}
```

---

### 4. Delete Message
**Endpoint:** `DELETE /api/messages/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required  
**Description:** Delete message

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Message deleted successfully",
  "data": {
    "id": "cc0e8400-e29b-41d4-a716-446655440001"
  }
}
```

---

### 5. Create Conversation
**Endpoint:** `POST /api/conversations`  
**HTTP Method:** POST  
**Authentication:** Required  
**Description:** Start new conversation

**Request Body:**
```json
{
  "name": "Onboarding - Priya Sharma",
  "participantIds": [
    "550e8400-e29b-41d4-a716-446655440050",
    "550e8400-e29b-41d4-a716-446655440051"
  ]
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Conversation created successfully",
  "data": {
    "id": "bb0e8400-e29b-41d4-a716-446655440010",
    "name": "Onboarding - Priya Sharma",
    "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
    "participantIds": [
      "550e8400-e29b-41d4-a716-446655440050",
      "550e8400-e29b-41d4-a716-446655440051"
    ],
    "createdAt": "2024-02-06T10:00:00Z",
    "updatedAt": "2024-02-06T10:00:00Z"
  }
}
```

---

### 6. Get User Conversations
**Endpoint:** `GET /api/conversations`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get conversations for logged-in user

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User conversations retrieved successfully",
  "data": [
    {
      "id": "bb0e8400-e29b-41d4-a716-446655440001",
      "name": "Onboarding - Priya Sharma",
      "createdByUserId": "550e8400-e29b-41d4-a716-446655440050",
      "participantIds": [
        "550e8400-e29b-41d4-a716-446655440050",
        "550e8400-e29b-41d4-a716-446655440051"
      ],
      "createdAt": "2024-02-06T10:00:00Z",
      "updatedAt": "2024-02-06T10:00:00Z"
    }
  ]
}
```

---

## Users API

### 1. Get All Users
**Endpoint:** `GET /api/users`  
**HTTP Method:** GET  
**Authentication:** Required (Admin)  
**Description:** Get paginated list of users

**Query Parameters:**
- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Users retrieved successfully",
  "data": {
    "items": [
      {
        "id": "550e8400-e29b-41d4-a716-446655440050",
        "firstName": "John",
        "lastName": "Manager",
        "email": "john@example.com",
        "phoneNumber": "+91 98765 43219",
        "profilePictureUrl": "https://example.com/profiles/john.jpg",
        "roles": ["Admin", "HR Manager"],
        "isActive": true,
        "createdAt": "2024-01-01T08:00:00Z",
        "updatedAt": "2024-02-06T10:00:00Z"
      }
    ],
    "totalCount": 25,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 3
  }
}
```

---

### 2. Get User by ID
**Endpoint:** `GET /api/users/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get user details

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User retrieved successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050",
    "firstName": "John",
    "lastName": "Manager",
    "email": "john@example.com",
    "phoneNumber": "+91 98765 43219",
    "profilePictureUrl": "https://example.com/profiles/john.jpg",
    "roles": ["Admin", "HR Manager"],
    "isActive": true,
    "createdAt": "2024-01-01T08:00:00Z",
    "updatedAt": "2024-02-06T10:00:00Z"
  }
}
```

---

### 3. Create User
**Endpoint:** `POST /api/users`  
**HTTP Method:** POST  
**Authentication:** Required (Admin)  
**Description:** Create new user account

**Request Body:**
```json
{
  "firstName": "Sarah",
  "lastName": "Recruiter",
  "email": "sarah@example.com",
  "password": "SecurePass@123",
  "phoneNumber": "+91 98765 43220",
  "roles": ["Recruiter"]
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "User created successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440052",
    "firstName": "Sarah",
    "lastName": "Recruiter",
    "email": "sarah@example.com",
    "phoneNumber": "+91 98765 43220",
    "profilePictureUrl": "",
    "roles": ["Recruiter"],
    "isActive": true,
    "createdAt": "2024-02-06T11:00:00Z",
    "updatedAt": "2024-02-06T11:00:00Z"
  }
}
```

---

### 4. Update User
**Endpoint:** `PUT /api/users/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Update user profile

**Request Body (All fields optional):**
```json
{
  "firstName": "John",
  "lastName": "Manager",
  "phoneNumber": "+91 98765 43219",
  "profilePictureUrl": "https://example.com/profiles/john.jpg",
  "roles": ["Admin", "HR Manager"],
  "isActive": true
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User updated successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050",
    "firstName": "John",
    "lastName": "Manager",
    "email": "john@example.com",
    "phoneNumber": "+91 98765 43219",
    "profilePictureUrl": "https://example.com/profiles/john.jpg",
    "roles": ["Admin", "HR Manager"],
    "isActive": true,
    "createdAt": "2024-01-01T08:00:00Z",
    "updatedAt": "2024-02-06T12:00:00Z"
  }
}
```

---

### 5. Delete User
**Endpoint:** `DELETE /api/users/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required (Admin)  
**Description:** Delete user account

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User deleted successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050"
  }
}
```

---

### 6. Get User Profile
**Endpoint:** `GET /api/users/{id}/profile`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get extended user profile

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User profile retrieved successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050",
    "firstName": "John",
    "lastName": "Manager",
    "email": "john@example.com",
    "phoneNumber": "+91 98765 43219",
    "profilePictureUrl": "https://example.com/profiles/john.jpg",
    "roles": ["Admin", "HR Manager"],
    "isActive": true,
    "department": "Human Resources",
    "title": "HR Manager",
    "reportingManager": "CEO",
    "createdAt": "2024-01-01T08:00:00Z",
    "updatedAt": "2024-02-06T10:00:00Z"
  }
}
```

---

### 7. Change Password
**Endpoint:** `PUT /api/users/{id}/change-password`  
**HTTP Method:** PUT  
**Authentication:** Required  
**Description:** Change user password

**Request Body:**
```json
{
  "currentPassword": "OldPass@123",
  "newPassword": "NewPass@456",
  "confirmPassword": "NewPass@456"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Password changed successfully",
  "data": null
}
```

---

### 8. Deactivate User
**Endpoint:** `PUT /api/users/{id}/deactivate`  
**HTTP Method:** PUT  
**Authentication:** Required (Admin)  
**Description:** Deactivate user account

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User deactivated successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050",
    "isActive": false
  }
}
```

---

### 9. Reactivate User
**Endpoint:** `PUT /api/users/{id}/reactivate`  
**HTTP Method:** PUT  
**Authentication:** Required (Admin)  
**Description:** Reactivate user account

**Request Body:** Empty

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User reactivated successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440050",
    "isActive": true
  }
}
```

---

## Skills API

### 1. Get All Skills
**Endpoint:** `GET /api/skills`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get all available skills

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Skills retrieved successfully",
  "data": [
    {
      "id": "dd0e8400-e29b-41d4-a716-446655440001",
      "name": "Machine Operation",
      "category": "Manufacturing",
      "description": "Expertise in operating industrial machinery",
      "demandLevel": 4
    },
    {
      "id": "dd0e8400-e29b-41d4-a716-446655440002",
      "name": "Quality Control",
      "category": "Manufacturing",
      "description": "Quality assurance and inspection",
      "demandLevel": 5
    }
  ]
}
```

---

### 2. Get Skill by ID
**Endpoint:** `GET /api/skills/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single skill

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Skill retrieved successfully",
  "data": {
    "id": "dd0e8400-e29b-41d4-a716-446655440001",
    "name": "Machine Operation",
    "category": "Manufacturing",
    "description": "Expertise in operating industrial machinery",
    "demandLevel": 4
  }
}
```

---

### 3. Create Skill
**Endpoint:** `POST /api/skills`  
**HTTP Method:** POST  
**Authentication:** Required (Admin)  
**Description:** Create new skill

**Request Body:**
```json
{
  "name": "Welding",
  "category": "Manufacturing",
  "description": "Professional welding and metal fabrication"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Skill created successfully",
  "data": {
    "id": "dd0e8400-e29b-41d4-a716-446655440010",
    "name": "Welding",
    "category": "Manufacturing",
    "description": "Professional welding and metal fabrication",
    "demandLevel": 3
  }
}
```

---

### 4. Update Skill
**Endpoint:** `PUT /api/skills/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required (Admin)  
**Description:** Update skill

**Request Body (All fields optional):**
```json
{
  "name": "Advanced Welding",
  "description": "Professional welding and advanced metal fabrication",
  "demandLevel": 5
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Skill updated successfully",
  "data": {
    "id": "dd0e8400-e29b-41d4-a716-446655440010",
    "name": "Advanced Welding",
    "category": "Manufacturing",
    "description": "Professional welding and advanced metal fabrication",
    "demandLevel": 5
  }
}
```

---

### 5. Delete Skill
**Endpoint:** `DELETE /api/skills/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required (Admin)  
**Description:** Delete skill

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Skill deleted successfully",
  "data": {
    "id": "dd0e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

### 6. Get Skills by Category
**Endpoint:** `GET /api/skills/category/{category}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get skills in specific category

**URL Parameters:**
- `category` (string) - Skill category

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Skills retrieved successfully",
  "data": [
    {
      "id": "dd0e8400-e29b-41d4-a716-446655440001",
      "name": "Machine Operation",
      "category": "Manufacturing",
      "description": "Expertise in operating industrial machinery",
      "demandLevel": 4
    }
  ]
}
```

---

## Departments API

### 1. Get All Departments
**Endpoint:** `GET /api/departments`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get all departments

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Departments retrieved successfully",
  "data": [
    {
      "id": "ee0e8400-e29b-41d4-a716-446655440001",
      "name": "Engineering",
      "description": "Software and hardware development",
      "managerId": "550e8400-e29b-41d4-a716-446655440053",
      "managerName": "Sarah Engineer"
    },
    {
      "id": "ee0e8400-e29b-41d4-a716-446655440002",
      "name": "Human Resources",
      "description": "HR and recruitment",
      "managerId": "550e8400-e29b-41d4-a716-446655440050",
      "managerName": "John Manager"
    }
  ]
}
```

---

### 2. Get Department by ID
**Endpoint:** `GET /api/departments/{id}`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get single department

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department retrieved successfully",
  "data": {
    "id": "ee0e8400-e29b-41d4-a716-446655440001",
    "name": "Engineering",
    "description": "Software and hardware development",
    "managerId": "550e8400-e29b-41d4-a716-446655440053",
    "managerName": "Sarah Engineer"
  }
}
```

---

### 3. Create Department
**Endpoint:** `POST /api/departments`  
**HTTP Method:** POST  
**Authentication:** Required (Admin)  
**Description:** Create new department

**Request Body:**
```json
{
  "name": "Sales",
  "description": "Sales and business development",
  "managerId": "550e8400-e29b-41d4-a716-446655440054"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Department created successfully",
  "data": {
    "id": "ee0e8400-e29b-41d4-a716-446655440010",
    "name": "Sales",
    "description": "Sales and business development",
    "managerId": "550e8400-e29b-41d4-a716-446655440054",
    "managerName": "Mike Sales"
  }
}
```

---

### 4. Update Department
**Endpoint:** `PUT /api/departments/{id}`  
**HTTP Method:** PUT  
**Authentication:** Required (Admin)  
**Description:** Update department

**Request Body (All fields optional):**
```json
{
  "name": "Sales & Marketing",
  "description": "Sales, marketing and business development",
  "managerId": "550e8400-e29b-41d4-a716-446655440054"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department updated successfully",
  "data": {
    "id": "ee0e8400-e29b-41d4-a716-446655440010",
    "name": "Sales & Marketing",
    "description": "Sales, marketing and business development",
    "managerId": "550e8400-e29b-41d4-a716-446655440054",
    "managerName": "Mike Sales"
  }
}
```

---

### 5. Delete Department
**Endpoint:** `DELETE /api/departments/{id}`  
**HTTP Method:** DELETE  
**Authentication:** Required (Admin)  
**Description:** Delete department

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department deleted successfully",
  "data": {
    "id": "ee0e8400-e29b-41d4-a716-446655440010"
  }
}
```

---

## Dashboard API

### 1. Get Dashboard Statistics
**Endpoint:** `GET /api/dashboard/stats`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get key dashboard statistics

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Dashboard statistics retrieved successfully",
  "data": {
    "totalCandidates": 150,
    "activeJobs": 12,
    "pendingApplications": 35,
    "scheduledInterviews": 8,
    "offersExtended": 5,
    "hiringConversionRate": 15.5
  }
}
```

---

### 2. Get Recent Activities
**Endpoint:** `GET /api/dashboard/recent-activities`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get recent activities

**Query Parameters:**
- `count` (int, default: 10) - Number of activities to retrieve

**Example Request:**
```
GET /api/dashboard/recent-activities?count=10
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Recent activities retrieved successfully",
  "data": [
    {
      "id": "ff0e8400-e29b-41d4-a716-446655440001",
      "activityType": "ApplicationReceived",
      "description": "New application received from Priya Sharma",
      "relatedEntityId": "550e8400-e29b-41d4-a716-446655440001",
      "relatedEntityType": "Candidate",
      "userId": "550e8400-e29b-41d4-a716-446655440050",
      "userName": "John Manager",
      "createdAt": "2024-02-06T10:00:00Z"
    }
  ]
}
```

---

### 3. Get Pipeline Overview
**Endpoint:** `GET /api/dashboard/pipeline`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get pipeline overview by stage

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Pipeline overview retrieved successfully",
  "data": [
    {
      "stageId": "gg0e8400-e29b-41d4-a716-446655440001",
      "stageName": "Applied",
      "candidateCount": 35,
      "applications": [
        {
          "id": "770e8400-e29b-41d4-a716-446655440001",
          "candidateName": "Priya Sharma",
          "status": "Applied",
          "matchScore": 75,
          "appliedDate": "2024-01-24T10:00:00Z",
          "currentStage": "Applied"
        }
      ]
    },
    {
      "stageId": "gg0e8400-e29b-41d4-a716-446655440002",
      "stageName": "Shortlisted",
      "candidateCount": 15,
      "applications": []
    }
  ]
}
```

---

### 4. Get Hiring Metrics
**Endpoint:** `GET /api/dashboard/hiring-metrics`  
**HTTP Method:** GET  
**Authentication:** Required  
**Description:** Get hiring metrics for date range

**Query Parameters:**
- `startDate` (DateTime, required) - Start date
- `endDate` (DateTime, required) - End date

**Example Request:**
```
GET /api/dashboard/hiring-metrics?startDate=2024-01-01&endDate=2024-02-06
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Hiring metrics retrieved successfully",
  "data": {
    "period": "2024-01-01 to 2024-02-06",
    "applicationsReceived": 50,
    "applicationsClosed": 5,
    "offersExtended": 5,
    "offersAccepted": 3,
    "averageTimeToHire": 25,
    "conversionRate": 10,
    "topSources": {
      "DirectApply": 20,
      "Referral": 15,
      "LinkedIn": 12,
      "JobBoard": 3
    },
    "topDepartments": {
      "Engineering": 20,
      "Sales": 15,
      "Operations": 15
    }
  }
}
```

---

## Summary Statistics

- **Total Controllers**: 12
- **Total Endpoints**: 70+
- **Authentication**: Bearer Token (JWT)
- **Response Format**: Standardized ApiResponse<T>
- **Error Format**: Standardized ErrorResponse
- **Status Codes**: 200 (OK), 201 (Created), 400 (Bad Request), 404 (Not Found), 500 (Server Error)

All endpoints follow RESTful conventions and are ready for implementation!
