namespace CoursesApi.Domain;

public class OfferingsCatalogApiCall : IProvideOfferings
{
    private readonly OfferingsApiAdapter _adapter;
    private readonly CourseCatalog _catalog;

    public OfferingsCatalogApiCall(OfferingsApiAdapter adapter, CourseCatalog catalog)
    {
        _adapter = adapter;
        _catalog = catalog;
    }

    public async Task<List<DateTime>> GetOfferingsForCourse(int courseId, CancellationToken token)
    {

        var hasCourse = await _catalog.GetCourseByIdAsync(courseId, token);
        if (hasCourse == null)
        {
            return null!;
        }
        else
        {
            // CALLING THE OTHER API
            var response = await _adapter.GetOfferingsForCourseAsync(courseId);
            if(response is null)
            {
                return new List<DateTime>();
            }
            else
            {
                return response.Data;
            }
        }
       
    }
}
