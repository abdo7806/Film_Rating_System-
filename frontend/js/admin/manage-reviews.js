document.addEventListener("DOMContentLoaded", () => {
    fetchReviews();
});

// جلب التقييمات من API
async function fetchReviews() {
    try {
        const res = await fetch("https://localhost:7215/api/Reviews/All", {
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("authToken")}`
            }
        });
        const reviews = await res.json();

        const list = document.getElementById("users-list");
        list.innerHTML = "";

        reviews.forEach(r => {
            const row = `
          <tr>
            <td>${r.id}</td>
            <td>${r.movieId}</td>
            <td>${r.userId}</td>
            <td>${r.rating} ⭐</td>
            <td>${r.comment}</td>
            <td>${new Date(r.createdAt).toLocaleString()}</td>
            <td>
              <button class="btn btn-danger btn-sm" onclick="deleteReview(${r.id})">🗑 حذف</button>
            </td>
          </tr>`;
            list.innerHTML += row;
        });

    } catch (err) {
        console.error("فشل تحميل التقييمات:", err);
    }
}

// حذف تقييم
async function deleteReview(id) {
    if (!confirm("هل أنت متأكد أنك تريد حذف هذا التقييم؟")) return;

    try {
        await fetch(`https://localhost:7215/api/Reviews/${id}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("authToken")}`
            }
        });
        fetchReviews();
    } catch (err) {
        console.error("فشل حذف التقييم:", err);
    }
}