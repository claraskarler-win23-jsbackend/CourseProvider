using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.GraphQl.Mutations;

//Mutation => saker som vi skriver in i systemet. Query => saker som vi hämtar ut ur systemet.
public class CourseMutation(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("createCourse")]
    public async Task<Course> CreateCourseAsync(CreateCourseRequest input)
    {
        return await _courseService.CreateCourseAsync(input);
    }

    [GraphQLName("updateCourse")]
    public async Task<Course> UpdateCourseAsync(UpdateCourseRequest input)
    {
        return await _courseService.UpdateCourseAsync(input);
    }

    [GraphQLName("deleteCourse")]
    public async Task<bool> DeleteCourseAsync(string id)
    {
        return await _courseService.DeleteCourseAsync(id);
    }
}
