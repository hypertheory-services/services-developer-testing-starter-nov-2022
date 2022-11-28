using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Domain;

public class CourseCatalog
{
    private readonly CoursesDataContext _context;
 

    public CourseCatalog(CoursesDataContext context)
    {
        _context = context;

    }

    public async Task<CoursesResponseModel> GetFullCatalogAsync(CancellationToken token)
    {
        var courses = await _context.Courses.Where(c => c.Retired == false)
            .Select(c => new CourseItemResponse
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Category = c.Category
            }).ToListAsync(token);
     
        return new CoursesResponseModel
        {
            Courses = courses,
            NumberOfBackendCourses = courses.Count(c => c.Category == CategoryType.Backend),
            NumberOfFrontendCourses = courses.Count(c => c.Category == CategoryType.Frontend)
        };
    }

    public async Task<CourseItemDetailsResponse?> GetCourseByIdAsync(int id, CancellationToken token)
    {

        var response = await _context.Courses.Where(c => c.Id == id && c.Retired == false)
            .Select(c => new CourseItemDetailsResponse
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Category = c.Category,
                Description = c.Description,
              
            }).SingleOrDefaultAsync(token);

       
        return response;
    }

    public async Task<CourseItemDetailsResponse> AddCourseAsync(CourseCreateRequest request, CategoryType category)
    {
        var courseToAdd = new CourseEntity
        {
            Title = request.Title,
            Description = request.Description,
            Retired = false,
            Category = category
        };
        _context.Courses.Add(courseToAdd);
        await _context.SaveChangesAsync();

        var response = new CourseItemDetailsResponse
        {
            Id = courseToAdd.Id.ToString(),
            Title = courseToAdd.Title,
            Description = courseToAdd.Description,
            Category = courseToAdd.Category
        };
        return response;
    }

    public async Task RemoveCourseAsync(int id)
    {
        var course = await _context.Courses.Where(c => id == c.Id && c.Retired == false).SingleOrDefaultAsync();

        if(course != null)
        {
            course.Retired = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateDescriptionAsync(int id, string description)
    {
        var course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == id && c.Retired == false);

        if(course != null )
        {
            course.Description = description;
            await _context.SaveChangesAsync();
            return true;
        } else
        {
            return false;
        }
    }
}
