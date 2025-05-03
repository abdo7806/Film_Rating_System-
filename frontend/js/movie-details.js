// جلب الـ ID من الرابط
const urlParams = new URLSearchParams(window.location.search);
const movieId = urlParams.get('movieId'); // جلب رقم الفيلم

if (movieId) {
    // جلب تفاصيل الفيلم باستخدام الـ ID
    fetchMovieDetails(movieId);
    fetchReviews(movieId);
} else {
    alert("Movie ID is missing.");
}

// جلب تفاصيل الفيلم
async function fetchMovieDetails(movieId) {
    try {
        const response = await fetch(`https://localhost:7215/api/Movie/All`);
        const movies = await response.json();
        const movie = movies.find(m => m.id === parseInt(movieId)); // استخدام الرقم للحصول على الفيلم
        if (movie) {
            document.getElementById('movie-image').src = movie.imageUrl;
            document.getElementById('movie-title').innerText = movie.title;
            document.getElementById('movie-description').innerText = movie.description;
            document.getElementById('movie-release-date').innerText = movie.releaseDate;
            document.getElementById('movie-genre').innerText = movie.genre;
        } else {
            alert('Movie not found');
        }
    } catch (error) {
        console.error('Error fetching movie details:', error);
    }
}
// Use the authToken for user-specific actions
function getUserIdFromToken() {
    // الحصول على الـ token من localStorage
    var token = localStorage.getItem("authToken");

    // التأكد من وجود الـ token
    if (token) {
        // فك تشفير الـ token (JWT) للحصول على الـ Payload
        var payload = parseJwt(token);
        // الحصول على الـ userId من الـ Payload
        // var userId = payload.UserId;
        //  alert("sdsw=" + userId);

        //   alert("sdsw=" + payload);
        //  console.log(payload);
        return payload.UserId; // Assuming the token contains userId
    }
    return null;
}



// دالة لفك تشفير الـ JWT
function parseJwt(token) {
    var base64Url = token.split('.')[1]; // الجزء الثاني من الـ JWT
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // تحويل URL-safe إلى Base64
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload); // إرجاع البيانات بتنسيق JSON
}

// جلب التقييمات الخاصة بالفيلم
async function fetchReviews(movieId) {
    try {
        const response = await fetch(`https://localhost:7215/api/Reviews/All`);
        const reviews = await response.json();

        const movieReviews = reviews.filter(r => r.movieId === parseInt(movieId));
        let reviewsHtml = '';
        let totalRating = 0;

        movieReviews.forEach(review => {
            reviewsHtml += `
                <div class="card review-card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Rating: ${review.rating}</h5>
                        <p class="card-text">${review.comment}</p>
                        <footer class="blockquote-footer">User ID: ${review.userId}</footer>
                    </div>
                </div>
            `;
            totalRating += review.rating;
        });

        const averageRating = movieReviews.length > 0 ? (totalRating / movieReviews.length).toFixed(1) : 'No ratings yet.';
        document.getElementById('average-rating').innerText = averageRating;
        document.getElementById('reviews-list').innerHTML = reviewsHtml;
    } catch (error) {
        console.error('Error fetching reviews:', error);
    }
}

// إضافة تقييم جديد
document.getElementById('review-form').addEventListener('submit', async(e) => {
    e.preventDefault();

    const rating = document.getElementById('rating').value;
    const comment = document.getElementById('comment').value;
    var userId = getUserIdFromToken();
    // alert("sds=" + userId);
    // alert("sds=" + userId);

    // منع التقييم المتكرر من نفس المستخدم
    const response = await fetch(`https://localhost:7215/api/Reviews/user/${userId}`);
    const userReviews = await response.json();
    const existingReview = userReviews.find(r => r.movieId === parseInt(movieId));
    //alert("sds=" + userId);
    //alert("movieId=" + movieId);

    if (existingReview) {
        alert('You have already reviewed this movie.');
        return;
    }

    const reviewData = {
        movieId: movieId,
        rating: rating,
        comment: comment
    };
    // alert("sds=" + comment);
    try {
        var token = localStorage.getItem("authToken");

        await fetch('https://localhost:7215/api/Reviews', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(reviewData)
        });
        fetchReviews(movieId); // تحديث التقييمات بعد الإضافة
    } catch (error) {
        console.error('Error adding review:', error);
    }
});