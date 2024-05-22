using Core;
using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface ISystemSettingsRepository
    {
        void CreateSystemSettings(SystemSettings settings);
        List<SystemSettings> GetAllSystemSettings();
        SystemSettings GetSystemSettingsById(string id);
        void UpdateSystemSettings(SystemSettings settings);
        void DeleteSystemSettings(string id);
    }
}