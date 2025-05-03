document.addEventListener("DOMContentLoaded", () => {
    fetchUsers();

    document.getElementById("user-form").addEventListener("submit", handleFormSubmit);
});

// جلب جميع المستخدمين من API
async function fetchUsers() {
    const response = await fetch("https://localhost:7215/api/Users/All");
    const users = await response.json();
    const list = document.getElementById("users-list");
    list.innerHTML = "";
    users.forEach(user => {
        list.innerHTML += `
					<tr>
							<td>${user.id}</td>
							<td>${user.username}</td>
							<td>${user.email}</td>
							<td>${user.role}</td>
							<td>
									<button class="btn btn-warning btn-sm" onclick="editUser(${user.id})">تعديل</button>
									<button class="btn btn-danger btn-sm" onclick="deleteUser(${user.id})">حذف</button>
							</td>
					</tr>
			`;
    });
}

// إرسال النموذج لإضافة أو تعديل مستخدم
async function handleFormSubmit(e) {
    e.preventDefault();

    const id = document.getElementById("user-id").value;
    const user = {
        username: document.getElementById("username").value,
        email: document.getElementById("email").value,
        password: document.getElementById("password").value,
        role: document.getElementById("role").value
    };
    // alert(id);
    const method = id ? "PUT" : "POST";
    const url = id ? `https://localhost:7215/api/Users/${id}` : "https://localhost:7215/api/Users/register";
    // alert(url);

    await fetch(url, {
        method,
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("authToken")}`
        },
        body: JSON.stringify(user)
    });

    document.getElementById("user-form").reset();
    document.getElementById("user-id").value = "";
    document.getElementById("password").style.visibility = "visible";

    fetchUsers();
}

// تعديل بيانات مستخدم
function editUser(id) {
    fetch(`https://localhost:7215/api/Users/${id}`)
        .then(res => res.json())
        .then(user => {
            document.getElementById("user-id").value = user.id;
            document.getElementById("username").value = user.username;
            document.getElementById("email").value = user.email;
            document.getElementById("role").value = user.role;
            document.getElementById("password").value = user.PasswordHash;
        });
    document.getElementById("password").style.visibility = "hidden";
}

// حذف مستخدم
async function deleteUser(id) {
    if (!confirm("هل أنت متأكد من حذف هذا المستخدم؟")) return;

    await fetch(`https://localhost:7215/api/Users/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("authToken")}`
        }
    });

    fetchUsers();
}