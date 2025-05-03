// js/admin/admin-auth.js
document.addEventListener("DOMContentLoaded", function() {
    const token = localStorage.getItem("authToken");
    const role = localStorage.getItem("userRole");

    if (!token || role !== "Admin") {
        alert("Access denied. Admins only.");
        window.location.href = "../login.html";
    }
});