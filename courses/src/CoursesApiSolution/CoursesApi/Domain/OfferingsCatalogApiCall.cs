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

    public async Task<List<DateTime>> GetOfferingsForCourse(int courseId)
    {

        var hasCourse = await _catalog.GetCourseByIdAsync(courseId);
        if (hasCourse == null)
        {
            return null;
        }
        else
        {
            var response = await _adapter.GetOfferingsForCourseAsync(courseId);
            return response!.Data;
        }
       
    }
}
