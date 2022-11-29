namespace CoursesApi.Domain;

public class OfferingsCatalog : IProvideOfferings
{

    public async Task<List<DateTime>> GetOfferingsForCourse(int courseId, CancellationToken token)
    {
        
        return new List<DateTime> { new DateTime(1969,04,20) };
    }
}
