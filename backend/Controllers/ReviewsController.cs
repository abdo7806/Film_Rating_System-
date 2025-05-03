using Film_Rating_System.Models.Repositories;
using Film_Rating_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Film_Rating_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        public readonly IRepository<Review> _ReviewRepository;
        private readonly AppDbContext _context;


        public ReviewsController(IRepository<Review> ReviewRepository, AppDbContext context)
        {
            this._ReviewRepository = ReviewRepository;
            _context = context;


        }

        [HttpGet("{id}", Name = "GetReviewById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Review> GetReviewById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            var Review = _ReviewRepository.GetById(id);

            if (Review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return Ok(Review);
        }

        // GET: api/<ReviewsController>
        [HttpGet("All", Name = "GetAllReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Review>> GetAllReviews() // Define a method to get all Reviews.
        {

            var data = _ReviewRepository.GetAll();

            if (data.Count == 0)
            {
                return NotFound("No Reviews Found!");
            }
            return Ok(data); // Returns the list of Reviews.
        }


        
        [HttpPost(Name = "AddReview")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddReview([FromBody] ReviewCreateDto dto/*, int userId*/)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var review = new Review
            {
                MovieId = dto.MovieId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

          


            _ReviewRepository.Add(review);


            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }




        // PUT api/<ReviewsController>/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Review>> UpdateReview(int id, [FromBody] Review updatedReview/*, int userId*/)
        {
            // استخراج UserId من التوكن
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // جلب التقييم من قاعدة البيانات
            var review = _ReviewRepository.GetById(id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            // التحقق من أن صاحب التقييم هو نفس المستخدم الحالي
            if (review.UserId != userId)
            {
                return Forbid("You are not allowed to edit this review.");
            }



            // التحقق من صحة التقييم
            if (updatedReview.Rating < 1 || updatedReview.Rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // تحديث الحقول المطلوبة فقط
            review.Rating = updatedReview.Rating;
            review.Comment = updatedReview.Comment;
            review.CreatedAt = DateTime.UtcNow;






            _ReviewRepository.Edit(id, review);

            return Ok(review);


        }

        // ✅ عرض التقييمات لمستخدم معين
        [HttpGet("user/{userId}")]
        public  ActionResult<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews =  _ReviewRepository.GetAll().Where(r => r.UserId == userId);
              

            return Ok(reviews);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteReview(int id)
        {
      
            int ReviewID = _ReviewRepository.Delete(id);
            if (ReviewID == -1)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return Ok($"Review with ID {id} has been deleted.");
        }
    }
}
