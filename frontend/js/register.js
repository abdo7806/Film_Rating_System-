// التعامل مع إرسال النموذج
document.getElementById("registerForm").addEventListener("submit", async function(event) {
    event.preventDefault();

    // الحصول على البيانات المدخلة
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    // التحقق من صحة البيانات
    if (!username || !email || !password) {
        document.getElementById("errorMessage").style.display = 'block';
        document.getElementById("errorMessage").innerText = "Please fill in all fields.";
        return;
    }

    try {
        const response = await fetch("https://localhost:7215/api/Users/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                username: username,
                email: email,
                password: password,
                role: "User"
            }),
        });

        const data = await response.json();
        if (response.ok) {
            alert("Account created successfully!");
            // إعادة التوجيه إلى صفحة تسجيل الدخول بعد نجاح التسجيل
            window.location.href = "../login.html";
        } else {
            document.getElementById("errorMessage").style.display = 'block';
            document.getElementById("errorMessage").innerText = data.message;
        }
    } catch (error) {
        document.getElementById("errorMessage").style.display = 'block';
        document.getElementById("errorMessage").innerText = "An error occurred while creating the account.";
    }
});