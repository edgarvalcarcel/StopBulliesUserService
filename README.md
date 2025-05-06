
---

## 🔐 Core Features

### ✅ User Identity & Authentication
- Email/password registration
- Secure login with JWT
- Password hashing (BCrypt)

### 🧩 Role & Claim Management
- Assign roles to users (Admin, Parent, Student, etc.)
- Manage custom claims for users and roles

### 🌐 External Authentication
- OAuth2 login via Google, Facebook (extensible)

### 🔄 Token Management
- Access + Refresh Tokens
- Token revocation and expiration logic

---

## 🧠 Domain Entities

- `User`: core user identity (email, password hash, status)
- `Role`: system roles
- `UserRole`: many-to-many relationship between users and roles
- `UserClaim`: per-user claims
- `RoleClaim`: claims by role
- `UserLogin`: external provider logins
- `UserToken`: refresh/reset tokens

---

## 🗃️ Database Tables

| Table         | Purpose                                      |
|---------------|----------------------------------------------|
| `Users`       | Stores user accounts                         |
| `Roles`       | Available system roles                       |
| `UserRoles`   | Links users to roles                         |
| `UserClaims`  | Custom per-user claims                       |
| `RoleClaims`  | Role-based access permissions                |
| `UserLogins`  | OAuth2 external login references             |
| `UserTokens`  | Refresh/reset tokens, expiration             |

---

## 🚀 API Endpoints

| Method | Route                            | Description                       |
|--------|----------------------------------|-----------------------------------|
| POST   | `/api/users/register`            | Register new user                 |
| POST   | `/api/users/login`               | Login and get access token        |
| POST   | `/api/users/token`               | Refresh token                     |
| PATCH  | `/api/users/{id}/roles`          | Assign roles to users             |
| POST   | `/api/users/claims`              | Add claim to a user               |
| POST   | `/api/roles/claims`              | Add claim to a role               |
| POST   | `/api/users/external-login`      | OAuth login with external provider|

---

## 🧪 Security and Testing

- Passwords hashed using BCrypt with salt
- JWT validation and claim-based authorization
- Unit and integration tests for core use cases
- Role and claim-based access control (RBAC)

---

## 🛠️ Deployment

- Containerized via Docker
- Compatible with Azure App Service or AKS
- Config via `appsettings.{env}.json`

---

## 🧭 Future Enhancements

- Multi-Factor Authentication (MFA)
- Admin audit trails and login history
- Consent tracking (COPPA, FERPA)

---

## 📄 License

MIT License

---

## 🤝 Contributions

Contributions and PRs are welcome to improve authentication features, logging, and compliance layers.

