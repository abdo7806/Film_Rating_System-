# 🎬 Film Rating System | نظام تقييم الأفلام

A full-stack platform where users can rate movies, while administrators manage movies, users, and reviews.

منصة متكاملة لتقييم الأفلام، حيث يمكن للمستخدمين تقييم الأفلام، ويقوم المسؤولون بإدارة الأفلام، المستخدمين، والتقييمات.

---

## 🔧 Technologies Used | التقنيات المستخدمة

### 🖥 Back-End:
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  
- Repository Pattern & DTOs  
- Role-Based Authorization

### 🌐 Front-End:
- HTML / CSS / JavaScript  
- Bootstrap  
- Vanilla JS (No Frameworks)

---

## 👥 User Roles | صلاحيات المستخدمين

### 👤 User | المستخدم:
- Login and registration | تسجيل الدخول وإنشاء حساب  
- View movies | استعراض الأفلام  
- Rate movies (once per movie) | تقييم كل فيلم مرة واحدة  

### 🛠 Admin | المشرف:
- 🎬 Movies: Add, update, delete, view | إدارة الأفلام (إضافة، تعديل، حذف، عرض)  
- 👤 Users: Add, update, delete, view | إدارة المستخدمين (إضافة، تعديل، حذف، عرض)  
- ⭐ Reviews: View and delete only | عرض التقييمات وحذفها فقط  

---

📁 Project Structure | هيكل المشروع
```
Film_Rating_System-/
├── backend/                          # الواجهة الخلفية
│   ├── Controllers/                  # Web API Controllers
│   ├── Models/                       # الطبقات النموذجية
│   │   ├── DTOs/                     # كائنات نقل البيانات (Data Transfer Objects)
│   │   └── Repositories/            # مستودعات الوصول للبيانات
│   ├── Services/                     # منطق الأعمال (Business Logic)
│   └── Program.cs                    # نقطة التشغيل
│
├── frontend/                         # الواجهة الأمامية
│   ├── index.html
│   ├── css/
│   └── js/
│
└── README.md  
```
## 🚀 How to Run | كيفية التشغيل

1. Run the API project in Visual Studio | شغّل مشروع الـ API من Visual Studio  
   - Apply migrations and update the database | نفّذ التحديثات لقاعدة البيانات  
2. Open the `frontend/` folder in a live server or browser | افتح مجلد الواجهة بمتصفح  
3. Start using the system | ابدأ باستخدام النظام

---

## 🌟 Features | المميزات

- JWT-based secure authentication | تسجيل دخول آمن باستخدام JWT  
- Admin/User role separation | فصل واضح بين صلاحيات المستخدم والمشرف  
- Organized front-end and back-end | تنظيم واضح في الواجهة والكود الخلفي  
- User-friendly and responsive design | تصميم سهل الاستخدام ومتجاوب

---

## 📸 Screenshots | لقطات شاشة

![Login Page](https://github.com/abdo7806/Film_Rating_System-/blob/main/1.png?raw=true)  
![register Page](https://github.com/abdo7806/Film_Rating_System-/blob/main/9.png?raw=true)  
![Movie show page](https://github.com/abdo7806/Film_Rating_System-/blob/main/2.png?raw=true)  
![Reviews Page](https://github.com/abdo7806/Film_Rating_System-/blob/main/3.png?raw=true)
![Rating comments](https://github.com/abdo7806/Film_Rating_System-/blob/main/4.png?raw=true)
![admin index](https://github.com/abdo7806/Film_Rating_System-/blob/main/5.png?raw=true)
![Movies Management](https://github.com/abdo7806/Film_Rating_System-/blob/main/6.png?raw=true)
![Manage Reviews](https://github.com/abdo7806/Film_Rating_System-/blob/main/8.png?raw=true)

---

## 📡 API Documentation

### 🎬 Movie Endpoints
| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/Movie` | GET | Get all movies | - |
| `/api/Movie/{id}` | GET | Get movie by ID | `id` (int) |
| `/api/Movie` | POST | Add new movie | `Movie` (JSON) |
| `/api/Movie/{id}` | PUT | Update movie | `id` (int), `Movie` (JSON) |
| `/api/Movie/{id}` | DELETE | Delete movie | `id` (int) |

### ⭐ Reviews Endpoints
| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/Reviews` | GET | Get all reviews | - |
| `/api/Reviews/{id}` | GET | Get review by ID | `id` (int) |
| `/api/Reviews/user/{userId}` | GET | Get user's reviews | `userId` (int) |
| `/api/Reviews` | POST | Add new review | `Review` (JSON) |
| `/api/Reviews/{id}` | PUT | Update review | `id` (int), `Review` (JSON) |
| `/api/Reviews/{id}` | DELETE | Delete review | `id` (int) |

### 👤 Users Endpoints
| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/Users/login` | POST | User login | `Credentials` (JSON) |
| `/api/Users/register` | POST | User registration | `User` (JSON) |
| `/api/Users/{id}` | GET | Get user by ID | `id` (int) |
| `/api/Users/{id}` | PUT | Update user | `id` (int), `User` (JSON) |
| `/api/Users/{id}` | DELETE | Delete user | `id` (int) |

## 👨‍💻 Developer | المطور

### 🙋‍♂️ About the Developer

I'm **Abdulsalam Dhahabi**, a passionate backend developer with strong expertise in:

✅ ASP.NET Core & Web APIs
✅ Database Design & SQL Server
✅ SOLID Principles & Clean Code
✅ Git & GitHub (2000+ problems solved)
✅ Desktop & Web App Development

🔗 GitHub: [github.com/abdo7806](https://github.com/abdo7806)
📧 Email: [balzhaby26@gmail.com](mailto:balzhaby26@gmail.com)
🌍 LinkedIn: [linkedin.com/in/abdulsalam-al-dhahabi-218887312](https://linkedin.com/in/abdulsalam-al-dhahabi-218887312)

---

## 🤝 Contributing | المساهمة

Contributions are welcome!  
Feel free to fork the repo, open issues, and submit pull requests.

مرحب بالمساهمات!  
يمكنك عمل fork للمستودع، فتح مشكلات (Issues)، وتقديم Pull Requests.
---


## 📃 License | الترخيص

This project is open-source for learning and personal use only.  


هذا المشروع مفتوح المصدر لأغراض التعلم والاستخدام الشخصي فقط.
