using Core;

namespace Api.Repository;

public interface IApplicationRepository
{
    void CreateApplication(Application application);
    
    List<Application> GetAllApplications();
    
    Application GetApplicationById(string id);
    
    void UpdateApplication(Application application);

    void DeleteApplication(string id);
}