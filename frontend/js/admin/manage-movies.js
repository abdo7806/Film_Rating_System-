document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem("authToken");
    const role = localStorage.getItem("userRole");

    if (!token || role !== "Admin") {
        alert("Access denied. Admins only.");
        window.location.href = "../login.html";
    } else {
        fetchMovies();
    }
    // الاستماع إلى حدث إرسال النموذج
    document.getElementById("movie-form").addEventListener("submit", handleFormSubmit);
});

// جلب الأفلام من الـ API
async function fetchMovies() {
    try {
        const response = await fetch("https://localhost:7215/api/Movie/All");
        const movies = await response.json();

        const tableBody = document.querySelector("#movies-list tbody");
        tableBody.innerHTML = ""; // تفريغ الجدول قبل إضافة الأفلام

        if (movies.length === 0) {
            tableBody.innerHTML = "<tr><td colspan='6' class='text-center'>لا يوجد أفلام لعرضها</td></tr>";
            return;
        }

        movies.forEach(movie => {
            tableBody.innerHTML += `
							<tr>
                                    <td>${movie.id}</td>
									<td>${movie.title}</td>
									<td>${movie.genre}</td>
									<td>${movie.releaseDate}</td>
									<td><img src="${movie.imageUrl}" alt="${movie.title}" class="img-thumbnail" style="width: 100px;"></td>
									<td>${movie.description}</td>
									<td>
											<button class="btn btn-warning btn-sm" onclick="editMovie(${movie.id})">تعديل</button>
											<button class="btn btn-danger btn-sm" onclick="deleteMovie(${movie.id})">حذف</button>
									</td>
							</tr>
					`;
        });
    } catch (error) {
        console.error("Error fetching movies:", error);
    }
}

// معالجة إرسال النموذج لإضافة أو تعديل فيلم
async function handleFormSubmit(e) {
    e.preventDefault();

    const id = document.getElementById("movie-id").value;
    const movie = {
        title: document.getElementById("title").value,
        genre: document.getElementById("genre").value,
        releaseDate: document.getElementById("releaseDate").value,
        Image: document.getElementById("imageUrl").files[0], // نحصل على الصورة من الحقل
        description: document.getElementById("description").value
    };
    const method = id ? "PUT" : "POST";
    const url = id ? `https://localhost:7215/api/Movie/${id}` : "https://localhost:7215/api/Movie";



    const formData = new FormData();
    formData.append("title", movie.title);
    formData.append("genre", movie.genre);
    formData.append("releaseDate", movie.releaseDate);
    formData.append("Image", movie.Image);
    formData.append("description", movie.description);

    await fetch(url, {
        method,
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("authToken")}`
        },
        body: formData
    });

    document.getElementById("movie-form").reset();
    document.getElementById("movie-id").value = "";
    fetchMovies();
}

// تعديل فيلم
function editMovie(id) {
    fetch(`https://localhost:7215/api/Movie/${id}`)
        .then(res => res.json())
        .then(movie => {
            document.getElementById("movie-id").value = movie.id;
            document.getElementById("title").value = movie.title;
            document.getElementById("genre").value = movie.genre;
            // تأكد من أن التاريخ في التنسيق الصحيح (YYYY-MM-DD)
            const releaseDate = movie.releaseDate ? new Date(movie.releaseDate).toISOString().split('T')[0] : '';
            document.getElementById("releaseDate").value = releaseDate;


            document.getElementById("description").value = movie.description;
            //  document.getElementById("imageUrl").value = movie.Image;
        });
}

// حذف فيلم
function deleteMovie(id) {
    if (!confirm("هل أنت متأكد أنك تريد حذف هذا الفيلم؟")) return;

    fetch(`https://localhost:7215/api/Movie/${id}`, { method: "DELETE" })
        .then(() => fetchMovies())
        .catch(error => console.error("Error deleting movie:", error));
}