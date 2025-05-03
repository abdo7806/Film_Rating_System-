$(document).ready(function() {
    // Handle form submission
    $("#loginForm").on("submit", function(event) {
        event.preventDefault();

        var username = $("#username").val();
        var password = $("#password").val();

        if (!username || !password) {
            alert("Please fill in all fields.");
            return;
        }

        // Create the login request object
        var loginRequest = {
            username: username,
            password: password
        };


        // Send login request to API
        $.ajax({
            url: "https://localhost:7215/api/Users/login", // Ensure to update with your API URL
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(loginRequest),

            success: function(response) {
                // On success, store the token
                localStorage.setItem("authToken", response.token);
                localStorage.setItem("userRole", response.role);

                alert("Login successful!");
                if (response.role == "Admin")
                    window.location.href = "admin/dashboard.html";
                else
                    window.location.href = "index.html"; // Redirect to home or dashboard
            },
            error: function(xhr, status, error) {
                alert("Login failed: " + xhr.responseText);
            }
        });
    });
});