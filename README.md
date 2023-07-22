
# Alegrario API - Embrace your emotions, but never let them take control


## Table of Contents

- [Introduction](#introduction)
- [How to Use](#how-to-use)
- [API Endpoints](#api-endpoints)
- [Used Technologies](#used-technologies)
- [Additional Resources](#additional-resources)
- [Expansions](#expansions)
- [Credits](#credits)

## Introduction

This API serves as the backend expansion for the Alegrario mobile app, which was initially developed during the Hackatruck bootcamp using NodeRed. The application provides functionalities for the emotion calendar, where they can record their emotions for each hour of the day, allowing for comprehensive emotional analysis and improved emotional control.

<h3>About Alegrario</h3>

The Alegrario App is a Swift-conceived application developed during the Hackatruck Bootcamp under 5 days. It addresses the challenge of controlling our emotions throughout the day, as we encounter situations that trigger both positive and negative feelings. Emotional control plays a crucial role in reducing anxiety, depression, and enhancing decision-making abilities while building resilience against frustrations. Taking inspiration from the movie "Inside Out," the app employs emoticons for emotion recording and color-based visualization on a calendar, making the experience simple and user-friendly.

<h3>Alegrario V 0.5</h3>

<a href="https://www.youtube.com/shorts/NAxOm4rwqWE" target="_blank" rel="noreferrer">  <img src="https://www.aracruz.es.leg.br/imagens/f2ea1ded4d037633f687ee389a571086logotipodoconedoyoutubebyvexels.png/image" alt="youtube" width="50" height="50"/> Alegrario v0.5 short <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Circle-icons-smartphone.svg/1024px-Circle-icons-smartphone.svg.png" alt="smartphone" width="50" height="50" /></a>

<a href="#" target="_blank" rel="noreferrer">  <img src="https://www.aracruz.es.leg.br/imagens/f2ea1ded4d037633f687ee389a571086logotipodoconedoyoutubebyvexels.png/image" alt="youtube" width="50" height="50"/> AlegrarioAPI <img src="https://salesforceprofs.com/wp-content/uploads/2019/12/api_rest.png" alt="restapi" width="80" height="60" /></a>




<h3>Screens</h3>
<div style="display: flex; align-items: flex-end;">
  <div style="flex: 25%; padding: 5px;">
    <img alt="AlegrarioCalendar" src="https://github.com/ViniciusLAAraujo/.NetAlegrarioAPI/assets/90988825/a8e5e5de-0f0e-4d5d-b4d5-a166a97cebae" style="width: 100%; border: 1px solid #ccc; width: 100%; height: 450px">
  </div>
  <div style="flex: 25%; padding: 5px;">
    <img src="https://github.com/ViniciusLAAraujo/.NetAlegrarioAPI/assets/90988825/fd7d166f-3ff5-4928-9f70-f934c9ab1120" alt="AlegrarioEmotion" style="width: 100%; border: 1px solid #ccc;width: 100%; height: 450px">
  </div>
  <div style="flex: 25%; padding: 5px;">
    <img src="https://github.com/ViniciusLAAraujo/.NetAlegrarioAPI/assets/90988825/857604db-8758-4574-b651-eda2dd329b27" alt="AlegrarioWeek" style="width: 100%; border: 1px solid #ccc;width: 100%; height: 450px">
  </div>
  <div style="flex: 25%; padding: 5px;">
    <img src="https://github.com/ViniciusLAAraujo/.NetAlegrarioAPI/assets/90988825/56e01bf9-b364-4a21-b6b5-10b8aecace61" alt="AlegrarioGraph" style="width: 100%; border: 1px solid #ccc; width: 100%; height: 450px">
  </div>
</div>

<h3>PDF Onboards</h3>

[Download PDF in PT-BR](https://github.com/ViniciusLAAraujo/AlegrarioPDFs/raw/master/ALEGR%C3%81RIO.pdf)

[Download PDF in ENG] - in comming


## How to use

## API Endpoints

<h3>Auth Endpoints</h3>

- `/Auth/Register`:  <span style="color: yellow;">[POST]</span>

<h4>Request:</h4>

```json
{
  "Email": "user@example.com",
  "Password": "password123",
  "PasswordConfirm": "password123"
}
```

- `/Auth/Login`:  <span style="color: yellow;">[POST]</span>

<h4>Request:</h4>

```json
{
  "Email": "user@example.com",
  "Password": "password123"
}
```

<h4>Response:</h4>

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

- `/Auth/RefreshToken`:  <span style="color: lightgreen;">[GET]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

<h4>Response (JWT Token String):</h4>

```json
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

- `/Auth/ResetPassword`:  <span style="color: lightblue;">[PUT]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "Email": "user@example.com",
  "Password": "newpassword123"
}
```
<h3>User Endpoints</h3>

- `/User/GetUsers/{userId}/{isActive}`: <span style="color: lightgreen;">[GET]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span>
<br>
<span style = " color: #0070cc; ">userId</span> <span style = " color: lightblue;">[integer]</span>: Filter users by their ID. (0 = all users)
<br>
<span style = " color: #0070cc; " > isActive </span> <span style = "color: lightblue;">[boolean]</span> : Filter active or inactive users. (false = inactive and active users ; true = only active users).</p>

<h4>Response:</h4>

```json
[
  {
    "userId": 1,
    "username": "john_doe",
    "email": "john@example.com",
    "isActive": true
  },
  {
    "userId": 2,
    "username": "jane_smith",
    "email": "jane@example.com",
    "isActive": true
  }
]
```

- `/User/UpsertUser`: <span style="color: lightblue;">[PUT]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "userId": 1, // Optional 
  "username": "john_doe",
  "email": "john@example.com",
  "isActive": true
}
```
<p><span style = " color: lightblue;">"userId":</span> Invalid or None creates new user, Valid updates given user.</p>

- `/User/DeleteUser/{userId}`: <span style="color: red;">[DELETE]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span>
<br>
<span style = " color: #0070cc; ">userId</span> <span style = " color: lightblue;">[integer]</span>:  ID of the user to be deleted (Also delete realated daycells as well).</p>

<h3>DayCell and Emotions Endpoints</h3>

- `/DayCell/GetDays/{Month}/{DateSearch}`: <span style="color: lightgreen;">[GET]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span>
<br>
<span style = " color: #0070cc; ">Month</span> <span style = " color: lightblue;">[integer]</span>: Filter the user's days by a specific month (1 to 12), Invalid <=0 or >12 returns current Month.
<br>
<span style = " color: #0070cc; ">DateSearch</span> <span style = " color: lightblue;">[string, format: <span style= "color: orange;">"yyyy-MM-dd"</span>]</span> : Filter specific date, All user's days off given month = 1900-01-01.</p>

<h4>Response:</h4>

```json
[
    {
    "UserId": 123,
    "CellDay": "2023-07-01"
  },
  {
    "UserId": 123,
    "CellDay": "2023-07-23"
  },
  {
    "UserId": 123,
    "CellDay": "2023-07-30"
  }
]

```
- `/DayCell/GetWeekDays`: <span style="color: lightgreen;">[GET]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

<h4>Response:</h4>

```json
[
  {
    "UserId": 123,
    "CellDay": "2023-07-15"
  },
  {
    "UserId": 123,
    "CellDay": "2023-07-16"
  }
]
```
- `/DayCell/InsertDayCell`: [POST]

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "CellDay": "2023-07-22"
}
```
- `/DayCell/DeleteDay`: <span style="color: red;">[DELETE]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "CellDay": "2023-07-22"
}
```
- `/DayCell/GetEmotions/{Month}/{HourId}/{EmotionValue}/{DateSearch}`: [GET]

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span>
<br>
<span style = " color: #0070cc; ">Month</span> <span style = " color: lightblue;">[integer]</span>: Filter the user's emotions by a specific month (1 to 12), Invalid <=0 or >12 returns all user's emotions from all months.
HourId [integer]: Filter the user's emotions by a specific hour (0 to 23), Invalid <0 or >23 returns all user's emotions from all hours.
<br>
<span style = " color: #0070cc; ">EmotionValue</span> <span style = " color: lightblue;">[integer]</span>:  Filter the user's emotions by a specific emotion value (1 to 4), Invalid <1 or >4 returns all user's emotions from all values.
<br>
<span style = " color: #0070cc; ">DateSearch</span> <span style = " color: lightblue;">[string, format: <span style= "color: orange;">"yyyy-MM-dd"</span>]</span> : Filter specific user's emotions on a given date, All user's emotions off a given date = 1900-01-01.</p>

<h4>Response:</h4>

```json
[
  {
    "UserId": 123,
    "CellDay": "2023-07-15",
    "HourId": 12,
    "EmotionValue": 1,
    "Comment": "Feeling happy today!",
    "Score": 8
  },
  {
    "UserId": 123,
    "CellDay": "2023-07-15",
    "HourId": 0,
    "EmotionValue": 2,
    "Comment": "Feeling neutral today!",
    "Score": 5
  }
]
```

- `/DayCell/UpsertEmotion`: [PUT]

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "CellDay": "2023-07-22",
  "HourId": 16, //Invalids <0 or >23 are set to 0 
  "EmotionValue": 2, //Invalids <1 or >4 are set to 2
  "Comment": "Feeling neutral.",
  "Score": 6 //Invalids <1 or >10 are set to 5
}
```
- `/DayCell/GetEmotionsStats/{DateSearch}/{EmotionValue}/{IncludeAllEmotions}`: <span style="color: lightgreen;">[GET]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span>
<br>
<span style = " color: #0070cc; ">DateSearch</span> <span style = " color: lightblue;">[string, format: <span style= "color: orange;">"yyyy-MM-dd"</span>]</span> : Filter specific user's emotions statistics on a given date, Invalid date = 1900-01-01.
<br>
<span style = " color: #0070cc; ">EmotionValue</span> <span style = " color: lightblue;">[integer]</span>:  Filter the user's emotions statistics by a specific emotion value (1 to 4), Invalid <1 or >4 returns all user's emotions statistics from all values combined.
<br>
<span style = " color: #0070cc; ">IncludeAllEmotions</span> <span style = " color: lightblue;">[boolean]</span>: When true return a separate emotion statistics for every give emotions in the user's day.</p>

<h4>Response:</h4>

```json
[
    {
        "emotionValue": 3,
        "amount": 2,
        "average": 7.500000,
        "emotion": "Angry"
    },
    {
        "emotionValue": 4,
        "amount": 1,
        "average": 8.000000,
        "emotion": "Sad"
    }
]
```
<h4>Or</h4>

```json
[
    {
        "emotionValue": 0,
        "amount": 3,
        "average": 7.666666,
        "emotion": "AllEmotions"
    }
]
```
- `/DayCell/DeleteEmotion`: <span style="color: red;">[DELETE]</span>

<h4>Request:</h4>
<p>Authorization: Bearer <span style="color: #0070cc; font-style: italic; font-weight: 600" >"your_access_token"</span></p>

```json
{
  "CellDay": "2023-07-22",
  "HourId": 16
}
```

## Used Technologies

- <a href="https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg" alt="dotnet" width="50" height="50"/> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="50" height="50"/> [.NET with C#]:  </a> <p>The project is built using the .NET framework with C# as the primary programming language</p>

- <a href="https://jwt.io/introduction" target="_blank" rel="noreferrer"> <img src="https://seeklogo.com/images/J/jwt-logo-11B708E375-seeklogo.com.png" alt="jwt" width="120" height="45"/> [JWT (JSON Web Tokens)]:  </a> <p>JWT is used for authentication and securing API endpoints.</p>

- <a href="https://learn.microsoft.com/en-us/sql/sql-server" target="_blank" rel="noreferrer">  <img src="https://static-00.iconduck.com/assets.00/sql-database-generic-icon-1521x2048-d0vdpxpg.png" alt="android" width="38" height="50"/> [MSSQL]:  </a> <p>The project utilizes Microsoft SQL Server as the relational database management system</p>

- <a href="https://azure.microsoft.com/" target="_blank" rel="noreferrer">  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/azure/azure-original.svg" alt="azure" width="40" height="40"/> [Azure]:  </a> <p>The project is hosted on Microsoft Azure </p>

- <a href="https://swagger.io/" target="_blank" rel="noreferrer">  <img src="https://help.apiary.io/images/swagger-logo.png" alt="swagger" width="50" height="50"/> [Swagger] </a> <p> & </p> <a href="https://www.postman.com/" target="_blank" rel="noreferrer">  <img src="https://www.svgrepo.com/show/354202/postman-icon.svg" alt="postman" width="50" height="50"/> [Postman]  </a> <p>API documentation and testing </p>



## Additional Resources

- [PBKDF2](https://datatracker.ietf.org/doc/html/rfc2898) - Learn more about the method used for generating passwords in this project. PBKDF2 is a cryptographic key derivation function that uses a salted hash to securely derive keys from passwords. It is widely used for password hashing and storage in security applications. The link leads to the RFC 2898 document, which specifies the PBKDF2 algorithm.


## Expansions

- Create Admin (not all users should be able to upser users, delete other users, etc)
- User deletion cascade to Emotions (deleting an user should delete emotions of their related daycells as well)
- Add get by emotion value and search by comment


## Credits

- [.NET Core with MS SQL: Beginner to Expert](https://www.udemy.com/course/net-core-with-ms-sql-beginner-to-expert/) by Dominic Tripod: A comprehensive Udemy course to learn .NET Core with MS SQL from beginner to expert level.

- [Hackatruck](https://hackatruck.com.br): Hackatruck Bootcamp: Powered by IBM and Instituto Eldorado.