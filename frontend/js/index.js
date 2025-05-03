document.addEventListener("DOMContentLoaded", function() {
    const moviesList = document.getElementById("movies-list");

    // جلب الأفلام من الـAPI
    fetch('https://localhost:7215/api/Movie/All')
        .then(response => response.json())
        .then(data => {
            if (data && Array.isArray(data)) {
                data.forEach(movie => {
                    const movieCard = document.createElement('div');
                    movieCard.classList.add('col-md-4');
                    movieCard.classList.add('movie-card');
                    movieCard.innerHTML = `
											<div class="card">
													<img src="${movie.imageUrl}" class="card-img-top" alt="${movie.title}"   width="70px" height="70px">
													<div class="card-body">
															<h5 class="movie-title">${movie.title}</h5>
															<p class="movie-description">${movie.description.substring(0, 100)}...</p>
													</div>
													<div class="card-footer">
															<a href="movie-details.html?movieId=${movie.id}" class="btn btn-light">View Details</a>
													</div>
											</div>
									`;
                    moviesList.appendChild(movieCard);
                });
            }
        })
        .catch(error => console.error('Error fetching movies:', error));
});